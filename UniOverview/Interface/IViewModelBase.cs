using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniOverview.Interface
{
	public interface IViewModelBase
	{
		public void Dispose();
		public void OnPropertyChanged(string propertyName);
		public void OnLoad();
	}
}
