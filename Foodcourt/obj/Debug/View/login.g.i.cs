﻿#pragma checksum "..\..\..\View\login.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3EEF8A87F34E5D1B320827541B3FA21B4D656AEE"
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
    /// login
    /// </summary>
    public partial class login : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\View\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btncls;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\View\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame Mainframe;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtusern;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\View\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtpwd;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\View\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnlogin;
        
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
            System.Uri resourceLocater = new System.Uri("/Foodcourt;component/view/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\login.xaml"
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
            this.btncls = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\View\login.xaml"
            this.btncls.Click += new System.Windows.RoutedEventHandler(this.btncls_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Mainframe = ((System.Windows.Controls.Frame)(target));
            return;
            case 3:
            this.txtusern = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\..\View\login.xaml"
            this.txtusern.LostFocus += new System.Windows.RoutedEventHandler(this.txtusern_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtpwd = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 58 "..\..\..\View\login.xaml"
            this.txtpwd.LostFocus += new System.Windows.RoutedEventHandler(this.txtpwd_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnlogin = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\View\login.xaml"
            this.btnlogin.Click += new System.Windows.RoutedEventHandler(this.btnlogin_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

