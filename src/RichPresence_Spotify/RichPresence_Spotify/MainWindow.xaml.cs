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

namespace RichPresence_Spotify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public enum UserMessageType
        {
            ConsoleLog,
            ErrorLog,
            Message
        }

        public void AddLogData(string logdata, UserMessageType messageType)
        {
            switch (messageType)
            {
                case UserMessageType.ConsoleLog:
                    logdata = DateTime.Now.ToShortTimeString() + " EVENT : " + logdata;
                    break;
                case UserMessageType.ErrorLog:
                    logdata = DateTime.Now.ToShortTimeString() + " ERROR : " + logdata;
                    break;
                case UserMessageType.Message:
                    logdata = DateTime.Now.ToShortTimeString() + " MESSAGE : " + logdata;
                    break;
            }
            Dispatcher.Invoke(() =>
            {
                UserVisibleLog.Document.Blocks.Add(new Paragraph(new Run(logdata)));
                UserVisibleLog.ScrollToEnd();
            });



        }

        private void InitializeApplication_Click(object sender, RoutedEventArgs e)
        {
            RichPresenceSpotify richPresenceSpotify = new RichPresenceSpotify();
            richPresenceSpotify.InitializeDiscordRichPresence();
            AddLogData("Plugin succesfully started", UserMessageType.ConsoleLog);
        }

        private void ShutownDiscord_Click(object sender, RoutedEventArgs e)
        {
            RichPresenceSpotify richPresenceSpotify = new RichPresenceSpotify();
            richPresenceSpotify.ShutDownCleanly();
        }

        private void ReadMe_Click(object sender, RoutedEventArgs e)
        {
            ReadMeForm readMeForm = new ReadMeForm();
            readMeForm.ShowDialog();
        }

        private void AttemptStartSpotify_Click(object sender, RoutedEventArgs e)
        {
            if (!SpotifyLocalAPI.IsSpotifyRunning())
            {
                SpotifyLocalAPI.RunSpotify();
            }
            else
            {
                AddLogData("Spotify is already running!", UserMessageType.ErrorLog);
            }
        }

        private void AttemptStartWebHelper_Click(object sender, RoutedEventArgs e)
        {
            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                SpotifyLocalAPI.RunSpotify();
            }
            else
            {
                AddLogData("Spotify WebHelper is already running!", UserMessageType.ErrorLog);
            }
        }

        private void AttemptKillSpotify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("spotify"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                AddLogData(ex.Message, UserMessageType.ErrorLog);
            }
        }

        //Shutdown cleanly regardless of user pressing shutdown
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RichPresenceSpotify richPresenceSpotify = new RichPresenceSpotify();
            richPresenceSpotify.ShutDownCleanly();
            Environment.Exit(0);
        }
    }
}
