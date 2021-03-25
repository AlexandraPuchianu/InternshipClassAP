using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Models
{
    public class InternshipModel
    {
        private readonly List<string> _members;

        public InternshipModel()
        {
            _members = new List<string>
            {
                "Borys",
                "Liova",
                "Orest",
            };
        }

        public IList<string> Members
        {
            get { return _members; }
        }
    }
}
