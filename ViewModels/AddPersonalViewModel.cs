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
    internal class AddPersonalViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;
        public AddPersonalViewModel()
        {

        }
        public AddPersonalViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
        public int PersonalAge { get; set; }
        public string PersonalFirstName { get; set; }
        public string PersonalMiddleName { get; set; }
        public string PersonalSurnameName { get; set; }
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

            bool check = ContextDB.GetContext().Personals.Any(el => el.First_name == PersonalFirstName &&
                                                            el.Middle_name == PersonalMiddleName &&
                                                            el.Surname == PersonalSurnameName &&
                                                            el.Age == PersonalAge); //&&
                                                            //el.Positions==PersonalPosition);
            if(!check)
            {
                Personal newPersonal = new Personal
                {
                    Age = PersonalAge,
                    First_name = PersonalFirstName,
                    Middle_name = PersonalMiddleName,
                    Surname = PersonalSurnameName,
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
        }
        /*public static string CreateUser(string name, string surName, string phone, Position position)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Users.Any(el => el.Name == name && el.SurName == surName && el.Position == position);
                if (!checkIsExist)
                {
                    User newUser = new User
                    {
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        private RelayCommand addNewPersonal;
        public RelayCommand AddNewPersonal
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (UserName == null || UserName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (UserSurName == null || UserSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "SurNameBlock");
                    }
                    //if (UserPhone == null || UserPhone.Replace(" ", "").Length == 0)
                    //{
                    //    SetRedBlockControll(wnd, "SurNameBlock");
                    //}
                    if (UserPosition == null)
                    {
                        MessageBox.Show("Укажите позицию");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }*/

    }
}
