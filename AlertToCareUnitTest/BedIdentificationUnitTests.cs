using AlertToCareApi.Utilities;
using Xunit;

namespace AlertToCareUnitTest
{
    public class BedIdentificationUnitTests
    {
        [Fact]
        public void FindCountOfBeds_ShouldGetTotalNumberOfBedsGivenIcuNo()
        {
            var _bedIdentification = new BedIdentification();
            var _totalNoOfBeds = _bedIdentification.FindCountOfBeds(3);
            Assert.True(_totalNoOfBeds == 2);

        }

        [Fact]
        public void FindBedSerialNumber_ShouldReturnBedSerialNumberGivenIcuNumber()
        {
            var _bedIdentification = new BedIdentification();
            var _bedSerialNumber = _bedIdentification.FindBedSerialNo(1);
            Assert.True(_bedSerialNumber == _bedIdentification.FindCountOfBeds(1) + 1);

        }
    }
}
