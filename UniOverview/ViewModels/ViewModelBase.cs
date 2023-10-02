using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Interface;

namespace UniOverview.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged, IViewModelBase
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public virtual void Dispose() { }

		public virtual void OnLoad() { }
	}
}
