using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UniOverview.Enums.Subjects;
using UniOverview.Models.Subjects;

namespace UniOverview.Helpers
{
	public static class FormValidation
	{
		public static bool ValidateBasicString(string[] strings)
		{
			foreach (string s in strings)
				return !string.IsNullOrEmpty(s);
			return true;
		}

		public static bool ValidateBasicString(string value)
		{
			return !string.IsNullOrEmpty(value);
		}

		public static bool ValidateIntegersAndFloatsHigherThenZero(int[]? integers, float[]? floats)
		{
			if (integers == null && floats == null)
				throw new ArgumentNullException(nameof(floats) + nameof(floats), "Všechny parametry jsou prázdné");
			if (integers != null && integers.Length != 0)
			{
				foreach (int i in integers)
					return i > 0;
			}

			if (floats != null && floats.Length != 0)
			{
				foreach (float f in floats)
					return f > 0;
			}

			return true;
		}

		public static string ValidateEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
				throw new ArgumentNullException(nameof(email), "Prázdný email na vstupu.");

			email = email.Replace(" ", "").Trim();
			if (!Constants.EmailRegex.Match(email).Success)
				throw new ArgumentNullException(nameof(email), "Nevalidní email na vstupu.");

			return email;
		}

		public static CompletionDetails ValidateAddNewSubjectCompletionDetails(
			string subjectType,
			string subjectTypeOfCompletion,
			string subjectFormOfCompletion
		)
		{
			if (string.IsNullOrEmpty(subjectType) || string.IsNullOrEmpty(subjectTypeOfCompletion) || string.IsNullOrEmpty(subjectFormOfCompletion))
				throw new ArgumentNullException(nameof(subjectType), "Jeden ze vstupů je prázdný");

			SubjectType _subjectType;
			TypeOfCompletion _typeOfCompletion;
			FormOfCompletion _formOfCompletion;

			Enum.TryParse(subjectType, out _subjectType);
			Enum.TryParse(subjectTypeOfCompletion, out _typeOfCompletion);
			Enum.TryParse(subjectFormOfCompletion, out _formOfCompletion);

			return new CompletionDetails()
			{
				TypeOfSubject = _subjectType,
				SubjectTypeOfCompletion = _typeOfCompletion,
				SubjectFormOfCompletion = _formOfCompletion
			};
		}
	}
}
