using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class TabTwoChildView : ContentPage, IViewFor<TabTwoChildViewModel>
    {
        public TabTwoChildView()
        {
            InitializeComponent();
        }

        TabTwoChildViewModel vm;
        public TabTwoChildViewModel ViewModel
        {
            get => vm;
            set
            {
                vm = value;
                BindingContext = vm;
            }
        }
    }
}
