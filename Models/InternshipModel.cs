using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Models
{
    public class InternshipModel
    {
        private readonly List<Intern> _members;

        public InternshipModel()
        {
            _members = new List<Intern>
            {
                new Intern { Name = "Borys", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Name = "Liova", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Name = "Orest", DateOfJoin = DateTime.Parse("2021-03-31") },
            };

        }

        public IList<Intern> Members
        {
            get { return _members; }
        }
    }
}
