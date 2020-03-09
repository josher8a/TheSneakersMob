using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TheSneakersMob.Services.Validations
{
    public class EnsureMaximumElementsAttribute : ValidationAttribute
    {
        private readonly int _maxElements;
        public EnsureMaximumElementsAttribute(int maxElements)
        {
            _maxElements = maxElements;
        }

        public override bool IsValid(object value)
        {
            if (value is IList list)
            {
                return list.Count <= _maxElements;
            }
            return false;
        }
    }
}