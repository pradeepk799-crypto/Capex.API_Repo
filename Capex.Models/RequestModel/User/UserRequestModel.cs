using Capex.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel.User
{
    public class UserDetailsRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }

    }
    public class SearchUserDetailsRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
    public class UserDetailsForInsertUpdateRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RoleId { get; set; }
        public string? DesignationId { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public bool? IsActive { get; set; }
    }
    public class RoleMasterDetailsRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }

    }

    public class RoleMasterDetailsForInsertUpdateRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class DesignationRoleMasterMappingRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public int? RoleId { get; set; }

    }

    public class DesignationRoleMasterMappingForInsertUpdateRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public int? RoleId { get; set; }
        public string? DesignationName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class EntityUserMappingRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public int? RoleId { get; set; }

    }

    public class EntityUserMappingForInsertUpdateRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public int? RoleId { get; set; }
        public int? EntityTypeId { get; set; }
        public string? EmployeeName { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class EntityUserMappingDetailsRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        //public int? RoleId { get; set; }

    }

}
