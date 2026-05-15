namespace Capex.Models.ResponseModel.Masters
{
    public class DepartmentResponseModel : ResponseModelBase
    {
        public IList<Department> DepartmentList{ get; set; }
    }
    public class Department  { 
        public int DepartmentId { get; set; }
        public string DepartmentNameEng { get; set; }
        public string DepartmentNameHi { get; set; }
    }


    public class DesignationResponseModel : ResponseModelBase
    {
        public IList<Designation> designationsList { get; set; }
    }
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationNameEng { get; set; }
        public string DesignationNameHi { get; set; }
    }




    public class officeResponseModel : ResponseModelBase
    {
        public IList<officeModel> officelist { get; set; }
    }
    public class officeModel
    {
        public int OfficeId { get; set; }
        public string OfficeNameEng { get; set; }
        public string OfficeNameHi { get; set; }
    } 
    
    public class JurisdictionGroupListModel : ResponseModelBase
    {
        public IList<JurisdictionGroupDDL> jurisdictionGroupList { get; set; }
    }
    public class JurisdictionGroupDDL
    {
        public int Id { get; set; }
        public string JurisdictionGroupName { get; set; }
    
    }

    public class FinancialYearResponseModel : ResponseModelBase
    {
        public IList<FinancialYear> FinancialYear { get; set; }
    }
    public class FinancialYear
    {
        public int FinYearID { get; set; }
        public string FinYear { get; set; }
      
    }




}
