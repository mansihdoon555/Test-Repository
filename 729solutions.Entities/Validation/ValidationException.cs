using System;

namespace _729solutions.Entities.Validation
{
    public class ValidationException : ApplicationException
    {
        private ValidationSummary _summary;

        public ValidationException(ValidationSummary summary)
        {
            _summary = summary;

        }

        public ValidationSummary Summary
        {
            get { return _summary; }
        }
    }
}
