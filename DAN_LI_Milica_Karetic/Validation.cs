using DAN_LI_Milica_Karetic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAN_LI_Milica_Karetic
{
    class Validation
    {
        internal bool ValidDoctorInput(tblDoctor doctor)
        {
            if (ValidJMBG(doctor.JMBG))
            {
                if(ValidBank(doctor.BankAccount))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Bank Account number must have 10 digits.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Wrong JMBG");
                return false;
            }
        }

        private bool ValidInsurence(string num)
        {

            if (num.Length < 9)
                return false;
            else return true;
        }

        private bool ValidBank(string num)
        {
            if (num.Length < 10)
                return false;
            else return true;
        }

        internal bool ValidUserInput(tblUser user)
        {
            if(ValidJMBG(user.JMBG))
            {
                if(ValidInsurence(user.HealthInsuranceNumber))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Health Insurance Number must have 9 digits.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Wrong JMBG");
                return false;
            }

        }


        /// <summary>
        /// Method for validating 13 digits for JMBG
        /// </summary>
        /// <param name="jmbg">Member JMBG</param>
        /// <returns>Valid or invalid</returns>
        private bool ValidJMBG(string jmbg)
        {
            int a = 0;
            for (int i = 0; i < jmbg.Length; i++)
            {
                if (jmbg.Length == 13)
                {
                    if (jmbg[i] >= '0' && jmbg[i] <= '9')
                        a++;
                }
            }
            //calls method to check is entered jmbg is valid
            if (a == 13)
                return CkeckJMBG(jmbg);
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method for checking validation for entered jmbg based on formula for JMBG
        /// </summary>
        /// <param name="jmbg">JMBG</param>
        /// <returns>Valid or invalid</returns>
        private bool CkeckJMBG(string jmbg)
        {

            int[] daysOfMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            // input in char array
            char[] array = jmbg.ToCharArray(0, 13);

            // first checks entered year of birth
            // separates digits to get year of birth
            char[] yearOfBirth = jmbg.ToCharArray(4, 3);
            int tempYear = 100 * (Convert.ToInt32(yearOfBirth[0] - '0')) +
                             10 * (Convert.ToInt32(yearOfBirth[1] - '0')) +
                                   Convert.ToInt32(yearOfBirth[2] - '0');

            // if someone is born in XXI century
            if (yearOfBirth[0] == '0')
                tempYear += 2000;
            // who is born in XX century
            else
                tempYear += 1000;

            // year of birth can't be less than 1900
            if (tempYear < 1900)
            {

                return false;
            }
            else
            {
                // year of birth can't be bigger than current year 2020
                if (tempYear > DateTime.Now.Year)
                {

                    return false;
                }
            }

            // ckecks month of birth
            // separates digits to get month of birth
            char[] monthOfBirth = jmbg.ToCharArray(2, 2);
            int tempMonth = 10 * (Convert.ToInt32(monthOfBirth[0] - '0')) +
                                  Convert.ToInt32(monthOfBirth[1] - '0');
            if (tempMonth > 12 || tempMonth < 1) // month has to be <= 12 and > 0 
            {

                return false;
            }

            // ckecks if year if birth is delinquent
            if (((tempYear % 4) == 0) && (((tempYear % 100) != 0) || ((tempYear % 400) == 0)))
            {
                // if is delinquent february has 29 days
                daysOfMonth[1] = 29;
            }

            // ckecks entered day of birth based on month 
            char[] dayOfBirth = jmbg.ToCharArray(0, 2);
            int tempDay = 10 * (Convert.ToInt32(dayOfBirth[0] - '0')) +
                               Convert.ToInt32(dayOfBirth[1] - '0');

            if (tempDay > daysOfMonth[tempMonth - 1] || tempDay < 1)
            {

                return false;
            }

            int sum = 0;

            // calculate control sum
            for (int i = 0; i < 6; i++)
                sum += (7 - i) * (Convert.ToInt32(array[i] - '0') + Convert.ToInt32(array[6 + i] - '0'));
            // rest of division sum with 11
            int rest = sum % 11;
            // difference
            int difference = 11 - rest;

            // if rest = 1, JMBG is INVALID
            if (rest == 1)
            {

                return false;
            }
            //If rest = 0 and control digit = 0 JMBG is VALID
            else if (rest == 0)
            {
                if (Convert.ToInt32(array[12] - '0').Equals(0))
                {

                    return true;
                }
                else
                {

                    return false;
                }
            }
            else if (difference == (array[12] - '0'))
            {

                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
