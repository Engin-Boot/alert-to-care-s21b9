using System.Linq;
using SharedProjects.Models;
using SharedProjects.Utilities;
using Xunit;

namespace AlertToCareUnitTest
{
    public class BedAllotmentUnitTests
    {
        
       
[Fact]

        public void GetAvailabilityOfBeds_ReturnsAListOfAvailaibleBeds()
        {
            int beds = 0;
            var bedAllotment = new BedAllotment();
            var availableListOfBeds = bedAllotment.GetAvailableBeds();
            beds += (from bed in availableListOfBeds
                     where !bed.OccupancyStatus
                     select bed).Count();
            Assert.True(beds == availableListOfBeds.Count());
        }

        [Fact]
        public void EmptyTheBed_ShouldUpdateOccupancyStatusToFalse()
        {
            Patients patient = new Patients { PatientName = "Nikita Kumari", BedId = 1, ContactNo = "9826376268", MonitoringStatus = 0, PatientId = 1 };
            var bedAllotment = new BedAllotment();
            bedAllotment.EmptyTheBed(patient);
            var allBeds = bedAllotment.GetAvailableBeds();
            foreach (var bed in allBeds)
            {
                Assert.True(bed.BedId == 1);
                break;
            }

        }

        //check
        [Fact]
        public void AlloTheBed_ShouldUpdateStatusWhenBedIdAndStatusIsGiven()
        {

        }
    }
}
