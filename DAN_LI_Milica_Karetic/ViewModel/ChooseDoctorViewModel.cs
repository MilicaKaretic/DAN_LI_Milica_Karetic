using DAN_LI_Milica_Karetic.Commands;
using DAN_LI_Milica_Karetic.Model;
using DAN_LI_Milica_Karetic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_LI_Milica_Karetic.ViewModel
{
    class ChooseDoctorViewModel : BaseViewModel
    {
        Service service = new Service();
        ChooseDoctor choose;

        #region Properties

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


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="mainOpen">Main window</param>
        public ChooseDoctorViewModel(ChooseDoctor chooseOpen)
        {
            choose = chooseOpen;
            DoctorList = service.GetAllDoctors().ToList();
        }
        #endregion

        #region Commands

        private ICommand chooseDoctor;
        /// <summary>
        /// Add student command
        /// </summary>
        public ICommand ChooseDoctor
        {
            get
            {
                if (chooseDoctor == null)
                {
                    chooseDoctor = new RelayCommand(param => ChooseDoctorExecute(), param => CanChooseDoctorExecute());
                }
                return chooseDoctor;
            }
        }

        /// <summary>
        /// Add student execute
        /// </summary>
        private void ChooseDoctorExecute()
        {
            try
            {
                if (Doctor != null)
                {
                    int doctorID = doctor.DoctorID;
                    service.ChooseDoctor(LoggedInUser.CurrentUser, doctorID);

                    MessageBox.Show("Successfuly chosen doctor. Click ok to exit.");

                    User userV = new User();
                    choose.Close();
                    userV.Show();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can add student execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanChooseDoctorExecute()
        {
            if (Doctor == null)
                return false;
            else
                return true;
        }

        #endregion
    }
}
