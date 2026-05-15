using Capex.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS4._0.Models.ResponseModel.Masters
{
    public class MasterDataResponse : ResponseModelBase
    {
        public IList<MasterDataType>? MasterDataList { get; set; }

    }

    public class MasterDataType
    {

        public int MasterDataId { get; set; }
        public string MasterDataNameEng { get; set; }
        public string MasterDataNameHi { get; set; }
        public int MasterDataParentId { get; set; }
        public int Code { get; set; }
    }

    public class PatwariQuestionDataResponse : ResponseModelBase
    {
        public IList<PatwariPrativedanDataType>? PatwariPrativedanDataTypeList { get; set; }

    }


    public class PatwariPrativedanDataType
    {

        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int QuestionId { get; set; }
        public string PatwariUserId { get; set; }
        public string? QuestionAnswer { get; set; }
        public int HeadId { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public Boolean Active { get; set; }
        public dynamic JsonValue { get; set; }
        public int ParentId { get; set; }
        public int ParentAnswerId { get; set; }

        public string? PoRemark { get; set; }
        public bool IsPatwariSubmited { get; set; }
    }

   

}
