using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Modem.Amt.Export
{
    public class DataProcess
    {
        public const decimal NoValue = -999666;

        public IDbConnection Connection { set; get; }

        protected IDbTransaction transaction;

        public struct QueryParam
        {
            public string Name { set; get; }
            public object Value { set; get; }
        }

        public delegate T MapperDelegate<T>(IDataReader record);

        protected IEnumerable<T> Map<T>(IDataReader query, MapperDelegate<T> recordMapper)
        {
            while (query.Read())
            {
                yield return recordMapper(query);
            }
        }

        protected IDbCommand CreateCommand(string query, params QueryParam[] queryParams)
        {
            IDbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Transaction = transaction;

            foreach (var param in queryParams)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = param.Name;
                p.Value = param.Value ?? DBNull.Value;

                cmd.Parameters.Add(p);
            }

            return cmd;
        }

        protected QueryParam[] CreateParams(object queryParameters)
        {
            if (queryParameters == null)
                return new QueryParam[0];

            var props = queryParameters.GetType().GetProperties();

            QueryParam[] pars = new QueryParam[props.Length];

            for (int i = 0; i < props.Length; ++i)
            {
                pars[i] = new QueryParam { Name = ":" + props[i].Name, Value = props[i].GetValue(queryParameters, null) };
            }
            return pars;
        }


        protected IDataReader Query(string query, params QueryParam[] queryParams)
        {
            using (var cmd = CreateCommand(query, queryParams))
            {
                return cmd.ExecuteReader();
            }
        }

        protected IDataReader Query(string query, object queryParameters)
        {
            return Query(query, CreateParams(queryParameters));
        }

        public IEnumerable<T> QueryAndMap<T>(string query, object queryParameters, MapperDelegate<T> recordMapper)
        {
            return Map(Query(query, queryParameters), recordMapper);
        }

        public T ConvertNullable<T>(object value)
        {
            if (value == DBNull.Value)
                return default(T);

            return (T)System.Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
        }

        public static List<decimal> LimitPointsProcess(List<Data.Parameter> parameters, string resultStringArray) {
            var parsedList = new List<decimal>();
            foreach (var parameter in parameters)
            {
                if (resultStringArray.Contains(parameter.Code))
                {
                    int symbolPos = resultStringArray.IndexOf(parameter.Code);
                    parsedList.Add(decimal.Parse(resultStringArray.Substring(symbolPos + parameter.Code.Length + 2, resultStringArray.IndexOf('\"', symbolPos) - symbolPos - parameter.Code.Length)));
                }
                else
                    parsedList.Add(NoValue);
            }
            return parsedList;
        }
    }
}
