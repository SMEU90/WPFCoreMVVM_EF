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
using WPFCoreMVVM_EF;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCoreMVVM_EF.ViewModels
{
    internal class AddPersonalViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;
        public AddPersonalViewModel()
        {
        }
        public AddPersonalViewModel(Personal personal)
        {
            NewPersonal = personal;
            PersonalPosition = NewPersonal.Position;
        }
        public AddPersonalViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
        public Personal NewPersonal { get; set; }
        public bool PersonalStatus { get; set; }
        public Position PersonalPosition { get; set; }

        private List<Position> allPositions = ContextDB.GetContext().Positions.ToList();//получение всех должностей
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
        }

        public ICommand AddNewPersonal
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPersonalExecuted, CanOpenAddNewPersonalExecute);
            }
        }
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
                                                            //el.Positions==PersonalPosition);
            if(!check)
            {
                Personal newPersonal = new Personal
                {
                    Age = NewPersonal.Age,
                    First_name = NewPersonal.First_name,
                    Middle_name = NewPersonal.Middle_name,
                    Surname = NewPersonal.Surname,
                    Position = PersonalPosition,
                };
                ContextDB.GetContext().Personals.Add(newPersonal);
                ContextDB.GetContext().SaveChanges();

            } else
            {
                MessageBox.Show("Данный сотрудник уже внесен в базу данных");
            }

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
            AddPositionWnd newPositionWindow = new AddPositionWnd();
            SetCenterPositionAndOpen(newPositionWindow);
            allPositions = ContextDB.GetContext().Positions.ToList();
            OnPropertyChanged("AllPositions");
        }
        

    }
}
