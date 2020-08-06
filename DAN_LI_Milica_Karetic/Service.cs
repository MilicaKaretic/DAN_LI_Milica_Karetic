using DAN_LI_Milica_Karetic.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAN_LI_Milica_Karetic
{
    class Service
    {
        Validation v = new Validation();
        /// <summary>
        /// Gets all information about users
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about sick leaves
        /// </summary>
        /// <returns>a list of found sick leaves</returns>
        public List<tblSickLeave> GetAllSickLeaves()
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    List<tblSickLeave> list = new List<tblSickLeave>();
                    list = (from x in context.tblSickLeaves select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about doctors
        /// </summary>
        /// <returns>a list of found doctors</returns>
        public List<tblDoctor> GetAllDoctors()
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    List<tblDoctor> list = new List<tblDoctor>();
                    list = (from x in context.tblDoctors select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblSickLeave AddSickLeave(tblSickLeave tblSickLeave)
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    tblSickLeave newSickLeave = new tblSickLeave();
                    newSickLeave.SickLeaveDate = tblSickLeave.SickLeaveDate;
                    newSickLeave.Reason = tblSickLeave.Reason;
                    newSickLeave.CompanyName = tblSickLeave.CompanyName;
                    newSickLeave.EmergencyCase = tblSickLeave.EmergencyCase;


                    context.tblSickLeaves.Add(newSickLeave);
                    context.SaveChanges();

                    tblSickLeave.SickLeaveID = newSickLeave.SickLeaveID;

                    return tblSickLeave;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
       

        public void ChooseDoctor(tblUser user, int doctorID)
        {

            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    tblUser userToEdit = context.tblUsers.FirstOrDefault(us => us.UserID == user.UserID);
                    userToEdit.DoctorID = doctorID;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets all information about sick leaves from current logged in doctor
        /// </summary>
        /// <param name="doctorID">Gets the doctorID from current logged in doctor</param>
        /// <returns>a list of found sick leaves</returns>
        public List<tblSickLeave> GetAllSickLeavesByDoctor(int doctorID)
        {
            try
            {
                List<tblSickLeave> list = new List<tblSickLeave>();
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    for (int j = 0; j < GetAllUsers().Count; j++)
                    {
                        // Searches for the users that contain dictors ID
                        if (GetAllUsers()[j].DoctorID == doctorID)
                        {
                            for (int i = 0; i < GetAllSickLeaves().Count; i++)
                            {
                                // Gets all the sick leaves from that user
                                if (GetAllSickLeaves()[i].SickLeaveID == GetAllUsers()[j].SickLeaveID)
                                {
                                    list.Add(GetAllSickLeaves()[i]);
                                }
                            }
                        }
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about sick leaves from current logged in patient
        /// </summary>
        /// <param name="sickLeaveID">sick leabe id</param>
        /// <returns>list of found sick leaves</returns>
        public List<tblSickLeave> GetAllSickLeavesByPatient(int sickLeaveID)
        {
            try
            {
                List<tblSickLeave> list = new List<tblSickLeave>();
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    for (int i = 0; i < GetAllSickLeaves().Count; i++)
                    {
                        if (GetAllSickLeaves()[i].SickLeaveID == sickLeaveID)
                        {
                            list.Add(GetAllSickLeaves()[i]);
                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }


       /// <summary>
       /// Register (add) new doctor
       /// </summary>
       /// <param name="doctor">Doctor to add</param>
       /// <returns>Addde doctor</returns>
        public tblDoctor AddDoctor(tblDoctor doctor)
        {

            if (v.ValidDoctorInput(doctor))
            {
                try
                {
                    using (HospitalDBEntities context = new HospitalDBEntities())
                    {
                        tblDoctor newDoctor = new tblDoctor();
                        newDoctor.FirstName = doctor.FirstName;
                        newDoctor.LastName = doctor.LastName;
                        newDoctor.Username = doctor.Username;
                        newDoctor.Password = doctor.Password;
                        newDoctor.JMBG = doctor.JMBG;
                        newDoctor.BankAccount = doctor.BankAccount;

                        context.tblDoctors.Add(newDoctor);
                        context.SaveChanges();

                        doctor.DoctorID = newDoctor.DoctorID;

                        return doctor;

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception" + ex.Message.ToString());
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Wrong data input. Please provide valid data to register as a doctor.");
                return null;
            }

        }

        public tblUser AddUser(tblUser user)
        {

            if (v.ValidUserInput(user))
            {
                try
                {
                    using (HospitalDBEntities context = new HospitalDBEntities())
                    {
                        tblUser newUser = new tblUser();
                        newUser.FirstName = user.FirstName;
                        newUser.LastName = user.LastName;
                        newUser.Username = user.Username;
                        newUser.Password = user.Password;
                        newUser.JMBG = user.JMBG;
                        newUser.HealthInsuranceNumber = user.HealthInsuranceNumber;

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();

                        user.DoctorID = newUser.UserID;

                        return user;

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception" + ex.Message.ToString());
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Wrong data input. Please provide valid data to register as a patient.");
                return null;
            }

        }
    }
}
