using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class HashTag : ValueObject
    {
        public string Title { get; private set; }

        public HashTag(string title)
        {
            Title = title;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Title;
        }
    }
}