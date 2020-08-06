using DAN_LI_Milica_Karetic.Model;
using DAN_LI_Milica_Karetic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            user = new tblUser();
            userView = addUserOpen;
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


    }
}
