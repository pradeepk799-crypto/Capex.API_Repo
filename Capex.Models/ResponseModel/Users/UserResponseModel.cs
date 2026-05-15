using Capex.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.ResponseModel.Users
{
    public class UserDetailsResponseModel : ResponseModelBase
    {
        public IList<UserDetails> UserDetails { get; set; }
    }
    public class UserDetails
    {
        public int EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RoleId { get; set; }
        public string? DesignationId { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }

        public string? DesignationName { get; set; }
        public string? RoleName { get; set; }


    }

    public class RoleMasterDetailsResponseModel : ResponseModelBase
    {
        public IList<RoleMasterDetails> RoleMasterDetails { get; set; }
    }
   
    public class RoleMasterDetails
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
       

    }
    public class DesignationRoleMasterMappingResponseModel : ResponseModelBase
    {
        public IList<DesignationRoleMasterMapping> DesignationRoleMasterMapping { get; set; }

    }

    public class DesignationRoleMasterMapping
    {
        public int RoleId { get; set; }
        public int DesignationId { get; set; }
        public string? DesignationName { get; set; }


    }
    public class EntityUserMappingResponseModel : ResponseModelBase
    {
        public IList<EntityUserMapping> EntityUserMapping { get; set; }

    }

    public class EntityUserMapping
    {
        public int RoleId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }


    }

    public class EntityUserMappingDetailsResponseModel : ResponseModelBase
    {
        public IList<EntityUserMappingDetails> EntityUserMappingDetails { get; set; }

    }

    public class EntityUserMappingDetails
    {
        public int RoleId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? RoleName { get; set; }
        public string? EntityName_E { get; set; }
        public string? EmployeeCode { get; set; }


    }


}
