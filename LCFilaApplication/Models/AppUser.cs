using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace LCFilaApplication.Models
{
    public partial class AppUser : IdentityUser, IQueryable
    {
        [JsonIgnore]
        public EmpresaLogin empresaLogin { get; set; }

        public Type ElementType => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
