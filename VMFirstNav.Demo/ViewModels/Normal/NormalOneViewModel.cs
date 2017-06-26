using System;
using CodeMill.VMFirstNav;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	public class NormalOneViewModel : IViewModel, INotifyPropertyChanged
	{
		INavigationService _navService;

		public NormalOneViewModel()
		{
			Title = "Normal";

            _navService = NavigationService.Instance;

			Description = "Normal navigation stack only";
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

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateToChild
		{
			get
			{
				if (_navToChild == null)
				{
					_navToChild = new Command(async () =>
				   {
					   await _navService.PushAsync<NormalOneChildViewModel>((vm) => vm.InitializeDisplay("Normal child!"));
				   }
					);
				}
				return _navToChild;
			}
		}
	}
}
