﻿#pragma checksum "..\..\..\ClientMenu.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4207969DB894BA891CFA58960188CDEC86B53ABC"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace HotelAdmin {
    
    
    /// <summary>
    /// ClientView
    /// </summary>
    public partial class ClientView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 120 "..\..\..\ClientMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NomTextBox;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\ClientMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailTextBox;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\ClientMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TelephoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\ClientMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AdresseTextBox;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\ClientMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\ClientMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ClientListView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HotelAdmin;component/clientmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ClientMenu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NomTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.EmailTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TelephoneTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.AdresseTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 141 "..\..\..\ClientMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AjouterClient_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 143 "..\..\..\ClientMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ModifierClient_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 145 "..\..\..\ClientMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SupprimerClient_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 147 "..\..\..\ClientMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ReinitialiserListe_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 163 "..\..\..\ClientMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RechercherClient_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ClientListView = ((System.Windows.Controls.ListView)(target));
            
            #line 167 "..\..\..\ClientMenu.xaml"
            this.ClientListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ClientListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 181 "..\..\..\ClientMenu.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.VoirClientsArchive_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

