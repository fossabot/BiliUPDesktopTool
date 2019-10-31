﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        /// <summary>
        /// 当前翻页
        /// </summary>
        private int current_page_index = 0;

        #endregion Private Fields

        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();

            Statistics_Box.Children.Add(new StatisticsPage());
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// 广播信息
        /// </summary>
        /// <param name="msg">信息内容</param>
        public void NotifyMsg(string msg)
        {
        }

        public void ToTab(int id)
        {
            switch (id)
            {
                case 0:
                    Btn_Home_MouseUp(null, null);
                    break;

                case 1:
                    Btn_Statistics_MouseUp(null, null);
                    break;

                default:
                    break;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void Btn_Close_MouseEnter(object sender, MouseEventArgs e)
        {
            Btn_Close.Background = new SolidColorBrush(Color.FromArgb(0xbf, 0xff, 0x00, 0x00));
        }

        private void Btn_Close_MouseLeave(object sender, MouseEventArgs e)
        {
            Btn_Close.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void Btn_Close_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Statistics_Box.Children.Clear();
            Close();
            GC.Collect();
            GC.Collect(1);
        }

        private void Btn_Home_MouseEnter(object sender, MouseEventArgs e)
        {
            if (current_page_index != 0)
            {
                Btn_Home.Background = new SolidColorBrush(Color.FromArgb(0x33, 0xff, 0xff, 0xff));
            }
        }

        private void Btn_Home_MouseLeave(object sender, MouseEventArgs e)
        {
            Btn_Home.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void Btn_Home_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (current_page_index != 0)
            {
                ThicknessAnimation an = new ThicknessAnimation
                {
                    From = Active_Bg.Margin,
                    To = new Thickness(0, 6, 0, 0),
                    Duration = new Duration(TimeSpan.FromMilliseconds(500))
                };
                Active_Bg.BeginAnimation(MarginProperty, an);

                an.From = RB_Wrap.Margin;
                an.To = new Thickness(0, 0, 0, -822);
                RB_Wrap.BeginAnimation(MarginProperty, an);

                Lbl_Title.Text = "首页";

                current_page_index = 0;
            }
        }

        private void Btn_MinSize_MouseEnter(object sender, MouseEventArgs e)
        {
            Btn_MinSize.Background = new SolidColorBrush(Color.FromArgb(0x7f, 0x23, 0xad, 0xe5));
        }

        private void Btn_MinSize_MouseLeave(object sender, MouseEventArgs e)
        {
            Btn_MinSize.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void Btn_MinSize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Btn_More_MouseEnter(object sender, MouseEventArgs e)
        {
            if (current_page_index != 2)
            {
                Btn_More.Background = new SolidColorBrush(Color.FromArgb(0x33, 0xff, 0xff, 0xff));
            }
        }

        private void Btn_More_MouseLeave(object sender, MouseEventArgs e)
        {
            Btn_More.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void Btn_More_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (current_page_index != 2)
            {
                ThicknessAnimation an = new ThicknessAnimation
                {
                    From = Active_Bg.Margin,
                    To = new Thickness(0, 106, 0, 0),
                    Duration = new Duration(TimeSpan.FromMilliseconds(500))
                };
                Active_Bg.BeginAnimation(MarginProperty, an);

                an.From = RB_Wrap.Margin;
                an.To = new Thickness(0, -822, 0, 0);
                RB_Wrap.BeginAnimation(MarginProperty, an);

                Lbl_Title.Text = "更多";

                current_page_index = 2;
            }
        }

        private void Btn_Statistics_MouseEnter(object sender, MouseEventArgs e)
        {
            if (current_page_index != 1)
            {
                Btn_Statistics.Background = new SolidColorBrush(Color.FromArgb(0x33, 0xff, 0xff, 0xff));
            }
        }

        private void Btn_Statistics_MouseLeave(object sender, MouseEventArgs e)
        {
            Btn_Statistics.Background = new SolidColorBrush(Color.FromArgb(0x02, 0xff, 0xff, 0xff));
        }

        private void Btn_Statistics_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (current_page_index != 1)
            {
                ThicknessAnimation an = new ThicknessAnimation
                {
                    From = Active_Bg.Margin,
                    To = new Thickness(0, 56, 0, 0),
                    Duration = new Duration(TimeSpan.FromMilliseconds(500))
                };
                Active_Bg.BeginAnimation(MarginProperty, an);

                an.From = RB_Wrap.Margin;
                an.To = new Thickness(0, -411, 0, -411);
                RB_Wrap.BeginAnimation(MarginProperty, an);

                Lbl_Title.Text = "数据展示设置";

                current_page_index = 1;
            }
        }

        private void Control_Box_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        #endregion Private Methods
    }
}