using SharedProjects.EntriesValidator;
using System;
using System.Collections.Generic;
using System.Text;
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
