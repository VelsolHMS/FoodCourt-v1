﻿#pragma checksum "..\..\..\View\BillView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1B7971D4E92C9BD0A0F9A6AA0946FBC9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Foodcourt.View.Oprs;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using RootLibrary.WPF.Localization;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Foodcourt.View.Oprs {
    
    
    /// <summary>
    /// BillView
    /// </summary>
    public partial class BillView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker bill_date;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnsearch;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox billwiseSearch;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnbillnoS;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgBill;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Card canview;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem close;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgcancel;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Card Dtlview;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Miprint;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Micancel;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MIclose;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtbillno;
        
        #line default
        #line hidden
        
        
        #line 199 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtbilldate;
        
        #line default
        #line hidden
        
        
        #line 202 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgdtlview;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtnetamount;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtcgst;
        
        #line default
        #line hidden
        
        
        #line 221 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtsgst;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtgttl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Foodcourt;component/view/billview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\BillView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 24 "..\..\..\View\BillView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bill_date = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.btnsearch = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\View\BillView.xaml"
            this.btnsearch.Click += new System.Windows.RoutedEventHandler(this.btnsearch_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.billwiseSearch = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnbillnoS = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\View\BillView.xaml"
            this.btnbillnoS.Click += new System.Windows.RoutedEventHandler(this.btnbillnoS_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.dgBill = ((System.Windows.Controls.DataGrid)(target));
            
            #line 47 "..\..\..\View\BillView.xaml"
            this.dgBill.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.dgBill_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.canview = ((MaterialDesignThemes.Wpf.Card)(target));
            return;
            case 8:
            this.close = ((System.Windows.Controls.MenuItem)(target));
            
            #line 122 "..\..\..\View\BillView.xaml"
            this.close.Click += new System.Windows.RoutedEventHandler(this.close_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dgcancel = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.Dtlview = ((MaterialDesignThemes.Wpf.Card)(target));
            return;
            case 11:
            this.Miprint = ((System.Windows.Controls.MenuItem)(target));
            
            #line 183 "..\..\..\View\BillView.xaml"
            this.Miprint.Click += new System.Windows.RoutedEventHandler(this.Miprint_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Micancel = ((System.Windows.Controls.MenuItem)(target));
            
            #line 184 "..\..\..\View\BillView.xaml"
            this.Micancel.Click += new System.Windows.RoutedEventHandler(this.Micancel_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.MIclose = ((System.Windows.Controls.MenuItem)(target));
            
            #line 185 "..\..\..\View\BillView.xaml"
            this.MIclose.Click += new System.Windows.RoutedEventHandler(this.MIclose_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.txtbillno = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 15:
            this.txtbilldate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.dgdtlview = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 17:
            this.txtnetamount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 18:
            this.txtcgst = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.txtsgst = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 20:
            this.txtgttl = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

