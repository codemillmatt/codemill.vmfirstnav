using System;
using CodeMill.VMFirstNav;
using System.ComponentModel;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	public class NormalChildTwoViewModel : IViewModel, INotifyPropertyChanged
	{
		INavigationService _navService;
		public NormalChildTwoViewModel()
		{
			Title = "Normal Two";
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

		Command _navigateThree;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command NavigateThree
		{
			get
			{
				if (_navigateThree == null)
				{
					_navigateThree = new Command(async () => await _navService.PushAsync<NormalChildThreeViewModel>());
				}

				return _navigateThree;
			}
		}
	}
}
