using System.Runtime.Serialization;


namespace Capex.Models.ResponseModel
{
    public class TokenResponseModel : ResponseModelBase
    {

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public string RefreshToken { get; set; }

        [DataMember]
        public DateTime? IssuedAt { get; set; }

        [DataMember]
        public DateTime? Expires { get; set; }

        [DataMember]
        public DateTime? RefreshTokenExpires { get; set; }

        public LoginUserModel LoginUserModel1 { get; set; }
        public List<UserMenuList> UserMenuList { get; set; }

        public UserApplicationInfoRequestModel LoginUserModel { get; set; }
    }

    /// AdditionalUserLoginResponseModel

    [DataContract]
    public class UserMenuList
    {

        public int? Id { get; set; }
        public int? RoleId { get; set; }
        public int? MenuId { get; set; }
        public int? OrderIndex { get; set; }
        public string? MenuNameHi { get; set; }
        public string? MenuNameEng { get; set; }
        public string? MenuPath { get; set; }
        public int? MenuParentId { get; set; }
        public int? MenuTypeId { get; set; }
        public string Class { get; set; }
        public string Icon { get; set; }
        public bool IsHiddenAction { get; set; }

        //[DataMember]
        //public UserLoginResponseModel UserLoginResponseModel { get; set; }
    }

    public class LoginUserModel
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
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

        public bool IsEKyc { get; set; }

        public long? OfficeId { get; set; }
        public long UserType { get; set; }

    }

    public class CitizenForgotPwdResponseModel : ResponseModelBase
    {

        [DataMember]
        public string MobileNo { get; set; }


    }
}
