using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNewMicroService.Shared.Services
{
    public interface IIdentityService
    {
        public Guid GetUserId { get; }
        public string UserName { get; }
    }
}
