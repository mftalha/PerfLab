using BenchmarkDotNet.Attributes;
using PerfLab.Core;

// Gen 0/1/2 aktivitelerini ve Heap Allocation (Tahsisat) miktarını görmek için zorunlu
[MemoryDiagnoser]
public class SpanBenchmarks
{
    private readonly SpanDemo _demo = new();

    [Benchmark(Baseline = true)]
    public int ClassicStringSplit()
    {
        return _demo.ParseWithStringSplit();
    }

    [Benchmark]
    public int ZeroAllocationSpan()
    {
        return _demo.ParseWithSpan();
    }
}
