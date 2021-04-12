using InternshipClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Services
{
    public interface IAddMemberSubscriber
    {
        void OnAddMember(Intern member);
    }
}
