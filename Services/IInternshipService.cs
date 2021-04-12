using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Hubs;
using InternshipClass.Models;

namespace InternshipClass.Services
{
    public interface IInternshipService
    {
        public Intern AddMember(Intern member);

        public IList<Intern> GetMembers();

        void RemoveMember(int id);

        void UpdateMember(Intern intern);
        void SubscribeToAddMember(IAddMemberSubscriber messageHub);
    }
}
