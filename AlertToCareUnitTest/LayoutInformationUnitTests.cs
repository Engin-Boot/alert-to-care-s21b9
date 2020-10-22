using AlertToCareApi.Utilities;
using Xunit;

namespace AlertToCareUnitTest
{
    public class LayoutInformationUnitTests
    {
        [Fact]
        public void FindNoOfIcus_ShouldGetTheNumberOfIcusWithAGivenLayout()
        {
            var _layoutInfo = new LayoutInformation();
            var _noOfIcus = _layoutInfo.FindNoOfIcus(4);
            Assert.True(_noOfIcus == 8);
        }
        [Fact]
        public void FindListOfIcus_ShouldReturnAListOfIcusGivenLayoutNo()
        {
            var _layoutInfo = new LayoutInformation();
            var _noOfIcus = _layoutInfo.FindListOfIcus(4);
            foreach(var icu in _noOfIcus)
            {
                Assert.True(icu.LayoutId == 4);
            }
        }
    }
}
