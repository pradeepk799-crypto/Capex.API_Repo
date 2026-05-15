using Capex.DomainModels.DomainRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel.Masters
{
    public class DemographyResponse : DomainResponseModelBase
    {
        public IList<Demography>? DemographyList { get; set; }
    }
    public class Demography
    {
        public int DemographyId { get; set; }
        public int DemographyTypeId { get; set; }
        public string? Demography_Name_Eng { get; set; }
        public string? Demography_Name_Hi { get; set; }
        public long LGDCode { get; set; }
        public string? DemographyType { get; set; }
        public string? PatwariHalkaNumber { get; set; }

    }

    public class DomainOfficeByRevenueHeadResponseModel : DomainResponseModelBase
    {
        public IList<DomainOfficeVillageResponseModel> domainofficevillageresponsemodel { get; set; }
    }
    public class DomainOfficeVillageResponseModel
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

    public class DomainNamantaranTypeResponseModel
    {
        public IList<DomainMutationType>? DMutationTypeList { get; set; }

    }
    public class DomainMutationType
    {
        public int MenuId { get; set; }
        public string? MenuNameEng { get; set; }
        public string? MenuNameHi { get; set; }
    }
    public class DomainOfficebyheadRequestModel : DomainRequestModelBase
    {
        public int? VillageId { get; set; }
        public int? RevenueHeadId { get; set; }

    }
    public class EntityTypeDomainResponseModel: DomainResponseModelBase
    {
               
        public IList<EntityTypeDomain>? EntityTypeDomain { get; set; }

    }
    public class EntityTypeDomain
    {

        public int Id { get; set; }
        public string? TextH { get; set; }
        public string? TextE { get; set; }
        public string? CreateDate { get; set; }


    }
    public class ConnectivityTypeDomainResponseModel : DomainResponseModelBase
    {

        public IList<ConnectivityTypeDomain>? ConnectivityTypeDomain { get; set; }

    }
    public class ConnectivityTypeDomain
    {

        public int Id { get; set; }
        public string? TextH { get; set; }
        public string? TextE { get; set; }

    }

    public class CheckMailIdDomainResponseModel : DomainResponseModelBase
    {

        public IList<CheckMailIdDomain>? CheckMailIdDomain { get; set; }

    }
    public class CheckMailIdDomain
    {

       
        public string? EmailId { get; set; }
        
    }

}
