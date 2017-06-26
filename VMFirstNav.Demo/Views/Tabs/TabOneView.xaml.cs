using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class TabOneView : ContentPage, IViewFor<TabOneViewModel>
    {
        public TabOneView()
        {
            InitializeComponent();
        }

        TabOneViewModel vm;
        public TabOneViewModel ViewModel
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
