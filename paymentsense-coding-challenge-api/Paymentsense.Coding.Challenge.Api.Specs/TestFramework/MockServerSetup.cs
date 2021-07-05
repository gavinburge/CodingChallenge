using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Paymentsense.Coding.Challenge.Api.Specs.TestFramework
{
    public static class MockServerSetup
    {
        public static WireMockServer MockServer;

        public static void Start()
        {
            MockServer = WireMockServer.Start(port: 44342);

            Reset();
        }

        public static void Stop()
        {
            MockServer.Stop();
        }

        public static void Reset()
        {
            MockServer.Reset();

            MockServer
                .Given(
                    Request.Create()
                            .WithPath("/rest/v2/all")
                            .UsingGet())
                .RespondWith(
                    Response.Create()
                            .WithStatusCode(200)
                            .WithHeader("Content-Type", "application/json")
                            .WithBodyFromFile("./TestFramework/Countries.json"));

            MockServer
                .Given(
                    Request.Create()
                            .WithPath("/rest/v2/name/France")
                            .UsingGet())
                .RespondWith(
                    Response.Create()
                            .WithStatusCode(200)
                            .WithHeader("Content-Type", "application/json")
                            .WithBodyFromFile("./TestFramework/CountryDetail.json"));
        }
    }
}
