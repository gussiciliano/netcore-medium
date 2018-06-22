using System;
using test_net_core_mvc.Helpers;
using Xunit;

namespace test.Unit
{
    public class UnitTestMathHelper
    {
        [Fact]
        public void TestSum()
        {
            float number1 = 4;
            float number2 = 10;
            float sum = 14;
            float testSum = MathHelper.Sum(number1, number2);
            Assert.Equal(sum, testSum);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(2, 3, 6)]
        public void testMultiplication(float number1, float number2, float multiplication)
        {
            float testMultiplication = MathHelper.Multiply(number1, number2);
            Assert.Equal(multiplication, testMultiplication);
        }
    }
}
