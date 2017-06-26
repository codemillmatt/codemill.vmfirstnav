using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;

namespace CodeMill.VMFirstNav
{
	public class NavigationService : INavigationService
	{
		static INavigationService instance;
		public static INavigationService Instance => instance ?? (instance = new NavigationService());

        private NavigationService()
        {

        }

        INavigation FormsNavigation
		{
			get
			{
				var tabController = Application.Current.MainPage as TabbedPage;
				var masterController = Application.Current.MainPage as MasterDetailPage;

				// First check to see if we're on a tabbed page, then master detail, finally go to overall fallback
				return tabController?.CurrentPage?.Navigation ??
									 (masterController?.Detail as TabbedPage)?.CurrentPage?.Navigation ?? // special consideration for a tabbed page inside master/detail
									 masterController?.Detail?.Navigation ??
									 Application.Current.MainPage.Navigation;
			}
		}

		// View model to view lookup - making the assumption that view model to view will always be 1:1
		readonly Dictionary<Type, Type> _viewModelViewDictionary = new Dictionary<Type, Type>();

		#region Replace

		// Because we're going to do a hard switch of the page, either return
		// the detail page, or if that's null, then the current main page       
		Page DetailPage
		{
			get
			{
				var masterController = Application.Current.MainPage as MasterDetailPage;

				return masterController?.Detail ?? Application.Current.MainPage;
			}
			set
			{
				var masterController = Application.Current.MainPage as MasterDetailPage;

				if (masterController != null)
				{
					masterController.Detail = value;
					masterController.IsPresented = false;
				}
				else
				{
					Application.Current.MainPage = value;
				}
			}
		}

		public void SwitchDetailPage<T>(T viewModel) where T : class, IViewModel
		{
            
			var view = InstantiateViewForSwitchDetailPage(viewModel);

			Page newDetailPage;

			// Tab pages shouldn't go into navigation pages
			if (view is TabbedPage)
				newDetailPage = (Page)view;
			else
				newDetailPage = new NavigationPage((Page)view);

			DetailPage = newDetailPage;
		}

		public void SwitchDetailPage<T>(Action<T> initialize = null) where T : class, IViewModel
		{
			T viewModel;

			// First instantiate the view model
			viewModel = Activator.CreateInstance<T>();
			initialize?.Invoke(viewModel);

			// Actually switch the page
			SwitchDetailPage(viewModel);
		}

		#endregion

		#region Registration

		public void RegisterViewModels(System.Reflection.Assembly asm)
		{
			// Loop through everything in the assembley that implements IViewFor<T>
			foreach (var type in asm.DefinedTypes.Where(dt => !dt.IsAbstract &&
				dt.ImplementedInterfaces.Any(ii => ii == typeof(IViewFor))))
			{

				// Get the IViewFor<T> portion of the type that implements it
				var viewForType = type.ImplementedInterfaces.FirstOrDefault(
					ii => ii.IsConstructedGenericType &&
					ii.GetGenericTypeDefinition() == typeof(IViewFor<>));

				// Register it, using the T as the key and the view as the value
				Register(viewForType.GenericTypeArguments[0], type.AsType());
			}
		}

		public void Register(Type viewModelType, Type viewType)
		{
			_viewModelViewDictionary.Add(viewModelType, viewType);
		}

		#endregion

		#region Pop

		public async Task PopAsync<T>(Action<T> reInitialize = null) where T : class, IViewModel
		{
			await PopAsync();

			var topPage = FormsNavigation.NavigationStack.Last() as IViewFor<T>;

			if (topPage != null)
				reInitialize?.Invoke(topPage.ViewModel);
		}

		public async Task PopAsync()
		{
			await FormsNavigation.PopAsync(true);
		}

		public async Task PopModalAsync()
		{
			await FormsNavigation.PopModalAsync(true);
		}

		public async Task PopToRootAsync(bool animate)
		{
			await FormsNavigation.PopToRootAsync(animate);
		}

		public void PopTo<T>() where T : class, IViewModel
		{
			var pagesToRemove = new List<Page>();
			var upper = FormsNavigation.NavigationStack.Count;

			// Loop through the nav stack backwards
			for (int i = upper - 1; i >= 0; i--)
			{
				var currentPage = FormsNavigation.NavigationStack[i] as IViewFor;

				// Stop the whole show if one of the pages isn't an IViewFor
				if (currentPage == null)
					return;

                var strongTypedPaged = currentPage as IViewFor<T>;

				// If we hit the view model type, break out
				if (strongTypedPaged != null)
					break;

				// Finally - always add to the list
				pagesToRemove.Add(currentPage as Page);
			}

            foreach (var item in pagesToRemove)
            {
                FormsNavigation.RemovePage(item);
            }
        }

        #endregion

        #region Push

        public async Task PushAsync<T>(T viewModel) where T : class, IViewModel
		{
			var view = InstantiateView(viewModel);

			await FormsNavigation.PushAsync((Page)view);
		}

		public async Task PushModalAsync<T>(T viewModel) where T : class, IViewModel
		{
			var view = InstantiateView(viewModel);

			// Most likely we're going to want to put this into a navigation page so we can have a title bar on it
			var nv = new NavigationPage((Page)view);

			await FormsNavigation.PushModalAsync(nv);
		}

		public async Task PushAsync<T>(Action<T> initialize = null) where T : class, IViewModel
		{
			T viewModel;

			// Instantiate the view model & invoke the initialize method, if any
			viewModel = Activator.CreateInstance<T>();
			initialize?.Invoke(viewModel);

			await PushAsync(viewModel);
		}

		public async Task PushModalAsync<T>(Action<T> initialize = null) where T : class, IViewModel
		{
			T viewModel;

			// Instantiate the view model & invoke the initialize method, if any
			viewModel = Activator.CreateInstance<T>();
			initialize?.Invoke(viewModel);

			await PushModalAsync(viewModel);
		}

		#endregion

		IViewFor<T> InstantiateView<T>(T viewModel) where T : class, IViewModel
		{
			// Figure out what type the view model is
			var viewModelType = viewModel.GetType();

			// look up what type of view it corresponds to
			var viewType = _viewModelViewDictionary[viewModelType];

			// instantiate it
			var view = (IViewFor<T>)Activator.CreateInstance(viewType);

			view.ViewModel = viewModel;

			return view;
		}

        IViewFor InstantiateViewForSwitchDetailPage<T>(T viewModel) where T : class, IViewModel
        {
            var viewModelType = viewModel.GetType();

            var viewType = _viewModelViewDictionary[viewModelType];

            var view = (IViewFor)Activator.CreateInstance(viewType);

            var viewModelProperty = view.GetType().GetRuntimeProperty(nameof(IViewFor<T>.ViewModel));

            viewModelProperty?.SetValue(view, viewModel);

            return view;
        }
    }
}
