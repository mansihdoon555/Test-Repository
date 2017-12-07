using System;
using _729solutions.Entities.Validation;

namespace _729solutions.Entities.Base
{
    public enum DataState
    {
        New,
        Updated,
        Deleted,
        UnChanged
    }

    /// <summary>
    /// Business Entity that is inherited by all other entities.
    /// </summary>
    [Serializable]
    public abstract class Entity
    {
        private int _Id;
        private DataState _state = DataState.New;
        protected bool _isValid;

        #region Constructors
        public Entity() { }
        public Entity(int id)
        {
            _Id = id;
        }
        #endregion

        #region Properties
        public virtual int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public DataState DataState
        {
            get { return _state; }
            set { _state = value; }
        }

        public bool IsValid
        {
            get { return _isValid; }
        }

        #endregion

        public ValidationSummary Validate(IValidator component)
        {
            ValidationSummary summary = component.Validate(this);

            _isValid = summary.IsValid;

            return summary;
        }

    }
}
