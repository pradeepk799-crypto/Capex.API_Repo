using Capex.Models.RequestModel;

namespace Capex.Models.ResponseModel.Masters
{
    public class DemographyResponseModel : ResponseModelBase
    {     
        public IList<DemographyModel> DemographyResponse {  get; set; }
    }
    public class DemographyModel
    {
        public int DemographyId { get; set; }
        public int DemographyTypeId { get; set; }
        public string? Demography_Name_Eng { get; set; }
        public string? Demography_Name_Hi { get; set; }
        public long LGDCode { get; set; }
        public string? DemographyType { get; set; }
        public string? PatwariHalkaNumber { get; set; }
    }

    public class QRCodeResponseModel
    {
        public byte[]? QRCode { get; set; }
    }

    public class OfficebyheadResponseModel : ResponseModelBase
    {
        public IList<OfficeheadResponseModel> OfficeTehsilResponseModellist { get; set; }
    }
    public class OfficeheadResponseModel
    {
        public int OfficeId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeNameEng { get; set; }
        public string OfficeNameHi { get; set; }
        public int DemographyId { get; set; }
        public string OfficeAddress { get; set; }
        public int OfficeLevelId { get; set; }
        public int DepartmentId { get; set; }
        public int OfficeTypeId { get; set; }
        public string LatLong { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }    
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int SubDivisionId { get; set; }
        public int TehsilId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
    }

  
    public class MutationType
    {
        public int MenuId { get; set; }
        public string? MenuNameEng { get; set; }
        public string? MenuNameHi { get; set; }
    }

    public class EntityTypeResponseModel : ResponseModelBase
    {
        public IList<EntityType> EntityType { get; set; }
    }
    public class EntityType
    {
        public int Id { get; set; }
        public string? TextH { get; set; }
        public string? TextE { get; set; }
        public string? CreatedDate { get; set; }


    }
    public class DiscomResponseModel : ResponseModelBase
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }

    }
    public class CirleResponseModel : ResponseModelBase
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? DiscomeCode { get; set; }


    }
    public class SubStationResponseModel : ResponseModelBase
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? CircleCode { get; set; }

    }
    public class ConnectivityTypeResponseModel : ResponseModelBase
    {
        public IList<ConnectivityType> ConnectivityType { get; set; }
    }
    public class ConnectivityType
    {
        public int Id { get; set; }
        public string? TextH { get; set; }
        public string? TextE { get; set; }

    }
    public class CheckMailIdResponseModel : ResponseModelBase
    {
        public IList<CheckMailId> CheckMailId { get; set; }
    }
    public class CheckMailId
    {

        public string? EmailId { get; set; }


    }
}
