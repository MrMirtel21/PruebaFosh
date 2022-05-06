using System.Text.Json.Serialization;

namespace Sat.Recruitment.Api.Models.Requests
{
    public class CreateUserRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("userType")]
        public string UserType { get; set; }
        [JsonPropertyName("money")]
        public string Money { get; set; }
    }
}
