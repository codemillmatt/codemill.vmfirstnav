using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class NormalModalPage : ContentPage, IViewFor<NormalModalViewModel>
    {
        public NormalModalPage()
        {
            InitializeComponent();
        }

        NormalModalViewModel vm;
        public NormalModalViewModel ViewModel
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
