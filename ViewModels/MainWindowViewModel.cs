using WPFCoreMVVM_EF.Services.Interfaces;
using WPFCoreMVVM_EF.ViewModels.Base;
using WPFCoreMVVM_EF.Views.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands;
using System.Windows.Input;
using System.Windows;
using System;


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
        /*private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }*/
        #region
        /*private RelayCommand openAddNewUserWnd;
        public RelayCommand OpenAddNewUserWnd
        {
            get
            {
                return openAddNewUserWnd ?? new RelayCommand(obj =>
                {
                    OpenAddUserWindowMethod();
                }
                );
            }
        }*/
        #endregion
    }
}
