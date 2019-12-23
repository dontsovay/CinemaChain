using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaChain.API.AuthModel
{
    public class ExtAuthSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
    internal class ExtUserAccessTokenValidation
    {
        public ExtUserAccessTokenData Data { get; set; }
    }

    internal class FacebookAppAccessToken
    {
        [JsonProperty("TokenType")]
        public string TokenType { get; set; }
        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }
    }
}
