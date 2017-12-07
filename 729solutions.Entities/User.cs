using _729solutions.Entities.Base;

namespace _729solutions.Entities
{
    public class UserData : Entity
    {
        #region Private Fields

        private string emailAddress;

        private string firstName;

        private string lastName;
        private string password;
        private string userName;

        #endregion

        #region Public Properties

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        #endregion

        #region Public Constructors

        public UserData(int Id) : base(Id)
        {
        }

        public UserData() : base()
        {
        }

        #endregion
    }
}
