using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace VMFirstNav.Demo
{
    public partial class MasterDetailRootPage : MasterDetailPage
    {
        public MasterDetailRootPage()
        {
            InitializeComponent();

			listNav.ViewModel = new MasterListNavViewModel();
			normalOne.ViewModel = new NormalOneViewModel();
        }
    }
}
