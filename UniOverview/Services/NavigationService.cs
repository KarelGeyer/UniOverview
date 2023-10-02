using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniOverview.ViewModels;

namespace UniOverview.Services
{
	public class NavigationService
	{
		public event Action CurrentViewModelChanged;
		private ViewModelBase ViewModel;
		private ViewModelBase HomeViewModel;
		private ViewModelBase MaterialsViewModel;
		private ViewModelBase SubjectsViewModel;
		private ViewModelBase SubjectDetailViewModel;
		private ViewModelBase AddNewSubjectViewModel;
		private ViewModelBase TeachersViewModel;

		private Dictionary<string, ViewModelBase> ViewModels;

		public NavigationService()
		{
			ViewModels = new Dictionary<string, ViewModelBase>();
		}

		public ViewModelBase CurrentViewModel
		{
			get => ViewModel;
			set
			{
				ViewModel = value;
				OnCurrentViewModelChanged();
			}
		}

		#region HOME
		public ViewModelBase NavHomeViewModel
		{
			get => HomeViewModel;
			set
			{
				HomeViewModel = value;
				ViewModels.Add("Home", value);
			}
		}
		#endregion

		#region MATERIALS
		public ViewModelBase NavMaterialsViewModel
		{
			get => MaterialsViewModel;
			set
			{
				MaterialsViewModel = value;
				ViewModels.Add("Materials", value);
			}
		}
		#endregion

		#region SUBJECTS
		public ViewModelBase NavSubjectsViewModel
		{
			get => SubjectsViewModel;
			set
			{
				SubjectsViewModel = value;
				ViewModels.Add("Subjects", value);
			}
		}

		public ViewModelBase NavSubjectDetailViewModel
		{
			get => SubjectDetailViewModel;
			set
			{
				SubjectDetailViewModel = value;
				ViewModels.Add("SubjectDetail", value);
			}
		}

		public ViewModelBase NavAddNewSubjectViewModel
		{
			get => AddNewSubjectViewModel;
			set
			{
				AddNewSubjectViewModel = value;
				ViewModels.Add("AddNewSubject", value);
			}
		}
		#endregion

		#region TEACHERS
		public ViewModelBase NavTeachersViewModel
		{
			get => TeachersViewModel;
			set
			{
				TeachersViewModel = value;
				ViewModels.Add("Teachers", value);
			}
		}
		#endregion

		public Dictionary<string, ViewModelBase> GetExistingViewModels
		{
			get => ViewModels;
		}

		private void OnCurrentViewModelChanged()
		{
			CurrentViewModelChanged?.Invoke();
		}
	}
}
