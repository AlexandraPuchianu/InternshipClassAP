using System;
using System.Collections.Generic;
using System.Linq;
using InternshipClass.Models;

namespace InternshipClass.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly InternshipModel _internshipModel = new ();

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = _internshipModel.Members.Single(_ => _.Id == id);
            _internshipModel.Members.Remove(itemToBeDeleted);
        }

        public Intern AddMember(Intern intern)
        {
            var maxId = _internshipModel.Members.Max(_ => _.Id);
            var newId = maxId + 1;

            intern.Id = newId;
            intern.DateOfJoin = DateTime.Now;

            _internshipModel.Members.Add(intern);
            return intern;
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdate = GetMemberById(intern.Id);
            itemToBeUpdate.Name = intern.Name;
        }

        public IList<Intern> GetMembers()
        {
            return _internshipModel.Members;
        }

        public Intern GetMemberById(int id)
        {
            var member = _internshipModel.Members.Single(_ => _.Id == id);
            return member;
        }
    }
}
