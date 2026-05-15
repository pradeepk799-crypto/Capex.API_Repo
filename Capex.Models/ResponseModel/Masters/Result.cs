namespace Capex.Models.ResponseModel.Masters
{
    public class Result<T>
    {
        public bool Status { get; set; }
        public List<string> Message { get; set; }
        public T Data { get; set; }

        public Result()
        {
            this.Status = false;
            this.Message = new List<string>() { };
        }
    }
    
}
