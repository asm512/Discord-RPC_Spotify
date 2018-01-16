using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;

namespace SpotifyRichPresence
{
    //Probably the class you're here for 
    class RichPresenceSpotify
    {
        internal static DiscordRPC.RichPresence Presence;
        internal static SpotifyLocalAPI _spotify = new SpotifyLocalAPI();
        StatusResponse status = _spotify.GetStatus();
        internal static MainWindow mainWindow = new MainWindow();

        public void InitializeDiscordRichPresence()
        {
            _spotify.Connect();
            _spotify.ListenForEvents = true;
            _spotify.OnTrackChange += _spotify_OnTrackChange;
            _spotify.OnPlayStateChange += _spotify_OnPlayStateChange;
            _spotify.OnTrackTimeChange += _spotify_OnTrackTimeChange;
            DiscordRPC.EventHandlers handlers = new DiscordRPC.EventHandlers();
            handlers.readyCallback += new DiscordRPC.ReadyCallback(ReadyCallback);
            handlers.disconnectedCallback += new DiscordRPC.DisconnectedCallback(DisconnectedCallback);
            handlers.errorCallback += new DiscordRPC.ErrorCallback(ErrorCallback);
            handlers.joinCallback += new DiscordRPC.JoinCallback(JoinCallback);
            handlers.spectateCallback += new DiscordRPC.SpectateCallback(SpectateCallback);
            handlers.requestCallback += new DiscordRPC.RequestCallback(RequestCallback);
            DiscordRPC.Initialize("385159254589571073", ref handlers, true, "");
            Presence = new DiscordRPC.RichPresence
            {
                largeImageKey = "spotify_logo_l"
            };
            UpdateRPC();
        }

        internal void UpdateRPC()
        {
            status = _spotify.GetStatus();

            if (status.Track.TrackResource.Name != null && !status.Track.IsAd())
            {
                Presence.state = status.Track.TrackResource.Name + " by " + status.Track.ArtistResource.Name;
                if (status.Playing)
                {
                    Presence.smallImageKey = "play";
                    Presence.smallImageText = "Playing";
                    Presence.startTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                }
                else
                {
                    Presence.smallImageKey = "pause";
                    Presence.smallImageText = "Paused";
                    Presence.startTimestamp = 0;
                }
                Presence.details =  status.Track.AlbumResource.Name;
                DiscordRPC.UpdatePresence(ref Presence);
            }
        }

        /// <summary>
        /// Track changed event
        /// </summary>
        private void _spotify_OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            UpdateRPC();
        }

        /// <summary>
        /// Time elapsed event
        /// </summary>
        private void _spotify_OnTrackTimeChange(object sender, TrackTimeChangeEventArgs e)
        {
            
        }

        /// <summary>
        /// Play state changed event (paused/playing)
        /// </summary>
        private void _spotify_OnPlayStateChange(object sender, PlayStateEventArgs e)
        {
            UpdateRPC();
        }

        private static void RequestCallback(DiscordRPC.JoinRequest request)
        {
        }

        private static void SpectateCallback(string secret)
        {
        }

        private static void JoinCallback(string secret)
        {
        }

        private static void ErrorCallback(int errorCode, string message)
        {
            mainWindow.AddLogItem("Discord Error Code : " + errorCode, MainWindow.LogType.Error);
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            mainWindow.AddLogItem("Discord Error Code : " + errorCode, MainWindow.LogType.Error);
        }

        private static void ReadyCallback()
        {
            mainWindow.AddLogItem("Connected succesfully to Discord", MainWindow.LogType.Message);
        }



    }
}
