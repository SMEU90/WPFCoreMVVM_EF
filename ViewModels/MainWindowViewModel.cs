using WPFCoreMVVM_EF.Services.Interfaces;
using WPFCoreMVVM_EF.ViewModels.Base;
using WPFCoreMVVM_EF.Views.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands;
using System.Windows.Input;
using System.Windows;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.Models.Base;
using WPFCoreMVVM_EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WPFCoreMVVM_EF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            //_closeApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            //_openAddNewPersonalWnd = new LambdaCommand(OnOpenAddNewPersonalWndExecuted, CanOpenAddNewPersonalWndExecute);
           // OpenAddNewPersonalWnd = new LambdaCommand(OnOpenAddNewPersonalWndExecuted, CanOpenAddNewPersonalWndExecute);

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
       // private ICommand _closeApplicationCommand;
        public ICommand CloseApplicationCommand {
            get
            {
                return new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            }
        }
       // private ICommand _openAddNewPersonalWnd;
        public ICommand OpenAddNewPersonalWnd {
            get
            {
                return new LambdaCommand(OnOpenAddNewPersonalWndExecuted, CanOpenAddNewPersonalWndExecute);
            }
        }
        private bool CanOpenAddNewPersonalWndExecute(object p) => true;
        private void OnOpenAddNewPersonalWndExecuted(object p)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ApplyBlurEffect(this);
            AddPersonalWnd newPersonalWindow = new AddPersonalWnd();
            SetCenterPositionAndOpen(newPersonalWindow);
            allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
            allPersonal = ContextDB.GetContext().Personals.ToList();//получение всех сотрудников
            OnPropertyChanged("AllPersonalItemsSource");

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
        private List<Personal> allPersonal = ContextDB.GetContext().Personals.ToList();//получение всех сотрудников
        public List<Personal> AllPersonalItemsSource
        {
            get
            {
                return allPersonal;
            }
            private set
            {
                allPersonal = value;
                OnPropertyChanged("AllPersonalItemsSource");
            }
        }

        public ICommand EditObject
        {
            get
            {
                return new LambdaCommand(OnEditObjectExecuted, CanEditObjectExecute);
            }
        }
        private bool CanEditObjectExecute(object p) => true;
        private void OnEditObjectExecuted(object p)
        {
            AddObjectWnd newObjectWnd = new AddObjectWnd();
            SetCenterPositionAndOpen(newObjectWnd, new AddObjectViewModel(ObjectSelectedItem));
            allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
            allPersonal = ContextDB.GetContext().Personals.ToList();//получение всех сотрудников
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
            allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
            allPersonal = ContextDB.GetContext().Personals.ToList();//получение всех сотрудников
            OnPropertyChanged("AllPersonalItemsSource");
        }
        
        public ICommand EditPersonal//////////////////////////
        {
            get
            {
                return new LambdaCommand(OnEditPersonalExecuted, CanEditPersonalExecute);
            }
        }

        private bool CanEditPersonalExecute(object p) => true;
        private void OnEditPersonalExecuted(object p)
        {
            AddPersonalWnd newPersonalWindow = new AddPersonalWnd();
            SetCenterPositionAndOpen(newPersonalWindow, new AddPersonalViewModel(PersonalSelectedItem));
            allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
            allPersonal = ContextDB.GetContext().Personals.ToList();//получение всех сотрудников
            OnPropertyChanged("AllPersonalItemsSource");
        }
        public Position PositionSelectedItem { get; set; }
        public List<Position> AllPostitionItemsSource
        {
            get
            {
                return allPositions;
            }
            private set
            {
                allPositions = value;
                OnPropertyChanged("AllPostitionItemsSource");
            }
        }
        public ICommand EditPosition//////////////////////////
        {
            get
            {
                return new LambdaCommand(OnEditPositionExecuted, CanEditPositionExecute);
            }
        }

        private bool CanEditPositionExecute(object p) => true;
        private void OnEditPositionExecuted(object p)
        {
            AddPositionWnd newPositionWindow = new AddPositionWnd();
            SetCenterPositionAndOpen(newPositionWindow, new AddPositionViewModel(PositionSelectedItem.Name));
            allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
            OnPropertyChanged("AllPostitionItemsSource");
        }
        public ICommand DeletePosition
        {
            get
            {
                return new LambdaCommand(OnDeletePositionExecuted, CanDeletePositionExecute);
            }
        }

        private bool CanDeletePositionExecute(object p) => true;
        private void OnDeletePositionExecuted(object p)
        {
            ContextDB.GetContext().Positions.Remove(PositionSelectedItem);
            ContextDB.GetContext().SaveChanges();
            allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
            OnPropertyChanged("AllPostitionItemsSource");
        }
        public ICommand OpenAddNewPositionWnd
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            }
        }
        private bool CanOpenAddNewPositionWndExecute(object p) => true;
        private void OnOpenAddNewPositionWndExecuted(object p)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ApplyBlurEffect(this);
            AddPositionWnd newPositionWindow = new AddPositionWnd();
            SetCenterPositionAndOpen(newPositionWindow);
            allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
            OnPropertyChanged("AllPostitionItemsSource");

        }
        public Models.Object ObjectSelectedItem { get; set; }
        
        private List<Models.Type> allType = ContextDB.GetContext().Types.ToList();//получение всех типов объектов оборудования
        private List<Account> allAccount = ContextDB.GetContext().Accounts.ToList();//получение всех типов объектов оборудования
        private List<Models.Object> allObject = ContextDB.GetContext().Objects.ToList();//получение всех объектов оборудования
        public List<Models.Object> AllObjectItemsSource
        {
            get
            {
                return allObject;
            }
            private set
            {
                allObject = value;
                OnPropertyChanged("AllObjectItemsSource");
            }
        }
        public ICommand OpenAddNewObjectWnd
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewObjectWndExecuted, CanOpenAddNewObjectWndExecute);
            }
        }

        private bool CanOpenAddNewObjectWndExecute(object p) => true;
        private void OnOpenAddNewObjectWndExecuted(object p)
        {
            AddObjectWnd newObjectWnd = new AddObjectWnd();
            SetCenterPositionAndOpen(newObjectWnd);
            allType = ContextDB.GetContext().Types.ToList();//получение всех типов объектов оборудования
            allAccount = ContextDB.GetContext().Accounts.ToList();//получение всех ремонтов объектов оборудования
            allObject = ContextDB.GetContext().Objects.ToList();//получение всех объектов оборудования
            OnPropertyChanged("AllObjectItemsSource");
        }
        public ICommand DeleteObject
        {
            get
            {
                return new LambdaCommand(OnDeleteObjectExecuted, CanDeleteObjectExecute);
            }
        }

        private bool CanDeleteObjectExecute(object p) => true;
        private void OnDeleteObjectExecuted(object p)
        {
            ContextDB.GetContext().Objects.Remove(ObjectSelectedItem);
            ContextDB.GetContext().SaveChanges();
            allType = ContextDB.GetContext().Types.ToList();//получение всех типов объектов оборудования
            allAccount = ContextDB.GetContext().Accounts.ToList();//получение всех типов объектов оборудования
            allObject = ContextDB.GetContext().Objects.ToList();//получение всех объектов оборудования
            OnPropertyChanged("AllObjectItemsSource");
        }
        public Account AccountSelectedItem { get; set; }
        public List<Account> AllAccountItemsSource
        {
            get
            {
                return allAccount;
            }
            private set
            {
                allAccount = value;
                OnPropertyChanged("AllAccountItemsSource");
            }
        }
        public ICommand DeleteAccount
        {
            get
            {
                return new LambdaCommand(OnDeleteAccountExecuted, CanDeleteAccountExecute);
            }
        }

        private bool CanDeleteAccountExecute(object p) => true;
        private void OnDeleteAccountExecuted(object p)
        {
            ContextDB.GetContext().Accounts.Remove(AccountSelectedItem);
            ContextDB.GetContext().SaveChanges();
            allAccount = ContextDB.GetContext().Accounts.ToList();//получение всех типов объектов оборудования
            OnPropertyChanged("AllAccountItemsSource");
        }
        public ICommand EditAccount////////////////////////////////////////
        {
            get
            {
                return new LambdaCommand(OnEditAccountExecuted, CanEditAccountExecute);
            }
        }

        private bool CanEditAccountExecute(object p) => true;
        private void OnEditAccountExecuted(object p)
        {
            ContextDB.GetContext().Accounts.Remove(AccountSelectedItem);
            ContextDB.GetContext().SaveChanges();
            allAccount = ContextDB.GetContext().Accounts.ToList();//получение всех типов объектов оборудования
            OnPropertyChanged("AllAccountItemsSource");
        }
        public ICommand OpenAddNewTypeWnd
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewTypeWndExecuted, CanOpenAddNewTypeWndExecute);
            }
        }

        private bool CanOpenAddNewTypeWndExecute(object p) => true;
        private void OnOpenAddNewTypeWndExecuted(object p)
        {
            AddTypeWnd newTypeWnd = new AddTypeWnd();
            SetCenterPositionAndOpen(newTypeWnd);
            allType = ContextDB.GetContext().Types.ToList();//получение всех типов объектов оборудования
            OnPropertyChanged("AllTypeItemsSource");
        }
        public Models.Type TypeSelectedItem { get; set; }

        public List<Models.Type> AllTypeItemsSource
        {
            get
            {
                return allType;
            }
            private set
            {
                allType = value;
                OnPropertyChanged("AllTypeItemsSource");
            }
        }

        public ICommand DeleteType
        {
            get
            {
                return new LambdaCommand(OnDeleteTypeExecuted, CanDeleteTypeExecute);
            }
        }

        private bool CanDeleteTypeExecute(object p) => true;
        private void OnDeleteTypeExecuted(object p)
        {
            ContextDB.GetContext().Types.Remove(TypeSelectedItem);
            ContextDB.GetContext().SaveChanges();
            allType = ContextDB.GetContext().Types.ToList();//получение всех типов объектов оборудования
            OnPropertyChanged("AllTypeItemsSource");
        }
        public ICommand EditType
        {
            get
            {
                return new LambdaCommand(OnEditTypeExecuted, CanEditTypeExecute);
            }
        }

        private bool CanEditTypeExecute(object p) => true;
        private void OnEditTypeExecuted(object p)
        {
            AddTypeWnd newTypeWnd = new AddTypeWnd();
            SetCenterPositionAndOpen(newTypeWnd, new AddTypeViewModel(TypeSelectedItem.Name));
            allType = ContextDB.GetContext().Types.ToList();//получение всех типов объектов оборудования
            OnPropertyChanged("AllTypeItemsSource");
        }

    }
}
