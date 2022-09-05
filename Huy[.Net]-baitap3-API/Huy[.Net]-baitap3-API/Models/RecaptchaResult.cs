using Microsoft.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Huy_.Net__baitap3_API.Models
{

    public class RecaptchaResult
    {
        private readonly HttpClient captchaClient;

        public RecaptchaResult(HttpClient captchaClient)
        {
            this.captchaClient = captchaClient;
        }

        public async Task<bool> IsValid(string captcha)
        {
            try
            {
                //6LdHcTwUAAAAAD_al4p9Zp7qMDvyZy6cCXW51hKEr
                var secretKey = "6LdHcTwUAAAAAD_al4p9Zp7qMDvyZy6cCXW51hKEr";
                var postTask = await captchaClient
                    .PostAsync($"?secret={secretKey}&response={captcha}", new StringContent(""));
                var result = await postTask.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(result);
                dynamic success = resultObject["success"];
                return (bool)success;
            }
            catch (Exception e)
            {
                // TODO: log this (in elmah.io maybe?)
                return false;
            }
        }

    }
}
