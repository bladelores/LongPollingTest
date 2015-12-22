using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Common;
using System.Configuration;
using Modem.Amt.Export.Data;
using Modem.Amt.Export.Data.Mappings;

namespace Modem.Amt.Export
{
    public class Store : DbContext
    {
        #region Queries
        private const string ActualDataFunctionQuery = @"
            SELECT 
                    get_actual_data_on_time_limit(:p_wellbore_id,:p_time,:p_parameter_list, :p_limit)  
                FROM dual";
        
        #endregion
        private DataProcess dataProcess;
        public DbSet<Wellbore> Wellbores { set; get; }
        public DbSet<WellboreState> WellboreStates { set; get; }
        public DbSet<Unit> Units { set; get; }
        public DbSet<State> States { set; get; }
        public DbSet<Parameter> Parameters { set; get; }
        public DbSet<ContinuousInterval> ContinuousIntervals { set; get; }
        static Store()
        {
            Database.SetInitializer<Store>(null);
        }
        
        public Store(): base("AmtEF")
        {
            var cs = ConfigurationManager.ConnectionStrings["Amt"];
            var factory = DbProviderFactories.GetFactory(cs.ProviderName);
            var con = factory.CreateConnection();
            con.ConnectionString = cs.ConnectionString;
            con.Open();

            dataProcess = new DataProcess();
            dataProcess.Connection = con;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UnitMapping());
            modelBuilder.Configurations.Add(new WellboreMapping());
            modelBuilder.Configurations.Add(new WellboreStateMapping());
            modelBuilder.Configurations.Add(new StateMapping());
            modelBuilder.Configurations.Add(new ParameterMapping());
            modelBuilder.Configurations.Add(new ContinuousIntervalMapping());
        }

        public void FillParameter(Parameter p)
        {
            p = Parameters.Where(x => x.Id == p.Id).SingleOrDefault();
        }

        public List<decimal> GetLimitPoints(long wellboreId, DateTime time, List<Parameter> parameters, StringBuilder queryString)
        {          
            string resultStringArray = dataProcess.QueryAndMap(ActualDataFunctionQuery, new { p_wellbore_id = wellboreId, p_time = time, p_parameter_list = queryString.Remove(0, 1).ToString(), p_limit = time.AddMinutes(-10) },
                x => dataProcess.ConvertNullable<string>(x[0])).SingleOrDefault();          
            return DataProcess.LimitPointsProcess(parameters, resultStringArray);
        }

        public List<decimal[]> GetData(List<Parameter> parameters, Wellbore wellbore, DateTime start, DateTime end)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            if (wellbore == null)
                throw new ArgumentNullException("wellbore");

            parameters.ForEach(x => FillParameter(x));

            StringBuilder queryBuilder = new StringBuilder();
            parameters.ForEach(x => queryBuilder.Append(", ").Append(x.Code));
            var limitCorrector = GetLimitPoints(wellbore.Id, start, parameters, queryBuilder);

            queryBuilder.Insert(0, "select time, ");
            queryBuilder.Append(" from temporal_measuring where wellbore_id = :wellboreId and time between :startTime and :endTime");

            var oldValues = new decimal[parameters.Count];
            for (var i = 0; i < parameters.Count; ++i)
                oldValues[i] = limitCorrector[i];

            DataProcess.MapperDelegate<decimal[]> mapper = x =>
            {
                var r = new decimal[parameters.Count + 1];

                for (var i = 0; i < parameters.Count; ++i)
                {
                    var val = x[i + 1];
                    r[i] = val == DBNull.Value ? oldValues[i] : Convert.ToDecimal(val) / parameters[i].Multiplier;
                }
                r[parameters.Count] = Convert.ToDateTime(x[0]).Ticks;

                oldValues = r;
                return r;
            };

            return new List<decimal[]>(dataProcess.QueryAndMap(queryBuilder.ToString(), new { wellboreId = wellbore.Id, startTime = start, endTime = end }, mapper));
        }
    }
}
