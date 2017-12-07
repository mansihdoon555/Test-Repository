using System;
using System.Collections.Generic;
using System.Web.Caching;
using _729solutions.Entities.Base;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace _729solutions.DataAccess.Base
{
    public abstract class CacheableDataMapper<T> : DataMapper<T>, ICacheableDataMapper where T : Entity, new()
    {
        protected string _TypeFullName = typeof(T).FullName;

        private bool _loadedFromDB = false;

        public bool LoadedFromDB
        {
            get { return _loadedFromDB; }
            set { _loadedFromDB = value; }
        }

        protected virtual string KeyName
        {
            get { return _TypeFullName; }
        }

        public void UpdateCache(List<T> updatedList)
        {
            HttpContext.Current.Cache.Remove(KeyName);

            Dictionary<int, T> cacheDictionary = new Dictionary<int, T>();
            foreach (T t in updatedList)
            {
                cacheDictionary.Add(t.Id, t);
            }

            HttpContext.Current.Cache.Add(KeyName, cacheDictionary, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 30, 0), CacheItemPriority.Normal, null);
        }

        public override List<T> Find()
        {
            LoadAllFromDB();

            List<T> entitiesList = new List<T>();

            entitiesList.AddRange(((Dictionary<int, T>)HttpContext.Current.Cache[KeyName]).Values);

            return entitiesList;
        }

        public override T Find(int id)
        {
            LoadAllFromDB();

            Dictionary<int, T> cacheDictionary = HttpContext.Current.Cache[KeyName] as Dictionary<int, T>;

            if (cacheDictionary != null && cacheDictionary.ContainsKey(id))
            {
                return cacheDictionary[id];
            }

            return null;
        }

        public override void Insert(T entity)
        {
            throw new NotImplementedException("Do not use this method!!!!");
        }

        public override void Insert(T entity, DbTransaction transaction)
        {
            throw new NotImplementedException("Do not use this method!!!!");
        }

        public override void Update(T entity)
        {
            throw new NotImplementedException("Do not use this method!!!!");
        }

        public override void Delete(T entity)
        {
            throw new NotImplementedException("Do not use this method!!!!");
        }

        protected virtual void LoadAllFromDB()
        {

            if (HttpContext.Current.Cache[KeyName] != null)
                return;

            LoadedFromDB = true;

            Database db = DatabaseFactory.CreateDatabase();

            IDataReader dr = null;

            Dictionary<int, T> entityDictionary;

            DbCommand command = db.GetStoredProcCommand(SP_Find);

            try
            {
                dr = db.ExecuteReader(command);

                entityDictionary = MapDictionary(dr);

                HttpContext.Current.Cache.Add(KeyName, entityDictionary, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 30, 0), CacheItemPriority.Normal, null);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (dr != null) dr.Dispose();
            }
        }

        protected virtual Dictionary<int, T> MapDictionary(IDataReader dr)
        {
            Dictionary<int, T> entityDictionary = new Dictionary<int, T>();

            while (dr.Read())
            {
                T entity = MapFields(dr);

                entity.DataState = DataState.UnChanged;

                entityDictionary.Add(entity.Id, entity);

                entity = null;
            }

            dr.Dispose();

            return entityDictionary;
        }

        #region ICacheableDataMapper Members


        public void UpdateCache(List<Entity> list)
        {
            UpdateCache(list.ConvertAll(new Converter<Entity, T>(ConverterDelegate)));
        }

        private static T ConverterDelegate(Entity entity)
        {
            return (T)entity;
        }

        #endregion
    }
}
