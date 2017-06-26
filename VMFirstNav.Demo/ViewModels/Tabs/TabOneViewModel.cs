using System;
using CodeMill.VMFirstNav;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	public class TabOneViewModel : IViewModel, INotifyPropertyChanged
	{
		readonly INavigationService _navService;
		public TabOneViewModel()
		{
			Title = "Tab One";

            _navService = NavigationService.Instance;
		}

		string title;
		public string Title
		{
			get => title;
			set
			{
				title = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
			}
		}


		ICommand _navToChild;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateToChild
		{
			get
			{
				if (_navToChild == null)
				{
					_navToChild = new Command(async () =>
				   {
					   await _navService.PushAsync<TabOneChildViewModel>((vm) => vm.InitializeDisplay("Title from initialization routine"));
				   }
					);
				}
				return _navToChild;
			}
		}
	}
}
