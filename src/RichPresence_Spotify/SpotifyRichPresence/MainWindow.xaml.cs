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

namespace SpotifyRichPresence
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NotifyIcon ni = new NotifyIcon();

        internal enum LogType
        {
            SongChanged,
            Warning,
            Error,
            SpotifyError
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FixWidth();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ClosingConfirmation())
            {
                e.Cancel = true;
                MinimizetoTaskbar();
            }
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

        private void AddLogItem(string logData, LogType logType)
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

            logTextbox.AppendText("\r\n" + logData);
            logTextbox.ScrollToEnd();
        }

    }
}
