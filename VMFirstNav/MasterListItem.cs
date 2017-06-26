using System;
namespace CodeMill.VMFirstNav
{
	public class MasterListItem<T> : IMasterListItem<T> where T : class, IViewModel
	{
		public string DisplayName { get; set; }

		public MasterListItem(string displayName)
		{
			DisplayName = displayName;
		}
	}
}
