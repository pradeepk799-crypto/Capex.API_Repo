using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel
{
    //public class DFileUploadResponseModel
    //{
    //    public Guid FileUploadID { get; set; }   
    //    public string? FilePath { get; set; }
    //    public Boolean status { get; set; }
    //    public string? msg { get; set; }
    //}


    public class DResponseUploadID
    {     
        public int UploadID { get; set; }
        public Boolean status { get; set; }


    }

}
