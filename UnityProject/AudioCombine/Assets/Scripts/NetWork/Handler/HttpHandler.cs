﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Qarth;
using System;
using UnityEngine.Networking;

public class HttpHandler
{
    public void GetAK(string cgid, Action<HttpAKData.DataReceive> callback)
    {
        var send = new HttpAKData.DataSend();
        Dictionary<string, string> qs = new Dictionary<string, string>();
        qs.Add(send.cgid, cgid);

        RestClient.Request(GetRequestHelper(HttpAKData.portPath, HttpAKData.portMethod, qs)).Then(response =>
        {
            //Debug.LogError(response.Request.uri);
            if (response.StatusCode == 200 && string.IsNullOrEmpty(response.Error))
            {
                var data = LitJson.JsonMapper.ToObject<HttpAKData.DataReceive>(response.Text);
                if (data != null && callback != null)
                {
                    if (data.retCode != "0000")
                    {
                        Log.e("data.code  = " + data.retCode);
                    }
                    callback.Invoke(data);
                    callback = null;
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(null);
                    callback = null;
                }
                Debug.LogError(response.StatusCode + " >>> " + response.Error);
            }
        }, reject =>
        {
            if (callback != null)
            {
                callback.Invoke(null);
                callback = null;
            }
            Debug.LogError(reject.Message);
        })
           .Catch(e => Debug.LogError(e));
    }

    public void Login(string loginName, string password, Action<HttpLoginData.DataReceive> callback)
    {
        var send = new HttpLoginData.DataSend();
        Dictionary<string, string> qs = new Dictionary<string, string>();
        qs.Add(send.loginName, loginName);
        qs.Add(send.password, password);

        RestClient.Request(GetRequestHelper(HttpLoginData.portPath, HttpLoginData.portMethod, qs)).Then(response =>
        {
            //Debug.LogError(response.Request.uri);
            if (response.StatusCode == 200 && string.IsNullOrEmpty(response.Error))
            {
                var data = LitJson.JsonMapper.ToObject<HttpLoginData.DataReceive>(response.Text);
                if (data != null && callback != null)
                {
                    if (data.retCode != "0000")
                    {
                        Log.e("data.code  = " + data.retCode);
                    }
                    callback.Invoke(data);
                    callback = null;
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(null);
                    callback = null;
                }
                Debug.LogError(response.StatusCode + " >>> " + response.Error);
            }
        }, reject =>
        {
            if (callback != null)
            {
                callback.Invoke(null);
                callback = null;
            }
            Debug.LogError(reject.Message);
        })
          .Catch(e => Debug.LogError(e));
    }

    public void PushTest(string loginToken, Action<HttpPushData.DataReceive> callback)
    {
        var send = new HttpPushData.DataSend();
        Dictionary<string, string> qs = new Dictionary<string, string>();
        //qs.Add(send.loginToken, loginToken);

        Dictionary<string, string> header = new Dictionary<string, string>();
        header.Add(send.loginToken, loginToken);

        RestClient.Request(GetRequestHelper(HttpPushData.portPath, HttpPushData.portMethod, qs, header)).Then(response =>
        {
            if (response.StatusCode == 200 && string.IsNullOrEmpty(response.Error))
            {
                var data = LitJson.JsonMapper.ToObject<HttpPushData.DataReceive>(response.Text);
                if (data != null && callback != null)
                {
                    if (data.retCode != "0000")
                    {
                        Log.e("data.code  = " + data.retCode);
                    }
                    callback.Invoke(data);
                    callback = null;
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(null);
                    callback = null;
                }
                Debug.LogError(response.StatusCode + " >>> " + response.Error);
            }
        }, reject =>
        {
            if (callback != null)
            {
                callback.Invoke(null);
                callback = null;
            }
            Debug.LogError(reject.Message);
        })
          .Catch(e => Debug.LogError(e));
    }

    public void Test(string msg, Action<HttpTestData.DataReceive> callback)
    {
        var send = new HttpTestData.DataSend();
        Dictionary<string, string> qs = new Dictionary<string, string>();
        //qs.Add(send.loginToken, loginToken);

        Dictionary<string, string> header = new Dictionary<string, string>();
        header.Add(send.jPushMsg, msg);

        RestClient.Request(GetRequestHelper(HttpTestData.portPath, HttpTestData.portMethod, qs, header)).Then(response =>
        {
            if (response.StatusCode == 200 && string.IsNullOrEmpty(response.Error))
            {
                var data = LitJson.JsonMapper.ToObject<HttpTestData.DataReceive>(response.Text);
                if (data != null && callback != null)
                {
                    if (data.retCode != "0000")
                    {
                        Log.e("data.code  = " + data.retCode);
                    }
                    callback.Invoke(data);
                    callback = null;
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(null);
                    callback = null;
                }
                Debug.LogError(response.StatusCode + " >>> " + response.Error);
            }
        }, reject =>
        {
            if (callback != null)
            {
                callback.Invoke(null);
                callback = null;
            }
            Debug.LogError(reject.Message);
        })
          .Catch(e => Debug.LogError(e));
    }


    RequestHelper GetRequestHelper(string path, string method, Dictionary<string, string> queries)
    {
        var request = new RequestHelper
        {
            Uri = NetWorkDefine.baseUrl + path,
            Method = method,
            Timeout = 10,
            Retries = 1,
            Params = queries,
            ContentType = "application/json",
            CertificateHandler = new BypassCertificate(),

        };
        return request;
    }




    RequestHelper GetRequestHelper(string path, string method, Dictionary<string, string> queries, Dictionary<string, string> headers)
    {
        var request = new RequestHelper
        {
            Uri = NetWorkDefine.baseUrl + path,
            Method = method,
            Timeout = 10,
            Retries = 1,
            Params = queries,
            ContentType = "application/json",
            CertificateHandler = new BypassCertificate(),
            Headers = headers,
        };
        return request;
    }
}


public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        //Simply return true no matter what
        return true;
    }
}
