using System;
using _729solutions.DataAccess.Base;

namespace _729solutions.DataAccess
{
    public class DataMapperFactory
    {
        public static IDataMapper GetDataMapper(Type entityType)
        {
            return new UserDataMapper();
        }

        public static ICacheableDataMapper GetCacheableDataMapper(Type entityType)
        {
            return null;
        }
    }
}
