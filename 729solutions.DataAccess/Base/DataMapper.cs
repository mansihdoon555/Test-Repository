using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using _729solutions.Entities.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace _729solutions.DataAccess.Base
{
    public abstract class DataMapper<T> : IDataMapper where T : Entity, new()
    {
        #region Stored Procedure Names

        protected virtual string SP_Find
        {
            get
            {
                throw new NotImplementedException(
                    "You must provide a Stored Procedure Name for Delete. To acomplish this you must override the method \"SP_Find\"");
            }
        }

        protected virtual string SP_FindByID
        {
            get
            {
                throw new NotImplementedException(
                    "You must provide a Stored Procedure Name for Get. To acomplish this you must override the method \"SP_FindByID\"");
            }
        }

        protected virtual string SP_FindByParentID
        {
            get
            {
                throw new NotImplementedException(
                    "You must provide a Stored Procedure Name for Get. To acomplish this you must override the method \"SP_FindByParentID\"");
            }
        }

        protected virtual string SP_Insert
        {
            get
            {
                throw new NotImplementedException(
                    "You must provide a Stored Procedure Name for Delete. To acomplish this you must override the method \"SP_Insert\"");
            }
        }

        protected virtual string SP_Update
        {
            get
            {
                throw new NotImplementedException(
                    "You must provide a Stored Procedure Name for Delete. To acomplish this you must override the method \"SP_Update\"");
            }
        }

        protected virtual string SP_Delete
        {
            get
            {
                throw new NotImplementedException(
                    "You must provide a Stored Procedure Name for Delete. To acomplish this you must override the method \"SP_Delete\"");
            }
        }

        #endregion

        #region Standard Public Access Methods Templates (overrideable)

        public virtual void Insert(T entity, DbTransaction transaction)
        {
            if (entity.DataState == DataState.UnChanged)
                return;
            else if (entity.DataState == DataState.Updated)
            {
                Update(entity, transaction);
                return;
            }
            else if (entity.DataState == DataState.Deleted)
            {
                Delete(entity, transaction);
                return;
            }


            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand(SP_Insert);

            MapObject(entity, command, db);

            DbConnection connection = null;

            connection = transaction.Connection;

            try
            {
                entity.Id = int.Parse(db.ExecuteScalar(command, transaction).ToString());

                InsertExtraTasks(entity, transaction);

                entity.DataState = DataState.UnChanged;
            }
            catch
            {
                //if (transaction != null) //si yo la inicie yo le hago rollback
                //    transaction.Rollback();

                throw;
            }
            finally
            {
                /*if (connection != null && connection.State == ConnectionState.Open)
                    connection.Dispose();*/
            }
        }

        public virtual void Insert(T entity)
        {
            if (entity.DataState == DataState.UnChanged)
                return;
            else if (entity.DataState == DataState.Updated)
            {
                Update(entity);
                return;
            }
            else if (entity.DataState == DataState.Deleted)
            {
                Delete(entity);
                return;
            }

            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand(SP_Insert);

            MapObject(entity, command, db);

            DbConnection connection = null;

            connection = db.CreateConnection();

            connection.Open();

            try
            {
                entity.Id = int.Parse(db.ExecuteScalar(command).ToString());

                InsertExtraTasks(entity);

                entity.DataState = DataState.UnChanged;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //if (connection != null && connection.State == ConnectionState.Open)
                //    connection.Dispose();
            }
        }

        public virtual void Delete(T entity)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand(SP_Delete);

            db.AddInParameter(command, "id", DbType.Int32, entity.Id);

            DbConnection connection = null;

            connection = db.CreateConnection();

            connection.Open();

            try
            {
                db.ExecuteNonQuery(command);

                DeleteExtraTasks(entity);

                entity.DataState = DataState.UnChanged;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //if (connection != null && connection.State == ConnectionState.Open)
                //    connection.Dispose();
            }
        }

        public virtual void Delete(T entity, DbTransaction transaction)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand(SP_Delete);

            db.AddInParameter(command, "id", DbType.Int32, entity.Id);

            DbConnection connection = null;

            connection = transaction.Connection;

            try
            {
                db.ExecuteNonQuery(command, transaction);

                DeleteExtraTasks(entity, transaction);

                entity.DataState = DataState.UnChanged;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //if (connection != null && connection.State == ConnectionState.Open)
                //    connection.Dispose();
            }
        }

        public virtual void Update(T entity, DbTransaction transaction)
        {
            if (entity.DataState == DataState.UnChanged) return;

            if (entity.DataState == DataState.New)
            {
                Insert(entity, transaction);
                return;
            }
            else if (entity.DataState == DataState.Deleted)
            {
                Delete(entity, transaction);
                return;
            }

            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand(SP_Update);

            db.AddInParameter(command, "id", DbType.Int32, entity.Id);

            MapObject(entity, command, db);

            DbConnection connection = null;

            connection = transaction.Connection;

            try
            {
                db.ExecuteNonQuery(command, transaction);

                UpdateExtraTasks(entity, transaction);

                entity.DataState = DataState.UnChanged;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //if (connection != null && connection.State == ConnectionState.Open)
                //    connection.Dispose();
            }
        }

        public virtual void Update(T entity)
        {
            if (entity.DataState == DataState.UnChanged) return;

            if (entity.DataState == DataState.New)
            {
                Insert(entity);
                return;
            }
            else if (entity.DataState == DataState.Deleted)
            {
                Delete(entity);
                return;
            }

            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand(SP_Update);

            db.AddInParameter(command, "id", DbType.Int32, entity.Id);

            MapObject(entity, command, db);

            DbConnection connection = null;

            connection = db.CreateConnection();

            connection.Open();

            try
            {
                db.ExecuteNonQuery(command);

                UpdateExtraTasks(entity);

                entity.DataState = DataState.UnChanged;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //if (connection != null && connection.State == ConnectionState.Open)
                //    connection.Dispose();
            }
        }

        public virtual T Find(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            T entity = new T();
            IDataReader dr = null;

            DbCommand command = db.GetStoredProcCommand(SP_FindByID);
            db.AddInParameter(command, "id", DbType.Int32, id);

            try
            {
                dr = db.ExecuteReader(command);

                if (dr.Read())
                    entity = MapFields(dr);

                entity.DataState = DataState.UnChanged;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (dr != null) dr.Dispose();
            }

            FindByIDExtraTasks(entity);

            return (entity);
        }

        public virtual List<T> Find(Entity entity)
        {
            Database db = DatabaseFactory.CreateDatabase();
            IDataReader dr = null;
            List<T> entityList;

            DbCommand command = db.GetStoredProcCommand(SP_FindByParentID);
            db.AddInParameter(command, "ID", DbType.Int32, entity.Id);

            try
            {
                dr = db.ExecuteReader(command);
                entityList = MapCollection(dr);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (dr != null) dr.Dispose();
            }

            return (entityList);
        }

        public virtual List<T> Find()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();

            Database db = factory.Create("myConnString");
            IDataReader dr = null;
            List<T> entityList;

            DbCommand command = db.GetStoredProcCommand(SP_Find);

            try
            {
                dr = db.ExecuteReader(command);
                entityList = MapCollection(dr);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (dr != null) dr.Dispose();
            }

            return (entityList);
        }

        #endregion

        #region Extra Task Methods

        protected virtual void InsertExtraTasks(T entity) { }
        protected virtual void DeleteExtraTasks(T entity) { }
        protected virtual void UpdateExtraTasks(T entity) { }
        protected virtual void InsertExtraTasks(T entity, DbTransaction transaction) { }
        protected virtual void DeleteExtraTasks(T entity, DbTransaction transaction) { }
        protected virtual void UpdateExtraTasks(T entity, DbTransaction transaction) { }
        protected virtual void FindByIDExtraTasks(T entity) { }
        protected virtual void FindAllExtraTasks(List<T> entityList) { }

        #endregion

        #region Abstract Mapping Methods

        protected abstract T MapFields(IDataReader dr);
        protected abstract void MapObject(T entity, DbCommand command, Database db);

        #endregion

        #region Virtual Methods

        protected virtual List<T> MapCollection(IDataReader dr)
        {
            List<T> entityList = new List<T>();

            while (dr.Read())
            {
                T entity = MapFields(dr);

                entity.DataState = DataState.UnChanged;
                entityList.Add(entity);

                entity = null;
            }

            dr.Dispose();

            return (entityList);
        }

        #endregion

        #region Public Methods

        public DbTransaction CreateTransaction()
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();

            connection.Open();

            return connection.BeginTransaction();
        }

        public void CommitTransaction(DbTransaction transaction)
        {
            if (transaction != null)
            {
                transaction.Commit();
                transaction.Dispose();
            }
        }

        public void RollBackTransaction(DbTransaction transaction)
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        #endregion

        #region IDataMapper Members

        public void Insert(Entity entity, DbTransaction transaction)
        {
            Insert((T)entity, transaction);
        }

        public void Insert(Entity entity)
        {
            Insert((T)entity);
        }

        public void Delete(Entity entity)
        {
            Delete((T)entity);
        }

        public void Delete(Entity entity, DbTransaction transaction)
        {
            Delete((T)entity, transaction);
        }

        public void Update(Entity entity, DbTransaction transaction)
        {
            Update((T)entity, transaction);
        }

        public void Update(Entity entity)
        {
            Update((T)entity);
        }

        Entity IDataMapper.Find(int id)
        {
            return Find(id);
        }

        List<Entity> IDataMapper.Find(Entity entity)
        {
            return Find(entity).ConvertAll(new Converter<T, Entity>(ConverterDelegate));
        }

        List<Entity> IDataMapper.Find()
        {
            return Find().ConvertAll(new Converter<T, Entity>(ConverterDelegate));
        }

        private static Entity ConverterDelegate(T entity)
        {
            return entity;
        }

        #endregion
    }
}