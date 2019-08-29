using Foodcourt.Model;
using Foodcourt.ViewModel;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for ItemName.xaml
    /// </summary>
    public partial class ItemName : Page
    {
        ItemsCAT item_c = new ItemsCAT();
        DataTable dt = new DataTable();
        DataTable getDetails;
        tax tax_n = new tax();
        public int error = 0;
        ItemNames items = new ItemNames();
        public ItemName()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            dt = items.FillGrid();
            itemsdg.ItemsSource = dt.DefaultView;
            DataTable cate = item_c.GetCategories();
            catgytxt.ItemsSource = cate.DefaultView;
            DataTable tax_names = tax_n.GetTaxNames();
            taxtxt.ItemsSource = tax_names.DefaultView;
            itemIdtxt.Text = items.GetItemId().ToString();
            itemIdtxt.IsReadOnly = true;
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        public static string ctgname;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (error != 0 || (string.IsNullOrWhiteSpace(itemIdtxt.Text)) || (string.IsNullOrWhiteSpace(nametxt.Text)) || (string.IsNullOrWhiteSpace(catgytxt.Text)) || (string.IsNullOrWhiteSpace(pricetxt.Text)) || (string.IsNullOrWhiteSpace(taxtxt.Text)) || (string.IsNullOrWhiteSpace(rpnametxt.Text)) || (string.IsNullOrWhiteSpace(status.Text)))
                {
                    MessageBox.Show("Please fill all fields");
                }
                else
                {
                    items.NAM_Name = nametxt.Text;
                    DataTable name_checking = items.NameChecking();
                    items.NAM_Rate = Convert.ToDecimal(pricetxt.Text);
                    items.CTG_Name = catgytxt.Text;
                    DataTable dtstlid = items.getstlid();
                    items.STL_ID = dtstlid.Rows[0]["STL_ID"].ToString();
                    items.NAM_Tax = taxtxt.Text;
                    items.NAM_Details = detailstxt.Text;
                    items.NAM_ReportingName = rpnametxt.Text;
                    items.NAM_Status = status.Text;
                    if (btnSave.Content.ToString() == "Update")
                    {
                        items.NAM_Id = itemIdtxt.Text;
                        items.Update();
                        MessageBox.Show("Updated Succesfully");
                    }
                    else
                    {
                        if (name_checking.Rows.Count > 0)
                        {
                            MessageBox.Show("Item Name Already Exists. Please Change the Name.!");
                        }
                        else
                        {
                            items.Insert();
                            MessageBox.Show("Inserted Succesfully");
                        }
                    }
                    Clear();
                    this.NavigationService.Refresh();
                    itemIdtxt.Text = items.GetItemId().ToString();
                    itemIdtxt.IsReadOnly = true;
                }
            }
            catch (SystemException)
            {
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.NavigationService.Refresh();
        }
        public void Clear()
        {
            nametxt.Text = "";
            catgytxt.Text = "";
            pricetxt.Text = "";
            taxtxt.Text = "";
            rpnametxt.Text = "";
            detailstxt.Text = "";
            status.Text = "";
            btnSave.Content = "Save";
            catgytxt.IsEnabled = true;
            DataTable cate = item_c.GetCategories();
            catgytxt.ItemsSource = cate.DefaultView;
            DataTable tax_names = tax_n.GetTaxNames();
            taxtxt.ItemsSource = tax_names.DefaultView;
        }
        private void itemsdg_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int selected_index = itemsdg.SelectedIndex;
            if(selected_index < 0)
            {

            }
            else
            {
                items.NAM_Id = dt.Rows[selected_index]["NAM_Id"].ToString();
                getDetails = items.GetAllDetails();
                itemIdtxt.Text = getDetails.Rows[0]["NAM_Id"].ToString();
                nametxt.Text = getDetails.Rows[0]["NAM_Name"].ToString();
                item_c.CTG_Id = getDetails.Rows[0]["CTG_Id"].ToString();
                item_c.GetCatName();
                catgytxt.Text = item_c.CTG_Name;
                pricetxt.Text = getDetails.Rows[0]["NAM_Rate"].ToString();
                taxtxt.Text = getDetails.Rows[0]["NAM_Tax"].ToString();
                detailstxt.Text = getDetails.Rows[0]["NAM_Details"].ToString();
                rpnametxt.Text = getDetails.Rows[0]["NAM_ReportingName"].ToString();
                status.Text = getDetails.Rows[0]["NAM_Status"].ToString();
                btnSave.Content = "Update";
                catgytxt.IsEnabled = false;
            }
        }

        private void catgytxt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (catgytxt.Text == "")
            {
            }
            else
            {
                item_c.CTG_Name = catgytxt.Text;
                item_c.GetActiveDate();
                DateTime active_date = Convert.ToDateTime(item_c.CTG_ActiveDate);
                if (active_date > DateTime.Today)
                {
                    //activedp.Text = active_date.ToString();
                }
                else
                {
                   //activedp.Text = Convert.ToString(DateTime.Today.Date);
                }
            }
        }
        private void catgytxt_DropDownClosed(object sender, EventArgs e)
        {
            if (catgytxt.Text == "")
            {
            }
            else
            {
                item_c.CTG_Name = catgytxt.Text;
                item_c.GetActiveDate();
                DateTime active_date = Convert.ToDateTime(item_c.CTG_ActiveDate);
                if (active_date > DateTime.Today)
                {
                    //activedp.Text = active_date.ToString();
                }
                else
                {
                    //activedp.Text = Convert.ToString(DateTime.Today.Date);
                }
            }
        }
    }
}
