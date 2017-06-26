using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class TabTwoView : ContentPage, IViewFor<TabTwoViewModel>
    {
        public TabTwoView()
        {
            InitializeComponent();
        }

        TabTwoViewModel vm;
        public TabTwoViewModel ViewModel
        {
            get => vm;
            set {
                vm = value;
                BindingContext = vm;
            }
        
        }
    }
}
