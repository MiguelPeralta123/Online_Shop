using System.Security.Claims;

namespace OnlineShop.Models
{
    public class JwtModel
    {
        public string key { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
        public string subject { get; set; }

        public static dynamic validateToken(ClaimsIdentity identity, List<userModel> usersList) 
        {
            try 
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Invalid token",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

                userModel user = usersList.FirstOrDefault(x => x.id.ToString() == id);

                return new
                {
                    success = true,
                    message = "Successful token validation",
                    result = user
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error while validating token: " + ex.Message,
                    result = ""
                };
            }
        }
    }
}
