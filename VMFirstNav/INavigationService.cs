using System;
using System.Threading.Tasks;

namespace CodeMill.VMFirstNav
{
    public interface INavigationService
    {
        void RegisterViewModels(System.Reflection.Assembly asm);
        void Register(Type viewModelType, Type viewType);

        Task PopAsync();
        Task PopModalAsync();
        void PopTo<T>() where T : class, IViewModel;
        Task PopAsync<T>(Action<T> reInitialize = null) where T : class, IViewModel;
        Task PushAsync<T>(T viewModel) where T : class, IViewModel;
        Task PushAsync<T>(Action<T> initialize = null) where T : class, IViewModel;
        Task PushModalAsync<T>(Action<T> initialize = null) where T : class, IViewModel;
        Task PushModalAsync<T>(T viewModel) where T : class, IViewModel;
        Task PopToRootAsync(bool animate);
        void SwitchDetailPage<T>(Action<T> initialize = null) where T : class, IViewModel;
        void SwitchDetailPage<T>(T viewModel) where T : class, IViewModel;
    }

}
