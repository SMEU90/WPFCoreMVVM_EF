using WPFCoreMVVM_EF.Services.Interfaces;
using WPFCoreMVVM_EF.ViewModels.Base;
using WPFCoreMVVM_EF.Views.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands;
using System.Windows.Input;
using System.Windows;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WPFCoreMVVM_EF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
           // CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        }

        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов!";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion
        public ICommand CloseApplicationCommand {
            get
            {
                return new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            }
        }
        public ICommand OpenAddNewPersonalWnd { 
            get
            {
                return new LambdaCommand(OnOpenAddNewPersonalWndExecuted, CanOpenAddNewPersonalWndExecute);
            }
        }
        private bool CanOpenAddNewPersonalWndExecute(object p) => true;
        private void OnOpenAddNewPersonalWndExecuted(object p)
        {
            AddPersonalWnd newPersonalWindow = new AddPersonalWnd();
            SetCenterPositionAndOpen(newPersonalWindow);
        }

        

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();//Current текущее приложение
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        public MainWindowViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
        private List<Position> allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
        private List<Personal> AllPersonal = ContextDB.GetContext().Personals.ToList();//получение всех сотрудников
        public List<Personal> AllPersonalItemsSource
        {
            get
            {
                return AllPersonal;
            }
            private set
            {
                AllPersonal = value;
                OnPropertyChanged("AllPersonalItemsSource");
            }
        }

        public ICommand UpdateDataGridPersonal
        {
            get
            {
                return new LambdaCommand(OnUpdateDataGridPersonalExecuted, CanUpdateDataGridPersonalExecute);
            }
        }
        private bool CanUpdateDataGridPersonalExecute(object p) => true;
        private void OnUpdateDataGridPersonalExecuted(object p)
        {
            allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
            AllPersonal = ContextDB.GetContext().Personals.ToList();//получение всех сотрудников
            OnPropertyChanged("AllPersonalItemsSource");
        }
        public Personal PersonalSelectedItem { get; set; }
        public ICommand DeletePersonal
        {
            get
            {
                return new LambdaCommand(OnDeletePersonalExecuted, CanDeletePersonalExecute);
            }
        }

        private bool CanDeletePersonalExecute(object p) => true;
        private void OnDeletePersonalExecuted(object p)
        {
            ContextDB.GetContext().Personals.Remove(PersonalSelectedItem);
            ContextDB.GetContext().SaveChanges();
        }
        
        public ICommand UpdatePersonal
        {
            get
            {
                return new LambdaCommand(OnUpdatePersonalExecuted, CanUpdatePersonalExecute);
            }
        }

        private bool CanUpdatePersonalExecute(object p) => true;
        private void OnUpdatePersonalExecuted(object p)
        {
            ContextDB.GetContext().Personals.Remove(PersonalSelectedItem);
            ContextDB.GetContext().SaveChanges();
        }
    }
}
