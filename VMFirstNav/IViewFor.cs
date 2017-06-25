using System;
namespace CodeMill.VMFirstNav
{
    // IViewFor should only be a marker interface
    public interface IViewFor { }

    public interface IViewFor<T> : IViewFor where T : IViewModel
    {
        T ViewModel { get; set; }
    }
}
