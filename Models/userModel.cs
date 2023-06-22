namespace OnlineShop.Models
{
    public class userModel
    {
        public userModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
