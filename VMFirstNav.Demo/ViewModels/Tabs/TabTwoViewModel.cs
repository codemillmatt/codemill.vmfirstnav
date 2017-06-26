using System;
using System.ComponentModel;
using System.Windows.Input;
using CodeMill.VMFirstNav;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	public class TabTwoViewModel : IViewModel, INotifyPropertyChanged
	{
		readonly INavigationService _navService;
		public TabTwoViewModel()
		{
			Title = "Tab Two";

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
						await _navService.PushAsync<TabTwoChildViewModel>();
					});
				}
				return _navToChild;
			}
		}
	}
}
