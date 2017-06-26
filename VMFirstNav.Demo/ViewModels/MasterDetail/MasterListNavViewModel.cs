using System;
using System.Collections.Generic;
using System.Windows.Input;
using CodeMill.VMFirstNav;
using Xamarin.Forms;
using System.ComponentModel;

namespace VMFirstNav.Demo
{
    public class MasterListNavViewModel : IViewModel, INotifyPropertyChanged
    {
		INavigationService _navService;

		// The overall list that will keep track of which view models can be navigated
		// to and displayed in the "master" portion of master/detail
		public List<IMasterListItem<IViewModel>> AvailablePages { get; set; }


		public MasterListNavViewModel()
		{
            _navService = NavigationService.Instance;

			// This is where we add the view models we can navigate to
			// And the descriptions to be displayed
			AvailablePages = new List<IMasterListItem<IViewModel>>();
			AvailablePages.Add(new MasterListItem<NormalOneViewModel>("Normal Nav"));
			AvailablePages.Add(new MasterListItem<RootTabViewModel>("Tab Pages"));

			Title = "Nav";
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

		ICommand _navCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateCommand
		{
			get
			{
				if (_navCommand == null)
				{
					_navCommand = new Command((selectedItem) =>
					{
						// Get the selected item from the command
						var itemToNavigate = selectedItem as IMasterListItem<IViewModel>;

						if (itemToNavigate != null)
						{
							// Get the view model type
							var viewModelType = itemToNavigate.GetType().GenericTypeArguments[0];

                            // Get a view model instance
                            var viewModel = Activator.CreateInstance(viewModelType) as IViewModel;

							// Perform the switch
							_navService.SwitchDetailPage(viewModel);
						}
					});
				}
				return _navCommand;
			}
		}

        public T Cast<T>(object input)
        {
            return (T)input;
        }
    }
}
