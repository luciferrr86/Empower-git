using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace A4.Empower.ViewModels
{
    public class UserPatchViewModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
