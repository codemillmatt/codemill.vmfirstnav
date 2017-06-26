using System;

namespace CodeMill.VMFirstNav
{
    public interface IMasterListItem<out T> where T : class, IViewModel
    {
    }
}
