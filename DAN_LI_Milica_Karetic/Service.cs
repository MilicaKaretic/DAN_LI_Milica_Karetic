using DAN_LI_Milica_Karetic.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LI_Milica_Karetic
{
    class Service
    {
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
    }
}
