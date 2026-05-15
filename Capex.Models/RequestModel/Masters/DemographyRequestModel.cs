using Capex.Models.RequestModel;

namespace Capex.Models.RequestModel.Masters
{
    public class DemographyRequestModel : RequestModelBase
    {
        public int? DemographyId { get; set; }
        public int DemographyTypeId { get; set; }
        public int? ParentDemographyId { get; set; }
    }

    public class MultipleDemographyRequestModel : RequestModelBase
    {
        public int? demographyTypeId { get; set; }
        public string demographyIdsList { get; set; }
    }
    public class OfficebyheadRequestModel : RequestModelBase
    {
        public int? VillageId { get; set; }
        public int? RevenueHeadId { get; set; }

    }

    public class NamantaranTypeReqModel : RequestModelBase
    {
        public int MenuTypeId { get; set; }
        public int MenuParentId { get; set; }
    }

    public class EntityTypeRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }

    }

    public class EntityDetailForInsertUpdateRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public string? EntityId { get; set; }
        public string? EntityNameH { get; set; }
        public string? EntityNameE { get; set; }
        public bool? IsActive { get; set; }
    }
    public class DiscomRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }

    }
    public class CircleRequestModel : RequestModelBase
    {
        public string? DiscomeCode { get; set; }
        public string? DiscomeId { get; set; }


    }
    public class SubStationRequestModel : RequestModelBase
    {
        public string? CircleCode { get; set; }
        public string? CircleId { get; set; }

    }
    public class ConnectivityTypeRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }

    }

    public class ConnectivityDetailForInsertUpdateRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public string? Connectivity_Id { get; set; }
        public string? Connectivity_Name_H { get; set; }
        public string? Connectivity_Name_E { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CheckMailIdRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }

    }

    public class EntityTypeListRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public string? EntityName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }



    }
}
