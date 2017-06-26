using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CodeMill.VMFirstNav;

namespace VMFirstNav.Demo
{
    public partial class NormalOneChildPage : ContentPage, IViewFor<NormalOneChildViewModel>
    {
        public NormalOneChildPage()
        {
            InitializeComponent();
        }

        NormalOneChildViewModel vm;
        public NormalOneChildViewModel ViewModel
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
