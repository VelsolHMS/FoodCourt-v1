using System.Windows.Controls;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for RPTS.xaml
    /// </summary>
    public partial class RPTS : Page
    {
        public RPTS()
        {
            InitializeComponent();
            ssrsreport.Navigate("http://desktop-aj201tk/Reports/Pages/Report.aspx?ItemPath=%2fReports%2fITEM_RATE_LIST");
        }
    }
}
