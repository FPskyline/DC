using Jiguang.JPush;
using Jiguang.JPush.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.JPush
{
    public class JPushClient
    {
        private const string BASE_URL = "https://api.jpush.cn/v3/push";

        public DeviceClient Device;
        public ScheduleClient Schedule;
        private ReportClient report;

        public ReportClient Report { get => report; set => report = value; }

        public static readonly HttpClient HttpClient;

        static JPushClient()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public JPushClient(string appKey, string masterSecret)
        {
            if (string.IsNullOrEmpty(appKey))
                throw new ArgumentNullException(nameof(appKey));

            if (string.IsNullOrEmpty(masterSecret))
                throw new ArgumentNullException(nameof(masterSecret));

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(appKey + ":" + masterSecret));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

            Report = new ReportClient();
            Device = new DeviceClient();
            Schedule = new ScheduleClient();
        }

        public async Task<HttpResponse> SendPushAsync(string jsonBody)
        {
            if (string.IsNullOrEmpty(jsonBody))
                throw new ArgumentNullException(nameof(jsonBody));

            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8);
            HttpResponseMessage msg = await HttpClient.PostAsync(BASE_URL, httpContent).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// <see cref="SendPush(PushPayload)"/>
        /// </summary>
        public async Task<HttpResponse> SendPushAsync(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            string body = payload.ToString();
            return await SendPushAsync(body);
        }

        /// <summary>
        /// 进行消息推送。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#_1"/>
        /// </summary>
        /// <param name="pushPayload"> 推送对象。<see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#_7"/> </param>
        public HttpResponse SendPush(PushPayload pushPayload)
        {
            Task<HttpResponse> task = Task.Run(() => SendPushAsync(pushPayload));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> IsPushValidAsync(string jsonBody)
        {
            if (string.IsNullOrEmpty(jsonBody))
                throw new ArgumentNullException(nameof(jsonBody));

            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8);
            var url = BASE_URL + "/validate";
            HttpResponseMessage msg = await HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// <see cref="IsPushValid(PushPayload)"/>
        /// </summary>
        public async Task<HttpResponse> IsPushValidAsync(PushPayload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            var body = payload.ToString();
            return await IsPushValidAsync(body);
        }

        /// <summary>
        /// 校验推送能否成功。与推送 API 的区别在于：不会实际向用户发送任何消息。 其他字段说明和推送 API 完全相同。
        /// </summary>
        /// <param name="pushPayload"> 推送对象。<see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#_7"/> </param>
        public HttpResponse IsPushValid(PushPayload pushPayload)
        {
            Task<HttpResponse> task = Task.Run(() => IsPushValidAsync(pushPayload));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetCIdList(int?, string)"/>
        /// </summary>
        public async Task<HttpResponse> GetCIdListAsync(int? count, string type)
        {
            if (count != null && count < 1 && count > 1000)
                throw new ArgumentOutOfRangeException(nameof(count));

            var url = BASE_URL + "/cid";

            if (count != null)
            {
                url += ("?count=" + count);

                if (!string.IsNullOrEmpty(type))
                    url += ("&type=" + type);
            }

            HttpResponseMessage msg = await HttpClient.GetAsync(url).ConfigureAwait(false);
            var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(msg.StatusCode, msg.Headers, content);
        }

        /// <summary>
        /// 获取 CId（推送的唯一标识符） 列表。
        /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#cid"/>
        /// </summary>
        /// <param name="count">不传默认为 1。范围为[1, 1000]</param>
        /// <param name="type">CId 的类型。取值："push" (默认) 或 "schedule"</param>
        public HttpResponse GetCIdList(int? count, string type)
        {
            Task<HttpResponse> task = Task.Run(() => GetCIdListAsync(count, type));
            task.Wait();
            return task.Result;
        }
    }
}
