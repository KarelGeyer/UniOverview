using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UniOverview.Commands
{
	/// <summary>
	/// For command pattern implementation
	/// </summary>
	public abstract class CommandBase : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		public virtual bool CanExecute(object? parameter)
		{
			return true;
		}

		public abstract void Execute(object? parameter);

		protected void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
