using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;

namespace SpotifyRichPresence
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NotifyIcon ni = new NotifyIcon();
        SpotifyLocalAPI _spotify = new SpotifyLocalAPI();

        public enum LogType
        {
            SongChanged,
            Warning,
            Error,
            SpotifyError, 
            Message
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Startup checks here
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FixWidth();
            if (!RunSpotifyStartupChecks())
            {
                AddLogItem("Startup checks have not passed, check that Spotify is running", LogType.SpotifyError);
            }
            else { StartRichPresence(); }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ClosingConfirmation())
            {
                e.Cancel = true;
                MinimizetoTaskbar();
                return;
            }
            DiscordRPC.Shutdown();
        }

        private void FixWidth(int width = 600, int height = 400)
        {
            this.MaxWidth = width;
            this.MinWidth = width;
            this.MaxHeight = height;
            this.MinHeight = height;
        }

        public bool ClosingConfirmation()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Minimize to taskbar?", "", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.None:
                    return false;
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return false;
            }
        }

        private void MinimizetoTaskbar()
        {
            this.Hide();
            ni.Visible = true;
            this.ShowInTaskbar = false;
            ni.BalloonTipIcon = ToolTipIcon.Info;
            ni.Icon = new System.Drawing.Icon("applicationIcon.ico");
            ni.Text = "Spotify Rich Presence Running";
            ni.DoubleClick +=
            delegate (object sender, EventArgs args)
            {
                this.Show();
                this.WindowState = WindowState.Normal;
                ni.Visible = false;
            };
        }

        public void AddLogItem(string logData, LogType logType)
        {
            switch (logType)
            {
                case LogType.SongChanged:
                    logData = "Song Currently Playing: " + logData;
                    break;
                case LogType.Warning:
                    logData = "Warning: " + logData;
                    break;
                case LogType.Error:
                    logData = "Error: " + logData;
                    break;
                case LogType.SpotifyError:
                    logData = "Spotify Error: " + logData;
                    break;
            }

            logData = DateTime.Now.ToLongTimeString() + " " + logData;

            Dispatcher.Invoke(() =>
            {
                logTextbox.AppendText("\r\n" + logData);
                logTextbox.ScrollToEnd();
            });
        }

        private bool RunSpotifyStartupChecks()
        {
            int x = 0;
            if (!SpotifyLocalAPI.IsSpotifyRunning())
            {
                AddLogItem("Spotify is not running", LogType.Warning);
            }
            else { x++; }
            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                AddLogItem("Spotify WebHelper is not running", LogType.Warning);
            }
            else { x++; }
            if (!_spotify.Connect())
            {
                AddLogItem("Failed to connect to Spotify", LogType.Warning);
            }
            else { x++; }

            if (x == 3) { return true; }
            else { return false; }
        }

        private void StartRichPresence()
        {
            RichPresenceSpotify richPresenceSpotify = new RichPresenceSpotify();
            richPresenceSpotify.InitializeDiscordRichPresence();
            AddLogItem("Starting RPC plugin", LogType.Message);
        }

        private void runSpotify_Click(object sender, RoutedEventArgs e)
        {
            SpotifyLocalAPI.RunSpotify();
            AddLogItem("Spotify started", LogType.Message);
        }

        private void runWebhelper_Click(object sender, RoutedEventArgs e)
        {
            SpotifyLocalAPI.RunSpotifyWebHelper();
            AddLogItem("Spotify Webhelper started", LogType.Message);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DiscordRPC.Shutdown();
            StartRichPresence();
        }

        private void shutdownRichPresence_Click(object sender, RoutedEventArgs e)
        {
            DiscordRPC.Shutdown();
        }
    }
}
