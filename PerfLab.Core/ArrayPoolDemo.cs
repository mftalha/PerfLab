namespace PerfLab.Core;

using System.Buffers;

public class ArrayPoolDemo
{
    private const int BufferSize = 100_000; // ~100 KB (LOH sınırı olan 85 KB üstü)

    // Klasik Yaklaşım: Her seferinde new byte[] -> LOH tahsisatı ve GC yükü
    public byte[] ProcessWithNewArray()
    {
        byte[] buffer = new byte[BufferSize];

        // Örnek bir işlem yapılıyormuş gibi
        buffer[0] = 255;
        buffer[BufferSize - 1] = 255;

        return buffer;
    }

    // Modern Yaklaşım: ArrayPool -> 0 Allocation, LOH yükü yok
    public void ProcessWithArrayPool()
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
        try
        {
            buffer[0] = 255;
            buffer[BufferSize - 1] = 255;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}
