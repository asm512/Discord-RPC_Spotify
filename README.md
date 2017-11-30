# Discord-RPC_Spotify

## Block Spotify Adverts before using this otherwise it'll throw a null exception!

Discord-RPC_Spotify is a program which, with help from the [Spotify Local API Library](https://github.com/JohnnyCrazy/SpotifyAPI-NET), exposes Spotify's current play status and displays it via Discord Rich Presence.

During the time of development, Discord decided to update the UI meaning that on single click on a user's name you won't be able to see the entire song, or most of it rather, but right click > profile will still yield the full information.

##### Kind if important note : the Discord API limits requests at intervals less than 15 seconds, meaning that without calculating the Unix time (maybe planned feature?!) the time shown in your state will only update every 15 seconds, sometimes slightly more depending on Discord's API reponse. 


### Old Discord Layout (Pre 30th November 2017)
![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/demo.png?raw=true "Old layout playing song")

![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/demo_2.png?raw=true "Old layout paused song")

### New Discord Layout (30th November 2017 onwards)
![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/new_demo.png?raw=true "New layout playing song")

![alt text](https://github.com/peaches6/Discord-RPC_Spotify/blob/master/media/new_demo_2.png?raw=true "New layout paused song")


### Upcoming features:
  - Custom prefixes for Artist display
  - Custom prefix for hover on large image
  - Custom text for main presence state (I'm listening to...)
  - Find a way to correctly handle ```status.Track.TrackResource.Name``` returning null (haven't looked into it properly)
  



### Third party libraries used
  - [SpotifyLocalAPI](https://github.com/JohnnyCrazy/SpotifyAPI-NET)
  - [MahApps.Metro](mahapps.com)
  - [Json.NET](https://www.newtonsoft.com/json)

### How to build the project
  - Install the third party libraries listed above (reccomended from NuGet),  and ensure that all are fully updated
  - Download the [Discord RPC DLL](https://github.com/discordapp/discord-rpc) and ensure it's in the ```bin``` folder
  - F5







### Development

Feel free to fork the project and file any merges if you think people would appreciate them! : ) 

MIT
