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

namespace WPFCoreMVVM_EF.ViewModels
{
    internal class AddPersonalViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;
        public string AddButtonText { get; set; }
        public AddPersonalViewModel()
        {
            AddButtonText = "Добавить сотрудника";
            AddNewPersonal = new LambdaCommand(OnOpenAddNewPersonalExecuted, CanOpenAddNewPersonalExecute);
            OpenAddNewPositionWnd = new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            NewPersonal = new Personal();

        }
        public AddPersonalViewModel(Personal personal)
        {
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
            //NewPersonal = personal;
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
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public ObservableCollection<Position> AllPositions
        {
            get
            {
                return StaticObservableCollections.allPositions;
            }
            set
            {
                StaticObservableCollections.allPositions = value;
                //OnPropertyChanged("AllPositions");
            }
        }
        /*private List<Position> allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
        public List<Position> AllPositions
        {
            get
            {
                return allPositions;
            }
            private set
            {
                allPositions = value;
                OnPropertyChanged("AllPositions");
            }
        }*/
        public Command AddNewPersonal { get; }
        /*public ICommand AddNewPersonal
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPersonalExecuted, CanOpenAddNewPersonalExecute);
            }
        }*/
        private bool CanOpenAddNewPersonalExecute(object p) => true;
        private void OnOpenAddNewPersonalExecuted(object p)
        {
            /*MessageBox.Show(PersonalPosition.Id.ToString());
            MessageBox.Show(PersonalPosition.Name);
            MessageBox.Show(PersonalPosition.ToString());*/

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
        /*public ICommand OpenAddNewPositionWnd
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            }
        }*/
        private bool CanOpenAddNewPositionWndExecute(object p) => true;
        private void OnOpenAddNewPositionWndExecuted(object p)
        {
            AddPositionWnd newPositionWindow = new AddPositionWnd();
            SetCenterPositionAndOpen(newPositionWindow);
            /*allPositions = ContextDB.GetContext().Positions.ToList();
            OnPropertyChanged("AllPositions");*/
        }
        

    }
}
