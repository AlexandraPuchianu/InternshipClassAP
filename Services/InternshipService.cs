using InternshipClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Services
{
    public class InternshipService
    {
        private readonly InternshipModel _internshipModel = new();

        public void RemoveMember(int index)
        {
            _internshipModel.Members.RemoveAt(index);
        }

        public string AddMember(string member)
        {
            _internshipModel.Members.Add(member);
            return member;
        }

        public InternshipModel GetClass()
        {
            return _internshipModel;
        }
    }
}
