using System;
using System.ComponentModel;
using System.Windows.Input;
using CodeMill.VMFirstNav;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	public class TabOneChildViewModel : IViewModel, INotifyPropertyChanged
	{
		readonly INavigationService _navService;
		public TabOneChildViewModel()
		{
			Title = "Tab One Child";

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

		public void InitializeDisplay(string theTitle)
		{
			Title = theTitle;
		}

		ICommand _navToParent;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateBack
		{
			get
			{
				if (_navToParent == null)
				{
					_navToParent = new Command(async () =>
					   await _navService.PopAsync<TabOneViewModel>(tovm => tovm.Title = "Back to the future")
					);
				}
				return _navToParent;
			}
		}
	}
}
