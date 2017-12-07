using System;
using System.Collections.Generic;
using _729solutions.BusinessLogic.Base;
using _729solutions.Entities;

namespace _729solutions.BusinessLogic
{
    public class User : BusinessComponent<UserData>
    {
        public List<UserData> Find(string Name, string LastName)
        {
            throw new NotImplementedException("Not Implemented");
        }
    }
}
