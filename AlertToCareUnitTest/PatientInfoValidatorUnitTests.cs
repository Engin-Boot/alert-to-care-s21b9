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
        [Fact]
        public void ValidateInfoAndCheckForAvailability_ShouldCheckPatientInfoAndBedAvailaibility()
        {
            string message = "";
            bool validInfo = false;
            PatientInfoValidator.ValidateInfoAndCheckForAvailability(patient, 32, ref validInfo, ref message);

            Assert.True(message == "Patient Information Is Incorrect, Please Recheck");
        }

       
    }
}
