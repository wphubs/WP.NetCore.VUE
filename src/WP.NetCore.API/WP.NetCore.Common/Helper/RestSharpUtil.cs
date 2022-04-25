﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Common.Helper
{
    public class RestSharpUtil
    {
        /// <summary>
        /// Get 请求
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="baseUrl"></param>
        /// <param name="url">接口:api/xx/yy</param>
        /// <param name="pragm">参数:id=2&name=张三</param>
        /// <returns></returns>
        public static async Task<T> GetApiAsync<T>(string baseUrl, string url, string pragm = "")
        {
            var client = new RestClient(baseUrl);
            var request = await client.ExecuteAsync(string.IsNullOrEmpty(pragm)
                ? new RestRequest(url, Method.GET)
                : new RestRequest($"{url}?{pragm}", Method.GET));

            if (request.StatusCode != HttpStatusCode.OK)
            {
                return (T)Convert.ChangeType(request.ErrorMessage, typeof(T));
            }
            dynamic temp = Newtonsoft.Json.JsonConvert.DeserializeObject(request.Content, typeof(T));
            return (T)temp;
        }

        public static T GetApi<T>(string baseUrl, string url, string pragm = "")
        {
            var client = new RestClient(baseUrl);
            var request = client.Execute(string.IsNullOrEmpty(pragm)
                ? new RestRequest(url, Method.GET)
                : new RestRequest($"{url}?{pragm}", Method.GET));

            if (request.StatusCode != HttpStatusCode.OK)
            {
                return (T)Convert.ChangeType(request.ErrorMessage, typeof(T));
            }
            dynamic temp = Newtonsoft.Json.JsonConvert.DeserializeObject(request.Content, typeof(T));
            return (T)temp;
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="url">完整的url</param>
        /// <param name="body">post body,可以匿名或者反序列化</param>
        /// <returns></returns>
        public static T PostApi<T>(string url, object body = null)
        {
            var client = new RestClient($"{url}");
            IRestRequest queest = new RestRequest();
            queest.Method = Method.POST;
            queest.AddHeader("Accept", "application/json");
            queest.AddJsonBody(body); // 可以使用 JsonSerializer
            var result = client.Execute(queest);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return (T)Convert.ChangeType(result.ErrorMessage, typeof(T));
            }
            dynamic temp = Newtonsoft.Json.JsonConvert.DeserializeObject(result.Content, typeof(T));
            return (T)temp;
        }


        public static T GetEncrypt<T>(string url, object pars)
        {
            var type = Method.GET;
            IRestResponse<T> reval = GetApiInfo<T>(url, pars, type);
            return reval.Data;
        }

        private static IRestResponse<T> GetApiInfo<T>(string url, object pars, Method type)
        {
            var request = new RestRequest(type);
            if (pars != null)
            {
                //request.AddObject(pars);
                foreach (var item in (IDictionary<string, object>)pars)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            }
            var client = new RestClient(url);
            client.CookieContainer = new System.Net.CookieContainer();
            IRestResponse<T> reval = client.Execute<T>(request);
            string err = "无异常";
            if (reval.ErrorException != null) err = reval.ErrorMessage;
            var aa = reval.Data;
            //LogsUtil.Info("Sinomedisite.Store.Api：" + "地址:【" + url + "】 请求参数:【" + Newtonsoft.Json.JsonConvert.SerializeObject(pars) + "】返回值:【" + Newtonsoft.Json.JsonConvert.SerializeObject(reval.Data) + "】 异常消息:【" + err + "】");
            if (reval.ErrorException != null)
            {
                throw new Exception(url + "：" + err);
            }
            return reval;
        }
    }
}
