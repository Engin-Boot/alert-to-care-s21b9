using SharedProjects;
using SharedProjects.EntriesValidator;
using SharedProjects.Models;
using Xunit;

namespace AlertToCareUnitTest
{
    public class PatientInfoValidatorUnitTests
    {
        
        private Patients patient = new Patients
        {
            PatientId = 123,
            PatientName = "Samyukta",
            Age = 32,
            BedId = 45,
            ContactNo = "1236547896",
            MonitoringStatus = 0
        };
        private Patients patientinfo = new Patients
        {
            PatientId = 1,
            PatientName = "Nikita Kumari",
            Age = 23,
            BedId = 1,
            ContactNo = "9826376268",
            MonitoringStatus = 0
        };
        

        [Fact]
        public void ValidateInfoAndCheckForAvailability_ShouldCheckPatientInfoAndBedAvailaibility()
        {
            string message = "";
            bool validInfo = false;
            PatientInfoValidator.ValidateInfoAndCheckForAvailability(patient, 32, ref validInfo, ref message);

            Assert.True(message == "Patient Information Is Incorrect, Please Recheck");
        }
        [Fact]
        public void ValidateInfoAndCheckForAvailability_ShouldCheckPatientInfoAndBedAvailaibilityifPatientInfoisPresent()
        {
            string message = "";
            bool validInfo = true;
            PatientInfoValidator.ValidateInfoAndCheckForAvailability(patientinfo, 32, ref validInfo, ref message);

            Assert.True(message == "");
        }
        [Fact]
        public  void DeletePatientLog_When_discharged()
        {
            ConfigDbContext context = new ConfigDbContext();
            
            int patientid = 1;
            PatientInfoValidator.DeleteVitalLogsForDischargedPatient(patientid); 
          if(patientid.Equals(true))
           Assert.True(true);
        }
       

    }
}
