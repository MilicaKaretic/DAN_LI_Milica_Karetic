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
    class AddDoctorViewModel : BaseViewModel
    {

        AddDoctor addDoctor;
        Service service = new Service();


        #region Constructor
        /// <summary>
        /// Constructor with the add doctor info window opening
        /// </summary>
        /// <param name="addDoctorOpen">opends the add doctor window</param>
        public AddDoctorViewModel(AddDoctor addDoctorOpen)
        {
            doctor = new tblDoctor();
            addDoctor = addDoctorOpen;
            DoctorList = service.GetAllDoctors().ToList();
        }
        #endregion

        #region Property
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

        /// <summary>
        /// Cheks if its possible to execute the add and edit commands
        /// </summary>
        private bool isUpdateDoctor;
        public bool IsUpdateDoctor
        {
            get
            {
                return isUpdateDoctor;
            }
            set
            {
                isUpdateDoctor = value;
            }
        }
        #endregion


        #region Commands
        /// <summary>
        /// Command that tries to save the new doctor
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
                if (service.AddDoctor(Doctor) != null)
                {
                    IsUpdateDoctor = true;

                    MessageBox.Show("Successfuly registered as doctor.");

                    addDoctor.Close();

                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the doctor
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Command that closes the add doctor or edit doctor window
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

                addDoctor.Close();

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
