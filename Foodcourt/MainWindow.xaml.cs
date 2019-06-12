using System;
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
using DAL;
using Foodcourt.Model;
using Foodcourt.View;
using Foodcourt.ViewModel;
using System.Data;
using System.Data.SqlClient;

namespace Foodcourt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        userpermissions user = new userpermissions();
        registration r = new registration();
        Color color = (Color)ColorConverter.ConvertFromString("#FF5180A0");
        public MainWindow()
        {
            InitializeComponent();
            HomePageNavigation();
        }
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            r.SIGNOUT = DateTime.Now.ToShortTimeString();
            r.UserUpdate();
            this.Close();
        }
        private void MIHome_Click(object sender, RoutedEventArgs e)
        {
            HomePageNavigation();
        }
        private void HomePageNavigation()
        {
            DataTable dt = user.OPERATION1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int HOME = int.Parse(dt.Rows[0]["HOME"].ToString());
                if (HOME == 1)
                {
                    MIHome.Background = Brushes.White;
                    MIHome.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/PosNew.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void btnprpt_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.MASTER1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int PROPERTY = int.Parse(dt.Rows[0]["PROPERTY"].ToString());
                if (PROPERTY == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = Brushes.White;
                    MIMasters.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    btnprpt.Foreground = Brushes.White;
                    btnreg.Foreground = Brushes.White;
                    btntax.Foreground = Brushes.White;
                    btnitemcategory.Foreground = Brushes.White;
                    btnitems.Foreground = Brushes.White;
                    btnoffers.Foreground = Brushes.White;
                    MIUP.Foreground = Brushes.White;
                    MISTA.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/Property.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void btntax_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.MASTER1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int TAX = int.Parse(dt.Rows[0]["TAX"].ToString());
                if (TAX == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = Brushes.White;
                    MIMasters.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    btnprpt.Foreground = Brushes.White;
                    btnreg.Foreground = Brushes.White;
                    btntax.Foreground = Brushes.White;
                    btnitemcategory.Foreground = Brushes.White;
                    btnitems.Foreground = Brushes.White;
                    btnoffers.Foreground = Brushes.White;
                    MIUP.Foreground = Brushes.White;
                    MISTA.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/Tax.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void btnitemcategory_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.MASTER1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int ITEM_CATEGORY = int.Parse(dt.Rows[0]["ITEM_CATEGORY"].ToString());
                if (ITEM_CATEGORY == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = Brushes.White;
                    MIMasters.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    btnprpt.Foreground = Brushes.White;
                    btnreg.Foreground = Brushes.White;
                    btntax.Foreground = Brushes.White;
                    btnitemcategory.Foreground = Brushes.White;
                    btnitems.Foreground = Brushes.White;
                    btnoffers.Foreground = Brushes.White;
                    MIUP.Foreground = Brushes.White;
                    MISTA.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/ItemCategory.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void btnitems_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.MASTER1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int ITEM_NAME = int.Parse(dt.Rows[0]["ITEM_NAME"].ToString());
                if (ITEM_NAME == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = Brushes.White;
                    MIMasters.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    btnprpt.Foreground = Brushes.White;
                    btnreg.Foreground = Brushes.White;
                    btntax.Foreground = Brushes.White;
                    btnitemcategory.Foreground = Brushes.White;
                    btnitems.Foreground = Brushes.White;
                    btnoffers.Foreground = Brushes.White;
                    MIUP.Foreground = Brushes.White;
                    MISTA.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/ItemName.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void btnreg_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.MASTER1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int USER_REGISTRATION = int.Parse(dt.Rows[0]["USER_REGISTRATION"].ToString());
                if (USER_REGISTRATION == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = Brushes.White;
                    MIMasters.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    btnprpt.Foreground = Brushes.White;
                    btnreg.Foreground = Brushes.White;
                    btntax.Foreground = Brushes.White;
                    btnitemcategory.Foreground = Brushes.White;
                    btnitems.Foreground = Brushes.White;
                    btnoffers.Foreground = Brushes.White;
                    MIUP.Foreground = Brushes.White;
                    MISTA.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/Registration.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void Pettycash_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.OPERATION1();
            if(dt.Rows.Count ==0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int PETTY_CASH = int.Parse(dt.Rows[0]["PETTY_CASH"].ToString());
                if(PETTY_CASH ==1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = Brushes.White;
                    MIOperations.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    Billview.Foreground = Brushes.White;
                    Pettycash.Foreground = Brushes.White;
                    paidouts.Foreground = Brushes.White;
                    MIMIS.Foreground = Brushes.White;
                    CashHandOver.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/PettyCash.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.OPERATION1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int PAIDOUTS = int.Parse(dt.Rows[0]["PAIDOUTS"].ToString());
                if (PAIDOUTS == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = Brushes.White;
                    MIOperations.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    Billview.Foreground = Brushes.White;
                    Pettycash.Foreground = Brushes.White;
                    paidouts.Foreground = Brushes.White;
                    MIMIS.Foreground = Brushes.White;
                    CashHandOver.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/Paidout.xaml", UriKind.RelativeOrAbsolute));
                    
                }
                else
                {
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void MIPRPT_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(new Uri("View/Property.xaml", UriKind.RelativeOrAbsolute));
        }
        private void MIREG_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(new Uri("View/Registration.xaml", UriKind.RelativeOrAbsolute));
        }
        private void MITax_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(new Uri("View/Tax.xaml", UriKind.RelativeOrAbsolute));
        }
        private void MIITMNAM_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(new Uri("View/ItemName.xaml", UriKind.RelativeOrAbsolute));
        }
        private void MIDash_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.OPERATION1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int DASHBOARD = int.Parse(dt.Rows[0]["DASHBOARD"].ToString());
                if (DASHBOARD == 1)
                {
                    MIDash.Background = Brushes.White;
                    MIDash.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/DashBoard.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
            
        }
        private void Billview_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.OPERATION1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int BILL_VIEW = int.Parse(dt.Rows[0]["BILL_VIEW"].ToString());
                if (BILL_VIEW == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    Billview.Foreground = Brushes.White;
                    Pettycash.Foreground = Brushes.White;
                    paidouts.Foreground = Brushes.White;
                    MIMIS.Foreground = Brushes.White;
                    CashHandOver.Foreground = Brushes.White;
                    MIOperations.Background = Brushes.White;
                    MIOperations.Foreground = new System.Windows.Media.SolidColorBrush(color); 
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/BillView.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void MIMIS_Click(object sender, RoutedEventArgs e)
        { 
            DataTable dt = user.OPERATION1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int MIS_COLLECTION = int.Parse(dt.Rows[0]["MIS_COLLECTION"].ToString());
                if (MIS_COLLECTION == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = Brushes.White;
                    MIOperations.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    Billview.Foreground = Brushes.White;
                    Pettycash.Foreground = Brushes.White;
                    paidouts.Foreground = Brushes.White;
                    MIMIS.Foreground = Brushes.White;
                    CashHandOver.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("Mis.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void itm_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.Report1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int ITEM_RATE_LIST = int.Parse(dt.Rows[0]["ITEM_RATE_LIST"].ToString());
                if (ITEM_RATE_LIST == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = Brushes.White;
                    MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/ItemRateListReport.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void IWS_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.Report1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int ITEM_WISE_SALES = int.Parse(dt.Rows[0]["ITEM_WISE_SALES"].ToString());
                if (ITEM_WISE_SALES == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = Brushes.White;
                    MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/Itemwiserpt.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void MIUP_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.MASTER1();
            if (dt.Rows.Count == 0)
            {
                this.MainFrame.Navigate(new Uri("View/Permissions.xaml", UriKind.RelativeOrAbsolute));
                //  MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int USER_PERMISSIONS = int.Parse(dt.Rows[0]["USER_PERMISSIONS"].ToString());
                if (USER_PERMISSIONS == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = Brushes.White;
                    MIMasters.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    btnprpt.Foreground = Brushes.White;
                    btnreg.Foreground = Brushes.White;
                    btntax.Foreground = Brushes.White;
                    btnitemcategory.Foreground = Brushes.White;
                    btnitems.Foreground = Brushes.White;
                    btnoffers.Foreground = Brushes.White;
                    MIUP.Foreground = Brushes.White;
                    MISTA.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/Permissions.xaml", UriKind.RelativeOrAbsolute));  
                }
                else
                {
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void DWS_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.Report1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int DAY_WISE_SALES = int.Parse(dt.Rows[0]["DAY_WISE_SALES"].ToString());
                if (DAY_WISE_SALES == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = Brushes.White;
                    MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/Daywiserpt.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void MWS_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.Report1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int MONTH_WISE_SALE = int.Parse(dt.Rows[0]["MONTH_WISE_SALE"].ToString());
                if (MONTH_WISE_SALE == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = Brushes.White;
                    MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/MonthWiseSale.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        private void TR_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = user.Report1();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            }
            else
            {
                int TAX_REPORT = int.Parse(dt.Rows[0]["TAX_REPORT"].ToString());
                if (TAX_REPORT == 1)
                {
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIMasters.Foreground = Brushes.White;
                    MIReports.Background = Brushes.White;
                    MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/taxrpt.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MainFrame.Content = null;
                    MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                }
            }
        }
        //private void BWS_Click(object sender, RoutedEventArgs e)
        //{
        //    DataTable dt = user.Report1();
        //    if (dt.Rows.Count == 0)
        //    {
        //        MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
        //    }
        //    else
        //    {
        //        int BILL_WISE_SALES = int.Parse(dt.Rows[0]["BILL_WISE_SALES"].ToString());
        //        if (BILL_WISE_SALES == 1)
        //        {
        //            MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
        //            MIHome.Foreground = Brushes.White;
        //            MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
        //            MIOperations.Foreground = Brushes.White;
        //            MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
        //            MIMasters.Foreground = Brushes.White;
        //            MIReports.Background = Brushes.White;
        //            MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
        //            MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
        //            MIDash.Foreground = Brushes.White;
        //            this.MainFrame.Navigate(new Uri("View/billwiserpt.xaml", UriKind.RelativeOrAbsolute));
        //        }
        //        else
        //        {
        //            MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
        //            MIReports.Foreground = Brushes.White;
        //            MainFrame.Content = null;
        //            MessageBox.Show("YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
        //        }
        //    }
        //}
        private void btnmini_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CashHandOver_Click(object sender, RoutedEventArgs e)
        {
            MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
            MIHome.Foreground = Brushes.White;
            MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
            MIOperations.Foreground = Brushes.White;
            Billview.Foreground = Brushes.White;
            Pettycash.Foreground = Brushes.White;
            paidouts.Foreground = Brushes.White;
            MIMIS.Foreground = Brushes.White;
            CashHandOver.Foreground = Brushes.White;
            MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
            MIMasters.Foreground = Brushes.White;
            MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
            MIReports.Foreground = Brushes.White;
            MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Foreground = Brushes.White;
            this.MainFrame.Navigate(new Uri("View/CashHand.xaml", UriKind.RelativeOrAbsolute));      
        }

        private void petty_Click(object sender, RoutedEventArgs e)
        {
            MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
            MIHome.Foreground = Brushes.White;
            MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
            MIOperations.Foreground = Brushes.White;
            MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
            MIMasters.Foreground = Brushes.White;
            MIReports.Background = Brushes.White;
            MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Foreground = Brushes.White;
            petty.Foreground = Brushes.White;
            this.MainFrame.Navigate(new Uri("View/Pettycashrpt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void paid_Click(object sender, RoutedEventArgs e)
        {
            MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
            MIHome.Foreground = Brushes.White;
            MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
            MIOperations.Foreground = Brushes.White;
            MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
            MIMasters.Foreground = Brushes.White;
            MIReports.Background = Brushes.White;
            MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Foreground = Brushes.White;
            petty.Foreground = Brushes.White;
            paid.Foreground = Brushes.White;
            this.MainFrame.Navigate(new Uri("View/Paidoutrpt.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Miscollection_Click(object sender, RoutedEventArgs e)
        {
            MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
            MIHome.Foreground = Brushes.White;
            MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
            MIOperations.Foreground = Brushes.White;
            MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
            MIMasters.Foreground = Brushes.White;
            MIReports.Background = Brushes.White;
            MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Foreground = Brushes.White;
            petty.Foreground = Brushes.White;
            paid.Foreground = Brushes.White;
            Miscollection.Foreground = Brushes.White;
            this.MainFrame.Navigate(new Uri("View/Miscollectiorpt.xaml", UriKind.RelativeOrAbsolute));
        }

      

        private void cashhandoverrpt_Click(object sender, RoutedEventArgs e)
        {
            MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
            MIHome.Foreground = Brushes.White;
            MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
            MIOperations.Foreground = Brushes.White;
            MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
            MIMasters.Foreground = Brushes.White;
            MIReports.Background = Brushes.White;
            MIReports.Foreground = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Foreground = Brushes.White;
            petty.Foreground = Brushes.White;
            paid.Foreground = Brushes.White;
            Miscollection.Foreground = Brushes.White;
            cashhandoverrpt.Foreground = Brushes.White;
            this.MainFrame.Navigate(new Uri("View/Cashhandrpt.xaml", UriKind.RelativeOrAbsolute));
        }
        private void MISTA_Click(object sender, RoutedEventArgs e)
        {
            //DataTable dt = user.MASTER1();
            //if (dt.Rows.Count == 0)
            //{
            //    MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
            //}
            //else
            //{
            //int ITEM_CATEGORY = int.Parse(dt.Rows[0]["ITEM_CATEGORY"].ToString());
            //if (ITEM_CATEGORY == 1)
            //{
                    MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIHome.Foreground = Brushes.White;
                    MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIOperations.Foreground = Brushes.White;
                    MIMasters.Background = Brushes.White;
                    MIMasters.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    btnprpt.Foreground = Brushes.White;
                    btnreg.Foreground = Brushes.White;
                    btntax.Foreground = Brushes.White;
                    btnitemcategory.Foreground = Brushes.White;
                    btnitems.Foreground = Brushes.White;
                    btnoffers.Foreground = Brushes.White;
                    MIUP.Foreground = Brushes.White;
                    MISTA.Foreground = Brushes.White;
                    MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIReports.Foreground = Brushes.White;
                    MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
                    MIDash.Foreground = Brushes.White;
                    this.MainFrame.Navigate(new Uri("View/Stalls.xaml", UriKind.RelativeOrAbsolute));
                //}
                //else
                //{
                //    MIMasters.Background = new System.Windows.Media.SolidColorBrush(color);
                //    MIMasters.Foreground = Brushes.White;
                //    MainFrame.Content = null;
                //    MessageBox.Show(" YOU DON'T HAVE PERMISSIONS TO OPEN THIS PAGE");
                //}
            //}
        }

        private void Btnoffers_Click(object sender, RoutedEventArgs e)
        {
            MIHome.Background = new System.Windows.Media.SolidColorBrush(color);
            MIHome.Foreground = Brushes.White;
            MIOperations.Background = new System.Windows.Media.SolidColorBrush(color);
            MIOperations.Foreground = Brushes.White;
            MIMasters.Background = Brushes.White;
            MIMasters.Foreground = new System.Windows.Media.SolidColorBrush(color);
            btnprpt.Foreground = Brushes.White;
            btnreg.Foreground = Brushes.White;
            btntax.Foreground = Brushes.White;
            btnitemcategory.Foreground = Brushes.White;
            btnitems.Foreground = Brushes.White;
            btnoffers.Foreground = Brushes.White;
            MIUP.Foreground = Brushes.White;
            MISTA.Foreground = Brushes.White;
            MIReports.Background = new System.Windows.Media.SolidColorBrush(color);
            MIReports.Foreground = Brushes.White;
            MIDash.Background = new System.Windows.Media.SolidColorBrush(color);
            MIDash.Foreground = Brushes.White;
            this.MainFrame.Navigate(new Uri("View/OffersZone.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}