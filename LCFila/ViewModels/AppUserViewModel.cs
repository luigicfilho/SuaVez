using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LCFila.ViewModels
{
    public class AppUserViewModel
    {
        public DateTimeOffset? LockoutEnd { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public string ConcurrencyStamp { get; set; }
        public string SecurityStamp { get; set; }

        public string PasswordHash { get; set; }

        public bool EmailConfirmed { get; set; }

        public string NormalizedEmail { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public Guid Id { get; set; }
        [JsonIgnore]
        public EmpresaLoginViewModel empresaLogin { get; set; }
    }
}
