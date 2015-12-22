using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LongPollingTest.Connections
{
    public class TestConnection: IRealtimeConnection
    {
        private long wellboreId;
        private List<string> parameters;

        public void ConfigureConnection(long wellboreId, List<string> parameters)
        {
            this.wellboreId = wellboreId;
            this.parameters = parameters;
        }

        public async System.Threading.Tasks.Task<decimal[]> GetNewData()
        {
            var data = await Task<decimal[]>.Run(() =>
            {
                var random = new Random();
                var randomArray = new decimal[parameters.Count];
                for (int i = 0; i < parameters.Count; i++) randomArray[i] = random.Next(0, 100);
                Thread.Sleep(1000);
                return randomArray;
            });
            Task.WaitAll();
            var resultRandom = new Random();
            if (resultRandom.Next(0, 10) == 9)
                return null;
            else
                return data;
        }
    }
}
