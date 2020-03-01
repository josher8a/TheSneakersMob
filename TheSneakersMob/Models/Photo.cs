using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Photo : ValueObject
    {
        public string Title { get; private set; }
        public string Url { get; private set; }
        public Photo(string title, string url)
        {
            Title = title;
            Url = url;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Title;
            yield return Url;
        }
    }
}