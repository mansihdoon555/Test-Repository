using System;
using System.Collections.Generic;
using System.Data.Common;
using _729solutions.Entities.Validation;
using _729solutions.Entities.Base;
using _729solutions.DataAccess.Base;
using _729solutions.DataAccess;

namespace _729solutions.BusinessLogic.Base
{
    public abstract class BusinessComponent<TData> : IValidator
            where TData : Entity, new()
    {

        #region Attributes

        private TData _data;

        #endregion

        #region Constructors

        public BusinessComponent()
        {
        }

        public BusinessComponent(TData entityData)
        {
            _data = entityData;
        }

        #endregion

        #region Properties

        public TData Data
        {
            get { return _data; }
        }

        public Boolean HasData
        {
            get { return (_data != null); }
        }

        #endregion

        #region Virtual Methods

        public virtual TData Find(int entityID)
        {
            IDataMapper dataMapper = GetDataMapper();

            Entity entity = dataMapper.Find(entityID);

            if (entity != null)
                entity.DataState = DataState.UnChanged;

            return (TData)entity;
        }

        public virtual List<TData> Find()
        {
            IDataMapper dataMapper = GetDataMapper();

            List<Entity> entitiesCollection = dataMapper.Find();

            return CastList(entitiesCollection);
        }

        public virtual void Insert(TData entity)
        {
            Insert(entity, null);
        }

        internal virtual void Insert(TData entity, DbTransaction transaction)
        {
            IDataMapper dataMapper = GetDataMapper();

            ValidationSummary summary = null;

            //Si la entidad ya fue validada entonces no hay que hacerlo de nuevo

            if (!entity.IsValid)
                summary = Validate(entity);

            if (summary == null || summary.IsValid)
                dataMapper.Insert(entity, transaction);
            else
                throw new ValidationException(summary);

            AfterInsertTasks(entity);
        }

        public virtual void Update(TData entity)
        {
            IDataMapper dataMapper = GetDataMapper();

            ValidationSummary summary = null;

            //Si la entidad ya fue validada entonces no hay que hacerlo de nuevo

            if (!entity.IsValid)
                summary = Validate(entity);

            if (summary == null || summary.IsValid)
                dataMapper.Update(entity);

            AfterUpdateTasks(entity);
        }

        public virtual void Update(TData entity, DbTransaction transaction)
        {
            IDataMapper dataMapper = GetDataMapper();

            ValidationSummary summary = null;

            //Si la entidad ya fue validada entonces no hay que hacerlo de nuevo

            if (!entity.IsValid)
                summary = Validate(entity);

            if (summary == null || summary.IsValid)
                dataMapper.Update(entity, transaction);

            AfterUpdateTasks(entity);
        }

        public virtual void Delete(TData entity)
        {
            IDataMapper dataMapper = GetDataMapper();

            dataMapper.Delete(entity);

            AfterDeleteTasks(entity);
        }

        public virtual void AfterInsertTasks(TData entity) { }

        public virtual void AfterUpdateTasks(TData entity) { }

        public virtual void AfterDeleteTasks(TData entity) { }

        #endregion

        protected List<TData> CastList(List<Entity> entitiesList)
        {
            return entitiesList.ConvertAll(new Converter<Entity, TData>(CasterDelegate));
        }

        private static TData CasterDelegate(Entity entity)
        {
            return (TData)entity;
        }

        protected List<Entity> DownCastList(List<TData> entitiesList)
        {
            return entitiesList.ConvertAll(new Converter<TData, Entity>(DownCasterDelegate));
        }

        private static Entity DownCasterDelegate(TData entity)
        {
            return entity;
        }

        protected static IDataMapper GetDataMapper()
        {
            return DataMapperFactory.GetDataMapper(typeof(TData));
        }

        #region Validation

        public ValidationSummary MarkEntityAsValid(TData entity)
        {
            ValidationSummary summary = entity.Validate(this);

            return summary;
        }

        #region IValidator Members

        public virtual ValidationSummary Validate(Entity entity)
        {
            return new ValidationSummary();
        }

        #endregion

        #endregion
    }
 }
