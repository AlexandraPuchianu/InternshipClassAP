﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Data;
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

        public void RemoveMember(int id)
        {
            var intern = db.Find<Intern>(id);
            db.Remove<Intern>(intern);
            db.SaveChanges();
        }

        public void UpdateMember(Intern intern)
        {
            throw new NotImplementedException();
        }
    }
}