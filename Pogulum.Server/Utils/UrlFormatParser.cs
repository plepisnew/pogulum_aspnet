namespace Pogulum.Server.Utils;

public class UrlFormatParser
{
    // https://static-cdn.jtvnw.net/ttv-boxart/27471_IGDB-{width}x{height}.jpg
    // =>
    // https://static-cdn.jtvnw.net/ttv-boxart/27471_IGDB-WIDTHxHEIGHT.jpg
    public string BoxArtUrlToConcrete(string boxArtUrl, int dimensions)
    {
        return boxArtUrl
            .Replace("{width}", dimensions.ToString())
            .Replace("{height}", dimensions.ToString());
    }

    // 401219626
    // =>
    // https://clips-media-assets2.twitch.tv/AT-cm%7C401219626.mp4
    public string InternalIdToBlobUrl(string internalId)
    {
        return $"https://clips-media-assets2.twitch.tv/AT-cm%7C{internalId}.mp4";
    }

    // https://clips-media-assets2.twitch.tv/AT-cm%7C401219626-preview-480x272.jpg
    // =>
    // 401219626
    public string ThumbnailUrlToInternalId(string thumbnailUrl)
    {
        return thumbnailUrl
            .Split("-preview-")[0]
            .Split("%7C")[1] + ".mp4";
    }

    // https://clips-media-assets2.twitch.tv/AT-cm%7C401219626-preview-480x272.jpg
    // =>
    // https://clips-media-assets2.twitch.tv/AT-cm%7C401219626.mp4
    public string ThumbnailUrlToBlobUrl(string thumbnailurl)
    {
        return InternalIdToBlobUrl(ThumbnailUrlToInternalId(thumbnailurl));
    }

    // https://clips.twitch.tv/EmpathicProductivePizzaTooSpicy
    // =>
    // EmpathicProductivePizzaTooSpicy
    public string ClipUrlToClipId(string clipUrl)
    {
        return clipUrl.Split("twitch.tv/")[1];
    }

    // EmpathicProductivePizzaTooSpicy
    // =>
    // https://clips.twitch.tv/EmpathicProductivePizzaTooSpicy
    public string ClipIdToClipUrl(string clipId)
    {
        return $"https://clips.twitch.tv/{clipId}";
    }
}