using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNewMicroService.Shared.Services
{
    public interface IIdentityService
    {
        public Guid UserId { get; }
        public string UserName { get; }
        public List<string> Roles => [];
    }
}
