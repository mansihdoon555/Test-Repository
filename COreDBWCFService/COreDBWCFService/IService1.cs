using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace COreDBWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        User getUsersbyEmail(string UserEmail);
        
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class User
    {
        public int _Id;
        public string _Email;
        public string _firstName;
        public string _lastName;
        public string _UserName;
        [DataMember]
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        [DataMember]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        [DataMember]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
    }
}
