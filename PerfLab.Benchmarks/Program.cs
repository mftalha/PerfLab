using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PerfLab.Core;

namespace PerfLab.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        // Benchmark'ı çalıştırıyoruz
        BenchmarkRunner.Run<ArrayPoolBenchmarks>();
    }
}

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

/*
 
[MemoryDiagnoser]: BenchmarkDotNet'e "GC aktivitelerini izle ve her metodun heap'te kaç byte allocation (tahsis) yaptığını tabloya ekle" der.

[Benchmark]: Ölçülecek olan spesifik metodları işaretler.

[Benchmark(Baseline = true)]: NormalAllocation metodunu "Referans (Taban) Noktası" kabul eder. Tablodaki Ratio (Oran) sütunu buna göre hesaplanır. Seninkinde Normal 1.0 iken, ArrayPool 0.001 oranına düşmüş.

*/