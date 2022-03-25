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
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCoreMVVM_EF.ViewModels
{
    internal class AddACcountViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;
        public Models.Object ObjectAdd { get; set; }
        public Account Account { get; set; }
        public AddACcountViewModel()
        {

        }
        public AddACcountViewModel(Models.Account account)
        {
            Account = account;
        }
        public AddACcountViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }


        private List<Models.Object> allObject = ContextDB.GetContext().Objects.ToList();//получение всех объектов оборудования
        public List<Models.Object> AllObject
        {
            get
            {
                return allObject;
            }
            private set
            {
                allObject = value;
                OnPropertyChanged("AllObject");
            }
        }

        public ICommand AddNewAccount
        {
            get
            {
                return new LambdaCommand(OnAddNewAccountExecuted, CanAddNewAccountExecute);
            }
        }
        private bool CanAddNewAccountExecute(object p) => true;
        private void OnAddNewAccountExecuted(object p)
        {
            /*MessageBox.Show(PersonalPosition.Id.ToString());
            MessageBox.Show(PersonalPosition.Name);
            MessageBox.Show(PersonalPosition.ToString());*/

          /*  bool check = ContextDB.GetContext().Objects.Any(el => el.Failure_time == NewObject.Failure_time &&
                                                                  el.Real_working_hours == NewObject.Real_working_hours &&
                                                                  el.Tech_working_hours == NewObject.Tech_working_hours &&
                                                                  el.Humidity == NewObject.Humidity &&
                                                                  el.Tech_humidity == NewObject.Tech_humidity &&
                                                                  el.Airiness == NewObject.Airiness &&
                                                                  el.Heat == NewObject.Heat &&
                                                                  el.Tech_heat == NewObject.Tech_heat &&
                                                                  el.All_working_hours == NewObject.All_working_hours &&
                                                                  el.Name == NewObject.Name &&
                                                                  el.Status == NewObject.Status &&
                                                                  el.Type == ObjectType);
            if (!check)
            {
                Models.Object obj = new Models.Object
                {
                    Failure_time = NewObject.Failure_time,
                    Real_working_hours = NewObject.Real_working_hours,
                    Tech_working_hours = NewObject.Tech_working_hours,
                    Humidity = NewObject.Humidity,
                    Tech_humidity = NewObject.Tech_humidity,
                    Airiness = NewObject.Airiness,
                    Heat = NewObject.Heat,
                    Tech_heat = NewObject.Tech_heat,
                    All_working_hours = NewObject.All_working_hours,
                    Note = NewObject.Note,
                    Name = NewObject.Name,
                    Status = NewObject.Status,
                    Type = ObjectType,
                    ////////////////////////////расчет Критерия надежности!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    Reliability = 1,

                };
                ContextDB.GetContext().Objects.Add(obj);
                ContextDB.GetContext().SaveChanges();
            }
            else
            {
                MessageBox.Show("Данное оборудование уже внесено в базу данных");
            }*/

        }
        public ICommand OpenAddNewObjectWnd
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            }
        }
        private bool CanOpenAddNewPositionWndExecute(object p) => true;
        private void OnOpenAddNewPositionWndExecuted(object p)
        {
            AddObjectWnd newObjectWindow = new AddObjectWnd();
            SetCenterPositionAndOpen(newObjectWindow);
            allObject = ContextDB.GetContext().Objects.ToList();//получение всех объектов оборудования
            OnPropertyChanged("AllObject");
        }
    }
}
