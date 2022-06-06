using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCoreMVVM_EF.ViewModels.Base;
using WPFCoreMVVM_EF.Views.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands;
using WPFCoreMVVM_EF.Services.Interfaces;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.Models.Base;
using System.Windows.Input;
using System.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands.Base;
using WPFCoreMVVM_EF;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace WPFCoreMVVM_EF.ViewModels//add validation
{
    internal class AddPersonalViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;
        public string AddButtonText { get; set; }
        public AddPersonalViewModel()
        {
            _Title = "Добавление сотрудника";
            AddButtonText = "Добавить сотрудника";
            AddNewPersonal = new LambdaCommand(OnOpenAddNewPersonalExecuted, CanOpenAddNewPersonalExecute);
            OpenAddNewPositionWnd = new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            NewPersonal = new Personal();

        }
        public AddPersonalViewModel(Personal personal)
        {
            _Title = "Редактирование сотрудника";
            AddButtonText = "Изменить сотрудника";
            NewPersonal = new Personal();
            NewPersonal.Age=personal.Age;
            NewPersonal.Status=personal.Status;
            NewPersonal.Surname=personal.Surname;
            NewPersonal.Middle_name=personal.Middle_name;
            NewPersonal.First_name=personal.First_name;
            NewPersonal.Id=personal.Id;
            NewPersonal.PositionId=personal.PositionId;
            NewPersonal.Position = personal.Position;
            OldPersonal = personal;
            _isNewPersonal = false;
            AddNewPersonal = new LambdaCommand(OnOpenAddNewPersonalExecuted, CanOpenAddNewPersonalExecute);
            OpenAddNewPositionWnd = new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
        }
        public AddPersonalViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
        public Personal NewPersonal { get; set; }
        private Personal OldPersonal { get; set; }
        private bool _isNewPersonal=true;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion
        public ObservableCollection<Position> AllPosition
        {
            get
            {
                return StaticObservableCollections.allPosition;
            }
            set
            {
                StaticObservableCollections.allPosition = value;
            }
        }
        public Command AddNewPersonal { get; }
        private bool CanOpenAddNewPersonalExecute(object p) => true;
        private void OnOpenAddNewPersonalExecuted(object p)
        {
            bool check = ContextDB.GetContext().Personals.Any(el => el.First_name == NewPersonal.First_name &&
                                                            el.Middle_name == NewPersonal.Middle_name &&
                                                            el.Surname == NewPersonal.Surname &&
                                                            el.Age == NewPersonal.Age); //&&
                                                            //el.Positions==PersonalPosition);///////////////////////////////////////////////////////////////////////
            if(!check)
            {
                Personal newPersonal = new Personal
                {
                    Age = NewPersonal.Age,
                    First_name = NewPersonal.First_name,
                    Middle_name = NewPersonal.Middle_name,
                    Surname = NewPersonal.Surname,
                    Position = NewPersonal.Position,
                };
                if (_isNewPersonal)
                {
                    StaticObservableCollections.allPersonal.Add(newPersonal);
                    ContextDB.GetContext().Personals.Add(newPersonal);
                    ContextDB.GetContext().SaveChanges();
                } else
                {
                    ContextDB.GetContext().Personals.Remove(OldPersonal);
                    StaticObservableCollections.allPersonal.Remove(OldPersonal);
                    StaticObservableCollections.allPersonal.Add(newPersonal);
                    ContextDB.GetContext().Personals.Add(newPersonal);
                    ContextDB.GetContext().SaveChanges();
                    OldPersonal = newPersonal;
                }
            } else
            {
                MessageBox.Show("Данный сотрудник уже внесен в базу данных");
            }

        }
        public Command OpenAddNewPositionWnd { get; }
        private bool CanOpenAddNewPositionWndExecute(object p) => true;
        private void OnOpenAddNewPositionWndExecuted(object p)
        {
            AddPositionWnd newPositionWindow = new AddPositionWnd();
            SetCenterPositionAndOpen(newPositionWindow);
        }
        

    }
}
