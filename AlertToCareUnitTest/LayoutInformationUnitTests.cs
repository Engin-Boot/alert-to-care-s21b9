
using SharedProjects.Utilities;
using Xunit;

namespace AlertToCareUnitTest
{
    public class LayoutInformationUnitTests
    {
        [Fact]
        public void FindNoOfIcus_ShouldGetTheNumberOfIcusWithAGivenLayout()
        {
            var layoutInfo = new LayoutInformation();
            var noOfIcus = layoutInfo.FindNoOfIcus(4);
            Assert.True(noOfIcus == 8);
        }
        [Fact]
        public void FindListOfIcus_ShouldReturnAListOfIcusGivenLayoutNo()
        {
            var layoutInfo = new LayoutInformation();
            var noOfIcus = layoutInfo.FindListOfIcus(4);
            foreach(var icu in noOfIcus)
            {
                Assert.True(icu.LayoutId == 4);
            }
        }
    }
}
