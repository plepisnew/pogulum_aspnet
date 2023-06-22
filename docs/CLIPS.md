## URL

`GET https://api.twitch.tv/helix/clips`

## Request Query Parameters

| **Paramater** | **Type** | **Required?** | **Description** |
| -             | -        | -             | -               |
| `broadcaster_id` | `String` | Yes | An ID that identifies the broadcaster whose video clips you want to get. Use this parameter to get clips that were captured from the broadcaster’s streams. |
| `game_id` | `String` | Yes | An ID that identifies the game whose clips you want to get. Use this parameter to get clips that were captured from streams that were playing this game. |
| `id` | `String` | Yes | An ID that identifies the clip to get. To specify more than one ID, include this parameter for each clip you want to get. For example, `id=foo&id=bar`. You may specify a maximum of 100 IDs. The API ignores duplicate IDs and IDs that aren’t found. |
| `started_at` | `String` | No | The start date used to filter clips. The API returns only clips within the start and end date window. Specify the date and time in RFC3339 format. |
| `ended_at` | `String` | No | The end date used to filter clips. If not specified, the time window is the start date plus one week. Specify the date and time in RFC3339 format. |
| `first` | `Integer` | No | The maximum number of clips to return per page in the response. The minimum page size is 1 clip per page and the maximum is 100. The default is 20. |
| `before` | `String` | No | The cursor used to get the previous page of results. The **Pagination** object in the response contains the cursor’s value. |
| `after` | `String` | No | The cursor used to get the next page of results. The **Pagination** object in the response contains the cursor’s value. |

## Response Body

| **Field** | **Type** | **Description** |
| -         | -        | -               |
| `data` | `Object[]` | The list of video clips. For clips returned by game_id or broadcaster_id, the list is in descending order by view count. For lists returned by id, the list is in the same order as the input IDs. |
| &nbsp;&nbsp;`id` | `String` | An ID that uniquely identifies the clip. |
| &nbsp;&nbsp;`url` | `String` | A URL to the clip. |
| &nbsp;&nbsp;`embed_url` | `String` | A URL that you can use in an iframe to embed the clip. |
| &nbsp;&nbsp;`broadcaster_id` | `String` | An ID that identifies the broadcaster that the video was clipped from. |
| &nbsp;&nbsp;`broadcaster_name` | `String` | The broadcaster’s display name. |
| &nbsp;&nbsp;`creator_id` | `String` | An ID that identifies the user that created the clip. |
| &nbsp;&nbsp;`creator_name` | `String` | The user’s display name. |
| &nbsp;&nbsp;`video_id` | `String` | An ID that identifies the video that the clip came from. This field contains an empty string if the video is not available. |
| &nbsp;&nbsp;`game_id` | `String` | The ID of the game that was being played when the clip was created. |
| &nbsp;&nbsp;`language` | `String` | The ISO 639-1 two-letter language code that the broadcaster broadcasts in. For example, en for English. The value is other if the broadcaster uses a language that Twitch doesn’t support. |
| &nbsp;&nbsp;`title` | `String` | The title of the clip. |
| &nbsp;&nbsp;`view_count` | `Integer` | The number of times the clip has been viewed.|
| &nbsp;&nbsp;`created_at` | `String` | The date and time of when the clip was created. The date and time is in RFC3339 format. |
| &nbsp;&nbsp;`thumbnail_url` | `String` | A URL to a thumbnail image of the clip. |
| &nbsp;&nbsp;`duration` | `Float` | The length of the clip, in seconds. Precision is 0.1. |
| &nbsp;&nbsp;`vod_offset` | `Integer` | The zero-based offset, in seconds, to where the clip starts in the video (VOD). Is **null** if the video is not available or hasn’t been created yet from the live stream (see `video_id`). <br /> Note that there’s a delay between when a clip is created during a broadcast and when the offset is set. During the delay period, `vod_offset` is **null**. The delay is indeterminant but is typically minutes long. |
| `pagination` | `Object` | The information used to page through the list of results. The object is empty if there are no more pages left to page through. |
| &nbsp;&nbsp;`cursor` | `String` | The cursor used to get the next page of results. Set the request’s after or before query parameter to this value depending on whether you’re paging forwards or backwards. |
