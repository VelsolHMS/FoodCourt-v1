﻿#pragma checksum "..\..\..\View\BillView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3D2A0E2C8E9F2D4A91E6E0AC2E98F92A236AC211"
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
        
        
        #line 28 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem pendingbills;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker bill_date;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnsearch;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox billwiseSearch;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnbillnoS;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgBill;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Card canview;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem close;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgcancel;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Card pendingview;
        
        #line default
        #line hidden
        
        
        #line 192 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem closee;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgpending;
        
        #line default
        #line hidden
        
        
        #line 211 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Card Dtlview;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Miprint;
        
        #line default
        #line hidden
        
        
        #line 226 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Micancel;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MIclose;
        
        #line default
        #line hidden
        
        
        #line 239 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtbillno;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtbilldate;
        
        #line default
        #line hidden
        
        
        #line 244 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgdtlview;
        
        #line default
        #line hidden
        
        
        #line 258 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtnetamount;
        
        #line default
        #line hidden
        
        
        #line 263 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtcgst;
        
        #line default
        #line hidden
        
        
        #line 268 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtsgst;
        
        #line default
        #line hidden
        
        
        #line 273 "..\..\..\View\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtdis;
        
        #line default
        #line hidden
        
        
        #line 278 "..\..\..\View\BillView.xaml"
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
            
            #line 27 "..\..\..\View\BillView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.pendingbills = ((System.Windows.Controls.MenuItem)(target));
            
            #line 28 "..\..\..\View\BillView.xaml"
            this.pendingbills.Click += new System.Windows.RoutedEventHandler(this.Pendingbills_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.bill_date = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.btnsearch = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\View\BillView.xaml"
            this.btnsearch.Click += new System.Windows.RoutedEventHandler(this.btnsearch_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.billwiseSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 52 "..\..\..\View\BillView.xaml"
            this.billwiseSearch.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.BillwiseSearch_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnbillnoS = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\View\BillView.xaml"
            this.btnbillnoS.Click += new System.Windows.RoutedEventHandler(this.btnbillnoS_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dgBill = ((System.Windows.Controls.DataGrid)(target));
            
            #line 61 "..\..\..\View\BillView.xaml"
            this.dgBill.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.dgBill_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.canview = ((MaterialDesignThemes.Wpf.Card)(target));
            return;
            case 9:
            this.close = ((System.Windows.Controls.MenuItem)(target));
            
            #line 132 "..\..\..\View\BillView.xaml"
            this.close.Click += new System.Windows.RoutedEventHandler(this.close_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgcancel = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.pendingview = ((MaterialDesignThemes.Wpf.Card)(target));
            return;
            case 12:
            this.closee = ((System.Windows.Controls.MenuItem)(target));
            
            #line 192 "..\..\..\View\BillView.xaml"
            this.closee.Click += new System.Windows.RoutedEventHandler(this.Closee_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.dgpending = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 14:
            this.Dtlview = ((MaterialDesignThemes.Wpf.Card)(target));
            return;
            case 15:
            this.Miprint = ((System.Windows.Controls.MenuItem)(target));
            
            #line 225 "..\..\..\View\BillView.xaml"
            this.Miprint.Click += new System.Windows.RoutedEventHandler(this.Miprint_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.Micancel = ((System.Windows.Controls.MenuItem)(target));
            
            #line 226 "..\..\..\View\BillView.xaml"
            this.Micancel.Click += new System.Windows.RoutedEventHandler(this.Micancel_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.MIclose = ((System.Windows.Controls.MenuItem)(target));
            
            #line 227 "..\..\..\View\BillView.xaml"
            this.MIclose.Click += new System.Windows.RoutedEventHandler(this.MIclose_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.txtbillno = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.txtbilldate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 20:
            this.dgdtlview = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 21:
            this.txtnetamount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 22:
            this.txtcgst = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 23:
            this.txtsgst = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 24:
            this.txtdis = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 25:
            this.txtgttl = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

