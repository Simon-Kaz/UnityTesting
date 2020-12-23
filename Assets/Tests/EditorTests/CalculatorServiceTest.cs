using NUnit.Framework;
using Services;

namespace Tests.EditorTests
{
    public class CalculatorServiceTest
    {
        private IService calculatorService;
        [OneTimeSetUp]
        public void ClassSetUp()
        {
            calculatorService = new CalculatorService();
        }

        [Test]
        [Description("Should return 0 if 0 passed in")]
        public void ShouldReturn0If0PassedIn()
        {
            Assert.AreEqual(0, calculatorService.Sum(0, 0));
        }

        [Test]
        [Description("Should return the value if 0 and a value are passed in")]
        public void ShouldReturnValueIf0andValuePassedIn()
        {
            Assert.AreEqual(1, calculatorService.Sum(0, 1));
        }

        [Test]
        [Description("Should sum 2 values")]
        public void ShouldSumTwoValues()
        {
            Assert.AreEqual(3, calculatorService.Sum(1, 2));
        }
    }
}