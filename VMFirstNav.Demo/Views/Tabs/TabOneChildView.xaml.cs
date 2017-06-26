using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class TabOneChildView : ContentPage, IViewFor<TabOneChildViewModel>
    {
        public TabOneChildView()
        {
            InitializeComponent();
        }

        TabOneChildViewModel vm;
        public TabOneChildViewModel ViewModel
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
