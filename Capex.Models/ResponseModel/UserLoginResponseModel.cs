namespace Capex.Models.ResponseModel
{
    public class UserLoginResponseModel
           { 
        
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string ProfileId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public Boolean IsResetPwd { get; set; }
        public int DepartmentId { get; set; }
        public int OfficeLevelId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int SubDivisionId { get; set; }
        public int TehsilId { get; set; }
        public int DesignationId { get; set; }
        public int RoleId { get; set; }
        public List<UserRoleDetails> UserRoleList { get; set; }
        public bool IsEKyc { get; set; }
        public long? OfficeId { get; set; }
        public long UserType { get; set; }
    }


    public class UserRoleDetails
    {      
        public int? Id { get; set; }
        public int? RoleId { get; set; }
        public int? MenuId { get; set; }
        public int? OrderIndex { get; set; }
        public string MenuNameHi { get; set; }
        public string MenuNameEng { get; set; }
        public string MenuPath { get; set; }
        public int? MenuParentId { get; set; }
        public int? MenuTypeId { get; set; }
        public string Class { get; set; }
        public string Icon { get; set; }
        public bool IsHiddenAction { get; set; }
    }

   
    public class UserApplicationInfoRequestModel
    {
        public int LoginId { get; set; }
        public string EmailId { get; set; }
        public int RoleId { get; set; }
        public int LoginTypeId { get; set; }
        public int ProfileId { get; set; }
        public int? ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string COEName { get; set; }
        public string NodalOfficerName { get; set; }
        public string NodalOfficerDesignation { get; set; }
        public int? UserTypeId { get; set; }
        public List<UserRoleDetails> UserRoleList { get; set; }
        public string? PasswordHash { get; set; }
    }


}
