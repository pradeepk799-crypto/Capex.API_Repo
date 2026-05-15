namespace Capex.Models.ResponseModel.Masters
{
    public class UserResponseModel : ResponseModelBase
    {

        public IList<UserModel> UserResponse { get; set; }
    }
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string UserType { get; set; }
        public string ProfileId { get; set; }
    }
}
