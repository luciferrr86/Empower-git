using A4.BAL;
using A4.BAL.Maintenance;
using A4.DAL.Core;
using A4.DAL.Entites;
using A4.DAL.Entites.Leave;
using A4.DAL.Entites.Maintenance;
using A4.DAL.Entites.Recruitment;
using AutoMapper;
using DAL.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4.Empower.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserModel>()
                   .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore());

            CreateMap<ApplicationUser, UserEditViewModel>()
                .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserEditViewModel, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore());

            CreateMap<ApplicationUser, UserPatchViewModel>()
                .ReverseMap();

            CreateMap<ApplicationRole, RoleModel>()
                .ForMember(d => d.Permissions, map => map.MapFrom(s => s.Claims))
                .ForMember(d => d.UsersCount, map => map.MapFrom(s => s.Users != null ? s.Users.Count : 0))
                .ReverseMap();
            CreateMap<RoleModel, ApplicationRole>();

            CreateMap<IdentityRoleClaim<string>, ClaimViewModel>()
                .ForMember(d => d.Type, map => map.MapFrom(s => s.ClaimType))
                .ForMember(d => d.Value, map => map.MapFrom(s => s.ClaimValue))
                .ReverseMap();

            CreateMap<IdentityRoleClaim<string>, PermissionViewModel>()
                .ConvertUsing((s, d, context) => context.Mapper.Map<PermissionViewModel>(ApplicationPermissions.GetPermissionByValue(s.ClaimValue)));


            CreateMap<FunctionalDepartmentViewModel, FunctionalDepartment>();
            CreateMap<FunctionalTitleViewModel, FunctionalTitle>();
            CreateMap<FunctionalDesignationViewModel, FunctionalDesignation>();
            CreateMap<FunctionalGroupViewModel, FunctionalGroup>();
            CreateMap<EmployeeSalaryViewModel, EmployeeSalary>().ReverseMap();
            CreateMap<EmployeeCtcViewModel, EmployeeCtc>().ReverseMap();
            CreateMap<SalaryCompViewModel, SalaryComponent>().ReverseMap();
            CreateMap<EmployeeAttendenceViewModel, EmployeeAttendence>().ReverseMap();
            CreateMap<EmployeeAttendence, EmployeeAttendenceViewModel>();
              // .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<EmployeeAttendenceViewModel, EmployeeAttendence>();
            //.ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<AttendenceSummaryViewModel, AttendenceSummary>().ReverseMap();
            CreateMap<ExcelCandidateDataViewModel, ExcelCandidateData>().ReverseMap();
        }
    }
}
