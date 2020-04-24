﻿using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;

namespace HtmlAgilityPackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrStrings = File.ReadAllLines(@"C:\MyFile\aaa.txt");

            using (var httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                UseDefaultCredentials = true,
                CookieContainer = new CookieContainer()
            })
            {
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(arrStrings[4]);
                    var values = new List<KeyValuePair<string, string>>();
                    var getRequest = new HttpRequestMessage(HttpMethod.Get, arrStrings[0]);
                    var getResponse = client.SendAsync(getRequest).Result;
                    if (getResponse.IsSuccessStatusCode)
                    {
                        var content = getResponse.Content.ReadAsStringAsync().Result;
                        #region 准备参数
                        var doc = new HtmlDocument();
                        doc.LoadHtml(content);
                        foreach (var input in doc.DocumentNode.SelectNodes("//form/input"))
                        {
                            Console.WriteLine(input.GetAttributeValue("name", "") + "=" + input.GetAttributeValue("value", ""));
                            values.Add(new KeyValuePair<string, string>(input.GetAttributeValue("name", ""), input.GetAttributeValue("value", "")));

                        }
                        Console.WriteLine("---------------------");
                        foreach (var input in doc.DocumentNode.SelectNodes("//form/div/input"))
                        {
                            Console.WriteLine(input.GetAttributeValue("name", "") + "=" + input.GetAttributeValue("value", ""));
                            if (input.GetAttributeValue("name", "") == "tid")
                            {
                                values.Add(new KeyValuePair<string, string>(input.GetAttributeValue("name", ""), arrStrings[1]));
                            }
                            if (input.GetAttributeValue("name", "") == "tpasswd")
                            {
                                values.Add(new KeyValuePair<string, string>(input.GetAttributeValue("name", ""), arrStrings[2]));
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        var content = getResponse.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("返回值：" + content);
                    }
                    var postRequest = new HttpRequestMessage(HttpMethod.Post, arrStrings[3]);
                    postRequest.Content = new FormUrlEncodedContent(values);
                    var postResponse = client.SendAsync(postRequest).Result;
                    if (postResponse.IsSuccessStatusCode)
                    {
                        var content = postResponse.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("返回值：" + content);
                    }
                    else
                    {
                        var content = postResponse.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("返回值：" + content);
                    }
                }
            }

        }
    }
}
