using DAN_LI_Milica_Karetic.Commands;
using DAN_LI_Milica_Karetic.Model;
using DAN_LI_Milica_Karetic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_LI_Milica_Karetic.ViewModel
{
    class LoginViewModel : BaseViewModel
    {

        Login view;
        Service service = new Service();

        #region Constructor
        public LoginViewModel(Login loginView)
        {
            view = loginView;
            user = new tblUser();
            UserList = service.GetAllUsers().ToList();
            DoctorList = service.GetAllDoctors().ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// Login info label
        /// </summary>
        private string labelInfo;
        public string LabelInfo
        {
            get
            {
                return labelInfo;
            }
            set
            {
                labelInfo = value;
                OnPropertyChanged("LabelInfo");
            }
        }

        /// <summary>
        /// Used for saving the current user
        /// </summary>
        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// List of all users in the application
        /// </summary>
        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

        /// <summary>
        /// Used for saving the current doctor
        /// </summary>
        private tblDoctor doctor;
        public tblDoctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        /// <summary>
        /// List of all doctors in the application
        /// </summary>
        private List<tblDoctor> doctorList;
        public List<tblDoctor> DoctorList
        {
            get
            {
                return doctorList;
            }
            set
            {
                doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to add a new patient
        /// </summary>
        private ICommand addNewPatient;
        public ICommand AddNewPatient
        {
            get
            {
                if (addNewPatient == null)
                {
                    addNewPatient = new RelayCommand(param => AddNewPatientExecute(), param => CanAddNewPatientExecute());
                }
                return addNewPatient;
            }
        }

        /// <summary>
        /// Executes the add patient command
        /// </summary>
        private void AddNewPatientExecute()
        {
            try
            {
                AddUser addPatient = new AddUser();
                addPatient.ShowDialog();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new patient
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewPatientExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to add a new doctor
        /// </summary>
        private ICommand addNewDoctor;
        public ICommand AddNewDoctor
        {
            get
            {
                if (addNewDoctor == null)
                {
                    addNewDoctor = new RelayCommand(param => AddNewDoctorExecute(), param => CanAddNewDoctorExecute());
                }
                return addNewDoctor;
            }
        }

        /// <summary>
        /// Executes the add Doctor command
        /// </summary>
        private void AddNewDoctorExecute()
        {
            try
            {
                AddDoctor addDoctor = new AddDoctor();
                addDoctor.ShowDialog();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new Doctor
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewDoctorExecute()
        {
            return true;
        }

        /// <summary>
        /// Command used to log the user into the application
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute);
                }
                return login;
            }
        }

        public object LoggedUser { get; private set; }
        public object LoggedDoctor { get; private set; }

        /// <summary>
        /// Login command execute and save logged in user
        /// </summary>
        /// <param name="obj"></param>
        private void LoginExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;
            bool found = false;
            string username = User.Username;

            for (int i = 0; i < UserList.Count; i++)
            {
                if (User.Username == UserList[i].Username && password == UserList[i].Password)
                {
                    LoggedInUser.CurrentUser = new tblUser
                    {
                        UserID = UserList[i].UserID,
                        FirstName = UserList[i].FirstName,
                        LastName = UserList[i].LastName,
                        JMBG = UserList[i].JMBG,
                        HealthInsuranceNumber = UserList[i].HealthInsuranceNumber,
                        Username = UserList[i].Username,
                        Password = UserList[i].Password,
                        DoctorID = UserList[i].DoctorID,
                        SickLeaveID = UserList[i].SickLeaveID
                    };
                    LabelInfo = "Logged in";
                    found = true;

                    if(DoctorList.Count == 0)
                    {
                        MessageBox.Show("There is no available doctors. Please try later.");
                    }
                    else if(LoggedInUser.CurrentUser.DoctorID == null)
                    {
                        ChooseDoctor chooseDoctor = new ChooseDoctor();
                        chooseDoctor.Show();
                        view.Close();
                    }
                    else
                    {
                        User users = new User();
                        view.Close();
                        users.Show();
                    }
                    
                    break;
                }
            }

            if(!found)
            {
                for (int i = 0; i < DoctorList.Count; i++)
                {
                    if (username == DoctorList[i].Username && password == DoctorList[i].Password)
                    {
                        LoggedInDoctor.CurrentDoctor = new tblDoctor
                        {
                            DoctorID = DoctorList[i].DoctorID,
                            FirstName = DoctorList[i].FirstName,
                            LastName = DoctorList[i].LastName,
                            JMBG = DoctorList[i].JMBG,
                            BankAccount = DoctorList[i].BankAccount,
                            Username = DoctorList[i].Username,
                            Password = DoctorList[i].Password
                        };
                        LabelInfo = "Logged in";
                        found = true;

                        Doctor doctors = new Doctor();
                        view.Close();
                        doctors.Show();
                        break;
                    }
                }
            }
           

            if (found == false)
            {
                LabelInfo = "Wrong Username or Password";
            }
        }
        #endregion
    }
}
