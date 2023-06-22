## URL

`GET https://api.twitch.tv/helix/games`

## Request Query Parameters

| **Paramater** | **Type** | **Required?** | **Description** |
| -             | -        | -             | -               |
| `id` | `String` | Yes | The ID of the category or game to get. Include this parameter for each category or game you want to get. For example, `&id=1234&id=5678`. You may specify a maximum of 100 IDs. The endpoint ignores duplicate and invalid IDs or IDs that weren’t found. |
| `name` | `String` | Yes | The name of the category or game to get. The name must exactly match the category’s or game’s title. Include this parameter for each category or game you want to get. For example, `&name=foo&name=bar`. You may specify a maximum of 100 names. The endpoint ignores duplicate names and names that weren’t found. |
| `igdb_id` | `String` | Yes | The [IGDB](https://www.igdb.com/) ID of the game to get. Include this parameter for each game you want to get. For example, `&igdb_id=1234&igdb_id=5678`. You may specify a maximum of 100 IDs. The endpoint ignores duplicate and invalid IDs or IDs that weren’t found. |

## Response Body

| **Field** | **Type** | **Description** |
| -         | -        | -               |
| `data` | `Object[]` | The list of categories and games. The list is empty if the specified categories and games weren’t found. |
| &nbsp;&nbsp;`id` | `String` | An ID that identifies the category or game. |
| &nbsp;&nbsp;`name` | `String` | The category’s or game’s name. |
| &nbsp;&nbsp;`box_art_url` | `String` | A URL to the category’s or game’s box art. You must replace the {width}x{height} placeholder with the size of image you want. |
| &nbsp;&nbsp;`igdb_id` | `String` | The ID that [IGDB](https://www.igdb.com/) uses to identify this game. If the IGDB ID is not available to Twitch, this field is set to an empty string. |