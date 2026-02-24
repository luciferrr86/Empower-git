using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace A4.Empower.Migrations
{
    public partial class AddBookingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModuleName = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcelEmployeeData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    WorkEmailAddress = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    FunctionalDepartment = table.Column<string>(nullable: true),
                    FunctionalGroup = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    PersonalEmailID = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ReportingHeadEmailId = table.Column<string>(nullable: true),
                    RollAccess = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    DateofJoining = table.Column<string>(nullable: true),
                    ReportingHeadName = table.Column<string>(nullable: true),
                    Band = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelEmployeeData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalBand",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    YearsOfExperience = table.Column<string>(nullable: true),
                    LocalOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalBand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalDepartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalDesignation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalDesignation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalTitle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalTitle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobInterviewType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobInterviewType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeavePeriod",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PeriodStart = table.Column<DateTime>(nullable: false),
                    PeriodEnd = table.Column<DateTime>(nullable: false),
                    IsLeavePeriodCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavePeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MassInterview",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Venue = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassInterview", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MassInterviewRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RoomName = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassInterviewRoom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ClientId = table.Column<string>(nullable: false),
                    ClientSecret = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    ConsentType = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Permissions = table.Column<string>(nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    RedirectUris = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Properties = table.Column<string>(nullable: true),
                    Resources = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    MyGoalInstructionText = table.Column<string>(nullable: true),
                    PlusesInstructionText = table.Column<string>(nullable: true),
                    DeltaInstructionText = table.Column<string>(nullable: true),
                    TrainingClassesInstructionText = table.Column<string>(nullable: true),
                    CurrentYearInstructionText = table.Column<string>(nullable: true),
                    NextYearInstructionText = table.Column<string>(nullable: true),
                    CareerDevInstructionText = table.Column<string>(nullable: true),
                    RatingInstructionText = table.Column<string>(nullable: true),
                    IsPerformanceStart = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceConfigFeedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LabelText = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceConfigFeedback", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceConfigRating",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RatingName = table.Column<string>(nullable: true),
                    RatingDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceConfigRating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    StatusText = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    ColorCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceYear",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    NoOfEmployee = table.Column<int>(nullable: false),
                    NextYearId = table.Column<Guid>(nullable: false),
                    Year = table.Column<string>(nullable: true),
                    IsYearActive = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PictureBinary = table.Column<byte[]>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    AltAttribute = table.Column<string>(nullable: true),
                    TitleAttribute = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ComapnyName = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetClient",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TimesheetFrequency = table.Column<int>(nullable: false),
                    TsimesheetEditUpto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetWorkingDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    WorkingDay = table.Column<string>(nullable: true),
                    WorkingDayValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetWorkingDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationModuleDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ApplicationModuleId = table.Column<Guid>(nullable: false),
                    SubModuleName = table.Column<string>(nullable: true),
                    ConfigType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationModuleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_ApplicationModuleDetail_ApplicationModule",
                        column: x => x.ApplicationModuleId,
                        principalTable: "ApplicationModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingSubCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ExpenseBookingCategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_SubCategory_Category",
                        column: x => x.ExpenseBookingCategoryId,
                        principalTable: "ExpenseBookingCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalGroup", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Group_Department",
                        column: x => x.DepartmentId,
                        principalTable: "FunctionalDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingTitleAmount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TitleID = table.Column<Guid>(nullable: false),
                    TilteID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingTitleAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseBookingTitleAmount_FunctionalTitle_TilteID",
                        column: x => x.TilteID,
                        principalTable: "FunctionalTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobVacancy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobTypeId = table.Column<Guid>(nullable: false),
                    NoOfvacancies = table.Column<int>(nullable: false),
                    PublishedDate = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: true),
                    JobLocation = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    SalaryRange = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    JobRequirements = table.Column<string>(nullable: true),
                    bIsClosed = table.Column<bool>(nullable: false),
                    bIsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancy", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_JobVacancy_JobType",
                        column: x => x.JobTypeId,
                        principalTable: "JobType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveHolidayList",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LeavePeriodId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Holidaydate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveHolidayList", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_HolidayList_LeavePeriod",
                        column: x => x.LeavePeriodId,
                        principalTable: "LeavePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LeavePeriodId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ColorCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_LeaveType_LeavePeriod",
                        column: x => x.LeavePeriodId,
                        principalTable: "LeavePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveWorkingDay",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LeavePeriodId = table.Column<Guid>(nullable: false),
                    WorkingDay = table.Column<string>(nullable: true),
                    WorkingDayValue = table.Column<string>(nullable: true),
                    LocalOrder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveWorkingDay", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_WorkingDay_LeavePeriod",
                        column: x => x.LeavePeriodId,
                        principalTable: "LeavePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MassInterviewDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    MassInterviewId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassInterviewDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_MassInterviewDetail_MassInterview",
                        column: x => x.MassInterviewId,
                        principalTable: "MassInterview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    Scopes = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpInitialRating",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceYearId = table.Column<Guid>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpInitialRating", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpInitialRating_PerformanceYear",
                        column: x => x.PerformanceYearId,
                        principalTable: "PerformanceYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsSuperAdmin = table.Column<bool>(nullable: false),
                    IsMailSent = table.Column<bool>(nullable: false),
                    PictureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesCompanyContact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SalesCompanyId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesCompanyContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesCompanyContact_SalesCompany_SalesCompanyId",
                        column: x => x.SalesCompanyId,
                        principalTable: "SalesCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesScheduleMeeting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SalesCompanyId = table.Column<Guid>(nullable: false),
                    Venue = table.Column<string>(nullable: true),
                    Writer = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Agenda = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ScheduleDate = table.Column<DateTime>(nullable: false),
                    Document = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesScheduleMeeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesScheduleMeeting_SalesCompany_SalesCompanyId",
                        column: x => x.SalesCompanyId,
                        principalTable: "SalesCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDailyCall",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SalesCompanyId = table.Column<Guid>(nullable: false),
                    SalesStatusId = table.Column<Guid>(nullable: false),
                    CallDateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDailyCall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesDailyCall_SalesCompany_SalesCompanyId",
                        column: x => x.SalesCompanyId,
                        principalTable: "SalesCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesDailyCall_SalesStatus_SalesStatusId",
                        column: x => x.SalesStatusId,
                        principalTable: "SalesStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TimesheetFrequencyId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TempalteName = table.Column<string>(nullable: true),
                    ScheduleType = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Monday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    Sunday = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetTemplate_TimesheetConfiguration",
                        column: x => x.TimesheetFrequencyId,
                        principalTable: "TimesheetConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingSubCategoryItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ExpenseBookingSubCategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingSubCategoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_SubCategoryItem_SubCategory",
                        column: x => x.ExpenseBookingSubCategoryId,
                        principalTable: "ExpenseBookingSubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobHRQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobVacancyId = table.Column<Guid>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Weightage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHRQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobHRQuestion_JobVacancy_JobVacancyId",
                        column: x => x.JobVacancyId,
                        principalTable: "JobVacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobScreeningQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobVacancyId = table.Column<Guid>(nullable: false),
                    Questions = table.Column<string>(nullable: true),
                    Weightage = table.Column<int>(nullable: false),
                    ControlType = table.Column<string>(nullable: true),
                    Option1 = table.Column<string>(nullable: true),
                    Option2 = table.Column<string>(nullable: true),
                    Option3 = table.Column<string>(nullable: true),
                    Option4 = table.Column<string>(nullable: true),
                    ChkOption1 = table.Column<bool>(nullable: false),
                    ChkOption2 = table.Column<bool>(nullable: false),
                    ChkOption3 = table.Column<bool>(nullable: false),
                    ChkOption4 = table.Column<bool>(nullable: false),
                    bIsRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobScreeningQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobScreeningQuestion_JobVacancy_JobVacancyId",
                        column: x => x.JobVacancyId,
                        principalTable: "JobVacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkillQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobVacancyId = table.Column<Guid>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Weightage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkillQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSkillQuestion_JobVacancy_JobVacancyId",
                        column: x => x.JobVacancyId,
                        principalTable: "JobVacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobVacancyLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobVacancyId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancyLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobVacancyLevel_JobVacancy_JobVacancyId",
                        column: x => x.JobVacancyId,
                        principalTable: "JobVacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LeavePeriodId = table.Column<Guid>(nullable: false),
                    LeaveTypeId = table.Column<Guid>(nullable: false),
                    BandId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LeavesPerYear = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRules", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_LeaveRules_Band",
                        column: x => x.BandId,
                        principalTable: "FunctionalBand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_LeaveRules_LeavePeriod",
                        column: x => x.LeavePeriodId,
                        principalTable: "LeavePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_LeaveRules_LeaveType",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MassInterviewPanel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VacancyId = table.Column<Guid>(nullable: false),
                    InterviewDateId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    BreakStartTime = table.Column<string>(nullable: true),
                    BreakEndTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassInterviewPanel", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_MassInterviewPanel_InterviewDetail",
                        column: x => x.InterviewDateId,
                        principalTable: "MassInterviewDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_MassInterviewPanel_JobVacancy",
                        column: x => x.VacancyId,
                        principalTable: "JobVacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    AuthorizationId = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: true),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: true),
                    Payload = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    ReferenceId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TitleId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    DesignationId = table.Column<Guid>(nullable: false),
                    BandId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    DOJ = table.Column<DateTime>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Employee_Band",
                        column: x => x.BandId,
                        principalTable: "FunctionalBand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_Employee_Designation",
                        column: x => x.DesignationId,
                        principalTable: "FunctionalDesignation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_Employee_Group",
                        column: x => x.GroupId,
                        principalTable: "FunctionalGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_Employee_Title",
                        column: x => x.TitleId,
                        principalTable: "FunctionalTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_ApplicationUser_Employee",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobCandidateProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    CurrentAddress = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    IdProofDetail = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    OfficialContactNo = table.Column<string>(nullable: true),
                    SkillSet = table.Column<string>(nullable: true),
                    CoverLetter = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ResumeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidateProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobCandidateProfile_Picture_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_ApplicationUser_CandidateProfile",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SalesScheduleMeetingId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesDocument_SalesScheduleMeeting_SalesScheduleMeetingId",
                        column: x => x.SalesScheduleMeetingId,
                        principalTable: "SalesScheduleMeeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesMinuteMeeting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SalesScheduleMeetingId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    ActionDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesMinuteMeeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesMinuteMeeting_SalesScheduleMeeting_SalesScheduleMeetingId",
                        column: x => x.SalesScheduleMeetingId,
                        principalTable: "SalesScheduleMeeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesScheduleMeetingExternal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SalesCompanyContactId = table.Column<Guid>(nullable: true),
                    SalesScheduleMeetingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesScheduleMeetingExternal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesScheduleMeetingExternal_SalesCompanyContact_SalesCompanyContactId",
                        column: x => x.SalesCompanyContactId,
                        principalTable: "SalesCompanyContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesScheduleMeetingExternal_SalesScheduleMeeting_SalesScheduleMeetingId",
                        column: x => x.SalesScheduleMeetingId,
                        principalTable: "SalesScheduleMeeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobVacancyLevelSkillQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobVacancyLevelId = table.Column<Guid>(nullable: false),
                    JobSkillQuestionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancyLevelSkillQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobVacancyLevelSkillQuestion_JobSkillQuestion_JobSkillQuestionId",
                        column: x => x.JobSkillQuestionId,
                        principalTable: "JobSkillQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobVacancyLevelSkillQuestion_JobVacancyLevel_JobVacancyLevelId",
                        column: x => x.JobVacancyLevelId,
                        principalTable: "JobVacancyLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LeavePeriodId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeaves_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeaves_LeavePeriod",
                        column: x => x.LeavePeriodId,
                        principalTable: "LeavePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePersonalDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    CurrentAddress = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    IdProofDetail = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    ContactNo = table.Column<string>(nullable: true),
                    OfficialContactNo = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    EmergencyContactNo = table.Column<string>(nullable: true),
                    EmergencyContactName = table.Column<string>(nullable: true),
                    EmergencyContactRelation = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePersonalDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Personal_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProfessionalDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    ProfileDesc = table.Column<string>(nullable: true),
                    DOJ = table.Column<DateTime>(nullable: false),
                    DOR = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfessionalDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Professional_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProxy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ProxyForId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProxy", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Employee_EmployeeProxy",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_Employee_EmployeeProxyFor",
                        column: x => x.ProxyForId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeQualificationDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HigherDegreeInstitue = table.Column<string>(nullable: true),
                    HighestQualification = table.Column<string>(nullable: true),
                    HDSpecialization = table.Column<string>(nullable: true),
                    HDPassingYear = table.Column<string>(nullable: true),
                    HDPercentage = table.Column<string>(nullable: true),
                    SecondaryInstitue = table.Column<string>(nullable: true),
                    SecondaryQualification = table.Column<string>(nullable: true),
                    SDSpecialization = table.Column<string>(nullable: true),
                    SDPassingYear = table.Column<string>(nullable: true),
                    SDPercentage = table.Column<string>(nullable: true),
                    SSCInstitue = table.Column<string>(nullable: true),
                    SSCQualification = table.Column<string>(nullable: true),
                    SSCSpecialization = table.Column<string>(nullable: true),
                    SSCPassingYear = table.Column<string>(nullable: true),
                    SSCPercentage = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeQualificationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Qualification_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    ExpenseBookingSubCategoryItemId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    ApprovedOrRejectedDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    File = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingRequest", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_BookingRequest_Department",
                        column: x => x.DepartmentID,
                        principalTable: "FunctionalDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_BookingRequest_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_BookingRequest_SubCategoryItem",
                        column: x => x.ExpenseBookingSubCategoryItemId,
                        principalTable: "ExpenseBookingSubCategoryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobVacancyLevelManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobVacancyLevelId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancyLevelManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobVacancyLevelManager_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobVacancyLevelManager_JobVacancyLevel_JobVacancyLevelId",
                        column: x => x.JobVacancyLevelId,
                        principalTable: "JobVacancyLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MassInterviewPanelVacancy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MangerId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    PanelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassInterviewPanelVacancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MassInterviewPanelVacancy_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_MassInterviewPanelVaccany_MassInterviewPanel",
                        column: x => x.PanelId,
                        principalTable: "MassInterviewPanel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpGoal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    PerformanceYearId = table.Column<Guid>(nullable: false),
                    PerformanceStatusId = table.Column<Guid>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpGoal", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpGoal_EmployeeMgr",
                        column: x => x.ManagerId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpGoal_PerformanceStatus",
                        column: x => x.PerformanceStatusId,
                        principalTable: "PerformanceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpGoal_PerformanceYear",
                        column: x => x.PerformanceYearId,
                        principalTable: "PerformanceYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceGoal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    PerformanceYearId = table.Column<Guid>(nullable: false),
                    GoalName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceGoal", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoal_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoal_PerformanceYear",
                        column: x => x.PerformanceYearId,
                        principalTable: "PerformanceYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceGoalMain",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    PerformanceStatusId = table.Column<Guid>(nullable: false),
                    PerformanceYearId = table.Column<Guid>(nullable: false),
                    IsManagerReleased = table.Column<bool>(nullable: false),
                    IsManagerActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceGoalMain", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMain_Employee",
                        column: x => x.ManagerId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMain_PerformanceStatus",
                        column: x => x.PerformanceStatusId,
                        principalTable: "PerformanceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMain_PerformanceYear",
                        column: x => x.PerformanceYearId,
                        principalTable: "PerformanceYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceInitailRating",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    PerformanceConfigRatingId = table.Column<Guid>(nullable: false),
                    PerformanceYearId = table.Column<Guid>(nullable: false),
                    IsCEOSignOff = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceInitailRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceInitailRating_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformanceInitailRating_PerformanceConfigRating_PerformanceConfigRatingId",
                        column: x => x.PerformanceConfigRatingId,
                        principalTable: "PerformanceConfigRating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceInitailRating_PerformanceYear",
                        column: x => x.PerformanceYearId,
                        principalTable: "PerformanceYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformancePresidentCouncil",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    PresidentId = table.Column<Guid>(nullable: false),
                    PerformanceYearId = table.Column<Guid>(nullable: false),
                    IsPresidentCouncil = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformancePresidentCouncil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformancePresidentCouncil_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformancePresidentCouncil_PerformanceYear",
                        column: x => x.PerformanceYearId,
                        principalTable: "PerformanceYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesScheduleMeetingInternal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    SalesScheduleMeetingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesScheduleMeetingInternal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesScheduleMeetingInternal_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesScheduleMeetingInternal_SalesScheduleMeeting_SalesScheduleMeetingId",
                        column: x => x.SalesScheduleMeetingId,
                        principalTable: "SalesScheduleMeeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetProject", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetProject_TimesheetClient",
                        column: x => x.ClientId,
                        principalTable: "TimesheetClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetProject_Employee",
                        column: x => x.ManagerId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetUserSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TimesheetTemplateId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ScheduleType = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Monday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    Sunday = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetUserSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetUserSchedule_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetUserSchedule_TimesheetTemplate",
                        column: x => x.TimesheetTemplateId,
                        principalTable: "TimesheetTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetUserSpan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    WeekStartDate = table.Column<DateTime>(nullable: false),
                    WeekEndDate = table.Column<DateTime>(nullable: false),
                    Month = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    TotalHour = table.Column<string>(nullable: true),
                    bIsUserSubmit = table.Column<bool>(nullable: false),
                    bIsManagerApprove = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetUserSpan", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetUserSpan_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobApplication",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobCandidateProfileId = table.Column<Guid>(nullable: false),
                    JobStatus = table.Column<int>(nullable: false),
                    JobVacancyId = table.Column<Guid>(nullable: false),
                    ApplicationType = table.Column<string>(nullable: true),
                    HRComment = table.Column<string>(nullable: true),
                    ScreeningScore = table.Column<string>(nullable: true),
                    HRScore = table.Column<string>(nullable: true),
                    SkillScore = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    OverallScore = table.Column<string>(nullable: true),
                    LevelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.Id);
                    table.ForeignKey(
                        name: "Foreign_JobApplication_ApplicationUser",
                        column: x => x.JobCandidateProfileId,
                        principalTable: "JobCandidateProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Foreign_JobApplication_JobVacancy",
                        column: x => x.JobVacancyId,
                        principalTable: "JobVacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobCandidateQualification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    HigherDegreeInstitue = table.Column<string>(nullable: true),
                    HighestQualification = table.Column<string>(nullable: true),
                    HDSpecialization = table.Column<string>(nullable: true),
                    HDPassingYear = table.Column<string>(nullable: true),
                    HDPercentage = table.Column<string>(nullable: true),
                    SecondaryInstitue = table.Column<string>(nullable: true),
                    SecondaryQualification = table.Column<string>(nullable: true),
                    SDSpecialization = table.Column<string>(nullable: true),
                    SDPassingYear = table.Column<string>(nullable: true),
                    SDPercentage = table.Column<string>(nullable: true),
                    SSCInstitue = table.Column<string>(nullable: true),
                    SSCQualification = table.Column<string>(nullable: true),
                    SSCSpecialization = table.Column<string>(nullable: true),
                    SSCPassingYear = table.Column<string>(nullable: true),
                    SSCPercentage = table.Column<string>(nullable: true),
                    JobCandidateProfilesId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidateQualification", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_JobQualification_JobCandidateProfile",
                        column: x => x.JobCandidateProfilesId,
                        principalTable: "JobCandidateProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobCandidateWorkExperience",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    ProfileDesc = table.Column<string>(nullable: true),
                    DOJ = table.Column<DateTime>(nullable: false),
                    DOR = table.Column<DateTime>(nullable: false),
                    JobCandidateProfilesId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidateWorkExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobCandidateWorkExperience_JobCandidateProfile_JobCandidateProfilesId",
                        column: x => x.JobCandidateProfilesId,
                        principalTable: "JobCandidateProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobCandidateProfileId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobDocument_JobCandidateProfile_JobCandidateProfileId",
                        column: x => x.JobCandidateProfileId,
                        principalTable: "JobCandidateProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MassInterviewScheduleCandidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CandidateId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    PanelId = table.Column<Guid>(nullable: false),
                    InterviewDate = table.Column<DateTime>(nullable: false),
                    InterviewTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassInterviewScheduleCandidate", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_MassInterviewScheduleCandiate_CandidateProfile",
                        column: x => x.CandidateId,
                        principalTable: "JobCandidateProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_MassInterviewScheduleCandiate_MassInterviewPanel",
                        column: x => x.PanelId,
                        principalTable: "MassInterviewPanel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_MassInterviewScheduleCandiate_MassInterviewRoom",
                        column: x => x.RoomId,
                        principalTable: "MassInterviewRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesMinuteMeetingInternal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    SalesMinuteMeetingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesMinuteMeetingInternal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesMinuteMeetingInternal_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesMinuteMeetingInternal_SalesMinuteMeeting_SalesMinuteMeetingId",
                        column: x => x.SalesMinuteMeetingId,
                        principalTable: "SalesMinuteMeeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeavesEntitlement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeLeavesId = table.Column<Guid>(nullable: false),
                    LeaveRulesId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Approved = table.Column<string>(nullable: true),
                    Pending = table.Column<string>(nullable: true),
                    Rejected = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeavesEntitlement", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeavesEntitlement_EmployeeLeaves",
                        column: x => x.EmployeeLeavesId,
                        principalTable: "EmployeeLeaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeavesEntitlement_LeaveRules",
                        column: x => x.LeaveRulesId,
                        principalTable: "LeaveRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingApprover",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    IsAllow = table.Column<bool>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    ExpenseBookingRequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingApprover", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseBookingApprover_ExpenseBookingRequest_ExpenseBookingRequestId",
                        column: x => x.ExpenseBookingRequestId,
                        principalTable: "ExpenseBookingRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_BookingRequest_Manager",
                        column: x => x.ManagerId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ExpenseBookingId = table.Column<int>(nullable: false),
                    PictureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseDocument_ExpenseBookingRequest_ExpenseBookingId",
                        column: x => x.ExpenseBookingId,
                        principalTable: "ExpenseBookingRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseDocument_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpDeltas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    Delta = table.Column<string>(nullable: true),
                    ManagerComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpDeltas", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpDeltas_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpDevGoal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    SkillDevelopment = table.Column<string>(nullable: true),
                    CareerInterest = table.Column<string>(nullable: true),
                    DevGoalBy = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpDevGoal", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpDevGoal_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpDevGoalDoc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileExtension = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    FileSize = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpDevGoalDoc", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpDevGoalDoc_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpFeedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    FeedBackEmpId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    FeedbackuserName = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    IsMailSent = table.Column<bool>(nullable: false),
                    IsSubmitted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceEmpFeedback_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpFeedback_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpGoalPresident",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    PresidentSignature = table.Column<string>(nullable: true),
                    PresidentComment = table.Column<string>(nullable: true),
                    PresidentYearComment = table.Column<string>(nullable: true),
                    PresidentSignOff = table.Column<bool>(nullable: false),
                    PresidentYearSignOff = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpGoalPresident", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpGoalPresident_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpMidYearGoal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceStatusId = table.Column<Guid>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    EmployeeAccComment = table.Column<string>(nullable: true),
                    ManagerAccComment = table.Column<string>(nullable: true),
                    EmployeeSignature = table.Column<string>(nullable: true),
                    FinalRating = table.Column<string>(nullable: true),
                    IsEmployeeGoalSaved = table.Column<bool>(nullable: false),
                    IsEmployeeGoalSubmitted = table.Column<bool>(nullable: false),
                    IsManagerGoalSaved = table.Column<bool>(nullable: false),
                    IsManagerGoalSubmitted = table.Column<bool>(nullable: false),
                    IsEmployeeRatingSaved = table.Column<bool>(nullable: false),
                    IsManagerRatingSaved = table.Column<bool>(nullable: false),
                    IsEmployeeRatingSubmitted = table.Column<bool>(nullable: false),
                    IsManagerRatingSubmitted = table.Column<bool>(nullable: false),
                    IsMidYearReviewCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpMidYearGoal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceEmpMidYearGoal_PerformanceEmpGoal_PerformanceEmpGoalId",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpMidYearGoal_PerformanceStatus",
                        column: x => x.PerformanceStatusId,
                        principalTable: "PerformanceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpPluses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    Pluses = table.Column<string>(nullable: true),
                    ManagerComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpPluses", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpPluses_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpTrainingClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    TrainingClasses = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpTrainingClasses", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpTrainingClasses_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpYearGoal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceStatusId = table.Column<Guid>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    EmployeeAccComment = table.Column<string>(nullable: true),
                    ManagerAccComment = table.Column<string>(nullable: true),
                    EmployeeSignature = table.Column<string>(nullable: true),
                    FinalRating = table.Column<string>(nullable: true),
                    IsEmployeeGoalSaved = table.Column<bool>(nullable: false),
                    IsEmployeeGoalSubmitted = table.Column<bool>(nullable: false),
                    IsManagerGoalSaved = table.Column<bool>(nullable: false),
                    IsManagerGoalSubmitted = table.Column<bool>(nullable: false),
                    IsEmployeeRatingSaved = table.Column<bool>(nullable: false),
                    IsManagerRatingSaved = table.Column<bool>(nullable: false),
                    IsEmployeeRatingSubmitted = table.Column<bool>(nullable: false),
                    IsManagerRatingSubmitted = table.Column<bool>(nullable: false),
                    IsReviewCompleted = table.Column<bool>(nullable: false),
                    IsEmployeeActive = table.Column<bool>(nullable: false),
                    IsEmployeeDeltaPlusSaved = table.Column<bool>(nullable: false),
                    IsEmployeeDeltaPlusSubmitted = table.Column<bool>(nullable: false),
                    IsManagerDeltaPlusSaved = table.Column<bool>(nullable: false),
                    IsManagerDeltaPlusSubmitted = table.Column<bool>(nullable: false),
                    IsEmployeeTrainingSaved = table.Column<bool>(nullable: false),
                    IsEmployeeTrainingSubmitted = table.Column<bool>(nullable: false),
                    IsManagerTrainingSaved = table.Column<bool>(nullable: false),
                    IsManagerTrainingSubmitted = table.Column<bool>(nullable: false),
                    IsEmployeeDevGoalSaved = table.Column<bool>(nullable: false),
                    IsEmployeeDevGoalSubmitted = table.Column<bool>(nullable: false),
                    IsManagerDevGoalSaved = table.Column<bool>(nullable: false),
                    IsManagerDevGoalSubmitted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpYearGoal", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpYearGoal_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpYearGoal_PerformanceStatus",
                        column: x => x.PerformanceStatusId,
                        principalTable: "PerformanceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceGoalMeasure",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceGoalId = table.Column<Guid>(nullable: false),
                    PerformanceGoalMainId = table.Column<Guid>(nullable: false),
                    MeasureText = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceGoalMeasure", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMeasure_PerformanceGoal",
                        column: x => x.PerformanceGoalId,
                        principalTable: "PerformanceGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMeasure_PerformanceGoalMain",
                        column: x => x.PerformanceGoalMainId,
                        principalTable: "PerformanceGoalMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetEmployeeProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TimeSheetProjectId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetEmployeeProject", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetEmployeeProject_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetEmployeeProject_TimeSheetProject",
                        column: x => x.TimeSheetProjectId,
                        principalTable: "TimesheetProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetUserDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ApproverlId = table.Column<Guid>(nullable: false),
                    TimesheetUserSpanId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TotalHour = table.Column<string>(nullable: true),
                    Day = table.Column<string>(nullable: true),
                    TimeSheetDate = table.Column<DateTime>(nullable: false),
                    bManagerApproved = table.Column<bool>(nullable: false),
                    bIsUserSaved = table.Column<bool>(nullable: false),
                    bIsUserSubmit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetUserDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetUserDetail_Employee",
                        column: x => x.ApproverlId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetUserDetail_TimesheetUserSpan",
                        column: x => x.TimesheetUserSpanId,
                        principalTable: "TimesheetUserSpan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplicationHRQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobApplicationId = table.Column<Guid>(nullable: false),
                    JobHRQuestionId = table.Column<Guid>(nullable: false),
                    ObtainedWeightage = table.Column<string>(nullable: true),
                    Weightage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationHRQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplicationHRQuestions_JobApplication_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalTable: "JobApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicationHRQuestions_JobHRQuestion_JobHRQuestionId",
                        column: x => x.JobHRQuestionId,
                        principalTable: "JobHRQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobApplicationScreeningQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobApplicationId = table.Column<Guid>(nullable: false),
                    JobScreeningQuestionId = table.Column<Guid>(nullable: false),
                    ObtainedWeightage = table.Column<string>(nullable: true),
                    Weightage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationScreeningQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplicationScreeningQuestion_JobApplication_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalTable: "JobApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicationScreeningQuestion_JobScreeningQuestion_JobScreeningQuestionId",
                        column: x => x.JobScreeningQuestionId,
                        principalTable: "JobScreeningQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobCandidateInterview",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobApplicationId = table.Column<Guid>(nullable: false),
                    JobInterviewTypeId = table.Column<Guid>(nullable: false),
                    JobVacancyLevelManagerId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<string>(nullable: true),
                    InterviewerComment = table.Column<string>(nullable: true),
                    IsInterviewCompleted = table.Column<bool>(nullable: false),
                    IsLevelCompleted = table.Column<bool>(nullable: false),
                    InterviewMode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidateInterview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobCandidateInterview_JobApplication_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalTable: "JobApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobCandidateInterview_JobInterviewType_JobInterviewTypeId",
                        column: x => x.JobInterviewTypeId,
                        principalTable: "JobInterviewType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobCandidateInterview_JobVacancyLevelManager_JobVacancyLevelManagerId",
                        column: x => x.JobVacancyLevelManagerId,
                        principalTable: "JobVacancyLevelManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LeaveStatusId = table.Column<Guid>(nullable: false),
                    LeavePeriodId = table.Column<Guid>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    LeavesEntitlementId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LeaveStartDate = table.Column<DateTime>(nullable: false),
                    LeaveEndDate = table.Column<DateTime>(nullable: false),
                    ReasonForApply = table.Column<string>(nullable: true),
                    Approvedby = table.Column<Guid>(nullable: false),
                    IsSubmitted = table.Column<bool>(nullable: false),
                    ManagerComment = table.Column<string>(nullable: true),
                    IsSave = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeaveDetail_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeaveDetail_LeavePeriod",
                        column: x => x.LeavePeriodId,
                        principalTable: "LeavePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeaveDetail_LeaveStatus",
                        column: x => x.LeaveStatusId,
                        principalTable: "LeaveStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeaveDetail_EmployeeLeavesEntitlement",
                        column: x => x.LeavesEntitlementId,
                        principalTable: "EmployeeLeavesEntitlement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_EmployeeLeaveDetail_Manager",
                        column: x => x.ManagerId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingInviteApprover",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    ExpenseBookingApproverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingInviteApprover", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_BookingRequest_Invite_Manager",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseBookingInviteApprover_ExpenseBookingApprover_ExpenseBookingApproverId",
                        column: x => x.ExpenseBookingApproverId,
                        principalTable: "ExpenseBookingApprover",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ApproverComment = table.Column<string>(nullable: true),
                    EmployeeComment = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    ExpenseBookingApproverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseBookingRequestDetails_ExpenseBookingApprover_ExpenseBookingApproverId",
                        column: x => x.ExpenseBookingApproverId,
                        principalTable: "ExpenseBookingApprover",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpFeedbackDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpFeedbackId = table.Column<Guid>(nullable: false),
                    PerformanceConfigFeedbackId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpFeedbackDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpFeedbackDetail_PerformanceConfigFeedback",
                        column: x => x.PerformanceConfigFeedbackId,
                        principalTable: "PerformanceConfigFeedback",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpFeedbackDetail_PerformanceEmpFeedback",
                        column: x => x.PerformanceEmpFeedbackId,
                        principalTable: "PerformanceEmpFeedback",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpGoalNextYear",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceEmpGoalId = table.Column<Guid>(nullable: false),
                    PerformanceGoalMeasureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpGoalNextYear", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpGoalNextYear_PerformanceEmpGoal",
                        column: x => x.PerformanceEmpGoalId,
                        principalTable: "PerformanceEmpGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpGoalNextYear_PerformanceGoalMeasure",
                        column: x => x.PerformanceGoalMeasureId,
                        principalTable: "PerformanceGoalMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpYearGoalDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceGoalMeasureId = table.Column<Guid>(nullable: false),
                    PerformanceEmpYearGoalId = table.Column<Guid>(nullable: false),
                    EmployeeComment = table.Column<string>(nullable: true),
                    ManagerComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpYearGoalDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpYearGoalDetail_PerformanceEmpYearGoal",
                        column: x => x.PerformanceEmpYearGoalId,
                        principalTable: "PerformanceEmpYearGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpYearGoalDetail_PerformanceGoalMeasure",
                        column: x => x.PerformanceGoalMeasureId,
                        principalTable: "PerformanceGoalMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceGoalMeasureFunc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FunctionalGroupId = table.Column<Guid>(nullable: false),
                    PerformanceGoalMeasureId = table.Column<Guid>(nullable: false),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceGoalMeasureFunc", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMeasureFunc_FunctionalGroup",
                        column: x => x.FunctionalGroupId,
                        principalTable: "FunctionalGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMeasureFunc_PerformanceGoalMeasure",
                        column: x => x.PerformanceGoalMeasureId,
                        principalTable: "PerformanceGoalMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceGoalMeasureIndiv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    PerformanceGoalMeasureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceGoalMeasureIndiv", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMeasureIndiv_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceGoalMeasureIndiv_PerformanceGoalMeasure",
                        column: x => x.PerformanceGoalMeasureId,
                        principalTable: "PerformanceGoalMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetUserDetailProjectHour",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    TimesheetUserDetailId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Hour = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetUserDetailProjectHour", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetUserDetailProjectHour_Project",
                        column: x => x.ProjectId,
                        principalTable: "TimesheetProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_TimesheetUserDetailProjectHour_TimesheetUserDetail",
                        column: x => x.TimesheetUserDetailId,
                        principalTable: "TimesheetUserDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplicationSkillQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    JobCandidateInterviewId = table.Column<Guid>(nullable: false),
                    JobVacancyLevelSkillQuestionId = table.Column<Guid>(nullable: false),
                    ObtainedWeightage = table.Column<string>(nullable: true),
                    Weightage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationSkillQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplicationSkillQuestion_JobCandidateInterview_JobCandidateInterviewId",
                        column: x => x.JobCandidateInterviewId,
                        principalTable: "JobCandidateInterview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobApplicationSkillQuestion_JobVacancyLevelSkillQuestion_JobVacancyLevelSkillQuestionId",
                        column: x => x.JobVacancyLevelSkillQuestionId,
                        principalTable: "JobVacancyLevelSkillQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseBookingRequestDetailInvite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ApproverComment = table.Column<string>(nullable: true),
                    EmployeeComment = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    ExpenseBookingInviteApproverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBookingRequestDetailInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseBookingRequestDetailInvite_ExpenseBookingInviteApprover_ExpenseBookingInviteApproverId",
                        column: x => x.ExpenseBookingInviteApproverId,
                        principalTable: "ExpenseBookingInviteApprover",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEmpMidYearGoalDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PerformanceGoalMeasureId = table.Column<Guid>(nullable: false),
                    PerformanceEmpMidYearGoalId = table.Column<Guid>(nullable: false),
                    EmployeeComment = table.Column<string>(nullable: true),
                    ManagerComment = table.Column<string>(nullable: true),
                    PerformanceEmpYearGoalDetailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEmpMidYearGoalDetail", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpMidYearGoalDetail_PerformanceEmpMidYearGoal",
                        column: x => x.PerformanceEmpMidYearGoalId,
                        principalTable: "PerformanceEmpMidYearGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceEmpMidYearGoalDetail_PerformanceEmpYearGoalDetail_PerformanceEmpYearGoalDetailId",
                        column: x => x.PerformanceEmpYearGoalDetailId,
                        principalTable: "PerformanceEmpYearGoalDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_PerformanceEmpMidYearGoalDetail_PerformanceGoalMeasure",
                        column: x => x.PerformanceGoalMeasureId,
                        principalTable: "PerformanceGoalMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationModuleDetail_ApplicationModuleId",
                table: "ApplicationModuleDetail",
                column: "ApplicationModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PictureId",
                table: "AspNetUsers",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BandId",
                table: "Employee",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DesignationId",
                table: "Employee",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_GroupId",
                table: "Employee",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_TitleId",
                table: "Employee",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveDetail_EmployeeId",
                table: "EmployeeLeaveDetail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveDetail_LeavePeriodId",
                table: "EmployeeLeaveDetail",
                column: "LeavePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveDetail_LeaveStatusId",
                table: "EmployeeLeaveDetail",
                column: "LeaveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveDetail_LeavesEntitlementId",
                table: "EmployeeLeaveDetail",
                column: "LeavesEntitlementId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveDetail_ManagerId",
                table: "EmployeeLeaveDetail",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_EmployeeId",
                table: "EmployeeLeaves",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_LeavePeriodId",
                table: "EmployeeLeaves",
                column: "LeavePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeavesEntitlement_EmployeeLeavesId",
                table: "EmployeeLeavesEntitlement",
                column: "EmployeeLeavesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeavesEntitlement_LeaveRulesId",
                table: "EmployeeLeavesEntitlement",
                column: "LeaveRulesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePersonalDetail_EmployeeId",
                table: "EmployeePersonalDetail",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProxy_EmployeeId",
                table: "EmployeeProxy",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProxy_ProxyForId",
                table: "EmployeeProxy",
                column: "ProxyForId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualificationDetail_EmployeeId",
                table: "EmployeeQualificationDetail",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingApprover_ExpenseBookingRequestId",
                table: "ExpenseBookingApprover",
                column: "ExpenseBookingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingApprover_ManagerId",
                table: "ExpenseBookingApprover",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingCategory_Name",
                table: "ExpenseBookingCategory",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingInviteApprover_EmployeeId",
                table: "ExpenseBookingInviteApprover",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingInviteApprover_ExpenseBookingApproverId",
                table: "ExpenseBookingInviteApprover",
                column: "ExpenseBookingApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingRequest_DepartmentID",
                table: "ExpenseBookingRequest",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingRequest_EmployeeId",
                table: "ExpenseBookingRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingRequest_ExpenseBookingSubCategoryItemId",
                table: "ExpenseBookingRequest",
                column: "ExpenseBookingSubCategoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingRequestDetailInvite_ExpenseBookingInviteApproverId",
                table: "ExpenseBookingRequestDetailInvite",
                column: "ExpenseBookingInviteApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingRequestDetails_ExpenseBookingApproverId",
                table: "ExpenseBookingRequestDetails",
                column: "ExpenseBookingApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingSubCategory_ExpenseBookingCategoryId",
                table: "ExpenseBookingSubCategory",
                column: "ExpenseBookingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingSubCategory_Name",
                table: "ExpenseBookingSubCategory",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingSubCategoryItem_ExpenseBookingSubCategoryId",
                table: "ExpenseBookingSubCategoryItem",
                column: "ExpenseBookingSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingSubCategoryItem_Name",
                table: "ExpenseBookingSubCategoryItem",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingTitleAmount_TilteID",
                table: "ExpenseBookingTitleAmount",
                column: "TilteID",
                unique: true,
                filter: "[TilteID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBookingTitleAmount_TitleID",
                table: "ExpenseBookingTitleAmount",
                column: "TitleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDocument_ExpenseBookingId",
                table: "ExpenseDocument",
                column: "ExpenseBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDocument_PictureId",
                table: "ExpenseDocument",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalBand_Name",
                table: "FunctionalBand",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalDepartment_Name",
                table: "FunctionalDepartment",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalDesignation_Name",
                table: "FunctionalDesignation",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalGroup_DepartmentId",
                table: "FunctionalGroup",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalGroup_Name",
                table: "FunctionalGroup",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalTitle_Name",
                table: "FunctionalTitle",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobCandidateProfileId",
                table: "JobApplication",
                column: "JobCandidateProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobVacancyId",
                table: "JobApplication",
                column: "JobVacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationHRQuestions_JobApplicationId",
                table: "JobApplicationHRQuestions",
                column: "JobApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationHRQuestions_JobHRQuestionId",
                table: "JobApplicationHRQuestions",
                column: "JobHRQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationScreeningQuestion_JobApplicationId",
                table: "JobApplicationScreeningQuestion",
                column: "JobApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationScreeningQuestion_JobScreeningQuestionId",
                table: "JobApplicationScreeningQuestion",
                column: "JobScreeningQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationSkillQuestion_JobCandidateInterviewId",
                table: "JobApplicationSkillQuestion",
                column: "JobCandidateInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationSkillQuestion_JobVacancyLevelSkillQuestionId",
                table: "JobApplicationSkillQuestion",
                column: "JobVacancyLevelSkillQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateInterview_JobApplicationId",
                table: "JobCandidateInterview",
                column: "JobApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateInterview_JobInterviewTypeId",
                table: "JobCandidateInterview",
                column: "JobInterviewTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateInterview_JobVacancyLevelManagerId",
                table: "JobCandidateInterview",
                column: "JobVacancyLevelManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateProfile_ResumeId",
                table: "JobCandidateProfile",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateProfile_UserId",
                table: "JobCandidateProfile",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateQualification_JobCandidateProfilesId",
                table: "JobCandidateQualification",
                column: "JobCandidateProfilesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateWorkExperience_JobCandidateProfilesId",
                table: "JobCandidateWorkExperience",
                column: "JobCandidateProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDocument_JobCandidateProfileId",
                table: "JobDocument",
                column: "JobCandidateProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHRQuestion_JobVacancyId",
                table: "JobHRQuestion",
                column: "JobVacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobInterviewType_Name",
                table: "JobInterviewType",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobScreeningQuestion_JobVacancyId",
                table: "JobScreeningQuestion",
                column: "JobVacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkillQuestion_JobVacancyId",
                table: "JobSkillQuestion",
                column: "JobVacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobType_Name",
                table: "JobType",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancy_JobTypeId",
                table: "JobVacancy",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancyLevel_JobVacancyId",
                table: "JobVacancyLevel",
                column: "JobVacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancyLevelManager_EmployeeId",
                table: "JobVacancyLevelManager",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancyLevelManager_JobVacancyLevelId",
                table: "JobVacancyLevelManager",
                column: "JobVacancyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancyLevelSkillQuestion_JobSkillQuestionId",
                table: "JobVacancyLevelSkillQuestion",
                column: "JobSkillQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancyLevelSkillQuestion_JobVacancyLevelId",
                table: "JobVacancyLevelSkillQuestion",
                column: "JobVacancyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHolidayList_LeavePeriodId",
                table: "LeaveHolidayList",
                column: "LeavePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHolidayList_Name",
                table: "LeaveHolidayList",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LeavePeriod_Name",
                table: "LeavePeriod",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_BandId",
                table: "LeaveRules",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_LeavePeriodId",
                table: "LeaveRules",
                column: "LeavePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_LeaveTypeId",
                table: "LeaveRules",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRules_Name",
                table: "LeaveRules",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveStatus_Name",
                table: "LeaveStatus",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveType_LeavePeriodId",
                table: "LeaveType",
                column: "LeavePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveType_Name",
                table: "LeaveType",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveWorkingDay_LeavePeriodId",
                table: "LeaveWorkingDay",
                column: "LeavePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_MassInterviewDetail_MassInterviewId",
                table: "MassInterviewDetail",
                column: "MassInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_MassInterviewPanel_InterviewDateId",
                table: "MassInterviewPanel",
                column: "InterviewDateId");

            migrationBuilder.CreateIndex(
                name: "IX_MassInterviewPanel_VacancyId",
                table: "MassInterviewPanel",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_MassInterviewPanelVacancy_EmployeeId",
                table: "MassInterviewPanelVacancy",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MassInterviewPanelVacancy_PanelId",
                table: "MassInterviewPanelVacancy",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_MassInterviewScheduleCandidate_CandidateId",
                table: "MassInterviewScheduleCandidate",
                column: "CandidateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MassInterviewScheduleCandidate_PanelId",
                table: "MassInterviewScheduleCandidate",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_MassInterviewScheduleCandidate_RoomId",
                table: "MassInterviewScheduleCandidate",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                table: "OpenIddictTokens",
                column: "ReferenceId",
                unique: true,
                filter: "[ReferenceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpDeltas_PerformanceEmpGoalId",
                table: "PerformanceEmpDeltas",
                column: "PerformanceEmpGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpDevGoal_PerformanceEmpGoalId",
                table: "PerformanceEmpDevGoal",
                column: "PerformanceEmpGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpDevGoalDoc_PerformanceEmpGoalId",
                table: "PerformanceEmpDevGoalDoc",
                column: "PerformanceEmpGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpFeedback_EmployeeId",
                table: "PerformanceEmpFeedback",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpFeedback_PerformanceEmpGoalId",
                table: "PerformanceEmpFeedback",
                column: "PerformanceEmpGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpFeedbackDetail_PerformanceConfigFeedbackId",
                table: "PerformanceEmpFeedbackDetail",
                column: "PerformanceConfigFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpFeedbackDetail_PerformanceEmpFeedbackId",
                table: "PerformanceEmpFeedbackDetail",
                column: "PerformanceEmpFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpGoal_ManagerId",
                table: "PerformanceEmpGoal",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpGoal_PerformanceStatusId",
                table: "PerformanceEmpGoal",
                column: "PerformanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpGoal_PerformanceYearId",
                table: "PerformanceEmpGoal",
                column: "PerformanceYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpGoalNextYear_PerformanceEmpGoalId",
                table: "PerformanceEmpGoalNextYear",
                column: "PerformanceEmpGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpGoalNextYear_PerformanceGoalMeasureId",
                table: "PerformanceEmpGoalNextYear",
                column: "PerformanceGoalMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpGoalPresident_PerformanceEmpGoalId",
                table: "PerformanceEmpGoalPresident",
                column: "PerformanceEmpGoalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpInitialRating_PerformanceYearId",
                table: "PerformanceEmpInitialRating",
                column: "PerformanceYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpMidYearGoal_PerformanceEmpGoalId",
                table: "PerformanceEmpMidYearGoal",
                column: "PerformanceEmpGoalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpMidYearGoal_PerformanceStatusId",
                table: "PerformanceEmpMidYearGoal",
                column: "PerformanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpMidYearGoalDetail_PerformanceEmpMidYearGoalId",
                table: "PerformanceEmpMidYearGoalDetail",
                column: "PerformanceEmpMidYearGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpMidYearGoalDetail_PerformanceEmpYearGoalDetailId",
                table: "PerformanceEmpMidYearGoalDetail",
                column: "PerformanceEmpYearGoalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpMidYearGoalDetail_PerformanceGoalMeasureId",
                table: "PerformanceEmpMidYearGoalDetail",
                column: "PerformanceGoalMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpPluses_PerformanceEmpGoalId",
                table: "PerformanceEmpPluses",
                column: "PerformanceEmpGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpTrainingClasses_PerformanceEmpGoalId",
                table: "PerformanceEmpTrainingClasses",
                column: "PerformanceEmpGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpYearGoal_PerformanceEmpGoalId",
                table: "PerformanceEmpYearGoal",
                column: "PerformanceEmpGoalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpYearGoal_PerformanceStatusId",
                table: "PerformanceEmpYearGoal",
                column: "PerformanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpYearGoalDetail_PerformanceEmpYearGoalId",
                table: "PerformanceEmpYearGoalDetail",
                column: "PerformanceEmpYearGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEmpYearGoalDetail_PerformanceGoalMeasureId",
                table: "PerformanceEmpYearGoalDetail",
                column: "PerformanceGoalMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoal_EmployeeId",
                table: "PerformanceGoal",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoal_PerformanceYearId",
                table: "PerformanceGoal",
                column: "PerformanceYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMain_ManagerId",
                table: "PerformanceGoalMain",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMain_PerformanceStatusId",
                table: "PerformanceGoalMain",
                column: "PerformanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMain_PerformanceYearId",
                table: "PerformanceGoalMain",
                column: "PerformanceYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMeasure_PerformanceGoalId",
                table: "PerformanceGoalMeasure",
                column: "PerformanceGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMeasure_PerformanceGoalMainId",
                table: "PerformanceGoalMeasure",
                column: "PerformanceGoalMainId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMeasureFunc_FunctionalGroupId",
                table: "PerformanceGoalMeasureFunc",
                column: "FunctionalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMeasureFunc_PerformanceGoalMeasureId",
                table: "PerformanceGoalMeasureFunc",
                column: "PerformanceGoalMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMeasureIndiv_EmployeeId",
                table: "PerformanceGoalMeasureIndiv",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoalMeasureIndiv_PerformanceGoalMeasureId",
                table: "PerformanceGoalMeasureIndiv",
                column: "PerformanceGoalMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceInitailRating_EmployeeId",
                table: "PerformanceInitailRating",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceInitailRating_PerformanceConfigRatingId",
                table: "PerformanceInitailRating",
                column: "PerformanceConfigRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceInitailRating_PerformanceYearId",
                table: "PerformanceInitailRating",
                column: "PerformanceYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformancePresidentCouncil_EmployeeId",
                table: "PerformancePresidentCouncil",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformancePresidentCouncil_PerformanceYearId",
                table: "PerformancePresidentCouncil",
                column: "PerformanceYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesCompanyContact_SalesCompanyId",
                table: "SalesCompanyContact",
                column: "SalesCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDailyCall_SalesCompanyId",
                table: "SalesDailyCall",
                column: "SalesCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDailyCall_SalesStatusId",
                table: "SalesDailyCall",
                column: "SalesStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_SalesScheduleMeetingId",
                table: "SalesDocument",
                column: "SalesScheduleMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesMinuteMeeting_SalesScheduleMeetingId",
                table: "SalesMinuteMeeting",
                column: "SalesScheduleMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesMinuteMeetingInternal_EmployeeId",
                table: "SalesMinuteMeetingInternal",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesMinuteMeetingInternal_SalesMinuteMeetingId",
                table: "SalesMinuteMeetingInternal",
                column: "SalesMinuteMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesScheduleMeeting_SalesCompanyId",
                table: "SalesScheduleMeeting",
                column: "SalesCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesScheduleMeetingExternal_SalesCompanyContactId",
                table: "SalesScheduleMeetingExternal",
                column: "SalesCompanyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesScheduleMeetingExternal_SalesScheduleMeetingId",
                table: "SalesScheduleMeetingExternal",
                column: "SalesScheduleMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesScheduleMeetingInternal_EmployeeId",
                table: "SalesScheduleMeetingInternal",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesScheduleMeetingInternal_SalesScheduleMeetingId",
                table: "SalesScheduleMeetingInternal",
                column: "SalesScheduleMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetClient_Contact",
                table: "TimesheetClient",
                column: "Contact",
                unique: true,
                filter: "[Contact] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetClient_EmailId",
                table: "TimesheetClient",
                column: "EmailId",
                unique: true,
                filter: "[EmailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetClient_Name",
                table: "TimesheetClient",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetEmployeeProject_EmployeeId",
                table: "TimesheetEmployeeProject",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetEmployeeProject_TimeSheetProjectId",
                table: "TimesheetEmployeeProject",
                column: "TimeSheetProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetProject_ClientId",
                table: "TimesheetProject",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetProject_ManagerId",
                table: "TimesheetProject",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetProject_ProjectName",
                table: "TimesheetProject",
                column: "ProjectName",
                unique: true,
                filter: "[ProjectName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetTemplate_TempalteName",
                table: "TimesheetTemplate",
                column: "TempalteName",
                unique: true,
                filter: "[TempalteName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetTemplate_TimesheetFrequencyId",
                table: "TimesheetTemplate",
                column: "TimesheetFrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetUserDetail_ApproverlId",
                table: "TimesheetUserDetail",
                column: "ApproverlId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetUserDetail_TimesheetUserSpanId",
                table: "TimesheetUserDetail",
                column: "TimesheetUserSpanId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetUserDetailProjectHour_ProjectId",
                table: "TimesheetUserDetailProjectHour",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetUserDetailProjectHour_TimesheetUserDetailId",
                table: "TimesheetUserDetailProjectHour",
                column: "TimesheetUserDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetUserSchedule_EmployeeId",
                table: "TimesheetUserSchedule",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetUserSchedule_TimesheetTemplateId",
                table: "TimesheetUserSchedule",
                column: "TimesheetTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetUserSpan_EmployeeId",
                table: "TimesheetUserSpan",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationModuleDetail");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveDetail");

            migrationBuilder.DropTable(
                name: "EmployeePersonalDetail");

            migrationBuilder.DropTable(
                name: "EmployeeProfessionalDetail");

            migrationBuilder.DropTable(
                name: "EmployeeProxy");

            migrationBuilder.DropTable(
                name: "EmployeeQualificationDetail");

            migrationBuilder.DropTable(
                name: "ExcelEmployeeData");

            migrationBuilder.DropTable(
                name: "ExpenseBookingRequestDetailInvite");

            migrationBuilder.DropTable(
                name: "ExpenseBookingRequestDetails");

            migrationBuilder.DropTable(
                name: "ExpenseBookingTitleAmount");

            migrationBuilder.DropTable(
                name: "ExpenseDocument");

            migrationBuilder.DropTable(
                name: "JobApplicationHRQuestions");

            migrationBuilder.DropTable(
                name: "JobApplicationScreeningQuestion");

            migrationBuilder.DropTable(
                name: "JobApplicationSkillQuestion");

            migrationBuilder.DropTable(
                name: "JobCandidateQualification");

            migrationBuilder.DropTable(
                name: "JobCandidateWorkExperience");

            migrationBuilder.DropTable(
                name: "JobDocument");

            migrationBuilder.DropTable(
                name: "LeaveHolidayList");

            migrationBuilder.DropTable(
                name: "LeaveWorkingDay");

            migrationBuilder.DropTable(
                name: "MassInterviewPanelVacancy");

            migrationBuilder.DropTable(
                name: "MassInterviewScheduleCandidate");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "PerformanceConfig");

            migrationBuilder.DropTable(
                name: "PerformanceEmpDeltas");

            migrationBuilder.DropTable(
                name: "PerformanceEmpDevGoal");

            migrationBuilder.DropTable(
                name: "PerformanceEmpDevGoalDoc");

            migrationBuilder.DropTable(
                name: "PerformanceEmpFeedbackDetail");

            migrationBuilder.DropTable(
                name: "PerformanceEmpGoalNextYear");

            migrationBuilder.DropTable(
                name: "PerformanceEmpGoalPresident");

            migrationBuilder.DropTable(
                name: "PerformanceEmpInitialRating");

            migrationBuilder.DropTable(
                name: "PerformanceEmpMidYearGoalDetail");

            migrationBuilder.DropTable(
                name: "PerformanceEmpPluses");

            migrationBuilder.DropTable(
                name: "PerformanceEmpTrainingClasses");

            migrationBuilder.DropTable(
                name: "PerformanceGoalMeasureFunc");

            migrationBuilder.DropTable(
                name: "PerformanceGoalMeasureIndiv");

            migrationBuilder.DropTable(
                name: "PerformanceInitailRating");

            migrationBuilder.DropTable(
                name: "PerformancePresidentCouncil");

            migrationBuilder.DropTable(
                name: "SalesDailyCall");

            migrationBuilder.DropTable(
                name: "SalesDocument");

            migrationBuilder.DropTable(
                name: "SalesMinuteMeetingInternal");

            migrationBuilder.DropTable(
                name: "SalesScheduleMeetingExternal");

            migrationBuilder.DropTable(
                name: "SalesScheduleMeetingInternal");

            migrationBuilder.DropTable(
                name: "TimesheetEmployeeProject");

            migrationBuilder.DropTable(
                name: "TimesheetUserDetailProjectHour");

            migrationBuilder.DropTable(
                name: "TimesheetUserSchedule");

            migrationBuilder.DropTable(
                name: "TimesheetWorkingDays");

            migrationBuilder.DropTable(
                name: "ApplicationModule");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "LeaveStatus");

            migrationBuilder.DropTable(
                name: "EmployeeLeavesEntitlement");

            migrationBuilder.DropTable(
                name: "ExpenseBookingInviteApprover");

            migrationBuilder.DropTable(
                name: "JobHRQuestion");

            migrationBuilder.DropTable(
                name: "JobScreeningQuestion");

            migrationBuilder.DropTable(
                name: "JobCandidateInterview");

            migrationBuilder.DropTable(
                name: "JobVacancyLevelSkillQuestion");

            migrationBuilder.DropTable(
                name: "MassInterviewPanel");

            migrationBuilder.DropTable(
                name: "MassInterviewRoom");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "PerformanceConfigFeedback");

            migrationBuilder.DropTable(
                name: "PerformanceEmpFeedback");

            migrationBuilder.DropTable(
                name: "PerformanceEmpMidYearGoal");

            migrationBuilder.DropTable(
                name: "PerformanceEmpYearGoalDetail");

            migrationBuilder.DropTable(
                name: "PerformanceConfigRating");

            migrationBuilder.DropTable(
                name: "SalesStatus");

            migrationBuilder.DropTable(
                name: "SalesMinuteMeeting");

            migrationBuilder.DropTable(
                name: "SalesCompanyContact");

            migrationBuilder.DropTable(
                name: "TimesheetProject");

            migrationBuilder.DropTable(
                name: "TimesheetUserDetail");

            migrationBuilder.DropTable(
                name: "TimesheetTemplate");

            migrationBuilder.DropTable(
                name: "EmployeeLeaves");

            migrationBuilder.DropTable(
                name: "LeaveRules");

            migrationBuilder.DropTable(
                name: "ExpenseBookingApprover");

            migrationBuilder.DropTable(
                name: "JobApplication");

            migrationBuilder.DropTable(
                name: "JobInterviewType");

            migrationBuilder.DropTable(
                name: "JobVacancyLevelManager");

            migrationBuilder.DropTable(
                name: "JobSkillQuestion");

            migrationBuilder.DropTable(
                name: "MassInterviewDetail");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.DropTable(
                name: "PerformanceEmpYearGoal");

            migrationBuilder.DropTable(
                name: "PerformanceGoalMeasure");

            migrationBuilder.DropTable(
                name: "SalesScheduleMeeting");

            migrationBuilder.DropTable(
                name: "TimesheetClient");

            migrationBuilder.DropTable(
                name: "TimesheetUserSpan");

            migrationBuilder.DropTable(
                name: "TimesheetConfiguration");

            migrationBuilder.DropTable(
                name: "LeaveType");

            migrationBuilder.DropTable(
                name: "ExpenseBookingRequest");

            migrationBuilder.DropTable(
                name: "JobCandidateProfile");

            migrationBuilder.DropTable(
                name: "JobVacancyLevel");

            migrationBuilder.DropTable(
                name: "MassInterview");

            migrationBuilder.DropTable(
                name: "PerformanceEmpGoal");

            migrationBuilder.DropTable(
                name: "PerformanceGoal");

            migrationBuilder.DropTable(
                name: "PerformanceGoalMain");

            migrationBuilder.DropTable(
                name: "SalesCompany");

            migrationBuilder.DropTable(
                name: "LeavePeriod");

            migrationBuilder.DropTable(
                name: "ExpenseBookingSubCategoryItem");

            migrationBuilder.DropTable(
                name: "JobVacancy");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "PerformanceStatus");

            migrationBuilder.DropTable(
                name: "PerformanceYear");

            migrationBuilder.DropTable(
                name: "ExpenseBookingSubCategory");

            migrationBuilder.DropTable(
                name: "JobType");

            migrationBuilder.DropTable(
                name: "FunctionalBand");

            migrationBuilder.DropTable(
                name: "FunctionalDesignation");

            migrationBuilder.DropTable(
                name: "FunctionalGroup");

            migrationBuilder.DropTable(
                name: "FunctionalTitle");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ExpenseBookingCategory");

            migrationBuilder.DropTable(
                name: "FunctionalDepartment");

            migrationBuilder.DropTable(
                name: "Picture");
        }
    }
}
