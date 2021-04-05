﻿using InternshipClass.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Models
{
    public static class SeedData
    {
        public static void Initialization(InternDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Interns.Any())
            {
                return;
            }

            var interns = new Intern[]
            {
                new Intern { Id = 1, Name = "Borys", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Id = 2, Name = "Liova", DateOfJoin = DateTime.Parse("2021-04-01") },
                new Intern { Id = 3, Name = "Orest", DateOfJoin = DateTime.Parse("2021-03-31") },
            };

            context.Interns.AddRange(interns);
            context.SaveChanges();
        }

    }
}