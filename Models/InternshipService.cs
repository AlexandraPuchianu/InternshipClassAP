using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Models
{
    public class InternshipService
    {
        private readonly List<string> _members;

        public InternshipService()
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
