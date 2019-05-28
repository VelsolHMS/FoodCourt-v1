﻿#pragma checksum "..\..\..\View\Property.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9F2EDE0D1E84E874487DF77D6B30447D17D7FE83"
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
    /// Property
    /// </summary>
    public partial class Property : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\View\Property.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nametxt;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View\Property.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addresstxt;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\View\Property.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox statetxt;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\View\Property.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox countrytxt;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\Property.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phonetxt;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\View\Property.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox gsttxt;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\View\Property.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnclear;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\View\Property.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnsave;
        
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
            System.Uri resourceLocater = new System.Uri("/Foodcourt;component/view/property.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Property.xaml"
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
            this.nametxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\..\View\Property.xaml"
            this.nametxt.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            return;
            case 2:
            this.addresstxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.statetxt = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.countrytxt = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.phonetxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\..\View\Property.xaml"
            this.phonetxt.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            return;
            case 6:
            this.gsttxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 59 "..\..\..\View\Property.xaml"
            this.gsttxt.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnclear = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\View\Property.xaml"
            this.btnclear.Click += new System.Windows.RoutedEventHandler(this.btnclear_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnsave = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\View\Property.xaml"
            this.btnsave.Click += new System.Windows.RoutedEventHandler(this.btnsave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

