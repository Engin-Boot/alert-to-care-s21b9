using SharedProjects.EntriesValidator;
using Xunit;

namespace AlertToCareUnitTest
{
    public class IcuValidatorUnitTests
    {
        
        private string expectedResult;
        private bool validIcu;
        [Fact]
        public void CheckForValidIcu_ShouldReturnTrueIfIcuIsPresent()
        {
            string msg = "";
            validIcu = false;

            expectedResult = "The Inserted ICU No Is Not Available";
            IcuValidator.CheckForValidIcu(99,ref validIcu, ref msg);
            Assert.True(msg == expectedResult);

            expectedResult = "";
            IcuValidator.CheckForValidIcu(1,ref validIcu, ref msg);
            Assert.True(msg == expectedResult);

        }
    }
}
