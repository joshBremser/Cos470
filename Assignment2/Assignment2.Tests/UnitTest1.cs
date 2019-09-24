using NUnit.Framework;
using Assignment2;
namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void calculateCostTest()
        {
            string a = "High Street";
            string b = "78 high strEEt!@#$%^&*()     ,,,,...//\\|";
            int acost = Assignment2.DollarWords.calculateCost(a);
            int bcost = Assignment2.DollarWords.calculateCost(b);
            Assert.AreEqual(acost,119);
            Assert.AreEqual(bcost,acost);
        }


    }
}