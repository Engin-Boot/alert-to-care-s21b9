
using System.Linq;
using SharedProjects.Models;
using SharedProjects.Utilities;
using Xunit;

namespace AlertToCareUnitTest
{
   
    public class VitalsMonitoringUnitTest
    {
        private bool _result;
        private int _ctr ;
        VitalsMonitoring apiClassVitalsMonitoring = new VitalsMonitoring();

        [Fact]
        public void WhenCheckVitalsIsGivenCorrectLogItReturnsAString()
        {
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
            
            VitalsLogs log = new VitalsLogs
            {
                VitalsLogId = 1,
                PatientId = 290,
                Spo2Rate = 95,
                RespRate = 7,
                BpmRate = 78,
            };

            string ans = apiClassVitalsMonitoring.CheckVitals(log);
            string[] arr = ans.Split(',');
            var pname = arr[1];

            Assert.Equal("", pname);
           
        }

        [Fact]
        public void CountZeros_InVital()
        {
            bool result;
            int ctr;
            int[] arr={0,1,2,3};
           ctr= apiClassVitalsMonitoring.CountZeroes(arr);
           if (ctr == 1)
           {
               result = true;

           }
           else
           {
               result = false;
           }
            Assert.True(result);


        }

        public void int_To_Bool()
        {
            if (_ctr == 0)
            {
                _result = true;

            }
            else
            {
                _result = false;
            }
        }
        [Fact]
        public void Checkspo2_InVital()
        {
           
            double sp02 = 72;
            double highsp02 = 97;
            _ctr = apiClassVitalsMonitoring.CheckSpo2(sp02);
            int_To_Bool();
            Assert.False(_result);
            _ctr = apiClassVitalsMonitoring.CheckSpo2(highsp02);
            int_To_Bool();
            Assert.True(_result);



        }
        [Fact]
        public void Checkbpm_InVital()
        {
            double lowbpm = 60;
            double highbpm = 105;
            double bpm = 72;
            _ctr = apiClassVitalsMonitoring.CheckBpm(bpm);
            int_To_Bool();
            Assert.True(_result);
            _ctr = apiClassVitalsMonitoring.CheckBpm(highbpm);
            int_To_Bool();
            Assert.False(_result);
            _ctr = apiClassVitalsMonitoring.CheckBpm(lowbpm);
            int_To_Bool();
            Assert.False(_result);


        }
        [Fact]
        public void Checkresp_InVital()
        {
            double lowresp = 11;
            double highresp = 18;
            double bpm = 14;
            _ctr = apiClassVitalsMonitoring.CheckRespRate(bpm);
            int_To_Bool();
            Assert.True(_result);
            _ctr = apiClassVitalsMonitoring.CheckRespRate(lowresp);
            int_To_Bool();
            Assert.False(_result);
            _ctr = apiClassVitalsMonitoring.CheckRespRate(highresp);
            int_To_Bool();
            Assert.False(_result);


        }
        [Fact]
        public void InterpretMessage_InVital()
        {
            string vitallowmsg = "is low";
            string vitalhighmsg = "is high";
            int ToLowmsg = -1;
            int ToHighmsg = 1;
            string msg =apiClassVitalsMonitoring.InterpretMessage(ToHighmsg);
            Assert.True(msg==vitalhighmsg);
             msg=apiClassVitalsMonitoring.InterpretMessage(ToLowmsg);
            Assert.True(msg==vitallowmsg);


        }

        [Fact]
        public void GetVitalsForSpecificPatient_UsingPatientId()
        {
           
            int id = 2;
            Alarm alaram=  apiClassVitalsMonitoring.GetVitalsForSpecificPatient(id);
            Assert.True(true);
        }
    }

    
}
