using InternshipClass.Data;
using InternshipClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Services
{
    public class InternshipService
    {
        private readonly InternshipModel _internshipModel = new ();

        public void RemoveMember(int index)
        {
            _internshipModel.Members.RemoveAt(index);
        }

        public int AddMember(Intern intern)
        {
            _internshipModel.Members.Add(intern);
            return intern.Id;
        }

        public void UpdateMember(Intern intern)
        {
            //_internshipModel.Members[];
        }

        public InternshipModel GetClass()
        {
            return _internshipModel;
        }
    }
}
