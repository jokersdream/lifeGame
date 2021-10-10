using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace lifeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool btnMusicPlay = true;

        SoundPlayer soundPlayer = new System.Media.SoundPlayer(Properties.Resources.天空之城);

        Map map = new Map();

        public MainWindow()
        {
            InitializeComponent();

            txtLength.Text = "25";
            txtWidth.Text = "25";
            lable_Generation.Content = 0;
            btnStart.IsEnabled = false;

            btn_music_Click(null, null);
        }

        private void btnMap_Click(object sender, RoutedEventArgs e)
        {
            map.initialize(int.Parse(txtLength.Text), int.Parse(txtWidth.Text));

            game.Rows = map.row;
            game.Columns = map.column;

            for (int i = 0; i != map.row; ++i)
            {
                for (int j = 0; j != map.column; ++j)
                {
                    Button button = new Button();
                    int number = i * map.row + j;
                    button.Name = "No_" + number;
                    button.BorderThickness = new Thickness(0.1);

                    if (map.map[i][j] == 0)
                    {
                        button.Background = Brushes.Black;
                    }
                    else if (map.map[i][j] == 1)
                    {
                        button.Background = Brushes.Green;
                    }
                    game.Children.Add(button);
                }
            }

            btnStart.IsEnabled = true;
            btnMap.IsEnabled = false;
        }

        private void update()    ///update game
        {
            map.refresh();

            foreach (int number in map.setLive)
            {
                string target = "No_" + number;
                Button button = new Button();

                foreach (Control control in game.Children)
                {
                    if (control.Name == target)
                    {
                        button = (Button)control;
                        break;
                    }
                }
                button.Background = Brushes.Green;
            }
            map.setLive.Clear();

            foreach (int number in map.setDie)
            {
                string target = "No_" + number;
                Button button = new Button();

                foreach (Control control in game.Children)
                {
                    if (control.Name == target)
                    {
                        button = (Button)control;
                        break;
                    }
                }
                button.Background = Brushes.Black;
            }
            map.setDie.Clear();

            lable_Generation.Content = map.generation;
        }

        DispatcherTimer timer = new DispatcherTimer();
        bool timerTickFlag = true;

        private void timer_Tick(object sender, EventArgs e)
        {
            update();
        }

        private void btn_music_Click(object sender, RoutedEventArgs e)
        {
            if (btnMusicPlay)
            {
                soundPlayer.PlayLooping();
                btnMusicPlay = false;
            }
            else
            {
                soundPlayer.Stop();
                btnMusicPlay = true;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (timerTickFlag)
            {
                timerTickFlag = false;
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = new TimeSpan(0, 0, 0, 1);
            }

            if ((string)btnStart.Content == "开始")
            {
                btnStart.Content = "暂停";
                timer.Start();
            }
            else
            {
                btnStart.Content = "开始";
                timer.Stop();
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnStart.Content == "暂停")
            {
                btnStart_Click(sender, e);
            }

            game.Children.Clear();

            lable_Generation.Content = 0;

            btnMap.IsEnabled = true;
            btnStart.IsEnabled = false;
        }
    }
}
