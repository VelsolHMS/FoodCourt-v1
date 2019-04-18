using Foodcourt.Model;
using System;
using System.Windows;
using System.Data;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        Userlogin ul = new Userlogin();
        amount am = new amount();
        registration r = new registration();

        public static string u,signin,uname, aaa, usign, udate,ddd;
        public static DateTime  u1,t1,di,dp;
        public static int count = 0;
        public login()
        {
            InitializeComponent();
        }
        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(txtusern.Text)))
            {
                MessageBox.Show("Username should not be empty.!");
            }
            else
            {
                if ((string.IsNullOrWhiteSpace(txtpwd.Password)))
                {
                    MessageBox.Show("Password should not be empty.!");
                }
                else
                {
                    ul.REG_UserName = txtusern.Text;
                    ul.REG_Password = txtpwd.Password;
                    ul.Password();
                    if (ul.a == 1)
                    {
                        u = ul.REG_UserName;
                        MainWindow mw = new MainWindow();
                        mw.Show();
                        signin = DateTime.Now.ToShortTimeString();
                        r.USERNAME = u;
                        r.SIGNIN = signin;
                        r.INSERT_DATE = DateTime.Now;
                        r.UserInsert();
                        DataTable DT = r.User();
                        uname = DT.Rows[0]["USERNAME"].ToString();
                        usign = Convert.ToDateTime(DT.Rows[0]["SIGNIN"]).ToShortTimeString();
                        u1 = Convert.ToDateTime(usign);
                        aaa = Convert.ToString(DateTime.Now.ToShortTimeString());
                        t1 = Convert.ToDateTime(aaa);
                        udate = Convert.ToDateTime(DT.Rows[0]["INSERT_DATE"]).ToShortDateString();
                        di = Convert.ToDateTime(udate);
                        ddd = Convert.ToString(DateTime.Now.ToShortDateString());
                        dp = Convert.ToDateTime(ddd);
                        if(di == dp)
                        {
                            if(DT.Rows.Count == 1)
                            {
                                am.Amount();
                            }
                            else
                            {

                            }
                        }
                        //if (t1 <= u1)
                        //{
                        //    am.Amount();
                        //}
                        //else
                        //{
                            
                        //}
                        this.Close();
                    }
                    else
                    {
                        txtpwd.Password = "";
                        MessageBox.Show("Incorrect Password");
                    }
                }
            }
        }
        private void btncls_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void txtusern_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(txtusern.Text)))
            {
                MessageBox.Show("Username should not be empty");
            }
            else
            {
                ul.REG_UserName = txtusern.Text;
                ul.User_Name();
                if (ul.a == 1)
                {
                }
                else
                {
                    txtusern.Text = "";
                    MessageBox.Show("Username does not exists");
                }
            }
        }
        private void txtpwd_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(txtusern.Text)))
            {
            }
            else
            {
                if ((string.IsNullOrWhiteSpace(txtpwd.Password)))
                {
                    MessageBox.Show("Password should not be empty.!");
                }
                else
                {
                    if ((string.IsNullOrWhiteSpace(txtusern.Text)))
                    {
                        MessageBox.Show("Username should not be empty.!");
                    }
                    else
                    {
                        ul.REG_UserName = txtusern.Text;
                        ul.REG_Password = txtpwd.Password;
                        ul.Password();
                        if (ul.a == 1)
                        {
                            u = ul.REG_UserName;
                        }
                        else
                        {
                            txtpwd.Password = "";
                            MessageBox.Show("Incorrect Password");
                        }
                    }
                }
            }
        }
    }
}