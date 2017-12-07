using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using _729solutions.DataAccess.Base;
using _729solutions.Entities;

namespace _729solutions.DataAccess
{
    public class UserDataMapper : DataMapper<UserData>
    {
        protected override string SP_Find
        {
            get { return "UsersFind"; }
        }

        protected override UserData MapFields(IDataReader dr)
        {
            UserData user = new UserData();

            user.EmailAddress = dr.GetString(dr.GetOrdinal("Email"));
            user.Id = dr.GetInt32(dr.GetOrdinal("Id"));
            user.LastName = dr.GetString(dr.GetOrdinal("LastName"));
            user.FirstName = dr.GetString(dr.GetOrdinal("FirstName"));
            user.UserName = dr.GetString(dr.GetOrdinal("UserName"));
            user.Password = dr.GetString(dr.GetOrdinal("Password"));

            return user;
        }

        protected override void MapObject(UserData entity, DbCommand command,
                                          Database db)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
