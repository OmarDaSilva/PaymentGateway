using Xunit;
using Moq;
using AutoFixture;

namespace PaymentGateway.Api.UnitTests
{
    public class CommandTests
    {
        [Fact]
        public void Add_Numbers()
        {
            var result = 1 + 2;
            Assert.Equal(2, result);
        }

       
    }
}

