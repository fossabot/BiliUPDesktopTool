﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// DataViewer.xaml 的交互逻辑
    /// </summary>
    public partial class DataViewer : UserControl
    {
        #region Public Fields

        public static readonly DependencyProperty DataModeProperty =
            DependencyProperty.Register("DataMode", typeof(List<string>), typeof(UserControl), new PropertyMetadata(new List<string>(3), new PropertyChangedCallback(ChangeView)));

        #endregion Public Fields

        #region Public Constructors

        public DataViewer()
        {
            InitializeComponent();

            SetValue(DataModeProperty, new List<string>(3));

            incr.PostiveAndNegativeChanged += Incr_PostiveAndNegativeChanged;
        }

        #endregion Public Constructors

        #region Public Properties

        public List<string> DataMode
        {
            get { return (List<string>)GetValue(DataModeProperty); }
            set { SetValue(DataModeProperty, value); }
        }

        /// <summary>
        /// 数据标题
        /// </summary>
        public string Title
        {
            get { return DataTitle.Text.ToString(); }
            set { DataTitle.Text = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public static void ChangeView(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DataViewer r = (DataViewer)sender;
            Binding binding_num, binding_incr;
            List<string> value = e.NewValue as List<string>;
            if (value.Count > 3) value.RemoveRange(0, value.Count - 3);
            if (value != null && value.Count == 3)
            {
                switch (value[0])
                {
                    case "video":
                        binding_num = new Binding()
                        {
                            Source = BiliUPData.Intance.video,
                            Mode = BindingMode.TwoWay,
                            Path = new PropertyPath(value[1])
                        };
                        binding_incr = new Binding()
                        {
                            Source = BiliUPData.Intance.video,
                            Mode = BindingMode.TwoWay,
                            Path = new PropertyPath(value[2])
                        };
                        BindingOperations.SetBinding(r.num, RollingNums.numProperty, binding_num);
                        BindingOperations.SetBinding(r.incr, RollingNums.numProperty, binding_incr);
                        break;

                    case "article":
                        binding_num = new Binding()
                        {
                            Source = BiliUPData.Intance.article,
                            Mode = BindingMode.TwoWay,
                            Path = new PropertyPath(value[1])
                        };
                        binding_incr = new Binding()
                        {
                            Source = BiliUPData.Intance.article,
                            Mode = BindingMode.TwoWay,
                            Path = new PropertyPath(value[2])
                        };
                        BindingOperations.SetBinding(r.num, RollingNums.numProperty, binding_num);
                        BindingOperations.SetBinding(r.incr, RollingNums.numProperty, binding_incr);
                        break;

                    default:
                        BindingOperations.ClearAllBindings(r.num);
                        BindingOperations.ClearAllBindings(r.incr);
                        break;
                }
                r.Visibility = Visibility.Visible;
            }
            else if (value != null)
            {
                BindingOperations.ClearAllBindings(r.num);
                BindingOperations.ClearAllBindings(r.incr);
                r.Visibility = Visibility.Visible;
            }
            else
            {
                r.Visibility = Visibility.Hidden;
            }
        }

        public void ChangeView(List<string> value)
        {
            Binding binding_num, binding_incr;
            if (value.Count > 3) value.RemoveRange(0, value.Count - 3);
            if (value != null && value.Count == 3)
            {
                switch (value[0])
                {
                    case "video":
                        binding_num = new Binding()
                        {
                            Source = BiliUPData.Intance.video,
                            Mode = BindingMode.TwoWay,
                            Path = new PropertyPath(value[1])
                        };
                        binding_incr = new Binding()
                        {
                            Source = BiliUPData.Intance.video,
                            Mode = BindingMode.TwoWay,
                            Path = new PropertyPath(value[2])
                        };
                        BindingOperations.SetBinding(num, RollingNums.numProperty, binding_num);
                        BindingOperations.SetBinding(incr, RollingNums.numProperty, binding_incr);
                        break;

                    case "article":
                        binding_num = new Binding()
                        {
                            Source = BiliUPData.Intance.article,
                            Mode = BindingMode.TwoWay,
                            Path = new PropertyPath(value[1])
                        };
                        binding_incr = new Binding()
                        {
                            Source = BiliUPData.Intance.article,
                            Mode = BindingMode.TwoWay,
                            Path = new PropertyPath(value[2])
                        };
                        BindingOperations.SetBinding(num, RollingNums.numProperty, binding_num);
                        BindingOperations.SetBinding(incr, RollingNums.numProperty, binding_incr);
                        break;

                    default:
                        BindingOperations.ClearAllBindings(num);
                        BindingOperations.ClearAllBindings(incr);
                        break;
                }
                Visibility = Visibility.Visible;
            }
            else if (value != null)
            {
                BindingOperations.ClearAllBindings(num);
                BindingOperations.ClearAllBindings(incr);
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Hidden;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// 初始化绑定
        /// </summary>
        private void BindingInit()
        {
            Binding bind_datatitle_color = new Binding()
            {
                Source = Skin.Instance,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("DesktopWnd_FontColor")
            };
            SetBinding(ForegroundProperty, bind_datatitle_color);
        }

        private void Incr_PostiveAndNegativeChanged(object sender, RollingNums.PostiveAndNegativeChangedEventArgs e)
        {
            if (!e.NewValue)
            {
                DoubleAnimation an = new DoubleAnimation
                {
                    From = 0,
                    To = 180,
                    Duration = new Duration(TimeSpan.FromMilliseconds(250))
                };
                trans.BeginAnimation(RotateTransform.AngleProperty, an);
            }
            else
            {
                DoubleAnimation an = new DoubleAnimation
                {
                    From = 180,
                    To = 0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(250))
                };
                trans.BeginAnimation(RotateTransform.AngleProperty, an);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeView(DataMode);

            if (Tag?.ToString() == "Desktop")
            {
                BindingInit();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            BindingOperations.ClearAllBindings(this);
            BindingOperations.ClearAllBindings(num);
            BindingOperations.ClearAllBindings(incr);

            incr.PostiveAndNegativeChanged -= Incr_PostiveAndNegativeChanged;
        }

        #endregion Private Methods
    }
}