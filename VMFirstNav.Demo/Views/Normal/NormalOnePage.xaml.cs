using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class NormalOnePage : ContentPage, IViewFor<NormalOneViewModel>
    {
        public NormalOnePage()
        {
            InitializeComponent();
        }

        NormalOneViewModel vm;
        public NormalOneViewModel ViewModel
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
