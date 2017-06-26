using System;
using System.ComponentModel;
using CodeMill.VMFirstNav;
namespace VMFirstNav.Demo
{
    public class RootTabViewModel : IViewModel, INotifyPropertyChanged
    {
        public RootTabViewModel()
        {
            Title = "Root Tabs";
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
