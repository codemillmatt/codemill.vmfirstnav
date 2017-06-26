using System;
using System.ComponentModel;
using System.Windows.Input;
using CodeMill.VMFirstNav;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	public class NormalOneChildViewModel : IViewModel, INotifyPropertyChanged
	{
		INavigationService _navService;
		public NormalOneChildViewModel()
		{
			Title = "Normal Child";

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

		public void InitializeDisplay(string description)
		{
			Description = description;
		}

		string _description;
		public string Description
		{
            get => _description;
			set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
		}

		ICommand _navToChild;
		public ICommand NavigatePopup
		{
			get
			{
				if (_navToChild == null)
				{
					_navToChild = new Command(async () =>
				   {
					   await _navService.PushModalAsync<NormalModalViewModel>();
				   });
				}
				return _navToChild;
			}
		}

		Command _navigateTwo;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command NavigateTwo
		{
			get
			{
				if (_navigateTwo == null)
					_navigateTwo = new Command(async () => await _navService.PushAsync<NormalChildTwoViewModel>());

				return _navigateTwo;
			}
		}
	}
}
