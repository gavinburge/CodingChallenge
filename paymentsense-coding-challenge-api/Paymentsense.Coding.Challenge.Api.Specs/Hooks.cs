using Paymentsense.Coding.Challenge.Api.Specs.TestFramework;
using TechTalk.SpecFlow;

namespace Paymentsense.Coding.Challenge.Api.Specs
{
    [Binding]
    public class Hooks
    {
        [BeforeTestRun]
        public static void Setup()
        {
            MockServerSetup.Start();
            TestServerSetup.StartServer();
        }

        [AfterTestRun]
        public static void Teardown()
        {
            MockServerSetup.Stop();
            TestServerSetup.StopServer();
        }
    }
}
