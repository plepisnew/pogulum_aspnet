using Pogulum.Server.Ffmpeg;
using Pogulum.Server.Utils;

namespace Pogulum.Server.Services;

public class VideoService
{
    private readonly UrlFormatParser _urlParser;

    private readonly FfmpegWrapper _ffmpeg;

    private static readonly string[] VALID_EXTENSIONS = new string[] { "mp4", "flv", "mov", "wmv", "avi", "mkv" };

    public VideoService(UrlFormatParser urlParser, FfmpegWrapper ffmpeg)
    {
        _urlParser = urlParser;
        _ffmpeg = ffmpeg;
    }

    // TODO: https://video.stackexchange.com/questions/18247/ffmpeg-concat-demuxer-corrupted-output-freezed-video-on-some-concatenated-parts
    public async Task<byte[]> ConcatenateClips(List<string> clipIds, string extension, bool useIds)
    {
        if (!VALID_EXTENSIONS.Contains(extension))
            throw new ArgumentOutOfRangeException(nameof(extension), $"Video format must be one of {String.Join(",", VALID_EXTENSIONS)}!");

        var blobUrls = useIds ? clipIds.Select(_urlParser.InternalIdToBlobUrl) : clipIds;

        var sessionId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        string concatRecipePath = Path.Join(FfmpegWrapper.PATH_TO_TEMP, $"{sessionId}_concat.txt");
        string outputPath = Path.Join(FfmpegWrapper.PATH_TO_TEMP, $"{sessionId}_output.{extension}");

        /* Get Blobs from twitch.tv and write to local FS */
        using (var client = new HttpClient())
        using (var writer = new StreamWriter(concatRecipePath))
        {
            for (int i = 0; i < blobUrls.Count(); i++)
            {
                string fileName = $"{sessionId}_clip_{i}.{extension}";
                string filePath = Path.Join(FfmpegWrapper.PATH_TO_TEMP, fileName);
                var videoBytes = await client.GetByteArrayAsync(blobUrls.ElementAt(i));

                await File.WriteAllBytesAsync(filePath, videoBytes);
                await writer.WriteLineAsync($"file {fileName}");
            }
        }

        /* Concatanate videos using concat protocol */
        await _ffmpeg.Engine.ExecuteAsync($"-f concat -safe 0 -i {concatRecipePath} -c copy {outputPath}", CancellationToken.None);
        var resultingBytes = await File.ReadAllBytesAsync(outputPath);

        /* Clean FS */
        File.Delete(concatRecipePath);
        File.Delete(outputPath);

        for (int i = 0; i < clipIds.Count(); i++)
            File.Delete(Path.Join(FfmpegWrapper.PATH_TO_TEMP, $"{sessionId}_clip_{i}.{extension}"));

        return resultingBytes;
    }
}