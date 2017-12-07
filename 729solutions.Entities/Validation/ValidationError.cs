using System;
using _729solutions.Entities.Base;

namespace _729solutions.Entities.Validation
{
    [Serializable]
    public class ValidationError
    {
        #region Private Members

        private string _errorCode;

        private Entity _entity;

        private string _entityXpathExpression;

        #endregion
        #region Constructors
        public ValidationError(string errorCode, Entity entity, string entityXpathExpression)
        {
            _errorCode = errorCode;
            _entity = entity;
            _entityXpathExpression = entityXpathExpression;
        }
        public ValidationError(string errorCode, Entity entity)
        {
            _errorCode = errorCode;
            _entity = entity;
        }
        #endregion
        #region Public Properties
        public Entity Entity
        {
            get { return _entity; }
        }

        public string ErrorCode
        {
            get { return _errorCode; }
        }
        #endregion
    }
}
