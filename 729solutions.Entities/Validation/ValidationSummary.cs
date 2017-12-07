using System;
using System.Collections.Generic;

namespace _729solutions.Entities.Validation
{
    [Serializable]
    public class ValidationSummary
    {
        private List<ValidationError> _errorList;

        public List<ValidationError> Errors
        {
            get
            {
                if (_errorList == null)
                    _errorList = new List<ValidationError>();

                return _errorList;
            }
            set { _errorList = value; }
        }

        public bool IsValid
        {
            get { return (!HasErrors); }
        }

        public bool HasErrors
        {
            get { return _errorList != null && _errorList.Count > 0; }
        }
    }
}
