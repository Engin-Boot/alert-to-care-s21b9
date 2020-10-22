using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using System.Linq;
using Xunit;

namespace AlertToCareUnitTest
{
    public class BedAllotmentUnitTests
    {
        [Fact]

        public void GetAvailabilityOfBeds_ReturnsAListOfAvailaibleBeds()
        {
            int beds = 0;
            var _bedAllotment = new BedAllotment();
            var _availableListOfBeds = _bedAllotment.GetAvailableBeds();
            beds += (from bed in _availableListOfBeds
                     where !bed.OccupancyStatus
                     select bed).Count();
            Assert.True(beds == _availableListOfBeds.Count());
        }

        [Fact]
        public void EmptyTheBed_ShouldUpdateOccupancyStatusToFalse()
        {
            Patients _patient = new Patients() { PatientName = "Nikita Kumari", Age = 23, BedId = 1, ContactNo = "9826376268", MonitoringStatus = 0, PatientId = 1 };
            var _bedAllotment = new BedAllotment();
            _bedAllotment.EmptyTheBed(_patient);
            var _allBeds = _bedAllotment.GetAvailableBeds();
            foreach (var bed in _allBeds)
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
