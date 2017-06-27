# CodeMill ViewModel First Navigation Library 
A Xamarin.Forms ViewModel First Navigation Library

### Build Status
![](https://ci.appveyor.com/api/projects/status/isojqjljf5v9xygj?svg=true)

### NuGet
[CodeMill.VMFirstNav](https://www.nuget.org/packages/CodeMill.VMFirstNav/)

## Installation
The installation is pretty straight forward - head on out to NuGet and search for [CodeMill.VMFirstNav](https://www.nuget.org/packages/CodeMill.VMFirstNav/). Or you could run `Install-Package CodeMill.VMFirstNav` from the package manager console.

At this time, this package will need to be installed into both the platform and core projects.

The library is .netstandard1.0 with a dependency on Xamarin.Forms 2.3.4.247 and above.

## The API
There are 3 main interfaces that are a part of this library.
- `IViewModel`
- `IViewFor<T>`
- `INavigationService`

`IViewModel` and `IViewFor<T>` are used to associate a view->view model pair.

`IViewModel` is nothing but a marker interface and should be put onto every view model class that is to participate in VM first navigation.

`IViewFor<T>` goes onto the `Pages` that are associated with the view model - the `T` will be the `IViewModel` it is associated with. There is one property that must be implemented: `T ViewModel { get; set; }`

In a class that implements `IViewFor<ChildViewModel>` I would recommend implementing that property as follows:
```
ChildViewModel vm;
public ChildViewModel ViewModel { 
    get => vm; 
    set
    {
        vm = value;
        BindingContext = vm;
    }
}
```

### Initialization
At runtime , the library will take care of finding the view -> view-model pairs when ```RegisterViewModels``` is called and it's passed the assembly of where the views and view models live. _(Currently they have to both be in the same assembly)._

You would do this in either the `AppDelegate` or `MainActivity` class:

```
NavigationService.Instance.RegisterViewModels(typeof(App).Assembly);
```

As you may have noted above - everything runs through a class called `NavigationService`. You don't instantiate it directly, because it needs to keep track of that internal view->view model dictionary, but rather accessed through a singleton called `Instance`. This way it'll stay in memory for the life of the app.

It conforms to another interface called `INavigationService` whose definition looks like the following:

```
public interface INavigationService
{
    void RegisterViewModels(System.Reflection.Assembly asm);
    void Register(Type viewModelType, Type viewType);

    Task PopAsync();
    Task PopAsync<T>(Action<T> reInitialize = null) where T : class, IViewModel;
    Task PopModalAsync();
    void PopTo<T>() where T : class, IViewModel;
    Task PopToRootAsync(bool animate);
    
    Task PushAsync<T>(T viewModel) where T : class, IViewModel;
    Task PushAsync<T>(Action<T> initialize = null) where T : class, IViewModel;
    Task PushModalAsync<T>(T viewModel) where T : class, IViewModel;
    Task PushModalAsync<T>(Action<T> initialize = null) where T : class, IViewModel;
       
    void SwitchDetailPage<T>(T viewModel) where T : class, IViewModel; 
    void SwitchDetailPage<T>(Action<T> initialize = null) where T : class, IViewModel;    
}
```
As I break these down one by one - when I refer to a view->view model combination being on the stack, I'm really only referring to the view (or `Page`) residing in the Xamarin.Forms `INavigation.NavigationStack` and then being able to find it's associated view model via the internal dictionary. So... instead of saying that... it's easier to refer to them as both being on the stack.

#### Registration
- `void RegisterViewModels(System.Reflection.Assembly asm)` - invoked in the `AppDelegate` or `MainActivity` to have the service enumerate all the view->view model pairs and register them in the internal dictionary.
- `void Register(Type viewModelType, Type viewType)` - used to register a single view->view model pair in the dictionary manually.

#### Popping
- `Task PopAsync()` - when used inside a view model, pops the associated view (or `Page`) off the navigation stack.
- `Task PopAsync<T>(Action<T> reInitialize = null) where T : class, IViewModel` - used to pop the current view->view model combination off the stack. If the next view model in the stack is of type `T` it will run whatever action passed in on that view model. In otherwise it will only perform a `PopAsync()`.
- `Task PopModalAsync()` - removes the current view->view model combination from the stack.
- `void PopTo<T>() where T : class, IViewModel` - pops everything off the stack until it finds a view->view model combination where the view model is of the type `T`. This is kind of hacky - as you may or may not see all of the previous pages being pulled from the stack.
- `Task PopToRootAsync(bool animate)` - removes everything from the navigation stack except for the root view->view model combination.

#### Pushing
- `Task PushAsync<T>(T viewModel) where T : class, IViewModel` - used to push a new view->view model combination onto the navigation stack. Here the view model has already been instantiated _before_ this function is called.
- `Task PushAsync<T>(Action<T> initialize = null) where T : class, IViewModel` - used to push a new view->view model combination onto the stack - and optionally invoke an action on the view model _before_ the combo is pushed. Here the view model is instantiated by the library.
- `Task PushModalAsync<T>(T viewModel) where T : class, IViewModel` - used to push a modal view->view model combination onto the stack when the instantiated view model already is in hand.
- `Task PushModalAsync<T>(Action<T> initialize = null) where T : class, IViewModel` - used to push a modal view->view model combination onto the stack and optionally invoke an action on the view model _before_ the combo is pushed. The view model is instantiated by the library.

#### Switching
Used with `MasterDetailPages` to switch the `MasterDetailPage.Detail` page or the `App.MainPage`.

- `void SwitchDetailPage<T>(T viewModel) where T : class, IViewModel` - used to switch the page when the view model is already instantiated.
- `void SwitchDetailPage<T>(Action<T> initialize = null) where T : class, IViewModel` - used to switch the page and optionally invoke a function on the view model _before_ the view->view model combination is make the active page.

##### A Word On Master/Detail Pages
The library also provides a class to help with `MasterDetailPages` when the master page is composed of a list with options that when selected open a new list. `MasterListItem<T>`. It contains one property, `DisplayName` that is meant for display in the `ListView`, and the `T` is the `IViewModel`.

The idea here is that when the item gets selected from the `ListView`, you'll have a `MasterListItem<T>` class. From that you can create extract the `T`, new up the view model, and then swap the `Detail` page.