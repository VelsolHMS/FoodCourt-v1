﻿#pragma checksum "..\..\..\View\CashHand.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71D513E9EE687DB8E260358484415C37BA7CE5FF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Foodcourt.View;
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


namespace Foodcourt.View {
    
    
    /// <summary>
    /// CashHand
    /// </summary>
    public partial class CashHand : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\View\CashHand.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtname;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\View\CashHand.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtamount;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\View\CashHand.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtreason;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\View\CashHand.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clear;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View\CashHand.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button submit;
        
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
            System.Uri resourceLocater = new System.Uri("/Foodcourt;component/view/cashhand.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\CashHand.xaml"
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
            this.txtname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtamount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtreason = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.clear = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\View\CashHand.xaml"
            this.clear.Click += new System.Windows.RoutedEventHandler(this.clear_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.submit = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\View\CashHand.xaml"
            this.submit.Click += new System.Windows.RoutedEventHandler(this.submit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

