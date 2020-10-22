using AlertToCareApi.Models;
using System.Linq;
using Xunit;

namespace AlertToCareUnitTest
{
    public class VitalsMonitoringUnitTest
    {
        [Fact]
        public void WhenCheckVitalsIsGivenCorrectLogItReturnsAString()
        {
            var apiClassVitalsMonitoring = new AlertToCareApi.Utilities.VitalsMonitoring();
            //var _context = new AlertToCareApi.ConfigDbContext();
            //VitalsLogs Log = _context.VitalsLogs.ToList().FirstOrDefault();

            VitalsLogs log = new VitalsLogs
            {
                VitalsLogId = 1,
                PatientId = 22,
                Spo2Rate = 95,
                RespRate = 7,
                BpmRate = 78,
            };

            string ans = apiClassVitalsMonitoring.CheckVitals(log);
            string[] arr = ans.Split(",").ToArray();

            Assert.True(ans.Length > 1);
            Assert.True(arr.Length == 5);
        }


        [Fact]
        public void WhenCheckVitalsIsGivenLogThatDoesNotExistInDatabaseItReturnsAnError()
        {
            var apiClassVitalsMonitoring = new AlertToCareApi.Utilities.VitalsMonitoring();
            VitalsLogs log = new VitalsLogs
            {
                VitalsLogId = 1,
                PatientId = 208,
                Spo2Rate = 95,
                RespRate = 7,
                BpmRate = 78,
            };

            string ans = apiClassVitalsMonitoring.CheckVitals(log);
            string[] arr = ans.Split(',');
            var pname = arr[1];

            Assert.Equal("", pname);
           
        }
    }
}
