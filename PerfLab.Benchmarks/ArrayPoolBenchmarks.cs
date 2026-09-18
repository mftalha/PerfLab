using BenchmarkDotNet.Attributes;
using PerfLab.Core;

namespace PerfLab.Benchmarks;

// MemoryDiagnoser: Belirteç olarak Gen 0/1/2 ve Allocated Bytes metriklerini rapora ekler
[MemoryDiagnoser]
public class ArrayPoolBenchmarks
{
    private readonly ArrayPoolDemo _demo = new();

    [Benchmark(Baseline = true)]
    public void NormalAllocation()
    {
        _demo.ProcessWithNewArray();
    }

    [Benchmark]
    public void ArrayPoolAllocation()
    {
        _demo.ProcessWithArrayPool();
    }
}
