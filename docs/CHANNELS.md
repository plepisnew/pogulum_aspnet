## URL

`GET https://api.twitch.tv/helix/channels`

## Request Query Parameters

| **Paramater** | **Type** | **Required?** | **Description** |
| -             | -        | -             | -               |
| `broadcaster_id` | `String` | Yes | The ID of the broadcaster whose channel you want to get. To specify more than one ID, include this parameter for each broadcaster you want to get. For example, `broadcaster_id=1234&broadcaster_id=5678`. You may specify a maximum of 100 IDs. The API ignores duplicate IDs and IDs that are not found.

## Response Body

| **Field** | **Type** | **Description** |
| -         | -        | -               |
| `data` | `Object[]` | A list that contains information about the specified channels. The list is empty if the specified channels weren’t found. |
| &nbsp;&nbsp;`broadcaster_id` | `String` | An ID that uniquely identifies the broadcaster. |
| &nbsp;&nbsp;`broadcaster_login` | `String` | The broadcaster’s login name. |
| &nbsp;&nbsp;`broadcaster_name` | `String` | The broadcaster’s display name. |
| &nbsp;&nbsp;`broadcaster_language` | `String` | The broadcaster’s preferred language. The value is an ISO 639-1 two-letter language code (for example, en for English). The value is set to “other” if the language is not a Twitch supported language. |
| &nbsp;&nbsp;`game_name` | `String` | 	The name of the game that the broadcaster is playing or last played. The value is an empty string if the broadcaster has never played a game. |
| &nbsp;&nbsp;`game_id` | `String` | 	An ID that uniquely identifies the game that the broadcaster is playing or last played. The value is an empty string if the broadcaster has never played a game. |
| &nbsp;&nbsp;`title` | `String` | 	The title of the stream that the broadcaster is currently streaming or last streamed. The value is an empty string if the broadcaster has never streamed. |
| &nbsp;&nbsp;`delay` | `Unsigned Integer` | The value of the broadcaster’s stream delay setting, in seconds. This field’s value defaults to zero unless 1) the request specifies a user access token, 2) the ID in the broadcaster_id query parameter matches the user ID in the access token, and 3) the broadcaster has partner status and they set a non-zero stream delay value. |
| &nbsp;&nbsp;`tags` | `String[]` | The tags applied to the channel. |