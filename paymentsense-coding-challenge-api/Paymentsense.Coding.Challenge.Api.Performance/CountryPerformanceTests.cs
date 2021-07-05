using NBench;
using Nito.AsyncEx;
using Paymentsense.Coding.Challenge.Api.Specs.TestFramework;
using Pro.NBench.xUnit.XunitExtensions;
using System.Diagnostics;
using System.Net.Http;
using Xunit.Abstractions;

namespace Paymentsense.Coding.Challenge.Api.Performance
{
    public class CountryPerformanceTests
    {
        private const string AddCounter = "AddCounter";
        private const double MaxMemoryAllocation = 256000000d;

        private HttpClient _client;
        private Counter _addCounter;

        public CountryPerformanceTests(ITestOutputHelper output)
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new XunitTraceListener(output));
        }

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            TestServerSetup.StartServer();
            MockServerSetup.Start();

            _client = TestServerSetup.TestServer.CreateClient();

            _addCounter = context.GetCounter(AddCounter);

            AsyncContext.Run(() => _client.GetAsync("/api/v1/countries"));
            AsyncContext.Run(() => _client.GetAsync("/api/v1/countries/paginated?PageNumber=1&PageSize=10"));
        }

        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
            TestServerSetup.StopServer();
            MockServerSetup.Stop();
        }

        [NBenchFact]
        [PerfBenchmark(
            Description = "Test to ensure that get all countries performs to an agreed standard and new chnages do not degrade.",
            NumberOfIterations = 1, 
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, 
            TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddCounter, MustBe.GreaterThan, 500)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, MaxMemoryAllocation)]
        public void GetAllCountries_ThroughputTest()
        {
            AsyncContext.Run(() => _client.GetAsync("/api/v1/countries"));
            _addCounter.Increment();
        }

        [NBenchFact]
        [PerfBenchmark(
            Description = "Test to ensure that get paged countries performs to an agreed standard and new chnages do not degrade.",
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000,
            TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddCounter, MustBe.GreaterThan, 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, MaxMemoryAllocation)]
        public void GetPagedCountries_ThroughputTest()
        {
            AsyncContext.Run(() => _client.GetAsync("/api/v1/countries/paginated?PageNumber=1&PageSize=10"));

            _addCounter.Increment();
        }

        [NBenchFact]
        [PerfBenchmark(
            Description = "Test to ensure that get country detail performs to an agreed standard and new chnages do not degrade.",
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000,
            TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddCounter, MustBe.GreaterThan, 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, MaxMemoryAllocation)]
        public void GetCountryDetail_ThroughputTest()
        {
            AsyncContext.Run(() => _client.GetAsync("/api/v1/countries/detail?countryName=France"));

            _addCounter.Increment();
        }
    }
}
