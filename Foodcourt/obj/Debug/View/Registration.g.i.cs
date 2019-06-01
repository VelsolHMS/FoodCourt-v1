﻿#pragma checksum "..\..\..\View\Registration.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6F07C5393F860147F8091D24B4F60241703FE14F"
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
using Foodcourt.ViewModel;
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
    /// Registration
    /// </summary>
    public partial class Registration : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox usertxt;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nametxt;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phonetxt;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordtxt;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox repasswordtxt;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbadmin;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbuser;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnclear;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnsave;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\View\Registration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Register;
        
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
            System.Uri resourceLocater = new System.Uri("/Foodcourt;component/view/registration.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Registration.xaml"
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
            this.usertxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\View\Registration.xaml"
            this.usertxt.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\View\Registration.xaml"
            this.usertxt.LostFocus += new System.Windows.RoutedEventHandler(this.usertxt_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.nametxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\..\View\Registration.xaml"
            this.nametxt.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            return;
            case 3:
            this.phonetxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\..\View\Registration.xaml"
            this.phonetxt.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            return;
            case 4:
            this.passwordtxt = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.repasswordtxt = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.rbadmin = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.rbuser = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.btnclear = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\..\View\Registration.xaml"
            this.btnclear.Click += new System.Windows.RoutedEventHandler(this.btnclear_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnsave = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\View\Registration.xaml"
            this.btnsave.Click += new System.Windows.RoutedEventHandler(this.btnsave_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Register = ((System.Windows.Controls.DataGrid)(target));
            
            #line 69 "..\..\..\View\Registration.xaml"
            this.Register.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Register_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

