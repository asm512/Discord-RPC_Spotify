using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Models;

namespace RichPresence_Spotify
{
    //Probably the class you're here for 
    class RichPresenceSpotify
    {
        internal static DiscordRPC.RichPresence Presence;
        private static SpotifyLocalAPI _spotify = new SpotifyLocalAPI();
        MainWindow mainWindow = new MainWindow();

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
            Presence = new DiscordRPC.RichPresence();
            Presence.largeImageKey = "spotify_logo_l";
            UpdatePresence(true);
        }

        private async void UpdatePresence(bool startup = true)
        {
            if (startup)
            {

            }
            else
            {
                await Task.Delay(15000);
            }

            StatusResponse status = _spotify.GetStatus();
            try
            {
                if (status.Track.TrackResource.Name != null && !status.Track.IsAd())
                {
                    Presence.state = "I'm listening to " + status.Track.TrackResource.Name + " by " + status.Track.ArtistResource.Name + " at " + TimeSpan.FromSeconds(status.PlayingPosition).ToString(@"mm\:ss").Replace("Time Elapsed: ", "");
                    if (status.Playing)
                    {
                        Presence.smallImageKey = "play";
                        Presence.smallImageText = "Playing";
                    }
                    else
                    {
                        Presence.smallImageKey = "pause";
                        Presence.smallImageText = "Paused";
                    }
                    Presence.details = "Album - " + status.Track.AlbumResource.Name;
                }
                else
                {
                    //Do nothing, or maybe show something related to an Advert
                }
                DiscordRPC.UpdatePresence(ref Presence);
            }
            catch (NullReferenceException)
            {
                mainWindow.AddLogData("Caught a null exception, most likely an ad was playing", MainWindow.UserMessageType.ConsoleLog);
                throw;
            }
            
        }

        public void ShutDownCleanly()
        {
            DiscordRPC.Shutdown();
            MainWindow mainWindow = new MainWindow();
            mainWindow.AddLogData("Successfully shutdown", MainWindow.UserMessageType.ConsoleLog);
        }

        private void _spotify_OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            UpdatePresence();
            mainWindow.AddLogData("Track Change event triggered", MainWindow.UserMessageType.ConsoleLog);
        }

        private void _spotify_OnTrackTimeChange(object sender, TrackTimeChangeEventArgs e)
        {
            UpdatePresence();
            //mainWindow.AddLogData("Track Time Change event triggered", MainWindow.UserMessageType.ConsoleLog);
        }

        private void _spotify_OnPlayStateChange(object sender, PlayStateEventArgs e)
        {
            UpdatePresence();
            mainWindow.AddLogData("Play State event triggered", MainWindow.UserMessageType.ConsoleLog);
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.AddLogData("Discord Error Code : " + (object)errorCode, MainWindow.UserMessageType.Message);
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.AddLogData("Discord Error Code : " + (object)errorCode, MainWindow.UserMessageType.Message);
        }

        private static void ReadyCallback()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.AddLogData("Connected succesfully", MainWindow.UserMessageType.Message);
        }
    }
}
