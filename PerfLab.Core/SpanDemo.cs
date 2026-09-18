namespace PerfLab.Core;

public class SpanDemo
{
    // Tipik bir log veya veri satırı
    private const string LogLine = "2026-09-17|INFO|UserLoggedInSuccessfully|200";

    // KÖTÜ YAKLAŞIM: Substring ve Split kullanımı (Heap'i çöplüğe çevirir)
    public int ParseWithStringSplit()
    {
        // 1. Array allocation (string[])
        // 2. Her bir parça için yeni string allocation'ları
        string[] parts = LogLine.Split('|');

        string date = parts[0];
        string level = parts[1];
        string message = parts[2];
        string statusCodeStr = parts[3];

        return int.Parse(statusCodeStr);
    }

    // İYİ YAKLAŞIM: Span ile Zero-Allocation
    public int ParseWithSpan()
    {
        // Heap'te yeni bir şey oluşmaz, sadece LogLine referansına bir "pencere" açarız
        ReadOnlySpan<char> span = LogLine.AsSpan();

        // 1. Bölüm
        int firstPipe = span.IndexOf('|');
        ReadOnlySpan<char> date = span.Slice(0, firstPipe);
        span = span.Slice(firstPipe + 1);

        // 2. Bölüm
        int secondPipe = span.IndexOf('|');
        ReadOnlySpan<char> level = span.Slice(0, secondPipe);
        span = span.Slice(secondPipe + 1);

        // 3. Bölüm
        int thirdPipe = span.IndexOf('|');
        ReadOnlySpan<char> message = span.Slice(0, thirdPipe);

        // 4. Bölüm (Kalan Kısım)
        ReadOnlySpan<char> statusCodeSpan = span.Slice(thirdPipe + 1);

        // .NET, string türetmeden doğrudan Span üzerinden int'e parse edebilir
        return int.Parse(statusCodeSpan);
    }
}
