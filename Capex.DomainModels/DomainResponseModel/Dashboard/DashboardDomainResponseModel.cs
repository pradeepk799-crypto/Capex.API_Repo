using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel.Dashboard
{
    public class DashboardDomainResponseModel : DomainResponseModelBase
    {
        public int? DRoleID { get; set; }
        public int? DTotalBuildingRegistered { get; set; }
        public int DTotalVendorRegistered { get; set; }
        public int? DTotalNumberOfDDO { get; set; }
        public int? DTotalPendingPayment { get; set; }
        public int? totalDistrict { get; set; }
        public int? totalDDOs { get; set; }
        public int? totalBuilding { get; set; }
        public int? totalMeter { get; set; }



    }

    public class DashboardVenderDistrictDetailsDomainResponseModel : DomainResponseModelBase
    {
        public int? RoleID { get; set; }
        public string? DistrictName { get; set; }
        public string? DistrictCode { get; set; }
        public string? Price { get; set; }

    }
    public class DashboardVenderDdoDetailsDomainResponseModel : DomainResponseModelBase
    {
        public int? RoleID { get; set; }
        public string? DDOCode { get; set; }
        public string? DDONameEn { get; set; }
        public string? EmailID { get; set; }
        public string? NodalPersonName_En { get; set; }
        public string? ContactDetails { get; set; }
        //public string? EmailID { get; set; }
        //public string? Address { get; set; }



    }
    public class DashboardVenderBuildingDetailsDomainResponseModel : DomainResponseModelBase
    {
        public int? RoleID { get; set; }
        public int? BuildingId { get; set; }
        public string? BuildingIdNumber { get; set; }
        public string? MeterSerialNo { get; set; }
        public string? SiteAddress { get; set; }
        public string? BeneficiaryName { get; set; }
        public int? SanctionedLoad { get; set; }
        public string? HESName { get; set; }
        public string? Phase { get; set; }
        public string? District { get; set; }
        public string? DDOName { get; set; }




    }

}
