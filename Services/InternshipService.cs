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

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = _internshipModel.Members.Single(_ => _.Id == id);
            _internshipModel.Members.Remove(itemToBeDeleted);
        }

        public int AddMember(Intern intern)
        {
            var maxId = _internshipModel.Members.Max(_ => _.Id);
            var newId = maxId + 1;
            
            intern.Id = maxId + 1;
            intern.DateOfJoin = DateTime.Now;
            
            _internshipModel.Members.Add(intern);
            return newId;
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdate = _internshipModel.Members.SingleOrDefault(_ => _.Id == intern.Id);
            itemToBeUpdate.Name = intern.Name;
        }

        public InternshipModel GetClass()
        {
            return _internshipModel;
        }
    }
}
