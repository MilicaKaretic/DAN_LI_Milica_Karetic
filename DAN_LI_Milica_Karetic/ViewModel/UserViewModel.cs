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
    class UserViewModel : BaseViewModel
    {
        User userView;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with the add user info window opening
        /// </summary>
        /// <param name="addUserOpen">opends the add user window</param>
        public UserViewModel(User addUserOpen)
        {
            sickLeave = new tblSickLeave();
            userView = addUserOpen;
            SickLeaveList = service.GetAllSickLeaves().ToList();
        }
        #endregion



        #region Property
        private tblSickLeave sickLeave;
        public tblSickLeave SickLeave
        {
            get
            {
                return sickLeave;
            }
            set
            {
                sickLeave = value;
                OnPropertyChanged("SickLeave");
            }
        }

        private List<tblSickLeave> sickLeaveList;
        public List<tblSickLeave> SickLeaveList
        {
            get
            {
                return sickLeaveList;
            }
            set
            {
                sickLeaveList = value;
                OnPropertyChanged("SickLeaveList");
            }
        }

       
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new employee
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
                AddUser adminView = new AddUser();
                if (service.AddSickLeave(SickLeave) != null)
                {

                    MessageBox.Show("Added sick leave. Click to ok to exit.");
                    Login log = new Login();
                    userView.Close();
                    log.Show();
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the employee
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
                Login log = new Login();
                userView.Close();
                log.Show();
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
