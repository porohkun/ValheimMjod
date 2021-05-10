using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ValheimMjod
{
    public class PlayFab
    {
        private const string TitleId = "333d2";

        public static void LoginUser()
        {
            var userId = Settings.Instance.Main.UserId ?? Guid.NewGuid().ToString();

            var data = new LoginWithCustomID()
            {
                TitleId = TitleId,
                CreateAccount = true,
                CustomId = userId,
                CustomTags = new Dictionary<string, string>()
                {
                    { "version", Settings.Version.ToString() },
                }
            };

            Task.Run(() => Send(data, r =>
            {
                var simpleResponse = JsonConvert.DeserializeObject<SimpleResponse>(r);
                if (simpleResponse.Code == 200)
                {
                    Settings.Instance.Main.UserId = userId;
                }
            }));
        }

        private static async void Send(object data, Action<string> callback)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"https://{TitleId}.playfabapi.com/Client/{data.GetType().Name}", content);
                var result = response.Content.ReadAsStringAsync().Result;
                callback?.Invoke(result);
            }
        }
    }

    public class LoginWithCustomID
    {
        public string TitleId;
        public bool CreateAccount;
        public string CustomId;
        public Dictionary<string, string> CustomTags;
    }

    public class SimpleResponse
    {
        [JsonProperty("code")]
        public int Code;
        [JsonProperty("status")]
        public string Status;
    }
}
