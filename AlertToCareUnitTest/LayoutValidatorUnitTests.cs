using SharedProjects.EntriesValidator;
using Xunit;

namespace AlertToCareUnitTest
{
    public class LayoutValidatorUnitTests
    {
        [Fact]
        public void CheckIfLayoutIsPresent_ShouldReturnTrueIfLayoutIsPresent()
        {
            Assert.False(LayoutValidator.CheckIfLayoutIsPresent(6));
            Assert.True(LayoutValidator.CheckIfLayoutIsPresent(1));
        }
    }
}
