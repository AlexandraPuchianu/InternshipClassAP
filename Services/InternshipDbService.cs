using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Data;
using InternshipClass.Hubs;
using InternshipClass.Models;
using Microsoft.Extensions.Configuration;

namespace InternshipClass.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;
        private IConfiguration configuration;
        private Location defaultLocation;

        public InternshipDbService(InternDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        public Intern AddMember(Intern member)
        {
            if (member.Location == null)
            {
                member.Location = GetDefaultLocation();
            }

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
            db.Entry(member).Reference(_ => _.Location).Load();
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

        private Location GetDefaultLocation()
        {
            if (defaultLocation == null)
            {
                var defaultLocationName = configuration["DefaultLocation"];
                defaultLocation = db.Locations.Where(_ => _.Name == defaultLocationName).OrderBy(_ => _.Id).FirstOrDefault();
            }

            return defaultLocation;
        }

        public void UpdateLocation(int id, int locationId)
        {
            var intern = db.Find<Intern>(id);
            var location = db.Find<Location>(locationId);
            intern.Location = location;
        }
    }
}
