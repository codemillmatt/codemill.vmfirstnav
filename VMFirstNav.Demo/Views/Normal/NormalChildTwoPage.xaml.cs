using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class NormalChildTwoPage : ContentPage, IViewFor<NormalChildTwoViewModel>
    {
        public NormalChildTwoPage()
        {
            InitializeComponent();
        }

        NormalChildTwoViewModel vm;
        public NormalChildTwoViewModel ViewModel { 
            get => vm; 
            set
            {
                vm = value;
                BindingContext = vm;
            }
        }
    }
}
