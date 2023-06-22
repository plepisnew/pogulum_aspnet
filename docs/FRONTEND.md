# Pages

| **Route** | **Name** | **Description** | **Framework** |
| -         | -        | -               | -             |
| `/` | Home Page | Main UI where trending broadcasters, clips and games are displayed. Clicking on an item sends user to `/scraper` with the selected item placed in inputs. | React |
| `navbar` | Navigation Bar | Has options for navigating to `/`, `/scraper`, `/studio`, `/profile` | ?? |
| `/scraper` | Scraper Page | Here clips can be fetched according to three main parameters (game, broadcaster, clip), scrolled through and filtered (by name, date etc). Found clips also display the user and game. Each clip can be scraped (placed into a cart). Number of scraped clips are displayed on the Studio icon in the navbar. | React |
| `/studio` | Studio Page | Displays all scraped clips. Scraped clips (and games and users) can still be favorited. Scraped clips can be removed from the cart. Scraped clips can be dragged into a video-editing interface. A clips length can be adjusted. The currently focused clip is embedded in the browser. The final clip can be previewed and downloaded. | React |
| `/profile?user={user}` | User Profile Page | Displays relevant information about the user, such as favorited clips, games, users, created clips, user details. | MVC |
| `/chatroom` | Chat Page | Displays a chat where users can communicate. Users can also embed their created clips in the chat. | MVC |
