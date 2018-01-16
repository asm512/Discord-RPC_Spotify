# Discord-RPC_Spotify


Discord-RPC_Spotify is a plugin which, with help from the [Spotify Local API Library](https://github.com/JohnnyCrazy/SpotifyAPI-NET), exposes Spotify's current play status and displays it via Discord Rich Presence.

During the time of development, Discord decided to update the UI meaning that on single click on a user's name you won't be able to see the entire song, or most of it rather, but right click > profile will still yield the full information.

##### Kind if important note : the Discord API limits requests at intervals less than 15 seconds, meaning that at certain points the current display may be outdated.


### Old Discord Layout (Pre 30th November 2017)
![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/demo.png?raw=true "Old layout playing song")

![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/demo_2.png?raw=true "Old layout paused song")

### New Discord Layout (30th November 2017 onwards)
![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/new_demo.png?raw=true "Old layout playing song")

![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/new_demo_2.png?raw=true "Old layout paused song")

### New Discord Layout (January? 2017 onwards)

![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/jan2017ui.png?raw=true "New layout UI")


### Stuff to add and fixes:
  - Find a way to correctly handle ```status.Track.TrackResource.Name``` returning null (haven't looked into it properly since I can't force an ad to play)
  



### Third party libraries used
  - [SpotifyLocalAPI](https://github.com/JohnnyCrazy/SpotifyAPI-NET)
  - [MahApps.Metro](mahapps.com)
  - [Json.NET](https://www.newtonsoft.com/json)

### How to build the project
  - Install the third party libraries listed above (reccomended from NuGet),  and ensure that all are fully updated
  - Ensure that any relative or full paths are corrected, such as path to icon
  - Download the [Discord RPC DLL](https://github.com/discordapp/discord-rpc) and ensure it's in the ```bin``` folder
  - Profit







### Development

Feel free to fork the project and PR any changes, feel free to open up an issue too.

MIT
