using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using Foodcourt.Model;
using Foodcourt.ViewModel;

namespace Foodcourt.View
{
    /// <summary>
    /// Interaction logic for ItemCategory.xaml
    /// </summary>
    public partial class ItemCategory : Page
    {
        ItemsCAT it = new ItemsCAT();
        public int error = 0;
        public ItemCategory()
        {
            DataF.COUNT = 0;
            InitializeComponent();
            DataF.COUNT = 1;
            txtId.Text =Convert.ToString(it.id());
            DataTable dtt = it.FillDataGrid();
            dgctg.ItemsSource = dtt.DefaultView;
            DataTable stalls = it.GetStalls();
            txtstall.ItemsSource = stalls.DefaultView;
            //dpactive.DisplayDateStart = DateTime.Today;
            //dpactive.Text = Convert.ToString(DateTime.Today.Date);
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                error++;
            else
                error--;
        }
        public void Clear()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtdts.Text = "";
            txtrpt.Text = "";
            txtstatus.SelectedIndex = 0;
        }
        public string a, b;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (error != 0 || (string.IsNullOrWhiteSpace(txtId.Text)) || (string.IsNullOrWhiteSpace(txtName.Text)) || (string.IsNullOrWhiteSpace(txtrpt.Text)) || (string.IsNullOrWhiteSpace(txtstatus.Text)) || (string.IsNullOrWhiteSpace(txtstall.Text)))
                {
                    if (txtId.Text == "")
                    { txtId.Text = ""; }
                    if (txtName.Text == "")
                    { txtName.Text = ""; }
                    //if (txtdts.Text == "")
                    //{ txtdts.Text = ""; }
                    if (txtrpt.Text == "")
                    { txtrpt.Text = ""; }
                    MessageBox.Show("Please fill all fields");
                }
                else
                {
                    it.CTG_Id = txtId.Text;
                    it.CTG_Name = txtName.Text;
                    DataTable Category_Checking = it.CategoryChecking();
                    it.CTG_Details = txtdts.Text;
                    it.CTG_Status = txtstatus.Text;
                    it.CTG_ReportingName = txtrpt.Text;
                    it.STL_Name = txtstall.Text;
                    DataTable dt = it.FillDataGrid();
                    if (dt.Rows.Count >= 0)
                    {
                        string a = "Save"; b = Convert.ToString(btnSave.Content);
                        if (a == b)
                        {
                            if (Category_Checking.Rows.Count > 0)
                            {
                                MessageBox.Show("Category Name Already Exists. Please Change the Name.!");
                            }
                            else
                            {
                                it.INSERT();
                            }
                        }
                        else
                        {
                            it.UPDATE();
                        }
                    }
                    DataTable dtt = it.FillDataGrid();
                    dgctg.ItemsSource = dtt.DefaultView;
                    Clear();
                    this.NavigationService.Refresh();
                    MessageBox.Show("Saved successfully");
                    btnSave.Content = "Save";
                }
            }
            catch(SystemException)
            { }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            btnSave.Content = "Save";
            this.NavigationService.Refresh();
        }
        private void dgctg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnSave.Content = "Modify";
            int i = dgctg.SelectedIndex;
            DataTable dt2 = it.filltable();
            if (dt2.Rows.Count == 0)
            {
            }
            else
            {
                if (i >= 0)
                {
                    txtId.Text = dt2.Rows[i]["CTG_Id"].ToString();
                    txtName.Text = dt2.Rows[i]["CTG_Name"].ToString();
                    txtdts.Text = dt2.Rows[i]["CTG_Details"].ToString();
                    txtrpt.Text = dt2.Rows[i]["CTG_ReportingName"].ToString();
                    txtstatus.Text = dt2.Rows[i]["CTG_Status"].ToString();
                    txtstall.Text = dt2.Rows[i]["STL_Name"].ToString();
                }
                else
                {
                }
            }
            dgctg.UnselectAll();
        }
    }
}
