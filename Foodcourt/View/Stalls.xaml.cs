using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for Stalls.xaml
    /// </summary>
    public partial class Stalls : Page
    {
        public int error = 0;
        public Stalls()
        {
            InitializeComponent();
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Dgstall_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
