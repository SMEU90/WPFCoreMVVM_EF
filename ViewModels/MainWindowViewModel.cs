using WPFCoreMVVM_EF.Services.Interfaces;
using WPFCoreMVVM_EF.ViewModels.Base;
using WPFCoreMVVM_EF.Views.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands;
using System.Windows.Input;
using System.Windows;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.Models.Base;
using WPFCoreMVVM_EF.Infrastructure.Commands.Base;
using WPFCoreMVVM_EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace WPFCoreMVVM_EF.ViewModels//add validation
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
        public MainWindowViewModel()
        {
            #region Инициализация команд
            OpenAddNewPersonalWnd = new LambdaCommand(OnOpenAddNewPersonalWndExecuted, CanOpenAddNewPersonalWndExecute);
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            DeletePersonal = new LambdaCommand(OnDeletePersonalExecuted, CanDeletePersonalExecute);
            EditPersonal = new LambdaCommand(OnEditPersonalExecuted, CanEditPersonalExecute); 
            OpenAddNewObjectWnd = new LambdaCommand(OnOpenAddNewObjectWndExecuted, CanOpenAddNewObjectWndExecute);
            DeleteObject = new LambdaCommand(OnDeleteObjectExecuted, CanDeleteObjectExecute);
            EditObject = new LambdaCommand(OnEditObjectExecuted, CanEditObjectExecute);
            EditPosition = new LambdaCommand(OnEditPositionExecuted, CanEditPositionExecute);
            DeletePosition = new LambdaCommand(OnDeletePositionExecuted, CanDeletePositionExecute);
            OpenAddNewPositionWnd = new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            DeleteAccount = new LambdaCommand(OnDeleteAccountExecuted, CanDeleteAccountExecute);
            EditAccount = new LambdaCommand(OnEditAccountExecuted, CanEditAccountExecute);
            //OpenAddNewAccountnWnd
            OpenAddNewTypeWnd = new LambdaCommand(OnOpenAddNewTypeWndExecuted, CanOpenAddNewTypeWndExecute);
            DeleteType = new LambdaCommand(OnDeleteTypeExecuted, CanDeleteTypeExecute);
            EditType = new LambdaCommand(OnEditTypeExecuted, CanEditTypeExecute);
            #endregion
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

        #region Command : CloseApplicationCommand - закрытие окна
        public Command CloseApplicationCommand { get; }
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();//Current текущее приложение
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion

        #region Персонал

        #region Свойства привязки к DataGrid
        public Personal PersonalSelectedItem { get; set; }
        public ObservableCollection<Personal> AllPersonalItemsSource
        {
            get
            {
            return StaticObservableCollections.allPersonal;
            }
            set
            {
                StaticObservableCollections.allPersonal = value;
            }
        }
        

        #endregion

        #region Command : OpenAddNewPersonalWnd - открытие окна добавления персонала
        public Command OpenAddNewPersonalWnd { get; }
        private bool CanOpenAddNewPersonalWndExecute(object p) => true;
        private void OnOpenAddNewPersonalWndExecuted(object p)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ApplyBlurEffect(this);
            AddPersonalWnd newPersonalWindow = new AddPersonalWnd();
            SetCenterPositionAndOpen(newPersonalWindow);
        }
        #endregion

        #region Command : DeletePersonal  - удаление персонала
        public Command DeletePersonal { get; }
        private bool CanDeletePersonalExecute(object p) => true;
        private void OnDeletePersonalExecuted(object p)
        {
            ContextDB.GetContext().Personals.Remove(PersonalSelectedItem);
            ContextDB.GetContext().SaveChanges();
            StaticObservableCollections.allPersonal.Remove(PersonalSelectedItem);
        }
        #endregion

        #region Command : EditPersonal  - редактирование персонала
        public Command EditPersonal { get; }
        private bool CanEditPersonalExecute(object p) => true;
        private void OnEditPersonalExecuted(object p)
        {
            AddPersonalWnd newPersonalWindow = new AddPersonalWnd();
            SetCenterPositionAndOpen(newPersonalWindow, new AddPersonalViewModel(PersonalSelectedItem));
        }
        #endregion

        #endregion

        #region Объект оборудования

        #region Свойства привязки к DataGrid

        public Models.Object ObjectSelectedItem { get; set; }
        public ObservableCollection<Models.Object> AllObjectItemsSource
        {
            get
            {
                return StaticObservableCollections.allObject;
            }
            private set
            {
                StaticObservableCollections.allObject = value;
            }
        }

        #endregion

        #region Command : OpenAddNewObjectWnd  - открытие окна добавления объекта оборудования
        public Command OpenAddNewObjectWnd { get; }
        private bool CanOpenAddNewObjectWndExecute(object p) => true;
        private void OnOpenAddNewObjectWndExecuted(object p)
        {
            AddObjectWnd newObjectWnd = new AddObjectWnd();
            SetCenterPositionAndOpen(newObjectWnd);
        }
        #endregion

        #region Command : DeleteObject  - удаление объекта оборудования
        public Command DeleteObject { get; }
        private bool CanDeleteObjectExecute(object p) => true;
        private void OnDeleteObjectExecuted(object p)
        {
            ContextDB.GetContext().Objects.Remove(ObjectSelectedItem);
            ContextDB.GetContext().SaveChanges();
            StaticObservableCollections.allObject.Remove(ObjectSelectedItem);
        }
        #endregion

        #region Command : EditObject  - редактирование объекта оборудования
        public Command EditObject { get; }
        private bool CanEditObjectExecute(object p) => true;
        private void OnEditObjectExecuted(object p)
        {
            AddObjectWnd newObjectWnd = new AddObjectWnd();
            SetCenterPositionAndOpen(newObjectWnd, new AddObjectViewModel(ObjectSelectedItem));
        }
        #endregion

        #endregion

        #region Должность

        #region Свойства привязки к DataGrid

        public Position PositionSelectedItem { get; set; }
        public ObservableCollection<Position> AllPostitionItemsSource
        {
            get
            {
                return StaticObservableCollections.allPosition;
            }
            private set
            {
                StaticObservableCollections.allPosition = value;
            }
        }

        #endregion

        #region Command : OpenAddNewPositionWnd  - открытие окна добавления должности
        public Command OpenAddNewPositionWnd { get; }
        private bool CanOpenAddNewPositionWndExecute(object p) => true;
        private void OnOpenAddNewPositionWndExecuted(object p)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ApplyBlurEffect(this);
            AddPositionWnd newPositionWindow = new AddPositionWnd();
            SetCenterPositionAndOpen(newPositionWindow);
        }
        #endregion

        #region Command : DeletePosition  - удаление должности
        public Command DeletePosition { get; }
        private bool CanDeletePositionExecute(object p) => true;
        private void OnDeletePositionExecuted(object p)
        {
            ContextDB.GetContext().Positions.Remove(PositionSelectedItem);
            ContextDB.GetContext().SaveChanges();
            StaticObservableCollections.allPosition.Remove(PositionSelectedItem);
        }
        #endregion

        #region Command : EditPosition  - редактирование должности
        public Command EditPosition { get; }
        private bool CanEditPositionExecute(object p) => true;
        private void OnEditPositionExecuted(object p)
        {
            AddPositionWnd newPositionWindow = new AddPositionWnd();
            SetCenterPositionAndOpen(newPositionWindow, new AddPositionViewModel(PositionSelectedItem));
        }
        #endregion

        #endregion

        #region Заявка

        #region Свойства привязки к DataGrid
        public Account AccountSelectedItem { get; set; }
        public ObservableCollection<Account> AllAccountItemsSource
        {
            get
            {
                return StaticObservableCollections.allAccount;
            }
            private set
            {
                StaticObservableCollections.allAccount = value;
            }
        }

        #endregion

        #region Command : OpenAddNewAccountnWnd  - открытие окна создания заявки
        public Command OpenAddNewAccountnWnd { get; }

        #endregion

        #region Command : DeleteAccount  - удаление заявки
        public Command DeleteAccount { get; }
        private bool CanDeleteAccountExecute(object p) => true;
        private void OnDeleteAccountExecuted(object p)
        {
            ContextDB.GetContext().Accounts.Remove(AccountSelectedItem);
            ContextDB.GetContext().SaveChanges();
            StaticObservableCollections.allAccount.Remove(AccountSelectedItem);
        }

        #endregion

        #region Command : EditAccount  - редактироване заявки
        public Command EditAccount { get; }
        private bool CanEditAccountExecute(object p) => true;
        private void OnEditAccountExecuted(object p)
        {
            ContextDB.GetContext().Accounts.Remove(AccountSelectedItem);
            ContextDB.GetContext().SaveChanges();
        }
        #endregion

        #endregion

        #region Типы оборудования

        #region Свойства привязки к DataGrid
        public Models.Type TypeSelectedItem { get; set; }
        public ObservableCollection<Models.Type> AllTypeItemsSource
        {
            get
            {
                return StaticObservableCollections.allType;
            }
            private set
            {
                StaticObservableCollections.allType = value;
            }
        }

        #endregion

        #region Command : OpenAddNewTypeWnd  - открытие окна добавления вида/типа оборудования
        public Command OpenAddNewTypeWnd { get; }
        private bool CanOpenAddNewTypeWndExecute(object p) => true;
        private void OnOpenAddNewTypeWndExecuted(object p)
        {
            AddTypeWnd newTypeWnd = new AddTypeWnd();
            SetCenterPositionAndOpen(newTypeWnd);
        }
        #endregion

        #region Command : DeleteType  - удаление вида
        public Command DeleteType { get; }
        private bool CanDeleteTypeExecute(object p) => true;
        private void OnDeleteTypeExecuted(object p)
        {
            ContextDB.GetContext().Types.Remove(TypeSelectedItem);
            ContextDB.GetContext().SaveChanges();
            StaticObservableCollections.allType.Remove(TypeSelectedItem);
        }

        #endregion

        #region Command : EditType  - редактироване вида
        public Command EditType { get; }
        private bool CanEditTypeExecute(object p) => true;
        private void OnEditTypeExecuted(object p)
        {
            AddTypeWnd newTypeWnd = new AddTypeWnd();
            SetCenterPositionAndOpen(newTypeWnd, new AddTypeViewModel(TypeSelectedItem));
        }
        #endregion

        #endregion


    }
}
