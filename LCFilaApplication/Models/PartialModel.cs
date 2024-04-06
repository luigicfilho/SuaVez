using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LCFilaApplication.Models
{
    [ModelMetadataType(typeof(AppUserMetadata))]
    public partial class AppUser
    {
    }

    public class AppUserMetadata
    {
        [Display(Name = "Nome de usuário")]
        public virtual string UserName { get; set; }
        //public virtual DateTimeOffset? LockoutEnd { get; set; }

        //public virtual bool TwoFactorEnabled { get; set; }

        //public virtual bool PhoneNumberConfirmed { get; set; }
        [Display(Name = "Telefone")]
        public virtual string PhoneNumber { get; set; }

        //public virtual string ConcurrencyStamp { get; set; }

        //public virtual string SecurityStamp { get; set; }

        //public virtual string PasswordHash { get; set; }

        //public virtual bool EmailConfirmed { get; set; }

        //public virtual string NormalizedEmail { get; set; }
        [Display(Name = "Email")]
        public virtual string Email { get; set; }

        //public virtual string NormalizedUserName { get; set; }

        //public virtual string UserName { get; set; }

        //public virtual TKey Id { get; set; }

        //public virtual bool LockoutEnabled { get; set; }

        //public virtual int AccessFailedCount { get; set; }
        //[Display(Name = "Ativo")]
        //public bool CkAtivo { get; set; }

        //[StringLength(8000)]
        //[Display(Name = "Sobre o Curso")]
        ////[Required(ErrorMessage = "Campo Sobre o Curso inválido!")]
        //public string NmSobreCurso { get; set; }

        //[StringLength(8000)]
        //[Display(Name = "Mercado de Trabalho")]
        ////[Required(ErrorMessage = "Campo Mercado de Trabalho inválido!")]
        //public string NmMercadoTrabalho { get; set; }

        //[StringLength(50)]
        //[Display(Name = "Duração")]
        //public string NmDuracao { get; set; }
    }

}
