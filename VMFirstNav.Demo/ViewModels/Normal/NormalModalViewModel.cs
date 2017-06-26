using System;
using CodeMill.VMFirstNav;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace VMFirstNav.Demo
{
	public class NormalModalViewModel : IViewModel, INotifyPropertyChanged
	{
		INavigationService _navService;

		public NormalModalViewModel()
		{
			Title = "Normal Modal";

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

		ICommand _dismissModal;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand DismissModalCommand
		{
			get
			{
				if (_dismissModal == null)
				{
					_dismissModal = new Command(async () => await _navService.PopModalAsync());
				}
				return _dismissModal;
			}
		}
	}
}
