using System;
using CodeMill.VMFirstNav;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	public class NormalChildThreeViewModel : IViewModel, INotifyPropertyChanged
	{
		INavigationService _navService;

		public NormalChildThreeViewModel()
		{
			Title = "Three";
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

		Command _navigateOne;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command NavigateOne
		{
			get
			{
				if (_navigateOne == null)
				{
					_navigateOne = new Command(() => _navService.PopTo<NormalOneChildViewModel>());
				}

				return _navigateOne;
			}
		}
	}
}
