using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaChain.API.AuthModel
{
    internal class ExtUserData
    {
        public long Id { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }


    internal class ExtUserAccessTokenData
    {
        [JsonProperty("AppId")]
        public long AppId { get; set; }
        public string Type { get; set; }
        public string Application { get; set; }
        [JsonProperty("ExpiresAt")]
        public long ExpiresAt { get; set; }
        [JsonProperty("IsValid")]
        public bool IsValid { get; set; }
        [JsonProperty("userId")]
        public string Username { get; set; }
    }

    public class ExtResp
    {
        public string AccessToken { get; set; }

        public string email { get; set; }
        public string Username { get; set; }

    }
    internal class UserInfo
    {
        public string Username { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
