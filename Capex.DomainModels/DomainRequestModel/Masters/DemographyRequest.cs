using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel.Masters
{
    public class DemographyRequest : DomainRequestModelBase
    {
        public int? DemographyId { get; set; }
        public int DemographyTypeId { get; set; }
        public int? ParentDemographyId { get; set; }
    }

    public class MultipleDemographyRequest : DomainRequestModelBase
    {
        public int? demographyTypeId { get; set; }
        public string demographyIdsList { get; set; }
        
    }
    public class EntityTypeDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }

    }
    public class EntityDetailForInsertUpdateDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }
        public string? EntityId { get; set; }
        public string? EntityNameH { get; set; }
        public string? EntityNameE { get; set; }
        public bool? IsActive { get; set; }


    }
    public class ConnectivityTypeDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }

    }
    public class ConnectivityDetailForInsertUpdateDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }
        public string? Connectivity_Id { get; set; }
        public string? Connectivity_Name_H { get; set; }
        public string? Connectivity_Name_E { get; set; }
        public bool? IsActive { get; set; }


    }
    public class CheckMailIdDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }

    }
    public class EntityTypeListDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }
        public string? EntityName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class ConnectivityTypeListDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }
        public string? ConnectivityName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsActive { get; set; }  

    }

}
