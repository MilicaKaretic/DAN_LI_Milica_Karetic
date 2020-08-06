using DAN_LI_Milica_Karetic.Commands;
using DAN_LI_Milica_Karetic.Model;
using DAN_LI_Milica_Karetic.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_LI_Milica_Karetic.ViewModel
{
    class AddUserViewModel : BaseViewModel
    {

        AddUser addUser;
        Service service = new Service();


        #region Constructor
        /// <summary>
        /// Constructor with the add user info window opening
        /// </summary>
        /// <param name="addUserOpen">opends the add user window</param>
        public AddUserViewModel(AddUser addUserOpen)
        {
            user = new tblUser();
            addUser = addUserOpen;
            UserList = service.GetAllUsers().ToList();
        }
        #endregion

        #region Property
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
        /// Cheks if its possible to execute the add and edit commands
        /// </summary>
        private bool isUpdateUser;
        public bool IsUpdateUser
        {
            get
            {
                return isUpdateUser;
            }
            set
            {
                isUpdateUser = value;
            }
        }
        #endregion


        #region Commands
        /// <summary>
        /// Command that tries to save the new user
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => this.CanSaveExecute);
                }
                return save;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                if (service.AddUser(User) != null)
                {
                    IsUpdateUser = true;
                    MessageBox.Show("Successfuly registered as patient.");
                    addUser.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the user
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Command that closes the add user or edit user window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {

                addUser.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
