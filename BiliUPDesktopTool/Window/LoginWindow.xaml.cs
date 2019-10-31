﻿using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace BiliUPDesktopTool
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Public Constructors

        public LoginWindow()
        {
            InitializeComponent();

            BindingInit();
        }

        public LoginWindow(Account account)
        {
            InitializeComponent();

            BindingInit();
        }

        #endregion Public Constructors

        #region Private Properties

        private static int page
        {
            get; set;
        }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// 显示模式窗口
        /// </summary>
        /// <param name="info">提示信息</param>
        /// <returns>操作是否成功</returns>
        public bool? ShowDialog(string info)
        {
            //lbl_stauts.Dispatcher.Invoke(delegate () { lbl_stauts.Visibility = Visibility.Visible; lbl_stauts.Content = info; });
            return ShowDialog();
        }

        /// <summary>
        /// 显示模式窗口
        /// </summary>
        /// <returns>操作是否成功</returns>
        public void ShowDialog(MethodFlag f)
        {
            ShowDialog();
        }

        #endregion Public Methods

        #region Private Methods

        private void BindingInit()
        {
            Binding bind_isautologin = new Binding
            {
                Source = Account.Instance,
                Path = new PropertyPath("IsAutoLogin"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            CB_AutoLogin.SetBinding(CheckBox.IsCheckedProperty, bind_isautologin);

            Binding bind_issavepwd = new Binding
            {
                Source = Account.Instance,
                Path = new PropertyPath("IsSavePassword"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            CB_MarkPwd.SetBinding(CheckBox.IsCheckedProperty, bind_issavepwd);
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Close_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }

        private void Btn_Login_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_UName.Text))
            {
                System.Windows.Forms.MessageBox.Show("账号不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(TB_Pwd.Password))
            {
                System.Windows.Forms.MessageBox.Show("密码不能为空！");
                return;
            }

            Account.Instance.UserName = TB_UName.Text;
            if ((bool)CB_MarkPwd.IsChecked)
            {
                Account.Instance.PassWord = TB_Pwd.Password;
            }

            BiliAccount.Account account = BiliAccount.Linq.ByPassword.LoginByPassword(TB_UName.Text, TB_Pwd.Password);
            Account.Instance.Cookies = account.strCookies;
            Account.Instance.Csrf_Token = account.CsrfToken;
            Account.Instance.Expires = account.Expires_Cookies;
            Account.Instance.AccessToken = account.AccessToken;
            Account.Instance.RefreshToken = account.RefreshToken;
            Account.Instance.Expires_AccessToken = account.Expires_AccessToken;
            Account.Instance.Uid = account.Uid;
            Account.Instance.LoginMode = Account.LOGINMODE.ByPassword;
            Account.Instance.Islogin = true;
            Account.Instance.GetInfo();

            Account.Instance.Save();

            Close();
        }

        private void Btn_TabChange_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            switch (page)
            {
                case 0://账号密码
                    ((Storyboard)this.FindResource("TabChangeA")).Begin();
                    page = 1;
                    break;

                case 1://二维码
                    ((Storyboard)this.FindResource("TabChangeB")).Begin();
                    page = 0;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 二维码刷新
        /// </summary>
        /// <param name="newQrCode"></param>
        private void ByQRCode_QrCodeRefresh(Bitmap newQrCode)
        {
            LoadQrCode(newQrCode);
        }

        /// <summary>
        /// 二维码登陆状态刷新
        /// </summary>
        /// <param name="status"></param>
        /// <param name="account"></param>
        private void ByQRCode_QrCodeStatus_Changed(BiliAccount.Linq.ByQRCode.QrCodeStatus status, BiliAccount.Account account = null)
        {
            switch (status)
            {
                case BiliAccount.Linq.ByQRCode.QrCodeStatus.Wating:
                    break;

                case BiliAccount.Linq.ByQRCode.QrCodeStatus.Scaned:
                    break;

                case BiliAccount.Linq.ByQRCode.QrCodeStatus.Success:
                    Account.Instance.Uid = account.Uid;
                    Account.Instance.Expires = account.Expires_Cookies;
                    Account.Instance.Cookies = account.strCookies;
                    Account.Instance.Csrf_Token = account.CsrfToken;
                    Account.Instance.Islogin = true;
                    Account.Instance.LoginMode = Account.LOGINMODE.ByQrcode;
                    Account.Instance.Save();
                    Close();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 加载二维码
        /// </summary>
        private void LoadQrCode(Bitmap qrCodeImage)
        {
            qrcodeBox.Dispatcher.Invoke(delegate ()
            {
                IntPtr myImagePtr = qrCodeImage.GetHbitmap();     //创建GDI对象，返回指针

                BitmapSource imgsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(myImagePtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());  //创建imgSource

                WinAPIHelper.DeleteObject(myImagePtr);

                qrcodeBox.Source = imgsource;
            });
        }

        /// <summary>
        /// 拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBk_Title_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TextBlock_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Btn_Login_MouseUp(null, null);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            BindingOperations.ClearAllBindings(CB_AutoLogin);
            BindingOperations.ClearAllBindings(CB_MarkPwd);

            BiliAccount.Linq.ByQRCode.CancelLogin();
            BiliAccount.Linq.ByQRCode.QrCodeStatus_Changed -= ByQRCode_QrCodeStatus_Changed;
            BiliAccount.Linq.ByQRCode.QrCodeRefresh -= ByQRCode_QrCodeRefresh;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TB_UName.Text = Account.Instance.UserName;
            TB_Pwd.Password = Account.Instance.PassWord;

            Bitmap qrCodeImage = BiliAccount.Linq.ByQRCode.LoginByQrCode();
            LoadQrCode(qrCodeImage);
            BiliAccount.Linq.ByQRCode.QrCodeStatus_Changed += ByQRCode_QrCodeStatus_Changed;
            BiliAccount.Linq.ByQRCode.QrCodeRefresh += ByQRCode_QrCodeRefresh;
        }

        #endregion Private Methods

        #region Public Classes

        public class MethodFlag { };

        #endregion Public Classes
    }
}