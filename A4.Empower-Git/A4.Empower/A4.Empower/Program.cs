using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Core;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using A4.Empower.ViewModels;
using A4.Empower.Helpers;
using A4.Empower.Authorization;
using Microsoft.AspNetCore.Identity;
using AppPermissions = A4.DAL.Core.ApplicationPermissions;
using A4.DAL.Entites;
using A4.DAL.Core;
using A4.DAL;
using Microsoft.Extensions.Logging;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// ----- Services Configuration -----

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"],
        b => b.MigrationsAssembly("A4.Empower"));
    options.UseOpenIddict();
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.ClaimsIdentity.UserNameClaimType = Claims.Name;
    options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
    options.ClaimsIdentity.RoleClaimType = Claims.Role;
});

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
               .UseDbContext<ApplicationDbContext>();
    })
    .AddServer(options =>
    {
        options.SetTokenEndpointUris("/connect/token");

        options.AllowPasswordFlow();
        options.AllowRefreshTokenFlow();

        options.AcceptAnonymousClients();

        // Note: Scope validation is disabled to match previous config.
        // Consider enabling for better security in production.
        options.DisableScopeValidation();

        options.AddEphemeralEncryptionKey()
               .AddEphemeralSigningKey();

        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(2));

        options.UseAspNetCore()
               .EnableTokenEndpointPassthrough();
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "http://localhost:4201") // Allow Angular dev server
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Empower360Plus API", Version = "v1" });

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("http://localhost:56071/connect/authorize"),
                TokenUrl = new Uri("http://localhost:56071/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "api1", "Demo API - full access" }
                }
            }
        }
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.ManageEmployeePolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageEmployee));
    options.AddPolicy(Policies.ManageDepartmentPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageDepartment));
    options.AddPolicy(Policies.ManageGroupPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageGroup));
    options.AddPolicy(Policies.ManageDesignationPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageDesignation));
    options.AddPolicy(Policies.ManageTitlePolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageTitle));
    options.AddPolicy(Policies.ManageRolePolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageRole));
    options.AddPolicy(Policies.ManageBandPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageBand));
    options.AddPolicy(Policies.ManageProcessSalaryPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageProcessSalary));
    options.AddPolicy(Policies.ManageCheckSalaryPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageCheckSalary));
    options.AddPolicy(Policies.ManageSalaryComponentPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageSalaryComponent));
    options.AddPolicy(Policies.ManageAllEmployeeSalaryPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageAllEmployeeSalary));
    options.AddPolicy(Policies.ManageSalaryComponentListPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageSalaryComponentList));

    options.AddPolicy(Policies.ManageMyLeavePolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageMyLeave));
    options.AddPolicy(Policies.ManageLeavePolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageLeave));
    options.AddPolicy(Policies.ManageHrLeavePolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageHrLeave));
    options.AddPolicy(Policies.ManageMyAttendancePolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageMyAttendance));
    options.AddPolicy(Policies.ManageAttendancePolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageAttendance));
    options.AddPolicy(Policies.ManageAttendanceDetailPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageAttendance));
    options.AddPolicy(Policies.ManageUploadAttendanceDetailPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageAttendance));
    options.AddPolicy(Policies.ManageAttendanceSummaryPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageAttendance));
    options.AddPolicy(Policies.ManageUploadAttendanceSummaryPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageAttendance));

    options.AddPolicy(Policies.ManageMyTimeshhetPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageMyTimesheet));
    options.AddPolicy(Policies.ManageTimesheetPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageTimesheet));

    options.AddPolicy(Policies.ManageHrViewPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageHrView));
    options.AddPolicy(Policies.ManageSetGoalPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageSetGoal));
    options.AddPolicy(Policies.ManageMyGoalPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageMyGoal));
    options.AddPolicy(Policies.ManageReviewGoalPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageReviewGoal));

    options.AddPolicy(Policies.ManageJobVaccancyPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageJobVaccancy));
    options.AddPolicy(Policies.ManageRecruitmentDashBoardPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageRecruitmentDasboard));
    options.AddPolicy(Policies.ManageInterviewPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageInterview));
    options.AddPolicy(Policies.ManageBulkSchedulingPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageBulkScheduling));
    options.AddPolicy(Policies.ManageJobVacancyListPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageJobVaccancyList));

    options.AddPolicy(Policies.ManageExpenseBookingPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageExpenseBooking));
    options.AddPolicy(Policies.ManageApprovedBookingPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageApprovedBooking));

    options.AddPolicy(Policies.ManageSalesMarketingPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageSalesMarketing));

    options.AddPolicy(Policies.ManageConfigurationPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageConfiguration));

    options.AddPolicy(Policies.ManageBlogPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageBlog));
});

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());
builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("SmtpConfig"));

// Business Services
builder.Services.AddScoped<IEmailer, Emailer>();
// Repositories
builder.Services.AddScoped<IUnitOfWork, HttpUnitOfWork>();
builder.Services.AddScoped<IAccountManager, AccountManager>();

// Auth Handlers
builder.Services.AddSingleton<IAuthorizationHandler, ManageEmployeeAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageGroupAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageDesignationAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageRoleAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageDepartmentAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageBandAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageTitleAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageProcessSalaryAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageCheckSalaryAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageSalaryComponentAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageAllEmployeeSalaryComponentAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageSalaryComponentListAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, ManageMyTimesheetAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageTimesheetAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, ManageMyLeaveAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageHrLeaveAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageLeaveAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageMyAttendanceAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageAttendanceAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageAttendanceDetailAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageUploadAttendanceDetailAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageAttendanceSummaryAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageUploadAttendanceSummaryAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, ManageHrViewAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageSetGoalAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageMyGoalAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageReviewGoalAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, ManageRecruitmentDashboardAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageJobVaccancyAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageInterviewAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageBulkSchedulingAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageJobVaccancyListAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, ManageExpenseBookingAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ManageExpenseApprovedAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, ManageSalesmarketingAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, ManageConfigurationAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, ManageBlogAuthorizationHandler>();

// DB Creation and Seeding
builder.Services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

// ----- Build the App -----

var app = builder.Build();

// ----- Database Seeding -----

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var databaseInitializer = services.GetRequiredService<IDatabaseInitializer>();
        databaseInitializer.SeedAsync().Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogCritical(LoggingEvents.INIT_DATABASE, ex, LoggingEvents.INIT_DATABASE.Name);
    }
}

// ----- Middleware Pipeline -----

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddFile(app.Configuration.GetSection("Logging"));

Utilities.ConfigureLogger(loggerFactory);
AccountTemplates.Initialize(app.Environment);
RecruitmentTemplates.Initialize(app.Environment);
LeaveTemplates.Initialize(app.Environment);
TimeSheetTemplates.Initialize(app.Environment);
PerformanceTemplates.Initialize(app.Environment);
ExpenseBookingTemplates.Initialize(app.Environment);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}



app.UseStaticFiles();

app.UseRouting();
app.UseCors("AngularApp");
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "A4.Empower API V1");
});

app.MapControllers();

app.Run();
