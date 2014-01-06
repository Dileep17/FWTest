using NUnit.Framework;

namespace Tests
{
    class Verify
    {
            public void AreEqual(object expected, object actual, string message, bool @continue = true)
            {
                try
                {
                    Assert.AreEqual(expected,actual,message);        
                }
                catch (AssertionException e)
                {
                    TestFailure.Failure(message);
                }
                
            }
    }
}
