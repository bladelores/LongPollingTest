using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LongPollingTest.Connections
{
    public interface IRealtimeConnection
    {
        void ConfigureConnection(long wellboreId, List<string> parameters);
        System.Threading.Tasks.Task<decimal[]> GetNewData();
    }
}
