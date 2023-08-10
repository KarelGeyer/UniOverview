using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums.Subjects;

namespace UniOverview.Commands
{
	public class BaseButtonCommand : CommandBase
	{
		private Action<object?>? MethodWithParamToExecute { get; set; }
		private Action? MethodWithoutParamToExecute { get; set; }

		public BaseButtonCommand(Action<object?>? methodWithParam = null, Action? methodWithoutParam = null)
		{
			MethodWithParamToExecute = methodWithParam;
			MethodWithoutParamToExecute = methodWithoutParam;
		}

		public override void Execute(object? parameter)
		{
			if (parameter is not null && MethodWithParamToExecute is not null)
			{
				MethodWithParamToExecute(parameter);
			}

			if (parameter is null && MethodWithoutParamToExecute is not null)
			{
				MethodWithoutParamToExecute();
			}
		}
	}
}
