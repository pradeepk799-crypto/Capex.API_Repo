namespace Capex.Models.ResponseModel.Masters
{
    public class RoleResponseModel : ResponseModelBase
    {

        public IList<RoleModel> RoleResponse { get; set; }
    }
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string DepartmentNameHi { get; set; }
        public int DepartmentId { get; set; }
        public string OfficeTypeHi { get; set; }
        public int OfficeTypeId { get; set; }
        public string MenuNameHi { get; set; }
        public int DefaultMenuId { get; set; }
    }

   public class RoleMenuMappingResponseModel: ResponseModelBase
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
    }
}
