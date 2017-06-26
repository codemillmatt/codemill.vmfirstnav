using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class NormalChildThreePage : ContentPage, IViewFor<NormalChildThreeViewModel>
    {
        public NormalChildThreePage()
        {
            InitializeComponent();
        }

        NormalChildThreeViewModel vm;
        public NormalChildThreeViewModel ViewModel { 
            get => vm; 
            set {
                vm = value;
                BindingContext = vm;
            }
        }
    }
}
