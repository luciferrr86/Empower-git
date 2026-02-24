using Microsoft.EntityFrameworkCore.Migrations;

namespace A4.Empower.Migrations
{
    public partial class StoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp_UspCheckDateRange = @"create procedure [dbo].[uspCheckDateRange]
        (
        @startdate datetime,
        @enddate datetime,
        @loginId uniqueidentifier,
        @isDate BIT output
        )
AS
BEGIN
 
        declare @check BIT 
        Begin
         If exists
        (select * from EmployeeLeaveDetail
         WHERE ((LeaveStartDate BETWEEN @startdate AND @enddate) 
     OR (LeaveEndDate BETWEEN @startdate AND @enddate) )and IsActive = 1 and EmployeeId = @loginId and LeaveStatusId not in (select  Id from LeaveStatus WHERE Name = 'Rejected' OR Name =  'Cancelled' ) )
     set @check=0
     Else
     set @check=1
     End

   -- select @check = @isDate
   select @isDate = @check

END";
            migrationBuilder.Sql(sp_UspCheckDateRange);

            var sp_uspGetAboveLevelEmployee = @"CREATE procedure [dbo].[uspGetAboveLevelEmployee]

@ReportingHead uniqueidentifier,
@LoginUserID uniqueidentifier

as

begin



WITH EmployeeCTE AS (

--ANCHOR

select * from (SELECT em.Id as EmpID,em.ManagerId  as ManagerID FROM Employee em where em.ManagerId=@ReportingHead ) as temp where EmpID =@LoginUserID
union all

	--Recursive Memeber:-

    SELECT em.Id as EmpID,

    em.ManagerId   as ManagerID

    FROM Employee em


	inner join EmployeeCTE as ect on em.Id =ect.ManagerID

	where em.Id!=em.ManagerId

)

SELECT * FROM EmployeeCTE 

end";
            migrationBuilder.Sql(sp_uspGetAboveLevelEmployee);

            var sp_uspGetBelowLevelEmployee = @"create procedure [dbo].[uspGetBelowLevelEmployee]
@LoginUserID uniqueidentifier,
@ReportingHead uniqueidentifier
as
begin
WITH EmployeeCTE AS (
    select * from (SELECT em.Id as EmpId,em.IsActive as Active,em.ManagerId  as ManagerId,0 AS [EmpLevel],em.Id as [Root],aspuser.FullName as Name,desg.Name as [FunctionalDesignation],fngroup.Name as [FunctionalGroup]
    FROM Employee em 
	--Join Employee Table Name:-
	inner join FunctionalDesignation desg on em.DesignationId=desg.Id
    inner join FunctionalGroup fngroup on em.GroupId=fngroup.Id
	inner join AspNetUsers aspuser on em.UserId=aspuser.Id
	where em.ManagerId=@ReportingHead) as temp where EmpID =@LoginUserID and Active=1
	union all
	--Recursive Memeber:-
   SELECT em.Id as EmpId,em.IsActive as Active,em.ManagerId  as ManagerId,ect.EmpLevel+1 AS [EmpLevel],ect.Root as [ROOT],aspuser.FullName as Name,desg.Name as [FunctionalDesignation],fngroup.Name as [FunctionalGroup]
    FROM Employee em
--Join Employee Table Name:-
    inner join FunctionalDesignation desg on em.DesignationId=desg.Id
    inner join FunctionalGroup fngroup on em.GroupId=fngroup.Id
	inner join AspNetUsers aspuser on em.UserId=aspuser.Id
	inner join EmployeeCTE as ect on em.ManagerId=ect.EmpId
	where em.Id!=em.ManagerId
)
SELECT * FROM EmployeeCTE where EmpId !=@LoginUserID and Active=1  order by EmpLevel asc

end";
            migrationBuilder.Sql(sp_uspGetBelowLevelEmployee);
            
            var sp_uspGetEmpPerformanceGoalMeasure = @"create procedure [dbo].[uspGetEmpPerformanceGoalMeasure]

@empId uniqueidentifier,

@perfromanceYearId uniqueidentifier



as

begin



declare @ReportingHead uniqueidentifier

declare @functionalGroup uniqueidentifier

set @ReportingHead=(select ManagerId from Employee where Id=@empId and IsActive=1)

set @functionalGroup=(select GroupId from Employee where Id=@empId and IsActive=1)

declare @temp table (

    idx int identity(1,1),

    EmpID uniqueidentifier,

    MnanagerID uniqueidentifier)



insert into @temp (EmpID, MnanagerID)

exec  uspGetAboveLevelEmployee @ReportingHead , @empId

declare @temp1 table (

    ID uniqueidentifier,

    QuadID uniqueidentifier

	)



declare @counter int

declare @RowCount int



--To get level

declare @ManagerID uniqueidentifier

declare @TopManagerID uniqueidentifier

declare @levelId uniqueidentifier



set @counter = 1

set @RowCount=(select count(idx) from @temp)

while @counter <= @RowCount

begin



set @ManagerID=(select MnanagerID from @temp where idx=@counter)

set @TopManagerID=(select ManagerId from Employee where Id=@ManagerID and IsActive=1)

INSERT INTO @temp1 (QuadID) 

    select perMesaure.Id from PerformanceGoalMeasure perMesaure

	inner join PerformanceGoalMeasureFunc perMeasFun on perMesaure.Id=perMeasFun.PerformanceGoalMeasureId

	where PerformanceGoalMainId= (select Id from PerformanceGoalMain where ManagerId=(select MnanagerID from @temp where idx=@counter) and PerformanceYearId=@perfromanceYearId)

	and perMeasFun.FunctionalGroupId=@functionalGroup and (perMeasFun.Level >=(SELECT dbo.fnLevel(@ManagerID,@TopManagerID,@empId)) ) and perMesaure.IsActive=1 and perMeasFun.IsActive=1	 

   set @counter = @counter + 1

end

insert into @temp1(QuadID)

 select PerformanceGoalMeasureId from PerformanceGoalMeasureIndiv  perind

 inner join PerformanceGoalMeasure perMesaure on perind.PerformanceGoalMeasureId=perMesaure.Id

 inner join PerformanceGoalMain permain on perMesaure.PerformanceGoalMainId= permain.Id 

 where EmployeeId=@empId and perind.IsActive=1 and permain.PerformanceYearId=@perfromanceYearId and perMesaure.IsActive=1

select * from @temp1


end"; 
            migrationBuilder.Sql(sp_uspGetEmpPerformanceGoalMeasure);
           
			var sp_uspGetExpenseLevelEmployee = @"CREATE procedure [dbo].[uspGetExpenseLevelEmployee]

@ReportingHead uniqueidentifier,
@LoginUserID uniqueidentifier

as

begin



WITH EmployeeCTE AS (

--ANCHOR

select * from (SELECT em.Id as EmpID,em.ManagerId  as ManagerID,0 AS [EMPLEVEL],em.TitleId as TitleId FROM Employee em where em.ManagerId=@ReportingHead ) as temp where EmpID =@LoginUserID
union all

	--Recursive Memeber:-

    SELECT em.Id as EmpID,

    em.ManagerId   as ManagerID,ect.EMPLEVEL+1,em.TitleId as TitleId

    FROM Employee em


	inner join EmployeeCTE as ect on em.Id =ect.ManagerID

	where em.Id!=em.ManagerId

)

SELECT * FROM EmployeeCTE 

end";
            migrationBuilder.Sql(sp_uspGetExpenseLevelEmployee);

            var sp_uspGetOrgData = @"CREATE procedure [dbo].[uspGetOrgData]
@LoginUserID int,
@ReportingHead int
as
begin
WITH EmployeeCTE AS (
    select * from (SELECT em.iID as EmpID,em.sReportingHead  as ManagerID,0 AS [EMPLEVEL],em.sFirstName+' '+em.sLastName as Name,desg.sName as [FunctionalDesignation],mgr.sFirstName+' '+mgr.sLastName as ManagerName
    FROM cEmployee em 
	--Join Employee Table Name:-
	inner join cFunctionalDesignation desg on em.objFunctionalDesignation=desg.iID
	inner join cEmployee mgr on em.sReportingHead=mgr.iID
	where em.sReportingHead=@ReportingHead) as temp where EmpID =@LoginUserID
	union all
	--Recursive Memeber:-
    SELECT em.iID as EmpID,em.sReportingHead  as ManagerID,ect.EMPLEVEL+1,em.sFirstName+' '+em.sLastName as Name,desg.sName as [FunctionalDesignation],mgr.sFirstName+' '+mgr.sLastName as ManagerName
    FROM cEmployee em
    inner join cEmployee mgr on em.sReportingHead=mgr.iID
    inner join cFunctionalDesignation desg on em.objFunctionalDesignation=desg.iID
	inner join EmployeeCTE as ect on em.sReportingHead=ect.EmpID
	where em.iID!=em.sReportingHead and em.bIsActive=1
)
SELECT * FROM EmployeeCTE where EmpID !=@LoginUserID 
end";
                migrationBuilder.Sql(sp_uspGetOrgData);

            var sp_uspGetTaskList = @"CREATE procedure [dbo].[uspGetTaskList]
@loginId uniqueIdentifier,
@quadId uniqueIdentifier
as
begin 
declare  @temp table(
id int identity,
Task varchar(1024),
EmpId uniqueIdentifier,
RedirectTo int
)
declare  @tempCEO table(
id int identity,
ManagerId uniqueIdentifier
)
declare  @tempReviewAssessment table(
id int identity,
EmpId uniqueIdentifier,
bIsEmpGoalSubmitted bit,
bIsMgrGoalSubmitted bit,
bIsEmpRatingSubmitted bit,
bIsMgrRatingSubmitted bit
)
declare  @tempPresidentSignoff table(
id int identity,
EmpId uniqueIdentifier
)
declare @empid uniqueIdentifier
declare @managerId uniqueIdentifier
declare @managerName varchar(100)
declare @empName varchar(100)
declare @reportinghead uniqueIdentifier
declare @check bit
declare @checkEmp bit
declare @Counter int
declare @RowCount int
declare @chkIsEmpGoalSubmitCount int
declare @chkIsEmpGoalSubmit int
declare @chkIsMgrReviewGoalSubmit int
declare @chkIsEmpRatingSubmit int
declare @chkIsMgrRatingSubmit int
declare @checkCEOSignOffEnable int
Declare @Id uniqueIdentifier
Declare @chkIsCEOSignoff bit
Declare @chkIsMidYearEnabled bit
declare @MidYearCompletionCount int
Declare @chkIsMidYearCompleted bit
declare @chkIsPresident bit
declare @empGoalId uniqueIdentifier
declare @presidentSignoff bit

--If LoginId is Top Manager-
 --if((select count(*) from cPerformanceGoalMain where iManagerId=3 and objPerformanceYear=1)>0) 
 --begin
 set @chkIsMidYearEnabled=(Select IsActive from  ApplicationModuleDetail where SubModuleName='Enable MidYear')
	set @MidYearCompletionCount=(Select COUNT(*) from PerformanceEmpMidYearGoal where IsMidYearReviewCompleted=0)
	set @chkIsMidYearCompleted=0
	if(@MidYearCompletionCount=0)
	begin
	set @chkIsMidYearCompleted=1
	end	
  set @reportinghead= (select ManagerId from Employee where UserId=@loginId)
  set @checkCEOSignOffEnable=(Select IsActive from  ApplicationModuleDetail where SubModuleName='Enable Initial rating')
if (@reportinghead=@loginId)
	 begin
	  set @check=(Select IsManagerReleased from  PerformanceGoalMain where ManagerId=@reportinghead and  PerformanceYearId=@quadId)
     if(@check=0)
	 begin
     INSERT INTO @temp (Task,EmpId,RedirectTo) values('Set Goal for your subordinates','',1)
     end
	--ceo manager signoff:-	
	if(@checkCEOSignOffEnable=1)
	begin				
    set  @RowCount=(select count(Id) from PerformanceInitailRating where PerformanceYearId=@quadId and IsCEOSignoff=0)
    if(@RowCount>0)
	begin				
    INSERT INTO @tempCEO select distinct ManagerId from PerformanceInitailRating where PerformanceYearId=@quadId and IsCEOSignoff=0
    end
	set @Counter=1
	while (@Counter <= @RowCount)
	begin
    set @managerId=(select ManagerId from @tempCEO where id=@Counter)
	set @check=(Select IsManagerReleased from  PerformanceGoalMain where ManagerId=@managerId and  PerformanceYearId=@quadId)
	if(@check=1)
	begin
	set @managerName=(select  FullName COLLATE Latin1_General_CI_AS from AspNetUsers where IsActive=1 and Id=(select UserId from Employee where Id=@managerId))
	INSERT INTO @temp (Task,EmpId,RedirectTo) values('Signoff review rating for '+@managerName+' ',@managerId,4)
	end
	set  @Counter=@Counter+1
	end
	end
    end
	else
    begin 
	set @check=(Select IsManagerReleased from  PerformanceGoalMain where ManagerId=@reportinghead and  PerformanceYearId=@quadId)
	set @checkEmp=(Select IsManagerReleased from  PerformanceGoalMain where ManagerId=@loginId and  PerformanceYearId=@quadId)
	if(@check=1 and @checkEmp=0 )
	begin
	INSERT INTO @temp (Task,EmpId,RedirectTo) values('Set Goal for your subordinates','',2)
	end
	end
	if(@chkIsMidYearEnabled=1 and @chkIsMidYearCompleted=0)
	begin
	set @chkIsEmpGoalSubmit=(select IsEmployeeGoalSubmitted from PerformanceEmpMidYearGoal midYear where (select top 1 EmployeeId from PerformanceEmpGoal empGoal where empGoal.Id=midYear.PerformanceEmpGoalId and PerformanceYearId=@quadId )=@loginId)
	end
	else
	begin
	set @chkIsEmpGoalSubmit=(select IsEmployeeGoalSubmitted from PerformanceEmpYearGoal endYear where (select top 1 EmployeeId from PerformanceEmpGoal empGoal where empGoal.Id=endYear.PerformanceEmpGoalId and PerformanceYearId=@quadId )=@loginId)	
	end
	if(@checkCEOSignOffEnable=0)
	begin
	set @chkIsCEOSignoff=1
	end
	else
	begin
    set @chkIsCEOSignoff=(select IsCEOSignoff from PerformanceInitailRating where PerformanceYearId=@quadId and EmployeeId=@loginId)
    end
	if(@check=1 and @chkIsEmpGoalSubmit=0 and @chkIsCEOSignoff=1)
	begin
    INSERT INTO @temp (Task,EmpId,RedirectTo) values('Complete your Self Assessment','',3)
	end	
	if(@chkIsMidYearEnabled=1 and @chkIsMidYearCompleted=0)
	begin 
	set @chkIsMgrReviewGoalSubmit=(select IsManagerGoalSaved from PerformanceEmpMidYearGoal midYear where (select top 1 EmployeeId from PerformanceEmpGoal empGoal where empGoal.Id=midYear.PerformanceEmpGoalId and PerformanceYearId=@quadId )=@loginId)	                                                                                                                                                                  
	set @chkIsEmpRatingSubmit=(select IsEmployeeRatingSubmitted from PerformanceEmpMidYearGoal midYear where (select top 1 EmployeeId from PerformanceEmpGoal empGoal where empGoal.Id=midYear.PerformanceEmpGoalId and PerformanceYearId=@quadId )=@loginId)	                                                                                                                                                                  
	end
	else
	begin
	set @chkIsMgrReviewGoalSubmit=(select IsManagerGoalSaved from PerformanceEmpYearGoal endYear where (select top 1 EmployeeId from PerformanceEmpGoal empGoal where empGoal.Id=endYear.PerformanceEmpGoalId and PerformanceYearId=@quadId )=@loginId)	                                                                                                                                                                  
	set @chkIsEmpRatingSubmit=(select IsEmployeeRatingSubmitted from PerformanceEmpYearGoal endYear where (select top 1 EmployeeId from PerformanceEmpGoal empGoal where empGoal.Id=endYear.PerformanceEmpGoalId and PerformanceYearId=@quadId )=@loginId)	                                                                                                                                                                  	
	end	
	if(@chkIsMgrReviewGoalSubmit=1 and @chkIsEmpRatingSubmit=0)
	begin
	INSERT INTO @temp (Task,EmpId,RedirectTo) values('Accept your rating','',3)
	end
	
	if(@chkIsMidYearEnabled=1 and @chkIsMidYearCompleted=0)
	begin 
	set @chkIsEmpGoalSubmitCount= (select COUNT(Id) from PerformanceEmpMidYearGoal where PerformanceEmpGoalId=(select Id from PerformanceEmpGoal empGoal where ManagerId=@loginId and PerformanceYearId=@quadId ) )
	if(@chkIsEmpGoalSubmitCount>0)
	begin
	set @Counter=1
	Insert into @tempReviewAssessment select empGoal.EmployeeId, midYear.IsEmployeeGoalSubmitted,midYear.IsManagerGoalSubmitted,midYear.IsEmployeeRatingSubmitted,midYear.IsManagerRatingSubmitted from PerformanceEmpMidYearGoal as midYear  join PerformanceEmpGoal empGoal on empGoal.Id=midYear.PerformanceEmpGoalId and empGoal.ManagerId=@loginId  and empGoal.PerformanceYearId=@quadId 
	set @chkIsEmpGoalSubmitCount=(select Count(*) from @tempReviewAssessment)
	while(@Counter<=@chkIsEmpGoalSubmitCount)
	begin 
	 set @Id = (select EmpId From @tempReviewAssessment where id=@Counter)
	 set @chkIsEmpGoalSubmit=(select bIsEmpGoalSubmitted From @tempReviewAssessment where id=@Counter)
	 set @chkIsMgrReviewGoalSubmit=(select bIsMgrGoalSubmitted From @tempReviewAssessment where id=@Counter)
	 set @chkIsEmpRatingSubmit=(select bIsEmpRatingSubmitted From @tempReviewAssessment where id=@Counter)
	 set @chkIsMgrRatingSubmit=(select bIsMgrRatingSubmitted From @tempReviewAssessment where id=@Counter)
     set @empName=(select FullName COLLATE Latin1_General_CI_AS from AspNetUsers where Id=(Select UserId from Employee where Id=@Id and IsActive=1))
	 if(@chkIsEmpGoalSubmit=1 and @chkIsMgrReviewGoalSubmit=0 )
	 begin
	 INSERT INTO @temp (Task,EmpId,RedirectTo) values('Review Assessment for '+@empName+'',@Id,4)
	 end
	 if(@chkIsEmpRatingSubmit=1 and @chkIsMgrRatingSubmit=0)
	 begin
	  INSERT INTO @temp (Task,EmpId,RedirectTo) values('Review rating acceptance from '+@empName+'',@Id,4)
	 end
	 set @Counter=@Counter+1
	 end
    end
	end
	else
	begin
	set @chkIsEmpGoalSubmitCount= (select COUNT(Id) from PerformanceEmpYearGoal where PerformanceEmpGoalId=(select Id from PerformanceEmpGoal empGoal where ManagerId=@loginId and PerformanceYearId=@quadId ) )	
	if(@chkIsEmpGoalSubmitCount>0)
	begin
	set @Counter=1
	Insert into @tempReviewAssessment select empGoal.EmployeeId, endYear.IsEmployeeGoalSubmitted,endYear.IsManagerGoalSubmitted,endYear.IsEmployeeRatingSubmitted,endYear.IsManagerRatingSubmitted from PerformanceEmpYearGoal as endYear  join PerformanceEmpGoal empGoal on empGoal.Id=endYear.PerformanceEmpGoalId and empGoal.ManagerId=@loginId  and empGoal.PerformanceYearId=@quadId 	
	set @chkIsEmpGoalSubmitCount=(select Count(*) from @tempReviewAssessment)
	while(@Counter<=@chkIsEmpGoalSubmitCount)
	begin 
	 set @Id = (select EmpId From @tempReviewAssessment where id=@Counter)
	 set @chkIsEmpGoalSubmit=(select bIsEmpGoalSubmitted From @tempReviewAssessment where id=@Counter)
	 set @chkIsMgrReviewGoalSubmit=(select bIsMgrGoalSubmitted From @tempReviewAssessment where id=@Counter)
	 set @chkIsEmpRatingSubmit=(select bIsEmpRatingSubmitted From @tempReviewAssessment where id=@Counter)
	 set @chkIsMgrRatingSubmit=(select bIsMgrRatingSubmitted From @tempReviewAssessment where id=@Counter)
     set @empName=(select FullName COLLATE Latin1_General_CI_AS from AspNetUsers where Id=(Select UserId from Employee where Id=@Id and IsActive=1))	 
	 if(@chkIsEmpGoalSubmit=1 and @chkIsMgrReviewGoalSubmit=0 )
	 begin
	 INSERT INTO @temp (Task,EmpId,RedirectTo) values('Review Assessment for '+@empName+'',@Id,4)
	 end
	 if(@chkIsEmpRatingSubmit=1 and @chkIsMgrRatingSubmit=0)
	 begin
	  INSERT INTO @temp (Task,EmpId,RedirectTo) values('Review rating acceptance from '+@empName+'',@Id,4)
	 end
	 set @Counter=@Counter+1
	 end
    end 
	end
	
set @chkIsPresident=(select IsPresidentCouncil from PerformancePresidentCouncil where EmployeeId=@loginId)
if(@chkIsPresident=1)
begin 
Insert into @tempPresidentSignoff (EmpId) select EmployeeId from PerformancePresidentCouncil where PresidentId=@loginId
set @Counter=1
set @RowCount=(select count(*) from @tempPresidentSignoff)
while (@Counter <= @RowCount)
begin
set @empid=(select EmpId from @tempPresidentSignoff where id=@Counter)
if(@chkIsMidYearEnabled=1 and @chkIsMidYearCompleted=0)
	begin
	set @chkIsMgrRatingSubmit=(select IsManagerRatingSubmitted From PerformanceEmpMidYearGoal midYear  join PerformanceEmpGoal empGoal on empGoal.Id=midYear.PerformanceEmpGoalId where empGoal.EmployeeId=@empid and empGoal.PerformanceYearId=@quadId)
	end
	else
	begin
	set @chkIsMgrRatingSubmit=(select IsManagerRatingSubmitted From PerformanceEmpYearGoal endYear  join PerformanceEmpGoal empGoal on empGoal.Id=endYear.PerformanceEmpGoalId where empGoal.EmployeeId=@empid and empGoal.PerformanceYearId=@quadId)
	end
if(@chkIsMgrRatingSubmit=1)
begin
if(@chkIsMidYearEnabled=1 and @chkIsMidYearCompleted=0)
	begin
	set @empGoalId=(select midYear.Id From PerformanceEmpMidYearGoal midYear  join PerformanceEmpGoal empGoal on empGoal.Id=midYear.PerformanceEmpGoalId where empGoal.EmployeeId=@empid and empGoal.PerformanceYearId=@quadId)
set @presidentSignoff=(Select PresidentSignOff from PerformanceEmpGoalPresident where PerformanceEmpGoalId=@empGoalId)
	end
	else
	begin
	set @empGoalId=(select endYear.Id From PerformanceEmpYearGoal endYear  join PerformanceEmpGoal empGoal on empGoal.Id=endYear.PerformanceEmpGoalId where empGoal.EmployeeId=@empid and empGoal.PerformanceYearId=@quadId)
set @presidentSignoff=(Select PresidentYearSignOff from PerformanceEmpGoalPresident where PerformanceEmpGoalId=@empGoalId)
	end
if(@presidentSignoff=0)
begin
set @empName=(select FullName COLLATE Latin1_General_CI_AS from AspNetUsers where Id=(Select UserId from Employee where Id=@Id and IsActive=1))
INSERT INTO @temp (Task,EmpId,RedirectTo) values(' Executive Manager Signoff for '+@empName+' ',@empid,4)
end
end
set  @Counter=@Counter+1
end
end


 --end
 select * from @temp
end";
            migrationBuilder.Sql(sp_uspGetTaskList);

			var sp_uspTruncateBulkUploadData = @"create procedure [dbo].[uspTruncateBulkUploadData] 
AS
BEGIN
   Truncate table [dbo].[ExcelEmployeeData] 
END";
				migrationBuilder.Sql(sp_uspTruncateBulkUploadData);



			var sp_uspValidateBulkUpload = @"Create procedure [dbo].[uspValidateBulkUpload] 
AS
DECLARE @ErrorMessage Varchar(max)
BEGIN
 BEGIN TRY      
   BEGIN TRAN     
 
SET @ErrorMessage = ''

    UPDATE ExcelEmployeeData SET [Status] = '' , ErrorMessage = ''
					
	UPDATE ExcelEmployeeData   SET [Status]= (CASE  WHEN (FullName='')  or FullName=null THEN 'Invalid'
	                                                WHEN (WorkEmailAddress='')  or WorkEmailAddress=null THEN 'Invalid'
													WHEN (Band='') or Band=null THEN 'Invalid'
													WHEN (FunctionalDepartment='') or FunctionalDepartment=null THEN 'Invalid'
													WHEN (FunctionalGroup='')  or FunctionalGroup=null THEN 'Invalid'
													WHEN (Designation='') or Designation=null THEN 'Invalid'
													WHEN (Title='') or Title=null THEN 'Invalid' 																										
													WHEN (ReportingHeadEmailId='') or ReportingHeadEmailId=null THEN 'Invalid' 
													WHEN (RollAccess='') or RollAccess=null THEN 'Invalid' 													
													WHEN (DateofJoining='') or DateofJoining=null THEN 'Invalid' 
													WHEN (Location='') or Location=null THEN 'Invalid' 
													WHEN (ReportingHeadName='') or ReportingHeadName=null THEN 'Invalid' 
													ELSE 'Valid'
													END)

     
	UPDATE ExcelEmployeeData SET [Status]= 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage <> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Except Personal Email all data is Compulsory,Please fill empty details') ELSE 'Except Personal Email all data is Compulsory,Please fill empty details' END) 	
	WHERE  FullName in('') or FullName IS NULL  or FunctionalDepartment='' or FunctionalDepartment IS NULL or FunctionalGroup='' or FunctionalGroup IS NULL or Location='' or Location IS NULL or
	       Designation='' or Designation IS NULL or  WorkEmailAddress='' or WorkEmailAddress IS NULL or Band='' or Band IS NULL or ReportingHeadEmailId='' or ReportingHeadEmailId IS NULL or
	       ReportingHeadName=''  or ReportingHeadName IS NULL or RollAccess=''  or RollAccess Is NULL  or Title='' or Title IS NULL or DateofJoining='' or DateofJoining IS NULL
	
	UPDATE ExcelEmployeeData SET [Status] = 'Invalid', ErrorMessage = (CASE WHEN ErrorMessage <> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Date of joining is not in proper format.Please enter date in MM/dd/yyyy format') ELSE 'Date of joining is 
not in proper format.Please enter date in MM/dd/yyyy format' END)  where ISDATE(DateofJoining)=0

    UPDATE ExcelEmployeeData  SET [Status] = 'Invalid', ErrorMessage= (CASE WHEN ErrorMessage <> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Work Email Id is not in proper format') ELSE 'Work Email Id is not in proper format' END) 
	from ExcelEmployeeData where
	patindex ('%[ &'',"":; !+=\/ () <>]% ', ltrim(rtrim(WorkEmailAddress))) > 0  
	or patindex('[@.-_]%', ltrim(rtrim(WorkEmailAddress))) > 0
	or patindex('%[@.-_]', ltrim(rtrim(WorkEmailAddress))) > 0
	or ltrim(rtrim(WorkEmailAddress)) not like '%@%.%'
	or ltrim(rtrim(WorkEmailAddress)) like '%..%'
	or ltrim(rtrim(WorkEmailAddress)) like '%@%@%'
	or ltrim(rtrim(WorkEmailAddress)) like '%.@%' or ltrim(rtrim(WorkEmailAddress)) like '%@.%'
	or ltrim(rtrim(WorkEmailAddress)) like '%.cm' or ltrim(rtrim(WorkEmailAddress)) like '%.co'
	or ltrim(rtrim(WorkEmailAddress)) like '%.or' or ltrim(rtrim(WorkEmailAddress)) like '%.ne'


	UPDATE ExcelEmployeeData  SET[Status] = 'Invalid', ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Reporting Head Email Id is not in proper format') ELSE 'Reporting Head Email Id is not in proper format' END) 
	from ExcelEmployeeData where
	patindex('%[ &'',"":; !+=\/()<>]%', ltrim(rtrim(ReportingHeadEmailId))) > 0
	or patindex('[@.-_]%', ltrim(rtrim(ReportingHeadEmailId))) > 0
	or patindex('%[@.-_]', ltrim(rtrim(ReportingHeadEmailId))) > 0
	or ltrim(rtrim(ReportingHeadEmailId)) not like '%@%.%'
	or ltrim(rtrim(ReportingHeadEmailId)) like '%..%'
	or ltrim(rtrim(ReportingHeadEmailId)) like '%@%@%'
	or ltrim(rtrim(ReportingHeadEmailId)) like '%.@%' or ltrim(rtrim(ReportingHeadEmailId)) like '%@.%'
	or ltrim(rtrim(ReportingHeadEmailId)) like '%.cm' or ltrim(rtrim(ReportingHeadEmailId)) like '%.co'
	or ltrim(rtrim(ReportingHeadEmailId)) like '%.or' or ltrim(rtrim(ReportingHeadEmailId)) like '%.ne'



	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Minimum two alphabets are required in Full Name') ELSE 'Minimum two alphabets are required in Full Name' END
) where len(FullName COLLATE DATABASE_DEFAULT) < 2
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Minimum two alphabets are required in Functional Group') ELSE 'Minimum two alphabets are required in Functional Group' END) where len(FunctionalGroup COLLATE DATABASE_DEFAULT) < 2
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Minimum two alphabets are required in Functional Department') ELSE 'Minimum two alphabets are required in Functional Department' END) where len(FunctionalDepartment COLLATE DATABASE_DEFAULT) < 2
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Minimum two alphabets are required in Functional Designation') ELSE 'Minimum two alphabets are required in Functional Designation' END) where len(Designation COLLATE DATABASE_DEFAULT) < 2
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Minimum two alphabets are required in Functional Title') ELSE 'Minimum two alphabets are required in Functional Title' END) where len(Title COLLATE DATABASE_DEFAULT) < 2


	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Special Characters in Full Name is not allowed') ELSE 'Special Characters in Full Name is not allowed' END) 
where FullName LIKE '%[!@#$%^&*()]%'
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Special Characters in Functional Group is not allowed') ELSE 'Special Characters in Functional Group is not allowed' END) where FunctionalGroup LIKE '%[!@#$%^&*()]%'
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Special Characters in Functional Department is not allowed') ELSE 'Special Characters in Functional Department is not allowed' END) where FunctionalDepartment LIKE '%[!@#$%^&*()]%'

	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Special Characters in Functional Designation is not allowed') ELSE 'Special Characters in Functional Designation is not allowed' END) where Designation LIKE '%[!@#$%^&*()]%'
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Special Characters in Functional Title is not allowed') ELSE 'Special Characters in Functional Title is not allowed' END) where Title LIKE '%[!@#$%^&*()]%'


	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Work Email Address already exist in database') ELSE 'Work Email Address already exist in database' END) where WorkEmailAddress COLLATE DATABASE_DEFAULT in (select Email from AspNetUsers) 		
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid',ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Role Access does not exist in database') ELSE 'Role Access does not exist in database' END) where RollAccess
 COLLATE DATABASE_DEFAULT not in (select Name from AspNetRoles) 	
	
	UPDATE ExcelEmployeeData SET[Status] = 'Invalid', ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Duplicate Data exist in Upload Excel') ELSE 'Duplicate Data exist in Upload Excel' END) where Id in(
	  SELECT Id from ExcelEmployeeData where WorkEmailAddress in (
	  SELECT      WorkEmailAddress
	  FROM        ExcelEmployeeData
	  GROUP BY WorkEmailAddress
	HAVING COUNT(1) > 1))
	
	INSERT INTO FunctionalTitle(Id, Name, CreatedDate, UpdatedDate, IsActive)
	SELECT NEWID() as Id,*FROM(select DISTINCT Title as Name, getdate() as CreatedDate, getdate() as UpdatedDate, 1 as IsActive FROM  ExcelEmployeeData WHERE Title IS NOT NULL and Title != '' and Title COLLATE DATABASE_DEFAULT not in (SELECT Name FROM  FunctionalTitle where IsActive = 1)) as sub

	INSERT INTO FunctionalDesignation(Id, Name, CreatedDate, UpdatedDate, IsActive)
	SELECT NEWID() as Id,*FROM(select DISTINCT Designation as Name, getdate() as CreatedDate, getdate() as UpdatedDate, 1 as IsActive FROM  ExcelEmployeeData WHERE Designation COLLATE DATABASE_DEFAULT not in (SELECT Name FROM  FunctionalDesignation where  IsActive = 1)) as sub


	INSERT INTO FunctionalDepartment(Id, Name, CreatedDate, UpdatedDate, IsActive)
	SELECT NEWID() as Id,*FROM(select DISTINCT FunctionalDepartment as Name, getdate() as CreatedDate, getdate() as UpdatedDate, 1 as IsActive  FROM ExcelEmployeeData
	WHERE FunctionalDepartment COLLATE DATABASE_DEFAULT not in (SELECT Name FROM FunctionalDepartment where  IsActive = 1)) as sub



	INSERT INTO FunctionalGroup(Id, Name, CreatedDate, UpdatedDate, IsActive, DepartmentId)
	SELECT NEWID() as Id,*FROM(select DISTINCT FunctionalGroup COLLATE DATABASE_DEFAULT as Name, Convert(varchar(1000), getdate())  COLLATE DATABASE_DEFAULT as CreatedDate, Convert(varchar(1000), getdate()) COLLATE DATABASE_DEFAULT as UpdatedDate, Convert(varchar(1000), 1) COLLATE DATABASE_DEFAULT as IsActive, Convert(varchar(1000), D.Id) COLLATE DATABASE_DEFAULT as DepartmentId FROM ExcelEmployeeData ED

	left join FunctionalDepartment D on ED.FunctionalDepartment COLLATE DATABASE_DEFAULT = D.Name COLLATE DATABASE_DEFAULT WHERE  FunctionalDepartment COLLATE DATABASE_DEFAULT <> '' and  FunctionalGroup COLLATE DATABASE_DEFAULT not in (SELECT Name COLLATE DATABASE_DEFAULT FROM FunctionalGroup)) as sub



	INSERT INTO FunctionalBand(Id, Name, CreatedDate, UpdatedDate, IsActive, LocalOrder)
	SELECT NEWID() as Id,*FROM(select DISTINCT Band as Name, getdate() as CreatedDate, getdate() as UpdatedDate, 1 as IsActive, 0 as LocalOrder FROM  ExcelEmployeeData WHERE Band COLLATE DATABASE_DEFAULT not in (SELECT Name FROM  FunctionalBand where IsActive = 1)) as sub



	UPDATE ExcelEmployeeData SET Title = ISNULL(CONVERT(varchar(1000), A.Id), B.Title) COLLATE DATABASE_DEFAULT
	FROM ExcelEmployeeData B LEFT JOIN FunctionalTitle A
   ON B.Title COLLATE DATABASE_DEFAULT = A.Name COLLATE DATABASE_DEFAULT
	WHERE B.[Status]= 'Valid'



	UPDATE ExcelEmployeeData SET Designation = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
	FROM ExcelEmployeeData B LEFT JOIN FunctionalDesignation A
   ON B.Designation COLLATE DATABASE_DEFAULT = A.Name COLLATE DATABASE_DEFAULT
	WHERE B.[Status]= 'Valid'



	UPDATE ExcelEmployeeData SET FunctionalDepartment = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
	FROM ExcelEmployeeData B LEFT JOIN FunctionalDepartment A
   ON B.FunctionalDepartment COLLATE DATABASE_DEFAULT = A.Name COLLATE DATABASE_DEFAULT
	WHERE B.[Status]= 'Valid'



	UPDATE ExcelEmployeeData SET FunctionalGroup = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
	FROM ExcelEmployeeData B LEFT JOIN FunctionalGroup A
   ON B.FunctionalGroup COLLATE DATABASE_DEFAULT = A.Name COLLATE DATABASE_DEFAULT
	WHERE B.[Status]= 'Valid'



	UPDATE ExcelEmployeeData SET RollAccess = Convert(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
	FROM ExcelEmployeeData B LEFT JOIN AspNetRoles A
   ON B.RollAccess COLLATE DATABASE_DEFAULT = A.Name COLLATE DATABASE_DEFAULT
	WHERE B.[Status]= 'Valid'



	UPDATE ExcelEmployeeData SET Band = ISNULL(CONVERT(varchar(1000), A.Id), B.Band) COLLATE DATABASE_DEFAULT
	FROM ExcelEmployeeData B LEFT JOIN FunctionalBand A
   ON B.Band COLLATE DATABASE_DEFAULT = A.Name COLLATE DATABASE_DEFAULT
	WHERE B.[Status]= 'Valid'

	INSERT INTO AspNetUsers(Id, AccessFailedCount, Email, NormalizedEmail, FullName, UserName, NormalizedUserName, PasswordHash, CreatedDate, EmailConfirmed, IsActive, IsEnabled, LockoutEnabled, PhoneNumberConfirmed, TwoFactorEnabled, UpdatedDate, IsSuperAdmin, IsMailSent, SecurityStamp)
	SELECT NEWID(),0,WorkEmailAddress,UPPER(WorkEmailAddress),FullName,WorkEmailAddress,UPPER(WorkEmailAddress),PasswordHash,getdate(),0,1,1,1,0,0,getdate(),0,0,NewID() FROM ExcelEmployeeData
	WHERE[Status] = 'Valid'
	--Just to get insert data into Employee table
	--ReportingHeadName will be updated once data inserted into employee
	UPDATE ExcelEmployeeData SET ReportingHeadName = A.Id COLLATE DATABASE_DEFAULT
	FROM ExcelEmployeeData B LEFT JOIN AspNetUsers A
   ON ReportingHeadEmailId COLLATE DATABASE_DEFAULT = A.Email COLLATE DATABASE_DEFAULT
	WHERE[Status] = 'Valid'

	UPDATE ExcelEmployeeData SET EmployeeId = A.Id COLLATE DATABASE_DEFAULT
	FROM ExcelEmployeeData B LEFT JOIN AspNetUsers A
   ON WorkEmailAddress COLLATE DATABASE_DEFAULT = A.Email COLLATE DATABASE_DEFAULT
	WHERE[Status] = 'Valid'


	INSERT INTO Employee(Id, CreatedDate, BandId, DOJ, DesignationId, GroupId, Location, ManagerId, TitleId, UpdatedDate, UserId, IsActive)
	SELECT NEWID(),getdate(),CAST(Band as uniqueidentifier) as BandId,DateofJoining,CAST(Designation as uniqueidentifier) as DesignationId, CAST(FunctionalGroup as uniqueidentifier) as FunctionalGroupId,
	Location, ISNULL(ReportingHeadName, '00000000-0000-0000-0000-000000000000') as ReportingHeadNameId, CAST(Title as uniqueidentifier) as TitleId,getdate(),EmployeeId as UserId,1 FROM ExcelEmployeeData
	WHERE[Status] = 'Valid'




	Insert into AspNetUserRoles(UserId, RoleId)
	select EmployeeId, RollAccess from ExcelEmployeeData A where A.[Status] = 'Valid'


	UPDATE ExcelEmployeeData SET ReportingHeadName = C.Id
	FROM ExcelEmployeeData B LEFT JOIN AspNetUsers A
   ON ReportingHeadEmailId COLLATE DATABASE_DEFAULT = A.Email COLLATE DATABASE_DEFAULT
	JOIN Employee C ON C.UserId = A.Id
	WHERE[Status] = 'Valid'



	UPDATE ExcelEmployeeData SET EmployeeId = C.Id
	FROM ExcelEmployeeData B LEFT JOIN AspNetUsers A
   ON WorkEmailAddress COLLATE DATABASE_DEFAULT = A.Email COLLATE DATABASE_DEFAULT
	JOIN Employee C ON C.UserId = A.Id
	WHERE[Status] = 'Valid'




	update Employee set ManagerId = IsNull(ex.ReportingHeadName, '00000000-0000-0000-0000-000000000000') from Employee em inner
																										 join ExcelEmployeeData ex

on em.Id = ex.EmployeeId



INSERT INTO EmployeePersonalDetail(Id, CreatedDate, DOB, EmailId, EmployeeId, UpdatedDate, IsActive)
	SELECT newid(),getdate(),getdate(),PersonalEmailID,EmployeeId,GETDATE(),1
	FROM ExcelEmployeeData A LEFT JOIN Employee B ON A.EmployeeId = B.Id
	WHERE A.[Status]= 'Valid'



	UPDATE ExcelEmployeeData SET[Status] = 'Invalid', ErrorMessage = (CASE WHEN ErrorMessage<> '' THEN concat(ErrorMessage COLLATE DATABASE_DEFAULT,',Reporting Head does not exist') ELSE 'Reporting Head does not exist' END)
	WHERE ReportingHeadEmailId not in (select Email COLLATE DATABASE_DEFAULT from AspNetUsers where IsActive = 1)	
	

	DELETE ep FROM EmployeePersonalDetail ep INNER JOIN ExcelEmployeeData d on
	ep.EmployeeId = d.EmployeeId  WHERE d.[Status]= 'Invalid' and ErrorMessage<>'Work Email Address already exist in database'

	DELETE a FROM AspNetUsers a INNER JOIN ExcelEmployeeData d on
	a.Email = d.WorkEmailAddress  WHERE d.[Status]= 'Invalid' and ErrorMessage<>'Work Email Address already exist in database'

	--DELETE c FROM Employee c INNER JOIN ExcelEmployeeData d on
	--c.Id = d.EmployeeId  WHERE d.[Status]= 'Invalid'

   --DELETE ur FROM AspNetUserRoles ur INNER JOIN AspNetUsers a on
	--ur.UserId = a.Id     INNER JOIN ExcelEmployeeData d on
   --a.Email = d.WorkEmailAddress  WHERE d.[Status]= 'Invalid'



UPDATE ExcelEmployeeData SET Title = CONVERT(varchar(1000), A.Name) COLLATE DATABASE_DEFAULT
FROM ExcelEmployeeData B INNER JOIN FunctionalTitle A
ON B.Title COLLATE DATABASE_DEFAULT = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
WHERE B.[Status]= 'Invalid'


UPDATE ExcelEmployeeData SET Designation = CONVERT(varchar(1000), A.Name) COLLATE DATABASE_DEFAULT
FROM ExcelEmployeeData B INNER JOIN FunctionalDesignation A
ON B.Designation COLLATE DATABASE_DEFAULT = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
WHERE B.[Status]= 'Invalid'



UPDATE ExcelEmployeeData SET FunctionalDepartment = CONVERT(varchar(1000), A.Name) COLLATE DATABASE_DEFAULT
FROM ExcelEmployeeData B INNER JOIN FunctionalDepartment A
ON B.FunctionalDepartment COLLATE DATABASE_DEFAULT = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
WHERE B.[Status]= 'Invalid'



UPDATE ExcelEmployeeData SET FunctionalGroup = CONVERT(varchar(1000), A.Name) COLLATE DATABASE_DEFAULT
FROM ExcelEmployeeData B INNER JOIN FunctionalGroup A
ON B.FunctionalGroup COLLATE DATABASE_DEFAULT = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
WHERE B.[Status]= 'Invalid'



UPDATE ExcelEmployeeData SET RollAccess = Convert(varchar(1000), A.Name) COLLATE DATABASE_DEFAULT
FROM ExcelEmployeeData B INNER JOIN AspNetRoles A
ON B.RollAccess COLLATE DATABASE_DEFAULT = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
WHERE B.[Status]= 'Invalid'


UPDATE ExcelEmployeeData SET Band = CONVERT(varchar(1000), A.Name) COLLATE DATABASE_DEFAULT
FROM ExcelEmployeeData B INNER JOIN FunctionalBand A
ON B.Band COLLATE DATABASE_DEFAULT = CONVERT(varchar(1000), A.Id) COLLATE DATABASE_DEFAULT
WHERE B.[Status]= 'Invalid'


	SELECT FullName as FirstName,WorkEmailAddress as WorkEmailID,FunctionalDepartment as FunctionalDepartment,
	       FunctionalGroup as FunctionalGroup , Designation as Designation,Title as Title,Band as Band,
		   ReportingHeadEmailId as ReportingHeadEmail,ReportingHeadName as ReportingHeadName,RollAccess as RollAccess,Location as Location,DateofJoining as DOJ,ErrorMessage as ErrorMgs FROM ExcelEmployeeData

	WHERE[Status] = 'Invalid'

	COMMIT TRAN
 END TRY

	BEGIN CATCH

	   rollback
 END CATCH
END";
			migrationBuilder.Sql(sp_uspValidateBulkUpload);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
