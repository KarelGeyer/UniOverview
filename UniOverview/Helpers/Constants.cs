using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UniOverview.Helpers
{
	public static class Constants
	{
		public static Regex EmailRegex = new Regex(
			"^[-a-zA-Z0-9~!$%^&*_=+}{\\'?]+(\\.[-a-zA-Z0-9~!$%^&*_=+}{\\'?]+)*@([a-zA-Z0-9_][-a-zA-Z0-9_]*(\\.[-a-zA-Z0-9_]+)*\\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-zA-Z]+)|([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}))(:[0-9]{1,5})?$"
		);

		public static Regex PhoneRegex = new Regex("^(\\+420|00420) ?[6-7][0-9][0-9] ?([0-9][0-9][0-9] ?[0-9][0-9][0-9]|[0-9][0-9] ?[0-9][0-9] ?[0-9][0-9])$");

		public static Regex PasswordRegex = new Regex("^(?!.* )(?=.*\\d)(?=.*[a-zA-Z]).{8,}$");
	}
}
