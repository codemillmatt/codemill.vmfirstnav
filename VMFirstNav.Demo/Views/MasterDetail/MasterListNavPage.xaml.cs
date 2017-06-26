using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class MasterListNavPage : ContentPage, IViewFor<MasterListNavViewModel>
    {
        public MasterListNavPage()
        {
            InitializeComponent();
        }

        MasterListNavViewModel vm;
        public MasterListNavViewModel ViewModel { get => vm; 
            set
            {
                vm = value;
                BindingContext = vm;
            }
        }
    }
}
