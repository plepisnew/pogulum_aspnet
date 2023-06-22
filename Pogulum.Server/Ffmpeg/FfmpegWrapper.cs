using FFmpeg.NET;

namespace Pogulum.Server.Ffmpeg;

public class FfmpegWrapper
{
    private static readonly string PATH_TO_FFMPEG = Path.Join(Directory.GetCurrentDirectory(), "Ffmpeg");

    public static readonly string PATH_TO_BINARY = Path.Join(PATH_TO_FFMPEG, "ffmpeg.exe");

    public static readonly string PATH_TO_TEMP = Path.Join(PATH_TO_FFMPEG, "temp");

    public Engine Engine { get; } = new Engine(PATH_TO_BINARY);
}