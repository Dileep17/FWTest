using NUnit.Framework;

namespace Tests
{
    class BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            TestFailure.Clear();
        }



        [TearDown]
        public void TearDown()
        {
            TestFailure.Publish();
        }


    }
}
