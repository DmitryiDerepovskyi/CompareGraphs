﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "21D5AA4AA3149FBB9137E77BC2C6F5A2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18444
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using eisiWare;


namespace CompareGraphs {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectFirstGraphFile;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClearFirstGraph;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectSecondGraphFile;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClearSecondGraph;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas csFirstGraph;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas csSecondGraph;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgFirstGraphG;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgFirstGraphP;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSecondGraphG;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSecondGraphP;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClearFirstGraph_Copy;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOkFirstGraph;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOkSecondGraph;
        
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
            System.Uri resourceLocater = new System.Uri("/CompareGrafs;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 6 "..\..\MainWindow.xaml"
            ((CompareGraphs.MainWindow)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Window_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnSelectFirstGraphFile = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\MainWindow.xaml"
            this.btnSelectFirstGraphFile.Click += new System.Windows.RoutedEventHandler(this.btnSelectFirstGraphFile_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnClearFirstGraph = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\MainWindow.xaml"
            this.btnClearFirstGraph.Click += new System.Windows.RoutedEventHandler(this.btnClearFirstGraph_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnSelectSecondGraphFile = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\MainWindow.xaml"
            this.btnSelectSecondGraphFile.Click += new System.Windows.RoutedEventHandler(this.btnSelectSecondGraphFile_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnClearSecondGraph = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\MainWindow.xaml"
            this.btnClearSecondGraph.Click += new System.Windows.RoutedEventHandler(this.btnClearSecondGraph_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.csFirstGraph = ((System.Windows.Controls.Canvas)(target));
            return;
            case 7:
            this.csSecondGraph = ((System.Windows.Controls.Canvas)(target));
            return;
            case 8:
            this.dgFirstGraphG = ((System.Windows.Controls.DataGrid)(target));
            
            #line 47 "..\..\MainWindow.xaml"
            this.dgFirstGraphG.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.dgPreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dgFirstGraphP = ((System.Windows.Controls.DataGrid)(target));
            
            #line 53 "..\..\MainWindow.xaml"
            this.dgFirstGraphP.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.dgPreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgSecondGraphG = ((System.Windows.Controls.DataGrid)(target));
            
            #line 60 "..\..\MainWindow.xaml"
            this.dgSecondGraphG.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.dgPreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 11:
            this.dgSecondGraphP = ((System.Windows.Controls.DataGrid)(target));
            
            #line 66 "..\..\MainWindow.xaml"
            this.dgSecondGraphP.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.dgPreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnClearFirstGraph_Copy = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\MainWindow.xaml"
            this.btnClearFirstGraph_Copy.Click += new System.Windows.RoutedEventHandler(this.btnCompareGraphs_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.btnOkFirstGraph = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\MainWindow.xaml"
            this.btnOkFirstGraph.Click += new System.Windows.RoutedEventHandler(this.btnOkFirstGraph_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.btnOkSecondGraph = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\MainWindow.xaml"
            this.btnOkSecondGraph.Click += new System.Windows.RoutedEventHandler(this.btnOkSecondGraph_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

