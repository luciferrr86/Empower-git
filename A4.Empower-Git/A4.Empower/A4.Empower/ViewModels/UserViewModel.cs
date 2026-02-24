using FluentValidation;
using A4.Empower.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using A4.BAL;
using System.ComponentModel.DataAnnotations.Schema;

namespace A4.Empower.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            UserModel = new List<UserModel>();
        }

        public List<UserModel> UserModel { get; set; }
        public List<DropDownList> RoleList { get; set; }
        public List<DropDownList> BandList { get; set; }
        public List<DropDownList> DesignationList { get; set; }
        public List<DropDownList> GroupList { get; set; }
        public List<DropDownList> ManagerList { get; set; }
        public List<DropDownList> TitleList { get; set; }

        public int TotalCount { get; set; }
    }
    public class UserModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required"), StringLength(200, ErrorMessage = "Email must be at most 200 characters"), EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsLockedOut { get; set; }

        [MinimumCount(1, ErrorMessage = "Roles cannot be empty")]
        public string[] Roles { get; set; }

        public string Designation { get; set; }

        public string DOJ { get; set; }

        public string Location { get; set; }
        public int EmployeeCode { get; set; }

        public string Group { get; set; }
        public string GroupId { get; set; }
        public string ManagerId { get; set; }
        public string TitleId { get; set; }
        public string RoleId { get; set; }
        public string BandId { get; set; }
        public string DesignationId { get; set; }
        public string EmpCode { get; set; }

    }
    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string TokenId { get; set; }
        public string EmailId { get; set; }
    }
}
