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
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using CrystalDecisions.CrystalReports.Engine;
using System.Text.RegularExpressions;
using System.Data;
using Foodcourt.Model;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for PosNew.xaml
    /// </summary>
    public partial class PosNew : Page
    {
        BillVIEW BV = new BillVIEW();
        POS1 pos = new POS1();
        property p = new property();
        ItemCategory ic = new ItemCategory();
        ItemsCAT it = new ItemsCAT();
        public static string aa;
        public DataTable dt;
        public int error = 0;
        Regex alp = new Regex(@"^[a-zA-Z0-9 -():']+$");
        Regex num = new Regex(@"^[0-9]+$");
        DataTable dn = new DataTable();
        public PosNew()
        {
            InitializeComponent();
            count = 0;
            DataTable dt = pos.GETPROPERTY();
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                txtname.Text = dt.Rows[0]["PRPT_Name"].ToString();
                txtbillno.Text = pos.BILLID().ToString();
                txttime.Text = DateTime.Now.ToShortTimeString();
            }
            sp.Visibility = Visibility.Visible;
            itemname.Focus();
            clear();
            c = 0; c1 = 0; c2 = 0; c3 = 0; c4 = 0; c5 = 0; c6 = 0; c7 = 0; c8 = 0; c9 = 0; c10 = 0; c11 = 0; c12 = 0; c13 = 0; c14 = 0; c15 = 0; c16 = 0; c17 = 0; c18 = 0; c19 = 0; c20 = 0; c21 = 0; c22 = 0; c23 = 0; c24 = 0; c25 = 0;
            DISITM.Text = "0"; DISITM1.Text = "0"; DISITM2.Text = "0"; DISITM3.Text = "0"; DISITM4.Text = "0"; DISITM5.Text = "0"; DISITM6.Text = "0"; DISITM7.Text = "0"; DISITM8.Text = "0";
            DISITM9.Text = "0"; DISITM10.Text = "0"; DISITM11.Text = "0"; DISITM12.Text = "0"; DISITM13.Text = "0"; DISITM14.Text = "0"; DISITM15.Text = "0"; DISITM16.Text = "0"; DISITM17.Text = "0";
            DISITM18.Text = "0"; DISITM19.Text = "0"; 
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        public void clear()
        {
            itemname.Text = ""; itemrate.Text = ""; quantity.Text = ""; total.Text = ""; itemname1.Text = ""; itemrate1.Text = ""; quantity1.Text = ""; total1.Text = "";
            itemname2.Text = ""; itemrate2.Text = ""; quantity2.Text = ""; total2.Text = ""; itemname3.Text = ""; itemrate3.Text = ""; quantity3.Text = ""; total3.Text = "";
            itemname4.Text = ""; itemrate4.Text = ""; quantity4.Text = ""; total4.Text = ""; itemname5.Text = ""; itemrate5.Text = ""; quantity5.Text = ""; total5.Text = "";
            itemname6.Text = ""; itemrate6.Text = ""; quantity6.Text = ""; total6.Text = ""; itemname7.Text = ""; itemrate7.Text = ""; quantity7.Text = ""; total7.Text = "";
            itemname8.Text = ""; itemrate8.Text = ""; quantity8.Text = ""; total8.Text = ""; itemname9.Text = ""; itemrate9.Text = ""; quantity9.Text = ""; total9.Text = "";
            itemname10.Text = ""; itemrate10.Text = ""; quantity10.Text = ""; total10.Text = ""; itemname11.Text = ""; itemrate11.Text = ""; quantity11.Text = ""; total11.Text = "";
            itemname12.Text = ""; itemrate12.Text = ""; quantity12.Text = ""; total12.Text = ""; itemname13.Text = ""; itemrate13.Text = ""; quantity13.Text = ""; total13.Text = "";
            itemname14.Text = ""; itemrate14.Text = ""; quantity14.Text = ""; total14.Text = ""; itemname15.Text = ""; itemrate15.Text = ""; quantity15.Text = ""; total15.Text = "";
            itemname16.Text = ""; itemrate16.Text = ""; quantity16.Text = ""; total16.Text = ""; itemname17.Text = ""; itemrate17.Text = ""; quantity17.Text = ""; total17.Text = "";
            itemname18.Text = ""; itemrate18.Text = ""; quantity18.Text = ""; total18.Text = ""; itemname19.Text = ""; itemrate19.Text = ""; quantity19.Text = ""; total19.Text = "";
            txtttl.Text = ""; txtgst.Text = ""; txtgttl.Text = "";
        }
        public static string id, itm, aaid, QY, itemnamestlid, stlid, offername,sa,st;
        public static int count,offid;
        public static decimal tot,tot1, tax, gst, gtotbill, tax5tot, tax18tot,tax5sum,tax18sum, maxamount, disper, amdis,disc,dec,discountper,discountam;
        public static decimal a, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19, a20, a21, a22, a23, a24, a25;
        public static int q, q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, q11, q12, q13, q14, q15, q16, q17, q18, q19, q20, q21, q22, q23, q24, q25;
        public static decimal r, r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18, r19, r20, r21, r22, r23, r24, r25;
        public static decimal t, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21, t22, t23, t24, t25;
        public static int c, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25;
        public decimal tax118, tax218, tax318, tax418, tax518, tax618, tax718, tax818, tax918, tax1018, tax1118, tax1218, tax1318, tax1418, tax1518, tax1618, tax1718, tax1818, tax1918, tax2018;
        public static decimal z, z1, z2, z3, z4, z5, z6, z7, z8, z9, z10, z11, z12, z13, z14, z15, z16, z17, z18, z19;
        public static decimal y, y1, y2, y3, y4, y5, y6, y7, y8, y9, y10, y11, y12, y13, y14, y15, y16, y17, y18, y19;
        public static decimal ab, ab1, ab2, ab3, ab4, ab5, ab6, ab7, ab8, ab9, ab10, ab11, ab12, ab13, ab14, ab15, ab16, ab17, ab18, ab19;
        public static decimal av, av1, av2, av3, av4, av5, av6, av7, av8, av9, av10, av11, av12, av13, av14, av15, av16, av17, av18, av19, av20, av21, av22, av23, av24, av25;
        private void DISITM_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity.Text == "")
            { quantity.Focus(); }
            else
            {
                distot = Convert.ToDecimal(total.Text);
                if (DISITM.IsEnabled == true && DISITM.Text != "")
                {
                    z = Convert.ToDecimal(DISITM.Text); y = (a * z) / 100;
                    av = a - y;
                    total.Text = Math.Round(av, 2, MidpointRounding.AwayFromZero).ToString();
                    ab = Convert.ToDecimal(total.Text);
                }
                else if(DISITM.Text == "")
                {
                    z = 0; total.Text = Math.Round(a, 2, MidpointRounding.AwayFromZero).ToString();
                    ab = Convert.ToDecimal(total.Text);
                }
            }
        }
        private void DISITM1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity1.Text == "")
            {
                quantity1.Focus();
            }
            else
            {
                distot1 = Convert.ToDecimal(total1.Text);
                if (DISITM1.IsEnabled == true && DISITM1.Text != "" )
                {
                    z1 = Convert.ToDecimal(DISITM1.Text); y1 = (a1 * z1) / 100;
                    av1 = a1 - y1;
                    total1.Text = Math.Round(av1, 2, MidpointRounding.AwayFromZero).ToString();
                    ab1 = Convert.ToDecimal(total1.Text);
                }
                else if (DISITM1.Text == "")
                {
                    z1 = 0; total1.Text = Math.Round(a1, 2, MidpointRounding.AwayFromZero).ToString();
                    ab1 = Convert.ToDecimal(total1.Text);
                }
            }
        }
        private void DISITM2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity2.Text == "")
            {
                quantity2.Focus();
            }
            else
            {
                distot2 = Convert.ToDecimal(total2.Text);
                if (DISITM2.IsEnabled == true && DISITM2.Text != "" )
                {
                    z2 = Convert.ToDecimal(DISITM2.Text); y2 = (a2 * z2) / 100;
                    av2 = a2 - y2;
                    total2.Text = Math.Round(av2, 2, MidpointRounding.AwayFromZero).ToString();
                    ab2 = Convert.ToDecimal(total2.Text);
                }
                else if (DISITM2.Text == "")
                {
                    z2= 0; total2.Text = Math.Round(a2, 2, MidpointRounding.AwayFromZero).ToString();
                    ab2 = Convert.ToDecimal(total2.Text);
                }
            }
        }
        private void DISITM3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity3.Text == "")
            {
                quantity3.Focus();
            }
            else
            {
                distot3 = Convert.ToDecimal(total3.Text);
                if (DISITM3.IsEnabled == true && DISITM3.Text != "" )
                {
                    z3 = Convert.ToDecimal(DISITM3.Text); y3 = (a3 * z3) / 100;
                    av3 = a3 - y3;
                    total3.Text = Math.Round(av3, 2, MidpointRounding.AwayFromZero).ToString();
                    ab3 = Convert.ToDecimal(total3.Text);
                }
                else if (DISITM3.Text == "")
                {
                    z3 = 0; total3.Text = Math.Round(a3, 2, MidpointRounding.AwayFromZero).ToString();
                    ab3 = Convert.ToDecimal(total3.Text);
                }
            }
        }

        private void DISITM4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity4.Text == "")
            {
                quantity4.Focus();
            }
            else
            {
                distot4 = Convert.ToDecimal(total4.Text);
                if (DISITM4.IsEnabled == true && DISITM4.Text != "" )
                {
                    z4 = Convert.ToDecimal(DISITM4.Text); y4 = (a4 * z4) / 100;
                    av4 = a4 - y4;
                    total4.Text = Math.Round(av4, 2, MidpointRounding.AwayFromZero).ToString();
                    ab4 = Convert.ToDecimal(total4.Text);
                }
                else if (DISITM4.Text == "")
                {
                    z4 = 0; total4.Text = Math.Round(a4, 2, MidpointRounding.AwayFromZero).ToString();
                    ab4 = Convert.ToDecimal(total4.Text);
                }
            }
        }

        private void DISITM5_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity5.Text == "")
            {
                quantity5.Focus();
            }
            else
            {
                distot5 = Convert.ToDecimal(total5.Text);
                if (DISITM5.IsEnabled == true && DISITM5.Text != "" )
                {
                    z5 = Convert.ToDecimal(DISITM5.Text); y5 = (a5 * z5) / 100;
                    av5 = a5 - y5;
                    total5.Text = Math.Round(av5, 2, MidpointRounding.AwayFromZero).ToString();
                    ab5 = Convert.ToDecimal(total5.Text);
                }
                else if (DISITM5.Text == "")
                {
                    z5 = 0; total5.Text = Math.Round(a5, 2, MidpointRounding.AwayFromZero).ToString();
                    ab5 = Convert.ToDecimal(total5.Text);
                }
            }
        }

        private void DISITM6_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity6.Text == "")
            {
                quantity6.Focus();
            }
            else
            {
                distot6 = Convert.ToDecimal(total6.Text);
                if (DISITM6.IsEnabled == true && DISITM6.Text != "" )
                {
                    z6 = Convert.ToDecimal(DISITM6.Text); y6 = (a6 * z6) / 100;
                    av6 = a6 - y6;
                    total6.Text = Math.Round(av6, 2, MidpointRounding.AwayFromZero).ToString();
                    ab6 = Convert.ToDecimal(total6.Text);
                }
                else if (DISITM6.Text == "")
                {
                    z6 = 0; total6.Text = Math.Round(a6, 2, MidpointRounding.AwayFromZero).ToString();
                    ab6 = Convert.ToDecimal(total6.Text);
                }
            }
        }

        private void DISITM7_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity7.Text == "")
            {
                quantity7.Focus();
            }
            else
            {
                distot7 = Convert.ToDecimal(total7.Text);
                if (DISITM7.IsEnabled == true && DISITM7.Text != "" )
                {
                    z7 = Convert.ToDecimal(DISITM7.Text); y7 = (a7 * z7) / 100;
                    av7 = a7 - y7;
                    total7.Text = Math.Round(av7, 2, MidpointRounding.AwayFromZero).ToString();
                    ab7 = Convert.ToDecimal(total7.Text);
                }
                else if (DISITM7.Text == "")
                {
                    z7 = 0; total7.Text = Math.Round(a7, 2, MidpointRounding.AwayFromZero).ToString();
                    ab7 = Convert.ToDecimal(total7.Text);
                }
            }
        }

        private void DISITM8_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity8.Text == "")
            {
                quantity8.Focus();
            }
            else
            {
                distot8 = Convert.ToDecimal(total8.Text);
                if (DISITM8.IsEnabled == true && DISITM8.Text != "" )
                {
                    z8 = Convert.ToDecimal(DISITM8.Text); y8 = (a8 * z8) / 100;
                    av8 = a8 - y8;
                    total8.Text = Math.Round(av8, 2, MidpointRounding.AwayFromZero).ToString();
                    ab8 = Convert.ToDecimal(total8.Text);
                }
                else if (DISITM8.Text == "")
                {
                    z8 = 0; total8.Text = Math.Round(a8, 2, MidpointRounding.AwayFromZero).ToString();
                    ab8 = Convert.ToDecimal(total8.Text);
                }
            }
        }

        private void DISITM9_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity9.Text == "")
            {
                quantity9.Focus();
            }
            else
            {
                distot9 = Convert.ToDecimal(total9.Text);
                if (DISITM9.IsEnabled == true && DISITM9.Text != "" )
                {
                    z9 = Convert.ToDecimal(DISITM9.Text); y9 = (a9 * z9) / 100;
                    av9 = a9 - y9;
                    total9.Text = Math.Round(av9, 2, MidpointRounding.AwayFromZero).ToString();
                    ab9 = Convert.ToDecimal(total9.Text);
                }
                else if (DISITM9.Text == "")
                {
                    z9 = 0; total9.Text = Math.Round(a9, 2, MidpointRounding.AwayFromZero).ToString();
                    ab9 = Convert.ToDecimal(total9.Text);
                }
            }
        }

        private void DISITM10_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity10.Text == "")
            {
                quantity10.Focus();
            }
            else
            {
                distot10 = Convert.ToDecimal(total10.Text);
                if (DISITM10.IsEnabled == true && DISITM10.Text != "" )
                {
                    z10 = Convert.ToDecimal(DISITM10.Text); y10 = (a10 * z10) / 100;
                    av10 = a10 - y10;
                    total10.Text = Math.Round(av10, 2, MidpointRounding.AwayFromZero).ToString();
                    ab10 = Convert.ToDecimal(total10.Text);
                }
                else if (DISITM10.Text == "")
                {
                    z10 = 0; total10.Text = Math.Round(a10, 2, MidpointRounding.AwayFromZero).ToString();
                    ab10 = Convert.ToDecimal(total10.Text);
                }
            }
        }

        private void DISITM11_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity11.Text == "")
            {
                quantity11.Focus();
            }
            else
            {
                distot11 = Convert.ToDecimal(total11.Text);
                if (DISITM11.IsEnabled == true && DISITM11.Text != "" )
                {
                    z11 = Convert.ToDecimal(DISITM11.Text); y11 = (a11 * z11) / 100;
                    av11 = a11 - y11;
                    total11.Text = Math.Round(av11, 2, MidpointRounding.AwayFromZero).ToString();
                    ab11 = Convert.ToDecimal(total11.Text);
                }
                else if (DISITM11.Text == "")
                {
                    z11 = 0; total11.Text = Math.Round(a11, 2, MidpointRounding.AwayFromZero).ToString();
                    ab11 = Convert.ToDecimal(total11.Text);
                }
            }
        }

        private void DISITM12_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity12.Text == "")
            {
                quantity12.Focus();
            }
            else
            {
                distot12 = Convert.ToDecimal(total12.Text);
                if (DISITM12.IsEnabled == true && DISITM12.Text != "" )
                {
                    z12 = Convert.ToDecimal(DISITM12.Text); y12 = (a12 * z12) / 100;
                    av12 = a12 - y12;
                    total12.Text = Math.Round(av12, 2, MidpointRounding.AwayFromZero).ToString();
                    ab12 = Convert.ToDecimal(total12.Text);
                }
                else if (DISITM12.Text == "")
                {
                    z12 = 0; total12.Text = Math.Round(a12, 2, MidpointRounding.AwayFromZero).ToString();
                    ab12 = Convert.ToDecimal(total12.Text);
                }
            }
        }

        private void DISITM13_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity13.Text == "")
            {
                quantity13.Focus();
            }
            else
            {
                distot13 = Convert.ToDecimal(total13.Text);
                if (DISITM13.IsEnabled == true && DISITM13.Text != "" )
                {
                    z13 = Convert.ToDecimal(DISITM13.Text); y13 = (a13 * z13) / 100;
                    av13 = a13 - y13;
                    total13.Text = Math.Round(av13, 2, MidpointRounding.AwayFromZero).ToString();
                    ab13 = Convert.ToDecimal(total13.Text);
                }
                else if (DISITM13.Text == "")
                {
                    z13 = 0; total13.Text = Math.Round(a13, 2, MidpointRounding.AwayFromZero).ToString();
                    ab13 = Convert.ToDecimal(total13.Text);
                }
            }
        }

        private void DISITM14_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity14.Text == "")
            {
                quantity14.Focus();
            }
            else
            {
                distot14 = Convert.ToDecimal(total14.Text);
                if (DISITM14.IsEnabled == true && DISITM14.Text != "" )
                {
                    z14 = Convert.ToDecimal(DISITM14.Text); y14 = (a14 * z14) / 100;
                    av14 = a14 - y14;
                    total14.Text = Math.Round(av14, 2, MidpointRounding.AwayFromZero).ToString();
                    ab14 = Convert.ToDecimal(total14.Text);
                }
                else if (DISITM14.Text == "")
                {
                    z14 = 0; total14.Text = Math.Round(a14, 2, MidpointRounding.AwayFromZero).ToString();
                    ab14 = Convert.ToDecimal(total14.Text);
                }
            }
        }

        private void DISITM15_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity15.Text == "")
            {
                quantity15.Focus();
            }
            else
            {
                distot15 = Convert.ToDecimal(total15.Text);
                if (DISITM15.IsEnabled == true && DISITM15.Text != "" )
                {
                    z15 = Convert.ToDecimal(DISITM15.Text); y15 = (a15 * z15) / 100;
                    av15 = a15 - y15;
                    total15.Text = Math.Round(av15, 2, MidpointRounding.AwayFromZero).ToString();
                    ab15 = Convert.ToDecimal(total15.Text);
                }
                else if (DISITM15.Text == "")
                {
                    z15 = 0; total15.Text = Math.Round(a15, 2, MidpointRounding.AwayFromZero).ToString();
                    ab15 = Convert.ToDecimal(total15.Text);
                }
            }
        }

        private void DISITM16_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity16.Text == "")
            {
                quantity16.Focus();
            }
            else
            {
                distot16 = Convert.ToDecimal(total16.Text);
                if (DISITM16.IsEnabled == true && DISITM16.Text != "" )
                {
                    z16 = Convert.ToDecimal(DISITM16.Text); y16 = (a16 * z16) / 100;
                    av16 = a16 - y16;
                    total16.Text = Math.Round(av16, 2, MidpointRounding.AwayFromZero).ToString();
                    ab16 = Convert.ToDecimal(total16.Text);
                }
                else if (DISITM16.Text == "")
                {
                    z16= 0; total16.Text = Math.Round(a16, 2, MidpointRounding.AwayFromZero).ToString();
                    ab16 = Convert.ToDecimal(total16.Text);
                }
            }
        }

        private void DISITM17_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity17.Text == "")
            {
                quantity17.Focus();
            }
            else
            {
                distot17 = Convert.ToDecimal(total17.Text);
                if (DISITM17.IsEnabled == true && DISITM17.Text != "" )
                {
                    z17 = Convert.ToDecimal(DISITM17.Text); y17 = (a17 * z17) / 100;
                    av17 = a17 - y17;
                    total17.Text = Math.Round(av17, 2, MidpointRounding.AwayFromZero).ToString();
                    ab17 = Convert.ToDecimal(total17.Text);
                }
                else if (DISITM17.Text == "")
                {
                    z17 = 0; total17.Text = Math.Round(a17, 2, MidpointRounding.AwayFromZero).ToString();
                    ab17 = Convert.ToDecimal(total17.Text);
                }
            }
        }

        private void DISITM18_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity18.Text == "")
            {
                quantity18.Focus();
            }
            else
            {
                distot18 = Convert.ToDecimal(total18.Text);
                if (DISITM18.IsEnabled == true && DISITM18.Text != "" )
                {
                    z18 = Convert.ToDecimal(DISITM18.Text); y18 = (a18 * z18) / 100;
                    av18 = a18 - y18;
                    total18.Text = Math.Round(av18, 2, MidpointRounding.AwayFromZero).ToString();
                    ab18 = Convert.ToDecimal(total18.Text);
                }
                else if (DISITM18.Text == "")
                {
                    z18 = 0; total18.Text = Math.Round(a18, 2, MidpointRounding.AwayFromZero).ToString();
                    ab18 = Convert.ToDecimal(total18.Text);
                }
            }
        }

        private void DISITM19_LostFocus(object sender, RoutedEventArgs e)
        {
            if (quantity19.Text == "")
            {
                quantity19.Focus();
            }
            else
            {
                distot19 = Convert.ToDecimal(total19.Text);
                if (DISITM19.IsEnabled == true && DISITM19.Text != "" )
                {
                    z19 = Convert.ToDecimal(DISITM19.Text); y19 = (a19 * z19) / 100;
                    av19 = a19 - y19;
                    total19.Text = Math.Round(av19, 2, MidpointRounding.AwayFromZero).ToString();
                    ab19 = Convert.ToDecimal(total19.Text);
                }
               else if (DISITM19.Text == "")
                {
                    z19 = 0; total19.Text = Math.Round(a19, 2, MidpointRounding.AwayFromZero).ToString();
                    ab19 = Convert.ToDecimal(total19.Text);
                }
            }
        }

        public decimal distot,distot1,distot2,distot3,distot4,distot5,distot6,distot7,distot8,distot9,distot10,distot11,distot12,distot13,distot14,distot15,distot16,distot17,distot18,distot19,distot20,distot21,distot22,distot23,distot24,distot25;
        

        DataTable d = new DataTable();
        public decimal dis, ins, gndtot, ttam;
        public static string stallname, STALLID;
        public static int j = 0, h = 0;
        public DataTable Billprint()
        {
            DataTable billprint = new DataTable();
            billprint.Columns.Add("Name", typeof(string));
            billprint.Columns.Add("Address", typeof(string));
            billprint.Columns.Add("GstNo", typeof(string));
            billprint.Columns.Add("BillNo", typeof(string));
            billprint.Columns.Add("Total", typeof(decimal));
            billprint.Columns.Add("Cgst", typeof(decimal));
            billprint.Columns.Add("Sgst", typeof(decimal));
            billprint.Columns.Add("GrandTotal", typeof(decimal));
            billprint.Columns.Add("Discount", typeof(decimal));
            DataRow DROW = billprint.NewRow();
            DataTable dt1 = pos.Address();
            DataTable dt2 = pos.items();
            DataTable dt3 = pos.DiscountAmount();
            DROW["Name"] = dt1.Rows[0]["PRPT_Name"].ToString();
            DROW["Address"] = dt1.Rows[0]["PRPT_Address"].ToString();
            DROW["GstNO"] = dt1.Rows[0]["PRPT_GST"].ToString();
            DROW["BillNo"] = txtbillno.Text;
            ttam = Convert.ToDecimal(dt2.Rows[0]["BILL_Amount"]);
            DROW["Total"] = ttam;
            decimal tax = Convert.ToDecimal(dt2.Rows[0]["BILL_Tax"].ToString());
            //DROW["Cgst"] = tax / 2;
            //DROW["Sgst"] = tax / 2;
            DROW["Cgst"] = tax5sum;
            DROW["Sgst"] = tax18sum;
            //dis = Convert.ToDecimal(dt2.Rows[0]["BILL_Discount"].ToString());
            //ins = Convert.ToDecimal(dt2.Rows[0]["Bill_InstantDis"].ToString());
            DROW["Discount"] = dt3.Rows[0]["Discount"].ToString();
            gndtot = Convert.ToDecimal(dt2.Rows[0]["BILL_Total"].ToString());
            DROW["GrandTotal"] = (int)Math.Round(gndtot);
            billprint.Rows.Add(DROW);
            return billprint;
        }
        public DataTable Billprint1()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Item", typeof(string));
            d.Columns.Add("Rate", typeof(decimal));
            d.Columns.Add("Quantity", typeof(int));
            d.Columns.Add("Amount", typeof(decimal));
            DataTable s = pos.PrintBill();
            for (int k = 0; k < s.Rows.Count; k++)
            {
                DataRow row = d.NewRow();
                row["Item"] = s.Rows[k]["BILITM_Name"].ToString();
                row["Rate"] = s.Rows[k]["BILITM_Rate"].ToString();
                row["Quantity"] = s.Rows[k]["BILLITM_Quanty"].ToString();
                decimal aa = Convert.ToDecimal(s.Rows[k]["BILITM_Rate"].ToString());
                int b = Convert.ToInt32(s.Rows[k]["BILLITM_Quanty"].ToString());
                row["Amount"] = aa * b;
                d.Rows.Add(row);
            }
            return d;
        }
        public DataTable kot()
        {
            DataTable d = new DataTable();
            d.Columns.Add("Item", typeof(string));
            d.Columns.Add("Qty", typeof(int));
            DataTable dstlss = pos.stlidsss();
            if (j < dstlss.Rows.Count)
            {
                STALLID = dstlss.Rows[j]["STL_ID"].ToString();
                DataTable dsn = pos.STALLNAME();
                stallname = dsn.Rows[0]["STL_Name"].ToString();
                DataTable s = pos.STLIDITEMNAMES();
                for (int i = 0; i < s.Rows.Count; i++)
                {
                    DataRow r = d.NewRow();
                    r["Item"] = s.Rows[i]["BILITM_Name"].ToString();
                    r["Qty"] = s.Rows[i]["BILLITM_Quanty"].ToString();
                    d.Rows.Add(r);
                }
                j = j + 1;
            }
            return d;
        }
        public DataTable kot1()
        {
            DataTable ds = new DataTable();
            ds.Columns.Add("BillNo", typeof(string));
            ds.Columns.Add("BillDate", typeof(DateTime));
            ds.Columns.Add("BillTime", typeof(DateTime));
            ds.Columns.Add("Stallname", typeof(string));
            DataTable dstlss = pos.stlidsss();
            DataRow r = ds.NewRow();
            r["BillNO"] = txtbillno.Text;
            r["BillDate"] = DateTime.Now.Date;
            r["BillTime"] = DateTime.Now;
            r["Stallname"] = stallname;
            ds.Rows.Add(r);
            return ds;
        }
        private void OfferCheck_Click(object sender, RoutedEventArgs e)
        {
            if(OfferCheck.IsChecked == true)
            {
                //cusName.IsEnabled = true;
                //cusMobile.IsEnabled = true;
                pos.CusName = cusName.Text;
                pos.CusMobile = cusMobile.Text;
            }
            else if(OfferCheck.IsChecked == false)
            {
                //cusName.IsEnabled = false;
                //cusMobile.IsEnabled = false;
                pos.CusName = cusName.Text;
                pos.CusMobile = cusMobile.Text;

            }
        }

        public decimal tax15, tax25, tax35, tax45, tax55, tax65, tax75, tax85, tax95, tax105, tax115, tax125, tax135, tax145, tax155, tax165, tax175, tax185, tax195, tax205;
        private void TOTITM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM.SelectedItem;
                itemname.Text = drv.Row["NAM_Name"].ToString();
                TOTITM.Visibility = Visibility.Collapsed;
                if (itemname.Text == "")
                { itemname.Focus(); }
                else
                {
                    id = itemname.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname.Text == "")
                            { }
                            else
                            { itemname.Text = ""; }
                        }
                        else
                        {
                            itemname.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname.Text;
                    quantity.Focus();
                }
            }
        }
        private void TOTITM1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM1.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM1.SelectedItem;
                itemname1.Text = drv.Row["NAM_Name"].ToString();
                TOTITM1.Visibility = Visibility.Collapsed;
                if (itemname1.Text == "")
                { itemname1.Focus(); }
                else
                {
                    id = itemname1.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname1.Text == "")
                            { }
                            else
                            { itemname1.Text = ""; }
                        }
                        else
                        {
                            itemname1.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate1.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname1.Text;
                    quantity1.Focus();
                }
            }
        }

        private void TOTITM2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM2.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM2.SelectedItem;
                itemname2.Text = drv.Row["NAM_Name"].ToString();
                TOTITM2.Visibility = Visibility.Collapsed;
                if (itemname2.Text == "")
                { itemname2.Focus(); }
                else
                {
                    id = itemname2.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname2.Text == "")
                            { }
                            else
                            { itemname2.Text = ""; }
                        }
                        else
                        {
                            itemname2.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate2.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname2.Text;
                    quantity2.Focus();
                }
            }
        }
        private void TOTITM3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM3.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM3.SelectedItem;
                itemname3.Text = drv.Row["NAM_Name"].ToString();
                TOTITM3.Visibility = Visibility.Collapsed;
                if (itemname3.Text == "")
                { itemname3.Focus(); }
                else
                {
                    id = itemname3.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname3.Text == "")
                            { }
                            else
                            { itemname3.Text = ""; }
                        }
                        else
                        {
                            itemname3.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate3.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname3.Text;
                    quantity3.Focus();
                }
            }
        }
        private void TOTITM4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM4.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM4.SelectedItem;
                itemname4.Text = drv.Row["NAM_Name"].ToString();
                TOTITM4.Visibility = Visibility.Collapsed;
                if (itemname4.Text == "")
                { itemname4.Focus(); }
                else
                {
                    id = itemname4.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname4.Text == "")
                            { }
                            else
                            { itemname4.Text = ""; }
                        }
                        else
                        {
                            itemname4.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate4.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname4.Text;
                    quantity4.Focus();
                }
            }
        }
        private void TOTITM5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM5.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM5.SelectedItem;
                itemname5.Text = drv.Row["NAM_Name"].ToString();
                TOTITM5.Visibility = Visibility.Collapsed;
                if (itemname5.Text == "")
                { itemname5.Focus(); }
                else
                {
                    id = itemname5.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname5.Text == "")
                            { }
                            else
                            { itemname5.Text = ""; }
                        }
                        else
                        {
                            itemname5.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate5.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname5.Text;
                    quantity5.Focus();
                }
            }
        }
        private void TOTITM6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM6.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM6.SelectedItem;
                itemname6.Text = drv.Row["NAM_Name"].ToString();
                TOTITM6.Visibility = Visibility.Collapsed;
                if (itemname6.Text == "")
                { itemname6.Focus(); }
                else
                {
                    id = itemname6.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname6.Text == "")
                            { }
                            else
                            { itemname6.Text = ""; }
                        }
                        else
                        {
                            itemname6.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate6.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname6.Text;
                    quantity6.Focus();
                }
            }
        }
        private void TOTITM7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM7.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM7.SelectedItem;
                itemname7.Text = drv.Row["NAM_Name"].ToString();
                TOTITM7.Visibility = Visibility.Collapsed;
                if (itemname7.Text == "")
                { itemname7.Focus(); }
                else
                {
                    id = itemname7.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname7.Text == "")
                            { }
                            else
                            { itemname7.Text = ""; }
                        }
                        else
                        {
                            itemname7.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate7.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname7.Text;
                    quantity7.Focus();
                }
            }
        }
        private void TOTITM8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM8.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM8.SelectedItem;
                itemname8.Text = drv.Row["NAM_Name"].ToString();
                TOTITM8.Visibility = Visibility.Collapsed;
                if (itemname8.Text == "")
                { itemname8.Focus(); }
                else
                {
                    id = itemname8.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname8.Text == "")
                            { }
                            else
                            { itemname8.Text = ""; }
                        }
                        else
                        {
                            itemname8.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate8.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname8.Text;
                    quantity8.Focus();
                }
            }
        }
        private void TOTITM9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM9.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM9.SelectedItem;
                itemname9.Text = drv.Row["NAM_Name"].ToString();
                TOTITM9.Visibility = Visibility.Collapsed;
                if (itemname9.Text == "")
                { itemname9.Focus(); }
                else
                {
                    id = itemname9.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname9.Text == "")
                            { }
                            else
                            { itemname9.Text = ""; }
                        }
                        else
                        {
                            itemname9.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate9.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname9.Text;
                    quantity9.Focus();
                }
            }
        }
        private void TOTITM10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM10.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM10.SelectedItem;
                itemname10.Text = drv.Row["NAM_Name"].ToString();
                TOTITM10.Visibility = Visibility.Collapsed;
                if (itemname10.Text == "")
                { itemname10.Focus(); }
                else
                {
                    id = itemname10.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname10.Text == "")
                            { }
                            else
                            { itemname10.Text = ""; }
                        }
                        else
                        {
                            itemname10.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate10.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname10.Text;
                    quantity10.Focus();
                }
            }
        }
        private void TOTITM11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM11.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM11.SelectedItem;
                itemname11.Text = drv.Row["NAM_Name"].ToString();
                TOTITM11.Visibility = Visibility.Collapsed;
                if (itemname11.Text == "")
                { itemname11.Focus(); }
                else
                {
                    id = itemname11.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname11.Text == "")
                            { }
                            else
                            { itemname11.Text = ""; }
                        }
                        else
                        {
                            itemname11.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate11.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname11.Text;
                    quantity11.Focus();
                }
            }
        }
        private void TOTITM12_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM12.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM12.SelectedItem;
                itemname12.Text = drv.Row["NAM_Name"].ToString();
                TOTITM12.Visibility = Visibility.Collapsed;
                if (itemname12.Text == "")
                { itemname12.Focus(); }
                else
                {
                    id = itemname12.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname12.Text == "")
                            { }
                            else
                            { itemname12.Text = ""; }
                        }
                        else
                        {
                            itemname12.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate12.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname12.Text;
                    quantity12.Focus();
                }
            }
        }
        private void TOTITM13_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM13.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM13.SelectedItem;
                itemname13.Text = drv.Row["NAM_Name"].ToString();
                TOTITM13.Visibility = Visibility.Collapsed;
                if (itemname13.Text == "")
                { itemname13.Focus(); }
                else
                {
                    id = itemname13.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname13.Text == "")
                            { }
                            else
                            { itemname13.Text = ""; }
                        }
                        else
                        {
                            itemname13.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate13.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname13.Text;
                    quantity13.Focus();
                }
            }
        }
        private void TOTITM14_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM14.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM14.SelectedItem;
                itemname14.Text = drv.Row["NAM_Name"].ToString();
                TOTITM14.Visibility = Visibility.Collapsed;
                if (itemname14.Text == "")
                { itemname14.Focus(); }
                else
                {
                    id = itemname14.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname14.Text == "")
                            { }
                            else
                            { itemname14.Text = ""; }
                        }
                        else
                        {
                            itemname14.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate14.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname14.Text;
                    quantity14.Focus();
                }
            }
        }
        private void TOTITM15_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM15.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM15.SelectedItem;
                itemname15.Text = drv.Row["NAM_Name"].ToString();
                TOTITM15.Visibility = Visibility.Collapsed;
                if (itemname15.Text == "")
                { itemname15.Focus(); }
                else
                {
                    id = itemname15.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname15.Text == "")
                            { }
                            else
                            { itemname15.Text = ""; }
                        }
                        else
                        {
                            itemname15.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate15.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname15.Text;
                    quantity15.Focus();
                }
            }
        }
        private void TOTITM16_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM16.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM16.SelectedItem;
                itemname16.Text = drv.Row["NAM_Name"].ToString();
                TOTITM16.Visibility = Visibility.Collapsed;
                if (itemname16.Text == "")
                { itemname16.Focus(); }
                else
                {
                    id = itemname16.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname16.Text == "")
                            { }
                            else
                            { itemname16.Text = ""; }
                        }
                        else
                        {
                            itemname16.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate16.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname16.Text;
                    quantity16.Focus();
                }
            }
        }
        private void TOTITM17_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM17.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM17.SelectedItem;
                itemname17.Text = drv.Row["NAM_Name"].ToString();
                TOTITM17.Visibility = Visibility.Collapsed;
                if (itemname17.Text == "")
                { itemname17.Focus(); }
                else
                {
                    id = itemname17.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname17.Text == "")
                            { }
                            else
                            { itemname17.Text = ""; }
                        }
                        else
                        {
                            itemname17.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate17.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname17.Text;
                    quantity17.Focus();
                }
            }
        }
        private void TOTITM18_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM18.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM18.SelectedItem;
                itemname18.Text = drv.Row["NAM_Name"].ToString();
                TOTITM18.Visibility = Visibility.Collapsed;
                if (itemname18.Text == "")
                { itemname18.Focus(); }
                else
                {
                    id = itemname18.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname18.Text == "")
                            { }
                            else
                            { itemname18.Text = ""; }
                        }
                        else
                        {
                            itemname18.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate18.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname18.Text;
                    quantity18.Focus();
                }
            }
        }
        private void TOTITM19_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TOTITM19.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)TOTITM19.SelectedItem;
                itemname19.Text = drv.Row["NAM_Name"].ToString();
                TOTITM19.Visibility = Visibility.Collapsed;
                if (itemname19.Text == "")
                { itemname19.Focus(); }
                else
                {
                    id = itemname19.Text;
                    if (alp.IsMatch(id))
                    {
                        DataTable dt = pos.itmname();
                        if (dt.Rows.Count == 0)
                        {
                            if (itemname19.Text == "")
                            { }
                            else
                            { itemname19.Text = ""; }
                        }
                        else
                        {
                            itemname19.Text = dt.Rows[0]["NAM_NAME"].ToString();
                            itemrate19.Text = dt.Rows[0]["NAM_Rate"].ToString();
                        }
                    }
                    itm = itemname19.Text;
                    quantity19.Focus();
                }
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (itemname.Text == ""&& itemname1.Text == ""&& itemname2.Text == ""&& itemname3.Text == ""&& itemname4.Text == ""&& itemname5.Text == ""&& itemname6.Text == ""&& itemname7.Text == ""&& itemname8.Text == ""&& itemname9.Text == ""&& itemname10.Text == ""
                && itemname11.Text == ""&& itemname12.Text == ""&& itemname13.Text == ""&& itemname14.Text == "" && itemname15.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data");
            }
            else
            { 
                save.IsEnabled = true;
                if (itemname.Text == "") { ab = 0; t = 0; }
                if (itemname1.Text == "") { ab1 = 0; t1 = 0; }
                if (itemname2.Text == "") { ab2 = 0; t2 = 0; } 
                if (itemname3.Text == "") { ab3 = 0; t3 = 0; }
                if (itemname4.Text == "") { ab4 = 0; t4 = 0; }
                if (itemname5.Text == "") { ab5 = 0; t5 = 0; }
                if (itemname6.Text == "") { ab6 = 0; t6 = 0; }
                if (itemname7.Text == "") { ab7 = 0; t7 = 0; }
                if (itemname8.Text == "") { ab8 = 0; t8 = 0; }
                if (itemname9.Text == "") { ab9 = 0; t9 = 0; }
                if (itemname10.Text == "") { ab10 = 0; t10 = 0; }
                if (itemname11.Text == "") { ab11 = 0; t11 = 0; }
                if (itemname12.Text == "") { ab12 = 0; t12 = 0; }
                if (itemname13.Text == "") { ab13 = 0; t13 = 0; }
                if (itemname14.Text == "") { ab14 = 0; t14 = 0; }
                if (itemname15.Text == "") { ab15 = 0; t15 = 0; }
                if (itemname16.Text == "") { ab16 = 0; t16 = 0; }
                if (itemname17.Text == "") { ab17 = 0; t17 = 0; }
                if (itemname18.Text == "") { ab18 = 0; t18 = 0; }
                if (itemname19.Text == "") { ab19 = 0; t19 = 0; }
                tot1 = Convert.ToDecimal(a + a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9 + a10 + a11 + a12 + a13 + a14 + a15 + a16 + a17 + a18+a19);
                tot = Convert.ToDecimal(ab + ab1 + ab2 + ab3 + ab4 + ab5 + ab6 + ab7 + ab8 + ab9 + ab10 + ab11 + ab12 + ab13 + ab14 + ab15 + ab16 + ab17 + ab18 + ab19);
                tax = Convert.ToDecimal(t + t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15 + t16 + t17 + t18 + t19);
                tax5tot = Convert.ToDecimal(tax15 + tax25 + tax35 + tax45 + tax55 + tax65 + tax75 + tax85 + tax95 + tax105 + tax115 + tax125 + tax135 + tax145 + tax155 + tax165 + tax175 + tax185 + tax195 + tax205);
                tax18tot = Convert.ToDecimal(tax118 + tax218 + tax318 + tax418 + tax518 + tax618 + tax718 + tax818 + tax918 + tax1018 + tax1118 + tax1218 + tax1318 + tax1418 + tax1518 + tax1618 + tax1718 + tax1818 + tax1918 + tax2018);
                discountper = z + z1 + z2 + z3 + z4 + z5 + z6 + z7 + z8 + z9 + z10 + z11 + z12 + z13 + z14 + z15 + z16 + z17 + z18 + z19;
                discountam = y + y1 + y2 + y3 + y4 + y5 + y6 + y7 + y8 + y9 + y10 + y11 + y12 + y13 + y14 + y15 + y16 + y17 + y18 + y19;
                gtotbill = (tot1- discountam) + tax;
                txtttl.Text = Math.Round(tot1, 2, MidpointRounding.AwayFromZero).ToString();
                txtgst.Text = Math.Round(tax5tot, 2, MidpointRounding.AwayFromZero).ToString();
                txtgst2.Text = Math.Round(tax18tot, 2, MidpointRounding.AwayFromZero).ToString();
                txtgttl.Text = Math.Round(gtotbill, 2, MidpointRounding.AwayFromZero).ToString();
                itmTotalDis.Text = discountam.ToString();
            }
        }
        public decimal billtot,discam;
        public void Save()
        {
            pos.bill = Convert.ToInt32(txtbillno.Text);
            pos.BILL_Amount = Convert.ToDecimal(txtttl.Text);
            pos.BILL_Tax = Convert.ToDecimal(txtgst.Text);
            discam = Convert.ToDecimal(itmTotalDis.Text); 
            pos.BILL_Discount = (int)Math.Round(discam);
            billtot = Convert.ToDecimal(txtgttl.Text); 
            pos.BILL_Total = (int)Math.Round(billtot);
            if (OfferCheck.IsChecked == true)
            {
                if (cusName.Text == "" || cusMobile.Text == "")
                {
                    MessageBox.Show("Please Enter Customer Details");
                }
                else
                {
                    pos.CusName = cusName.Text;
                    pos.CusMobile = cusMobile.Text;
                }
                pos.BILL_Status = "Pending";
            }
            else
            {
                pos.CusName = cusName.Text;
                pos.CusMobile = cusMobile.Text;
                pos.BILL_Status = "Settled";
            }
            pos.insertbill();
            pos.A = Convert.ToInt32(txtbillno.Text);
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    if (itemname.Text != "" || itemrate.Text != "")
                    {
                        int A = Convert.ToInt32(quantity.Text); decimal B = A * Convert.ToDecimal(itemrate.Text); pos.Discount = y; pos.DiscountPer = z;
                        decimal gst = 0; itemnamestlid = itemname.Text; DataTable dts0 = pos.getstlid(); stlid = dts0.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax15 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax118 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 1)
                {
                    if (itemname1.Text != "" || itemrate1.Text != "")
                    {
                        int A = Convert.ToInt32(quantity1.Text); decimal B = A * Convert.ToDecimal(itemrate1.Text); pos.Discount = y1; pos.DiscountPer = z1;
                        decimal gst = 0; itemnamestlid = itemname1.Text; DataTable dts1 = pos.getstlid(); stlid = dts1.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname1.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate1.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax25 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax218 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 2)
                {
                    if (itemname2.Text != "" || itemrate2.Text != "")
                    {
                        int A = Convert.ToInt32(quantity2.Text); decimal B = A * Convert.ToDecimal(itemrate2.Text); pos.Discount = y2; pos.DiscountPer = z2;
                        decimal gst = 0; itemnamestlid = itemname2.Text; DataTable dts2 = pos.getstlid(); stlid = dts2.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname2.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate2.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax35 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax318 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 3)
                {

                    if (itemname3.Text != "" || itemrate3.Text != "")
                    {
                        int A = Convert.ToInt32(quantity3.Text); decimal B = A * Convert.ToDecimal(itemrate3.Text); pos.Discount = y3; pos.DiscountPer = z3;
                        decimal gst = 0; itemnamestlid = itemname3.Text; DataTable dts3 = pos.getstlid(); stlid = dts3.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname3.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate3.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax45 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax418 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 4)
                {
                    if (itemname4.Text != "" || itemrate4.Text != "")
                    {
                        int A = Convert.ToInt32(quantity4.Text); decimal B = A * Convert.ToDecimal(itemrate4.Text); pos.Discount = y4; pos.DiscountPer = z4;
                        decimal gst = 0; itemnamestlid = itemname4.Text; DataTable dts4 = pos.getstlid(); stlid = dts4.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname4.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate4.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax55 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax518 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 5)
                {
                    if (itemname5.Text != "" || itemrate5.Text != "")
                    {
                        int A = Convert.ToInt32(quantity5.Text); decimal B = A * Convert.ToDecimal(itemrate5.Text); pos.Discount = y5; pos.DiscountPer = z5;
                        decimal gst = 0; itemnamestlid = itemname5.Text; DataTable dts5 = pos.getstlid(); stlid = dts5.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname5.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate5.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax65 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax618 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 6)
                {
                    if (itemname6.Text != "" || itemrate6.Text != "")
                    {
                        int A = Convert.ToInt32(quantity6.Text); decimal B = A * Convert.ToDecimal(itemrate6.Text); pos.Discount = y6; pos.DiscountPer = z6;
                        decimal gst = 0; itemnamestlid = itemname6.Text; DataTable dts6 = pos.getstlid(); stlid = dts6.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname6.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate6.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax75 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax718 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 7)
                {
                    if (itemname7.Text != "" || itemrate7.Text != "")
                    {
                        int A = Convert.ToInt32(quantity7.Text); decimal B = A * Convert.ToDecimal(itemrate7.Text); pos.Discount = y7; pos.DiscountPer = z7;
                        decimal gst = 0; itemnamestlid = itemname7.Text; DataTable dts7 = pos.getstlid(); stlid = dts7.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname7.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate7.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax85 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax818 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 8)
                {
                    if (itemname8.Text != "" || itemrate8.Text != "")
                    {
                        int A = Convert.ToInt32(quantity8.Text); decimal B = A * Convert.ToDecimal(itemrate8.Text); pos.Discount = y8; pos.DiscountPer = z8;
                        decimal gst = 0; itemnamestlid = itemname8.Text; DataTable dts8 = pos.getstlid(); stlid = dts8.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname8.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate8.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax95 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax918 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 9)
                {
                    if (itemname9.Text != "" || itemrate9.Text != "")
                    {
                        int A = Convert.ToInt32(quantity9.Text); decimal B = A * Convert.ToDecimal(itemrate9.Text); pos.Discount = y9; pos.DiscountPer = z9;
                        decimal gst = 0; itemnamestlid = itemname9.Text; DataTable dts9 = pos.getstlid(); stlid = dts9.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname9.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate9.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax105 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1018 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 10)
                {
                    if (itemname10.Text != "" || itemrate10.Text != "")
                    {
                        int A = Convert.ToInt32(quantity10.Text); decimal B = A * Convert.ToDecimal(itemrate10.Text); pos.Discount = y10; pos.DiscountPer = z10;
                        decimal gst = 0; itemnamestlid = itemname10.Text; DataTable dts10 = pos.getstlid(); stlid = dts10.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname10.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate10.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax115 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1118 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 11)
                {
                    if (itemname11.Text != "" || itemrate11.Text != "")
                    {
                        int A = Convert.ToInt32(quantity11.Text); decimal B = A * Convert.ToDecimal(itemrate11.Text); pos.Discount = y11; pos.DiscountPer = z11;
                        decimal gst = 0; itemnamestlid = itemname11.Text; DataTable dts11 = pos.getstlid(); stlid = dts11.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname11.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate11.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax125 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1218 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 12)
                {
                    if (itemname12.Text != "" || itemrate12.Text != "")
                    {
                        int A = Convert.ToInt32(quantity12.Text); decimal B = A * Convert.ToDecimal(itemrate12.Text); pos.Discount = y12; pos.DiscountPer = z12;
                        decimal gst = 0; itemnamestlid = itemname12.Text; DataTable dts12 = pos.getstlid(); stlid = dts12.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname12.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate12.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax135 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1318 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 13)
                {
                    if (itemname13.Text != "" || itemrate13.Text != "")
                    {
                        int A = Convert.ToInt32(quantity13.Text); decimal B = A * Convert.ToDecimal(itemrate13.Text); pos.Discount = y13; pos.DiscountPer = z13;
                        decimal gst = 0; itemnamestlid = itemname13.Text; DataTable dts13 = pos.getstlid(); stlid = dts13.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname13.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate13.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax145 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1418 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 14)
                {
                    if (itemname14.Text != "" || itemrate14.Text != "")
                    {
                        int A = Convert.ToInt32(quantity14.Text); decimal B = A * Convert.ToDecimal(itemrate14.Text); pos.Discount = y14; pos.DiscountPer = z14;
                        decimal gst = 0; itemnamestlid = itemname14.Text; DataTable dts14 = pos.getstlid(); stlid = dts14.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname14.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate14.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax155 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1518 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 15)
                {
                    if (itemname15.Text != "" || itemrate15.Text != "")
                    {
                        int A = Convert.ToInt32(quantity15.Text); decimal B = A * Convert.ToDecimal(itemrate15.Text); pos.Discount = y15; pos.DiscountPer = z15;
                        decimal gst = 0; itemnamestlid = itemname15.Text; DataTable dts15 = pos.getstlid(); stlid = dts15.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname15.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate15.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax165 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1618 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 16)
                {
                    if (itemname16.Text != "" || itemrate16.Text != "")
                    {
                        int A = Convert.ToInt32(quantity16.Text); decimal B = A * Convert.ToDecimal(itemrate16.Text); pos.Discount = y16; pos.DiscountPer = z16;
                        decimal gst = 0; itemnamestlid = itemname16.Text; DataTable dts16 = pos.getstlid(); stlid = dts16.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname16.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate16.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax175 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1718 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 17)
                {
                    if (itemname17.Text != "" || itemrate17.Text != "")
                    {
                        int A = Convert.ToInt32(quantity17.Text); decimal B = A * Convert.ToDecimal(itemrate17.Text); pos.Discount = y17; pos.DiscountPer = z17;
                        decimal gst = 0; itemnamestlid = itemname17.Text; DataTable dts17 = pos.getstlid(); stlid = dts17.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname17.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate17.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax185 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1818 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 18)
                {
                    if (itemname18.Text != "" || itemrate18.Text != "")
                    {
                        int A = Convert.ToInt32(quantity18.Text); decimal B = A * Convert.ToDecimal(itemrate18.Text); pos.Discount = y18; pos.DiscountPer = z18;
                        decimal gst = 0; itemnamestlid = itemname18.Text; DataTable dts18 = pos.getstlid(); stlid = dts18.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname18.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate18.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax195 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1918 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
                if (i == 19)
                {
                    if (itemname19.Text != "" || itemrate19.Text != "")
                    {
                        int A = Convert.ToInt32(quantity19.Text); decimal B = A * Convert.ToDecimal(itemrate19.Text); pos.Discount = y19; pos.DiscountPer = z19;
                        decimal gst = 0; itemnamestlid = itemname19.Text; DataTable dts19 = pos.getstlid(); stlid = dts19.Rows[0]["STL_ID"].ToString();
                        DataTable d = pos.gsttax(); if (d.Rows.Count == 0) { gst = 0; } else { gst = Convert.ToDecimal(d.Rows[0]["TAX_Percentage"]); }
                        pos.BILLITM_Name = itemname19.Text; pos.BILLITM_Rate = Convert.ToDecimal(itemrate19.Text); pos.BILLITM_Tax = (gst * B) / 100; pos.BILLITM_Quanty = A; pos.Insertitm();
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax205 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax2018 = (pos.BILLITM_Rate * pos.BILLITM_Quanty) * 18 / 100;
                        }
                    }
                }
            }
            tax5sum = tax15 + tax25 + tax35 + tax45 + tax55 + tax65 + tax75 + tax85 + tax95 + tax105 + tax115 + tax125 + tax135 + tax145 + tax155 + tax165 + tax175 + tax185 + tax195 + tax205;
            tax18sum = tax118 + tax218 + tax318 + tax418 + tax518 + tax618 + tax718 + tax818 + tax918 + tax1018 + tax1118 + tax1218 + tax1318 + tax1418 + tax1518 + tax1618 + tax1718 + tax1818 + tax1918 + tax2018;


            MessageBox.Show("Inserted successfully");
            ReportDocument re = new ReportDocument();
            DataTable d1 = Billprint1();
            pos1 = d1;
            re.Load("../../REPORTS/PRINTBILL1.rpt");
            DataTable ds = Billprint();
            pos11 = ds;
            re.Load("../../REPORTS/Printbill.rpt");
            re.SetDataSource(pos11);
            re.Subreports[0].SetDataSource(pos1);
            re.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            ReportDocument res = new ReportDocument();
            res.Load("../../REPORTS/kotprint.rpt");
            DataTable dstlss = pos.stlidsss();
            for (int i = 1; i <= dstlss.Rows.Count; i++)
            {
                DataTable d3 = kot();
                aaa = d3;
                DataTable d2 = kot1();
                abc = d2;
                res.Load("../../REPORTS/kotprint1.rpt");
                res.SetDataSource(abc);
                res.Subreports[0].SetDataSource(aaa);
                res.PrintToPrinter(1, false, 0, 0);
                res.Refresh();
            }
            res.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;

            re.PrintToPrinter(1, false, 0, 0);
            re.Refresh();

            clear();
            j = 0;
            this.NavigationService.Refresh();
            count = 0;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OfferCheck.IsChecked == true)
            {
                if (cusName.Text == "" || cusMobile.Text == "")
                {
                    MessageBox.Show("Please Enter valid Data");
                }
                else
                {
                    Save();
                    pos.BILL_Status = "Pending";
                }
            }
            else
            {
                Save();
            }
            
        }
        public static DataTable pos1, pos11,aaa,abc;
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Refresh();
        }
        private void Itemname_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname.Text = "";
                TOTITM.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM.Focus();
                TOTITM.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate.Text = "";
                quantity.Text = ""; total.Text = "";
                TOTITM.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname.Text == "")
                {
                    itemname.Text = ""; itemrate.Text = "";
                    quantity.Text = ""; total.Text = "";
                    TOTITM.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (itemname.Text.Trim() != "")
            {
                aaid = itemname.Text;
                pos.likea = itemname.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM.Visibility = Visibility.Visible;
                    itemname.Text = aaid;
                    if (TOTITM.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM.ItemsSource = dn.DefaultView;
                    TOTITM.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname.Text == "")
                    { }
                    else
                    { itemname.Text = ""; }
                }
            }
            else { itemname.Text = ""; }
            itm = itemname.Text;
        }
        private void Itemname1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname1.Text = "";
                TOTITM1.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM1.Focus();
                TOTITM1.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate1.Text = "";
                quantity1.Text = ""; total1.Text = "";
                TOTITM1.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname1.Text == "")
                {
                    itemrate1.Text = "";
                    quantity1.Text = ""; total1.Text = "";
                    TOTITM1.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname1_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname1.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname1.Text == "")
                    { }
                    else
                    { itemname1.Text = ""; }
                }
            }
            else { itemname1.Text = ""; }
            itm = itemname1.Text;
        }
        private void Itemname1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            if (itemname1.Text.Trim() != "")
            {
                aaid = itemname1.Text;
                pos.likea = itemname1.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM1.Visibility = Visibility.Visible;
                    itemname1.Text = aaid;
                    if (TOTITM1.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM1.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM1.ItemsSource = dn.DefaultView;
                    TOTITM1.Visibility = Visibility.Collapsed;
                }
            }
            else
            { TOTITM1.Visibility = Visibility.Collapsed; }
        }
        private void Quantity1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname1.Text == "") { itemname1.Focus(); }
            else { DISITM1.IsEnabled = true; }
        }
        private void Total1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity1.Text == "") { quantity1.Focus(); }
            else { itemname2.Focus(); TOTITM2.ItemsSource = dn.DefaultView; }
        }
        private void Quantity1_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM1.Text = "0";
            QY = quantity1.Text;
            if (quantity1.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q1 = Convert.ToInt32(quantity1.Text);
                r1 = Convert.ToDecimal(itemrate1.Text);
                total1.Text = (q1 * r1).ToString();
                a1 = Convert.ToDecimal(total1.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t1 = 0; }
                    else
                    { t1 = a1 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax25 = Convert.ToDecimal(total1.Text) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax218 = Convert.ToDecimal(total1.Text) * 18 / 100;
                        }
                    }
                }
                sp2.Visibility = Visibility.Visible;
                if (c1 == 0)
                { count++; c1 = 1; }
                TOTITM1.Visibility = Visibility.Collapsed;

            }
            else
            { quantity1.Text = ""; total1.Text = ""; }
        }
        private void Itemname2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname2.Text = "";
                TOTITM2.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM2.Focus();
                TOTITM2.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate2.Text = "";
                quantity2.Text = ""; total2.Text = "";
                TOTITM2.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname2.Text == "")
                {
                    itemname2.Text = ""; itemrate2.Text = "";
                    quantity2.Text = ""; total2.Text = "";
                    TOTITM2.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname2_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname2.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname2.Text == "")
                    { }
                    else
                    { itemname2.Text = ""; }
                }
            }
            else { itemname2.Text = ""; }
            itm = itemname2.Text;
        }
        private void Quantity2_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM2.Text = "0";
            QY = quantity2.Text;
            if (quantity2.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data");
            }
            else if (num.IsMatch(QY))
            {
                q2 = Convert.ToInt32(quantity2.Text);
                r2 = Convert.ToDecimal(itemrate2.Text);
                total2.Text = (q2 * r2).ToString();
                a2 = Convert.ToDecimal(total2.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t2 = 0; }
                    else
                    {
                        t2 = a2 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax35 = Convert.ToDecimal(total2.Text) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax318 = Convert.ToDecimal(total2.Text) * 18 / 100;
                        }
                    }
                }
                sp3.Visibility = Visibility.Visible;
                if (c2 == 0)
                { count++; c2 = 1; }
                TOTITM2.Visibility = Visibility.Collapsed;
               
            }
            else
            { quantity2.Text = ""; total2.Text = ""; }
        }
        private void Total2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity2.Text == "") { quantity2.Focus(); }
            else { itemname3.Focus(); TOTITM3.ItemsSource = dn.DefaultView; }
        }
        private void Quantity2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname2.Text == "") { itemname2.Focus(); }
            else { DISITM2.IsEnabled = true; }
        }
        private void Itemname2_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            if (itemname2.Text.Trim() != "")
            {
                aaid = itemname2.Text;
                pos.likea = itemname2.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM2.Visibility = Visibility.Visible;
                    itemname2.Text = aaid;
                    if (TOTITM2.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM2.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM2.ItemsSource = dn.DefaultView;
                    TOTITM2.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM2.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname3.Text = "";
                TOTITM3.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM3.Focus();
                TOTITM3.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate3.Text = "";
                quantity3.Text = ""; total3.Text = "";
                TOTITM3.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname3.Text == "")
                {
                    itemname3.Text = ""; itemrate3.Text = "";
                    quantity3.Text = ""; total3.Text = "";
                    TOTITM3.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname3_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname3.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname3.Text == "")
                    { }
                    else
                    { itemname3.Text = ""; }
                }
            }
            else { itemname3.Text = ""; }
            itm = itemname3.Text;
        }
        private void Itemname3_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;

            if (itemname3.Text.Trim() != "")
            {
                aaid = itemname3.Text;
                pos.likea = itemname3.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM3.Visibility = Visibility.Visible;
                    itemname3.Text = aaid;
                    if (TOTITM3.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM3.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM3.ItemsSource = dn.DefaultView;
                    TOTITM3.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM3.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity3_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname3.Text == "") { itemname3.Focus(); }
            else { DISITM3.IsEnabled = true; }
        }
        private void Quantity3_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM3.Text = "0";
            QY = quantity3.Text;
            if (quantity3.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q3 = Convert.ToInt32(quantity3.Text);
                r3 = Convert.ToDecimal(itemrate3.Text);
                total3.Text = (q3 * r3).ToString();
                a3 = Convert.ToDecimal(total3.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t3 = 0; }
                    else
                    { t3 = a3 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax45 = Convert.ToDecimal(total3.Text) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax418 = Convert.ToDecimal(total3.Text) * 18 / 100;
                        }
                    }
                }
                    sp4.Visibility = Visibility.Visible;
                    if (c3 == 0)
                    { count++; c3 = 1; }
                    TOTITM3.Visibility = Visibility.Collapsed;
            }
            else
            { quantity3.Text = ""; total3.Text = ""; }
        }
        private void Total3_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity3.Text == "") { quantity3.Focus(); }
            else { itemname4.Focus(); TOTITM4.ItemsSource = dn.DefaultView; }
        }
        private void Itemname4_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;

            if (itemname4.Text.Trim() != "")
            {
                aaid = itemname4.Text;
                pos.likea = itemname4.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM4.Visibility = Visibility.Visible;
                    itemname4.Text = aaid;
                    if (TOTITM4.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM4.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM4.ItemsSource = dn.DefaultView;
                    TOTITM4.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM4.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname4_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname4.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname4.Text == "")
                    { }
                    else
                    { itemname4.Text = ""; }
                }
            }
            else { itemname4.Text = ""; }
            itm = itemname4.Text;
        }
        private void Quantity4_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM4.Text = "0";
            QY = quantity4.Text;
            if (quantity4.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q4 = Convert.ToInt32(quantity4.Text);
                r4 = Convert.ToDecimal(itemrate4.Text);
                total4.Text = (q4 * r4).ToString();
                a4 = Convert.ToDecimal(total4.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t4 = 0; }
                    else
                    { t4 = a4 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax55 = a4 * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax518 = a4 * 18 / 100;
                        }
                    }
                }
                    sp5.Visibility = Visibility.Visible;
                    if (c4 == 0)
                    { count++; c4 = 1; }
                    TOTITM4.Visibility = Visibility.Collapsed;
            }
            else
            { quantity4.Text = ""; total4.Text = ""; }
        }
        private void Quantity4_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname4.Text == "") { itemname4.Focus(); }
            else { DISITM4.IsEnabled = true; }
        }
        private void Total4_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity4.Text == "") { quantity4.Focus(); }
            else { itemname5.Focus(); TOTITM5.ItemsSource = dn.DefaultView; }
        }
        private void Itemname4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname4.Text = "";
                TOTITM4.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM4.Focus();
                TOTITM4.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate4.Text = "";
                quantity4.Text = ""; total4.Text = "";
                TOTITM4.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname4.Text == "")
                {
                    itemrate4.Text = "";
                    quantity4.Text = ""; total4.Text = "";
                    TOTITM4.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname5.Text = "";
                TOTITM5.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM5.Focus();
                TOTITM5.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate5.Text = "";
                quantity5.Text = ""; total5.Text = "";
                TOTITM5.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname5.Text == "")
                {
                    itemrate5.Text = "";
                    quantity5.Text = ""; total5.Text = "";
                    TOTITM5.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname5_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname5.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname5.Text == "")
                    { }
                    else
                    { itemname5.Text = ""; }
                }
            }
            else { itemname5.Text = ""; }
            itm = itemname5.Text;
        }
        private void Itemname5_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;

            if (itemname5.Text.Trim() != "")
            {
                aaid = itemname5.Text;
                pos.likea = itemname5.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM5.Visibility = Visibility.Visible;
                    itemname5.Text = aaid;
                    if (TOTITM5.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM5.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM5.ItemsSource = dn.DefaultView;
                    TOTITM5.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM5.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity5_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname5.Text == "") { itemname5.Focus(); }
            else { DISITM5.IsEnabled = true; }
        }
        private void Total5_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity5.Text == "") { quantity5.Focus(); }
            else { itemname6.Focus(); TOTITM6.ItemsSource = dn.DefaultView; }
        }
        private void Quantity5_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM5.Text = "0";
            QY = quantity5.Text; if (quantity5.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q5 = Convert.ToInt32(quantity5.Text);
                r5 = Convert.ToDecimal(itemrate5.Text);
                total5.Text = (q5 * r5).ToString();
                a5 = Convert.ToDecimal(total5.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t5 = 0; }
                    else
                    { t5 = a5 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax65 = a5 * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax618 = a5 * 18 / 100;
                        }
                    }
                }
                    sp6.Visibility = Visibility.Visible;
                    if (c5 == 0)
                    { count++; c5 = 1; }
                    TOTITM5.Visibility = Visibility.Collapsed;
            }
            else
            { quantity5.Text = ""; total5.Text = ""; }
        }
        private void Itemname6_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;

            if (itemname6.Text.Trim() != "")
            {
                aaid = itemname6.Text;
                pos.likea = itemname6.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM6.Visibility = Visibility.Visible;
                    itemname6.Text = aaid;
                    if (TOTITM6.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM6.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM6.ItemsSource = dn.DefaultView;
                    TOTITM6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM6.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity6_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM6.Text = "0";
            QY = quantity6.Text;
            if (quantity6.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q6 = Convert.ToInt32(quantity6.Text);
                r6 = Convert.ToDecimal(itemrate6.Text);
                total6.Text = (q6 * r6).ToString();
                a6 = Convert.ToDecimal(total6.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t6 = 0; }
                    else
                    { t6 = a6 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax75 = a6 * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax718 = a6 * 18 / 100;
                        }
                    }
                }
                    sp7.Visibility = Visibility.Visible;
                    if (c6 == 0)
                    { count++; c6 = 1; }
                    TOTITM6.Visibility = Visibility.Collapsed;
            }
            else
            { quantity6.Text = ""; total6.Text = ""; }
        }
        private void Itemname6_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname6.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname6.Text == "")
                    { }
                    else
                    { itemname6.Text = ""; }
                }
            }
            else { itemname6.Text = ""; }
            itm = itemname6.Text;
        }
        private void Itemname6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname6.Text = "";
                TOTITM6.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM6.Focus();
                TOTITM6.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate6.Text = "";
                quantity6.Text = ""; total6.Text = "";
                TOTITM6.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname6.Text == "")
                {
                    itemrate6.Text = "";
                    quantity6.Text = ""; total6.Text = "";
                    TOTITM6.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Quantity6_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname6.Text == "") { itemname6.Focus(); }
            else { DISITM6.IsEnabled = true; }
        }
        private void Total6_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity6.Text == "") { quantity6.Focus(); }
            else { itemname7.Focus(); TOTITM7.ItemsSource = dn.DefaultView; }
        }
        private void Itemname7_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname7.Text = "";
                TOTITM7.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM7.Focus();
                TOTITM7.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate7.Text = "";
                quantity7.Text = ""; total7.Text = "";
                TOTITM7.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname7.Text == "")
                {
                    itemrate7.Text = "";
                    quantity7.Text = ""; total7.Text = "";
                    TOTITM7.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname7_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname7.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname7.Text == "")
                    { }
                    else
                    { itemname7.Text = ""; }
                }
            }
            else { itemname7.Text = ""; }
            itm = itemname7.Text;
        }
        private void Itemname7_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;

            if (itemname7.Text.Trim() != "")
            {
                aaid = itemname7.Text;
                pos.likea = itemname7.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM7.Visibility = Visibility.Visible;
                    itemname7.Text = aaid;
                    if (TOTITM7.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM7.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM7.ItemsSource = dn.DefaultView;
                    TOTITM7.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM7.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity7_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname7.Text == "") { itemname7.Focus(); }
            else { DISITM7.IsEnabled = true; }
        }
        private void Total7_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity7.Text == "") { quantity7.Focus(); }
            else { itemname8.Focus(); TOTITM8.ItemsSource = dn.DefaultView; }
        }
        private void Quantity7_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM7.Text = "0";
            QY = quantity7.Text;
            if (quantity7.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data");
            }
            else if (num.IsMatch(QY))
            {
                q7 = Convert.ToInt32(quantity7.Text);
                r7 = Convert.ToDecimal(itemrate7.Text);
                total7.Text = (q7 * r7).ToString();
                a7 = Convert.ToDecimal(total7.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t7 = 0; }
                    else
                    { t7 = a7 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax85 = a7 * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax818 = a7 * 18 / 100;
                        }
                    }
                }
                sp8.Visibility = Visibility.Visible;
                if (c7 == 0)
                { count++; c7 = 1; }
                TOTITM7.Visibility = Visibility.Collapsed;
            }
            else
            { quantity7.Text = ""; total7.Text = ""; }
        }
        private void Itemname8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname8.Text = "";
                TOTITM8.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM8.Focus();
                TOTITM8.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate8.Text = "";
                quantity8.Text = ""; total8.Text = "";
                TOTITM8.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname8.Text == "")
                {
                    itemrate8.Text = "";
                    quantity8.Text = ""; total8.Text = "";
                    TOTITM8.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname8_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname8.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname8.Text == "")
                    { }
                    else
                    {
                        itemname8.Text = "";
                    }
                }
            }
            else { itemname8.Text = ""; }
            itm = itemname8.Text;
        }
        private void Itemname8_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;

            if (itemname8.Text.Trim() != "")
            {
                aaid = itemname8.Text;
                pos.likea = itemname8.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM8.Visibility = Visibility.Visible;
                    itemname8.Text = aaid;
                    if (TOTITM8.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM8.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM8.ItemsSource = dn.DefaultView;
                    TOTITM8.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM8.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity8_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname8.Text == "") { itemname8.Focus(); }
            else { DISITM8.IsEnabled = true; }
        }
        private void Quantity8_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM8.Text = "0";
            QY = quantity8.Text;
            if (quantity8.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q8 = Convert.ToInt32(quantity8.Text);
                r8 = Convert.ToDecimal(itemrate8.Text);
                total8.Text = (q8 * r8).ToString();
                a8 = Convert.ToDecimal(total8.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t8 = 0; }
                    else
                    { t8 = a8 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax95 = a8 * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax918 = a8 * 18 / 100;
                        }
                    }
                }
                    sp9.Visibility = Visibility.Visible;
                    if (c8 == 0)
                    { count++; c8 = 1; }
                    TOTITM8.Visibility = Visibility.Collapsed;
            }
            else
            { quantity8.Text = ""; total8.Text = ""; }
        }
        private void Total8_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity8.Text == "") { quantity8.Focus(); }
            else { itemname9.Focus(); TOTITM9.ItemsSource = dn.DefaultView; }
        }
        private void Itemname9_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname9.Text = "";
                TOTITM9.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM9.Focus();
                TOTITM9.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate9.Text = "";
                quantity9.Text = ""; total9.Text = "";
                TOTITM9.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname9.Text == "")
                {
                    itemrate9.Text = "";
                    quantity9.Text = ""; total9.Text = "";
                    TOTITM9.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname9_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname9.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname9.Text == "")
                    { }
                    else
                    {
                        itemname9.Text = "";
                    }
                }
            }
            else { itemname9.Text = ""; }
            itm = itemname9.Text;
        }
        private void Itemname9_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;

            if (itemname9.Text.Trim() != "")
            {
                aaid = itemname9.Text;
                pos.likea = itemname9.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM9.Visibility = Visibility.Visible;
                    itemname9.Text = aaid;
                    if (TOTITM9.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM9.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM9.ItemsSource = dn.DefaultView;
                    TOTITM9.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM9.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity9_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname9.Text == "") { itemname9.Focus(); }
            else { DISITM9.IsEnabled = true; }
        }
        private void Total9_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity9.Text == "") { quantity9.Focus(); }
            else { itemname10.Focus(); TOTITM10.ItemsSource = dn.DefaultView; }
        }
        private void Quantity9_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM9.Text = "0";
            QY = quantity9.Text;
            if (quantity9.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q9 = Convert.ToInt32(quantity9.Text);
                r9 = Convert.ToDecimal(itemrate9.Text);
                total9.Text = (q9 * r9).ToString();
                a9 = Convert.ToDecimal(total9.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t9 = 0; }
                    else
                    { t9 = a9 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax105 = a9 * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1018 = a9 * 18 / 100;
                        }
                    }
                }
                    sp10.Visibility = Visibility.Visible;
                    if (c9 == 0)
                    { count++; c9 = 1; }
                    TOTITM9.Visibility = Visibility.Collapsed;
            }
            else
            { quantity9.Text = ""; total9.Text = ""; }
        }
        private void Itemname10_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;

            if (itemname10.Text.Trim() != "")
            {
                aaid = itemname10.Text;
                pos.likea = itemname10.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM10.Visibility = Visibility.Visible;
                    itemname10.Text = aaid;
                    if (TOTITM10.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM10.ItemsSource = DT.DefaultView; }
                }
                else
                {
                    TOTITM10.ItemsSource = dn.DefaultView;
                    TOTITM10.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM10.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname10_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname10.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname10.Text == "")
                    { }
                    else
                    {
                        itemname10.Text = "";
                    }
                }
            }
            else { itemname10.Text = ""; }
            itm = itemname10.Text;
        }
        private void Itemname10_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname10.Text = "";
                TOTITM10.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM10.Focus();
                TOTITM10.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate10.Text = "";
                quantity10.Text = ""; total10.Text = "";
                TOTITM10.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname10.Text == "")
                {
                    itemrate10.Text = "";
                    quantity10.Text = ""; total10.Text = "";
                    TOTITM10.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Quantity10_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname10.Text == "") { itemname10.Focus(); }
            else { DISITM10.IsEnabled = true; }
        }
        private void Quantity10_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM10.Text = "0";
            QY = quantity10.Text;
            if (quantity10.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q10 = Convert.ToInt32(quantity10.Text);
                r10 = Convert.ToDecimal(itemrate10.Text);
                total10.Text = (q10 * r10).ToString();
                a10 = Convert.ToDecimal(total10.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t10 = 0; }
                    else
                    { t10 = a10 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                        {
                            tax115 = a10 * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax1118 = a10 * 18 / 100;
                        }
                    }
                }
                    sp11.Visibility = Visibility.Visible;
                    if (c10 == 0)
                    { count++; c10 = 1; }
                    TOTITM10.Visibility = Visibility.Collapsed;
            }
            else { quantity10.Text = ""; total10.Text = ""; }
        }
        private void Total10_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity10.Text == "") { quantity10.Focus(); }
            else { itemname11.Focus(); TOTITM11.ItemsSource = dn.DefaultView; }
        }
        private void Itemname11_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname11.Text = "";
                TOTITM11.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM11.Focus();
                TOTITM11.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate11.Text = "";
                quantity11.Text = ""; total11.Text = "";
                TOTITM11.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname11.Text == "")
                {
                    itemrate11.Text = "";
                    quantity11.Text = ""; total11.Text = "";
                    TOTITM11.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname11_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname10.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname10.Text == "")
                    { }
                    else
                    {
                        itemname10.Text = "";
                    }
                }
            }
            else { itemname10.Text = ""; }
            itm = itemname10.Text;
        }
        private void Itemname11_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;

            if (itemname11.Text.Trim() != "")
            {
                aaid = itemname11.Text;
                pos.likea = itemname11.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM11.Visibility = Visibility.Visible;
                    itemname11.Text = aaid;
                    if (TOTITM11.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM11.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM11.ItemsSource = dn.DefaultView;
                    TOTITM11.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM11.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity11_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname11.Text == "") { itemname11.Focus(); }
            else { DISITM11.IsEnabled = true; }
        }
        private void Total11_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity11.Text == "") { quantity11.Focus(); }
            else { itemname12.Focus(); TOTITM12.ItemsSource = dn.DefaultView; }
        }
        private void Quantity11_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM11.Text = "0";
            QY = quantity11.Text;
            if (num.IsMatch(QY))
            {
                if (quantity11.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q11 = Convert.ToInt32(quantity11.Text);
                    r11 = Convert.ToDecimal(itemrate11.Text);
                    total11.Text = (q11 * r11).ToString();
                    a11 = Convert.ToDecimal(total11.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t11 = 0; }
                        else
                        { t11 = a11 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax125 = a11 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1218 = a11 * 18 / 100;
                            }
                        }
                    }
                        sp12.Visibility = Visibility.Visible;
                        if (c11 == 0)
                        { count++; c11 = 1; }
                        TOTITM11.Visibility = Visibility.Collapsed;
                }
            }
            else { quantity11.Text = ""; total11.Text = ""; }
        }
        private void Itemname12_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;
            TOTITM11.Visibility = Visibility.Collapsed;

            if (itemname12.Text.Trim() != "")
            {
                aaid = itemname12.Text;
                pos.likea = itemname12.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM12.Visibility = Visibility.Visible;
                    itemname12.Text = aaid;
                    if (TOTITM12.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM12.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM12.ItemsSource = dn.DefaultView;
                    TOTITM12.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM12.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname12_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname12.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname12.Text == "")
                    { }
                    else
                    {
                        itemname12.Text = "";
                    }
                }
            }
            else { itemname12.Text = ""; }
            itm = itemname12.Text;
        }

        private void Itemname12_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname12.Text = "";
                TOTITM12.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM12.Focus();
                TOTITM12.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate12.Text = "";
                quantity12.Text = ""; total12.Text = "";
                TOTITM12.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname12.Text == "")
                {
                    itemrate12.Text = "";
                    quantity12.Text = ""; total12.Text = "";
                    TOTITM12.ItemsSource = dn.DefaultView;
                }
            }
        }

        private void Quantity12_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname12.Text == "") { itemname12.Focus(); }
            else { DISITM12.IsEnabled = true; }
        }

        private void Total12_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity12.Text == "") { quantity12.Focus(); }
            else { itemname13.Focus(); TOTITM13.ItemsSource = dn.DefaultView; }
        }
        private void Quantity12_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM12.Text = "0";
            QY = quantity12.Text;
            if (num.IsMatch(QY))
            {
                if (quantity12.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q12 = Convert.ToInt32(quantity12.Text);
                    r12 = Convert.ToDecimal(itemrate12.Text);
                    total12.Text = (q12 * r12).ToString();
                    a12 = Convert.ToDecimal(total12.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t12 = 0; }
                        else
                        { t12 = a12 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax135 = a12 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1318 = a12 * 18 / 100;
                            }
                        }
                    }
                        sp13.Visibility = Visibility.Visible;
                        if (c12 == 0)
                        { count++; c12 = 1; }
                        TOTITM12.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                quantity12.Text = ""; total12.Text = "";
            }
        }

        private void Itemname13_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname13.Text = "";
                TOTITM13.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM13.Focus();
                TOTITM13.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate13.Text = "";
                quantity13.Text = ""; total13.Text = "";
                TOTITM13.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname13.Text == "")
                {
                    itemrate13.Text = "";
                    quantity13.Text = ""; total13.Text = "";
                    TOTITM13.ItemsSource = dn.DefaultView;
                }
            }
        }

        private void Itemname13_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname13.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname13.Text == "")
                    { }
                    else
                    {
                        itemname13.Text = "";
                    }
                }
            }
            else { itemname13.Text = ""; }
            itm = itemname13.Text;
        }
        private void Itemname13_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;
            TOTITM11.Visibility = Visibility.Collapsed;
            TOTITM12.Visibility = Visibility.Collapsed;

            if (itemname13.Text.Trim() != "")
            {
                aaid = itemname13.Text;
                pos.likea = itemname13.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM13.Visibility = Visibility.Visible;
                    itemname13.Text = aaid;
                    if (TOTITM13.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM13.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM13.ItemsSource = dn.DefaultView;
                    TOTITM13.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM13.Visibility = Visibility.Collapsed;
            }
        }

        private void Quantity13_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname13.Text == "") { itemname13.Focus(); }
            else { DISITM13.IsEnabled = true; }
        }

        private void Total13_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity13.Text == "") { quantity13.Focus(); }
            else { itemname14.Focus(); TOTITM14.ItemsSource = dn.DefaultView; }
        }

        private void Quantity13_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM13.Text = "0";
            QY = quantity13.Text;
            if (num.IsMatch(QY))
            {
                if (quantity13.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q13 = Convert.ToInt32(quantity13.Text);
                    r13 = Convert.ToDecimal(itemrate13.Text);
                    total13.Text = (q13 * r13).ToString();
                    a13 = Convert.ToDecimal(total13.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t13 = 0; }
                        else
                        { t13 = a13 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax145 = a13 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1418 = a13 * 18 / 100;
                            }
                        }
                    }
                        sp14.Visibility = Visibility.Visible;
                        if (c13 == 0)
                        { count++; c13 = 1; }
                        TOTITM13.Visibility = Visibility.Collapsed;
                }
            }
            else { quantity13.Text = ""; total13.Text = ""; }
        }

        private void Itemname14_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname14.Text = "";
                TOTITM14.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM14.Focus();
                TOTITM14.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate14.Text = "";
                quantity14.Text = ""; total14.Text = "";
                TOTITM14.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname14.Text == "")
                {
                    itemrate14.Text = "";
                    quantity14.Text = ""; total14.Text = "";
                    TOTITM14.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname14_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname14.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname14.Text == "")
                    { }
                    else
                    {
                        itemname14.Text = "";
                    }
                }
            }
            else { itemname14.Text = ""; }
            itm = itemname14.Text;
        }
        private void Itemname14_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;
            TOTITM11.Visibility = Visibility.Collapsed;
            TOTITM12.Visibility = Visibility.Collapsed;
            TOTITM13.Visibility = Visibility.Collapsed;
            if (itemname14.Text.Trim() != "")
            {
                aaid = itemname14.Text;
                pos.likea = itemname14.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM14.Visibility = Visibility.Visible;
                    itemname14.Text = aaid;
                    if (TOTITM14.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM14.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM14.ItemsSource = dn.DefaultView;
                    TOTITM14.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM14.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity14_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname14.Text == "") { itemname14.Focus(); }
            else { DISITM14.IsEnabled = true; }
        }

        private void Total14_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity14.Text == "") { quantity14.Focus(); }
            else { itemname15.Focus(); TOTITM15.ItemsSource = dn.DefaultView; }
        }

        private void Quantity14_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM14.Text = "0";
            QY = quantity14.Text;
            if (num.IsMatch(QY))
            {
                if (quantity14.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q14 = Convert.ToInt32(quantity14.Text);
                    r14 = Convert.ToDecimal(itemrate14.Text);
                    total14.Text = (q14 * r14).ToString();
                    a14 = Convert.ToDecimal(total14.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t14 = 0; }
                        else
                        { t14 = a14 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax155 = a14 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1518 = a14 * 18 / 100;
                            }
                        }
                    }
                        sp15.Visibility = Visibility.Visible;
                        if (c14 == 0)
                        { count++; c14 = 1; }
                        TOTITM14.Visibility = Visibility.Collapsed;
                }
            }
            else { quantity14.Text = ""; total14.Text = ""; }
        }

        private void Itemname15_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname15.Text = "";
                TOTITM15.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM15.Focus();
                TOTITM15.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate15.Text = "";
                quantity15.Text = ""; total15.Text = "";
                TOTITM15.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname15.Text == "")
                {
                    itemrate15.Text = "";
                    quantity15.Text = ""; total15.Text = "";
                    TOTITM15.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname15_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname15.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname15.Text == "")
                    { }
                    else
                    {
                        itemname15.Text = "";
                    }
                }
            }
            else { itemname15.Text = ""; }
            itm = itemname15.Text;
        }
        private void Itemname15_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;
            TOTITM11.Visibility = Visibility.Collapsed;
            TOTITM12.Visibility = Visibility.Collapsed;
            TOTITM13.Visibility = Visibility.Collapsed;
            TOTITM14.Visibility = Visibility.Collapsed;

            if (itemname15.Text.Trim() != "")
            {
                aaid = itemname15.Text;
                pos.likea = itemname15.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM15.Visibility = Visibility.Visible;
                    itemname15.Text = aaid;
                    if (TOTITM15.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM15.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM15.ItemsSource = dn.DefaultView;
                    TOTITM15.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM15.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity15_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname15.Text == "") { itemname15.Focus(); }
            else { DISITM15.IsEnabled = true; }
        }
        private void Quantity15_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM15.Text = "0";
            QY = quantity15.Text;
            if (num.IsMatch(QY))
            {
                if (quantity15.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q15 = Convert.ToInt32(quantity15.Text);
                    r15 = Convert.ToDecimal(itemrate15.Text);
                    total15.Text = (q15 * r15).ToString();
                    a15 = Convert.ToDecimal(total15.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t15 = 0; }
                        else
                        { t15 = a15 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax165 = a15 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1618 = a15 * 18 / 100;
                            }
                        }
                    }
                        sp16.Visibility = Visibility.Visible;
                        if (c15 == 0)
                        { count++; c15 = 1; }
                        TOTITM15.Visibility = Visibility.Collapsed;
                }
            }
            else { quantity15.Text = ""; total15.Text = ""; }
        }
        private void Total15_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity15.Text == "") { quantity15.Focus(); }
            else { itemname16.Focus(); TOTITM16.ItemsSource = dn.DefaultView; }
        }
        private void Itemname16_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname16.Text = "";
                TOTITM16.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM16.Focus();
                TOTITM16.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate16.Text = "";
                quantity16.Text = ""; total16.Text = "";
                TOTITM16.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname16.Text == "")
                {
                    itemrate16.Text = "";
                    quantity16.Text = ""; total16.Text = "";
                    TOTITM16.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname16_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname16.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname16.Text == "")
                    { }
                    else
                    {
                        itemname16.Text = "";
                    }
                }
            }
            else { itemname16.Text = ""; }
            itm = itemname16.Text;
        }

        private void Itemname16_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;
            TOTITM11.Visibility = Visibility.Collapsed;
            TOTITM12.Visibility = Visibility.Collapsed;
            TOTITM13.Visibility = Visibility.Collapsed;
            TOTITM14.Visibility = Visibility.Collapsed;
            TOTITM15.Visibility = Visibility.Collapsed;

            if (itemname16.Text.Trim() != "")
            {
                aaid = itemname16.Text;
                pos.likea = itemname16.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM16.Visibility = Visibility.Visible;
                    itemname16.Text = aaid;
                    if (TOTITM16.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM16.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM16.ItemsSource = dn.DefaultView;
                    TOTITM16.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM16.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity16_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname16.Text == "") { itemname16.Focus(); }
            else { DISITM16.IsEnabled = true; }
        }

        private void Total16_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity16.Text == "") { quantity16.Focus(); }
            else { itemname17.Focus(); TOTITM17.ItemsSource = dn.DefaultView; }
        }
        private void Quantity16_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM16.Text = "0";
            QY = quantity16.Text;
            if (num.IsMatch(QY))
            {
                if (quantity16.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q16 = Convert.ToInt32(quantity16.Text);
                    r16 = Convert.ToDecimal(itemrate16.Text);
                    total16.Text = (q16 * r16).ToString();
                    a16 = Convert.ToDecimal(total16.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t16 = 0; }
                        else
                        { t16 = a16 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax175 = a16 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1718 = a16 * 18 / 100;
                            }
                        }
                    }
                        sp17.Visibility = Visibility.Visible;
                        if (c16 == 0)
                        { count++; c16 = 1; }
                        TOTITM16.Visibility = Visibility.Collapsed;
                }
            }
            else
            { quantity16.Text = ""; total16.Text = ""; }
        }
        private void Itemname17_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname17.Text = "";
                TOTITM17.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM17.Focus();
                TOTITM17.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate17.Text = "";
                quantity17.Text = ""; total17.Text = "";
                TOTITM17.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname17.Text == "")
                {
                    itemrate17.Text = "";
                    quantity17.Text = ""; total17.Text = "";
                    TOTITM17.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname17_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname17.Text;

            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname17.Text == "")
                    { }
                    else
                    {
                        itemname17.Text = "";
                    }
                }
            }
            else { itemname17.Text = ""; }
            itm = itemname17.Text;
        }

        private void Quantity17_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM17.Text = "0";
            QY = quantity17.Text;
            if (num.IsMatch(QY))
            {
                if (quantity17.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q17 = Convert.ToInt32(quantity17.Text);
                    r17 = Convert.ToDecimal(itemrate17.Text);
                    total17.Text = (q17 * r17).ToString();
                    a17 = Convert.ToDecimal(total17.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t17 = 0; }
                        else
                        { t17 = a17 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax185 = a17 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1818 = a17 * 18 / 100;
                            }
                        }
                    }
                        sp18.Visibility = Visibility.Visible;
                        if (c17 == 0)
                        { count++; c17 = 1; }
                        TOTITM17.Visibility = Visibility.Collapsed;
                }
            }
            else
            { quantity17.Text = ""; total17.Text = ""; }
        }
        private void Total17_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity17.Text == "") { quantity17.Focus(); }
            else { itemname18.Focus(); TOTITM18.ItemsSource = dn.DefaultView; }
        }
        private void Quantity17_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname17.Text == "") { itemname17.Focus(); }
            else { DISITM17.IsEnabled = true; }
        }

        private void Itemname17_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;
            TOTITM11.Visibility = Visibility.Collapsed;
            TOTITM12.Visibility = Visibility.Collapsed;
            TOTITM13.Visibility = Visibility.Collapsed;
            TOTITM14.Visibility = Visibility.Collapsed;
            TOTITM15.Visibility = Visibility.Collapsed;
            TOTITM16.Visibility = Visibility.Collapsed;

            if (itemname17.Text.Trim() != "")
            {
                aaid = itemname17.Text;
                pos.likea = itemname17.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM17.Visibility = Visibility.Visible;
                    itemname17.Text = aaid;
                    if (TOTITM17.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM17.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM17.ItemsSource = dn.DefaultView;
                    TOTITM17.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM17.Visibility = Visibility.Collapsed;
            }
        }

        private void Itemname18_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname18.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname18.Text == "")
                    { }
                    else
                    {
                        itemname18.Text = "";
                    }
                }
            }
            else { itemname18.Text = ""; }
            itm = itemname18.Text;
        }
        private void Itemname18_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;
            TOTITM11.Visibility = Visibility.Collapsed;
            TOTITM12.Visibility = Visibility.Collapsed;
            TOTITM13.Visibility = Visibility.Collapsed;
            TOTITM14.Visibility = Visibility.Collapsed;
            TOTITM15.Visibility = Visibility.Collapsed;
            TOTITM16.Visibility = Visibility.Collapsed;
            TOTITM17.Visibility = Visibility.Collapsed;

            if (itemname18.Text.Trim() != "")
            {
                aaid = itemname18.Text;
                pos.likea = itemname18.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM18.Visibility = Visibility.Visible;
                    itemname18.Text = aaid;
                    if (TOTITM18.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM18.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM18.ItemsSource = dn.DefaultView;
                    TOTITM18.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM18.Visibility = Visibility.Collapsed;
            }
        }
        private void Itemname18_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname18.Text = "";
                TOTITM18.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM18.Focus();
                TOTITM18.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate18.Text = "";
                quantity18.Text = ""; total18.Text = "";
                TOTITM18.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname18.Text == "")
                {
                    itemrate18.Text = "";
                    quantity18.Text = ""; total18.Text = "";
                    TOTITM18.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Quantity18_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname18.Text == "") { itemname18.Focus(); }
            else { DISITM18.IsEnabled = true; }
        }
        private void Itemname19_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                itemname19.Text = "";
                TOTITM19.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Down)
            {
                TOTITM19.Focus();
                TOTITM19.SelectedIndex = 0;
            }
            else if (e.Key == Key.Delete)
            {
                itemrate19.Text = "";
                quantity19.Text = ""; total19.Text = "";
                TOTITM19.ItemsSource = dn.DefaultView;
            }
            else if (e.Key == Key.Back)
            {
                if (itemname19.Text == "")
                {
                    itemrate19.Text = "";
                    quantity19.Text = ""; total19.Text = "";
                    TOTITM19.ItemsSource = dn.DefaultView;
                }
            }
        }
        private void Itemname19_LostFocus(object sender, RoutedEventArgs e)
        {
            id = itemname19.Text;
            if (alp.IsMatch(id))
            {
                DataTable dt = pos.itmname();

                if (dt.Rows.Count == 0)
                {
                    if (itemname19.Text == "")
                    { }
                    else
                    {itemname19.Text = "";}
                }
            }
            else { itemname19.Text = ""; }
            itm = itemname19.Text;
        }
        private void Itemname19_TextChanged(object sender, TextChangedEventArgs e)
        {
            TOTITM.Visibility = Visibility.Collapsed;
            TOTITM1.Visibility = Visibility.Collapsed;
            TOTITM2.Visibility = Visibility.Collapsed;
            TOTITM3.Visibility = Visibility.Collapsed;
            TOTITM4.Visibility = Visibility.Collapsed;
            TOTITM5.Visibility = Visibility.Collapsed;
            TOTITM6.Visibility = Visibility.Collapsed;
            TOTITM7.Visibility = Visibility.Collapsed;
            TOTITM8.Visibility = Visibility.Collapsed;
            TOTITM9.Visibility = Visibility.Collapsed;
            TOTITM10.Visibility = Visibility.Collapsed;
            TOTITM11.Visibility = Visibility.Collapsed;
            TOTITM12.Visibility = Visibility.Collapsed;
            TOTITM13.Visibility = Visibility.Collapsed;
            TOTITM14.Visibility = Visibility.Collapsed;
            TOTITM15.Visibility = Visibility.Collapsed;
            TOTITM16.Visibility = Visibility.Collapsed;
            TOTITM17.Visibility = Visibility.Collapsed;
            TOTITM18.Visibility = Visibility.Collapsed;

            DataTable dt = pos.itemslist();
            if (itemname19.Text.Trim() != "")
            {
                aaid = itemname19.Text;
                pos.likea = itemname19.Text;
                DataTable DT = pos.itms1();
                if (DT.Rows.Count != 0)
                {
                    TOTITM19.Visibility = Visibility.Visible;
                    itemname19.Text = aaid;
                    if (TOTITM19.SelectedIndex != -1)
                    { }
                    else
                    { TOTITM19.ItemsSource = DT.DefaultView; }

                }
                else
                {
                    TOTITM19.ItemsSource = dt.DefaultView;
                    TOTITM19.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                TOTITM19.Visibility = Visibility.Collapsed;
            }
        }
        private void Quantity19_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname19.Text == "") { itemname19.Focus(); }
            else { DISITM19.IsEnabled = true; }
        }
        private void Total19_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        private void Quantity19_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM19.Text = "0";
            QY = quantity19.Text;
            if (num.IsMatch(QY))
            {
                if (quantity19.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q19 = Convert.ToInt32(quantity19.Text);
                    r19 = Convert.ToDecimal(itemrate19.Text);
                    total19.Text = (q19 * r19).ToString();
                    a19 = Convert.ToDecimal(total19.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t19 = 0; }
                        else
                        { t19 = a19 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax205 = a19 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax2018 = a19 * 18 / 100;
                            }
                        }
                    }
                        if (c19 == 0)
                        { count++; c19 = 1; }
                        TOTITM19.Visibility = Visibility.Collapsed;
                }
            }
            else { quantity19.Text = ""; total19.Text = ""; }
        }    
        private void Yes_Click(object sender, RoutedEventArgs e)
        {

        }
        private void No_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OffYes_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OffNo_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OfferOk_Click(object sender, RoutedEventArgs e)
        {

        }
        public decimal disperin,totdisamount,totdisbill,totinsbill;
        
        
        private void Total18_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity18.Text == "") { quantity18.Focus(); }
            else { itemname19.Focus(); TOTITM19.ItemsSource = dn.DefaultView; }
        }
        private void Quantity18_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM18.Text = "0";
            QY = quantity18.Text;
            if (num.IsMatch(QY))
            {
                if (quantity18.Text == "")
                {
                    MessageBox.Show("Please Enter Valid Data ");
                }
                else
                {
                    q18 = Convert.ToInt32(quantity18.Text);
                    r18 = Convert.ToDecimal(itemrate18.Text);
                    total18.Text = (q18 * r18).ToString();
                    a18 = Convert.ToDecimal(total18.Text);
                    DataTable DT = pos.gsttax();
                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                        if (gst == 0)
                        { t18 = 0; }
                        else
                        { t18 = a18 * gst / 100; if (gst == Convert.ToDecimal(5.00))
                            {
                                tax195 = a18 * 5 / 100;
                            }
                            else if (gst == Convert.ToDecimal(18.00))
                            {
                                tax1918 = a18 * 18 / 100;
                            }
                        }
                    }
                        sp19.Visibility = Visibility.Visible;
                        if (c18 == 0)
                        { count++; c18 = 1; }
                }
            }
            else
            {
                quantity18.Text = ""; total18.Text = "";
            }
        }
        private void Quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            DISITM.Text = "0";
            QY = quantity.Text;
            if (quantity.Text == "")
            {
                MessageBox.Show("Please Enter Valid Data ");
            }
            else if (num.IsMatch(QY))
            {
                q = Convert.ToInt32(quantity.Text);
                r = Convert.ToDecimal(itemrate.Text);
                total.Text = (q * r).ToString();
                a = Convert.ToDecimal(total.Text);
                DataTable DT = pos.gsttax();
                if (DT.Rows.Count == 0)
                { }
                else
                {
                    gst = Convert.ToDecimal(DT.Rows[0]["TAX_Percentage"].ToString());
                    if (gst == 0)
                    { t = 0; }
                    else
                    {
                        t = a * gst / 100;
                        if (gst == Convert.ToDecimal(5.00))
                        {
                            tax15 = Convert.ToDecimal(total.Text) * 5 / 100;
                        }
                        else if (gst == Convert.ToDecimal(18.00))
                        {
                            tax118 = Convert.ToDecimal(total.Text) * 18 / 100;
                        }
                    }
                }
                    sp1.Visibility = Visibility.Visible;
                    if (c == 0)
                    { count++; c = 1; }
                    TOTITM.Visibility = Visibility.Collapsed; 
            }
            else
            { quantity.Text = ""; total.Text = ""; }
        }
        private void Total_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantity.Text == "")
            { quantity.Focus(); }
            else { itemname1.Focus(); TOTITM1.ItemsSource = dn.DefaultView; }
            
        }
        private void Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemname.Text == "") { itemname.Focus(); }
            else { DISITM.IsEnabled = true; }
        }
    }
}
