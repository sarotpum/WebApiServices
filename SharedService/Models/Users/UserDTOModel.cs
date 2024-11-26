namespace SharedService.Models.User
{
    public class UserDTOModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreate { get; set; }
        public string Token { get; set; }

        public UserDTOModel(string fullName, string email, string userName, DateTime dateCreate)
        {
            this.FullName = fullName;
            this.Email = email;
            this.UserName = userName;
            this.DateCreate = dateCreate;
        }
    }
}
