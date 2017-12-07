using System.Collections.Generic;
using _729solutions.BusinessLogic;
using _729solutions.Entities;
using NUnit.Framework;

namespace _729solutions.UnitTests
{
    [TestFixture]
    public class UserTests
    {
        public UserTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [Test]
        public void TestFind()
        {
            List<UserData> users = new User().Find();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

        [Test]
        public void TestFindByNameLastName()
        {
            string name = "asdf";
            List<UserData> users = new User().Find(name, string.Empty);

            Assert.IsNotNull(users);

            foreach (UserData user in users)
            {
               Assert.IsTrue(user.UserName.Contains(name));
            }

        }
    }
}
