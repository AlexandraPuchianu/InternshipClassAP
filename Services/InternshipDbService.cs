using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Data;
using InternshipClass.Hubs;
using InternshipClass.Models;

namespace InternshipClass.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;

        public InternshipDbService(InternDbContext db)
        {
            this.db = db;
        }

        public Intern AddMember(Intern member)
        {
            db.Interns.AddRange(member);
            db.SaveChanges();
            return member;
        }

        public IList<Intern> GetMembers()
        {
            var interns = db.Interns.ToList();
            return interns;
        }

        public Intern GetMemberById(int id)
        {
            var member = db.Find<Intern>(id);
            return member;
        }

        public void RemoveMember(int id)
        {
            var intern = GetMemberById(id);
            if (intern == null) return;
            db.Remove<Intern>(intern);
            db.SaveChanges();
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdate = GetMemberById(intern.Id);
            itemToBeUpdate.Name = intern.Name;
            if (intern.DateOfJoin > DateTime.MinValue)
            {
                itemToBeUpdate.DateOfJoin = intern.DateOfJoin;
            }

            db.Interns.Update(itemToBeUpdate);
            db.SaveChanges();
        }
    }
}
