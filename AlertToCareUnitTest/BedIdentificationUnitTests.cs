
using SharedProjects.Utilities;
using Xunit;

namespace AlertToCareUnitTest
{
    public class BedIdentificationUnitTests
    {
        [Fact]
        public void FindCountOfBeds_ShouldGetTotalNumberOfBedsGivenIcuNo()
        {
            var bedIdentification = new BedIdentification();
            var totalNoOfBeds = bedIdentification.FindCountOfBeds(3);
            Assert.True(totalNoOfBeds == 2);

        }

        [Fact]
        public void FindBedSerialNumber_ShouldReturnBedSerialNumberGivenIcuNumber()
        {
            var bedIdentification = new BedIdentification();
            var bedSerialNumber = bedIdentification.FindBedSerialNo(1);
            Assert.True(bedSerialNumber == bedIdentification.FindCountOfBeds(1) + 1);

        }
    }
}
