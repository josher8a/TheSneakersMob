using System;
using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Feedback : ValueObject
    {
        public int Stars { get; set; }
        public string Comment { get; set; }
        public Feedback(int stars, string comment)
        {
            if(stars < 0 || stars >5)
                throw new ArgumentOutOfRangeException(nameof(stars));
                
            Stars = stars;
            Comment = comment;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Stars;
            yield return Comment;
        }
    }
}