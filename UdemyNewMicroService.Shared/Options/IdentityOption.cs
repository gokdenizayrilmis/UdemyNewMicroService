using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyNewMicroService.Shared.Options
{
    public class IdentityOption
    {
        public required string Address { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}
