using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class RootTabPage : TabbedPage, IViewFor<RootTabViewModel>
    {
        public RootTabPage()
        {
            InitializeComponent();

			tabOne.ViewModel = new TabOneViewModel();
			tabTwo.ViewModel = new TabTwoViewModel();
        }

        RootTabViewModel vm;
        public RootTabViewModel ViewModel
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
