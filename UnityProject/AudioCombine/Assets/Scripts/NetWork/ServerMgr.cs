using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Qarth;
using System.Linq;
using UnityEngine.Networking;

public class ServerMgr : TMonoSingleton<ServerMgr>
{
    public void GetAK(Action<HttpAKData.DataReceive> callback)
    {
        RestClient.Request(GetRequestHelper_Post(ApiPath.AK)).Then(response =>
       {
           Debug.LogError("response:" + response.Request);
           Debug.LogError("response:" + response.Text);
           if (response.StatusCode == 200 && string.IsNullOrEmpty(response.Error))
           {
               Debug.LogError("11111111");
               // var data = LitJson.JsonMapper.ToObject<HttpLoginData.DataReceive>(response.Text);
               // if (data != null && callback != null)
               // {
               //     if (data.code != 0)
               //     {
               //         Log.e("data.code  = " + data.code + "  missionId = " + missionId);// + "  mission_id= " + data.data.record.mission_id);
               //     }
               //     callback.Invoke(data);
               //     callback = null;
               // }
           }
           else
           {
               Debug.LogError("22222");
               if (callback != null)
               {
                   callback.Invoke(null);
                   callback = null;
               }
               Debug.LogError(response.StatusCode + " >>> " + response.Error);
           }
       }, reject =>
       {
           Debug.LogError("333333");
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

        Boo.Lang.Hash arg = new Boo.Lang.Hash();
        arg[send.loginName] = loginName;
        arg[send.password] = password;

        RestClient.Request(GetRequestHelper_Post(ApiPath.LOGIN, arg)).Then(response =>
        {
            Debug.LogError("response:" + response.Request);
            Debug.LogError("response:" + response.Text);
            if (response.StatusCode == 200 && string.IsNullOrEmpty(response.Error))
            {
                Debug.LogError("11111111");
                // var data = LitJson.JsonMapper.ToObject<HttpLoginData.DataReceive>(response.Text);
                // if (data != null && callback != null)
                // {
                //     if (data.code != 0)
                //     {
                //         Log.e("data.code  = " + data.code + "  missionId = " + missionId);// + "  mission_id= " + data.data.record.mission_id);
                //     }
                //     callback.Invoke(data);
                //     callback = null;
                // }
            }
            else
            {
                Debug.LogError("22222");
                if (callback != null)
                {
                    callback.Invoke(null);
                    callback = null;
                }
                Debug.LogError(response.StatusCode + " >>> " + response.Error);
            }
        }, reject =>
        {
            Debug.LogError("333333");
            if (callback != null)
            {
                callback.Invoke(null);
                callback = null;
            }
            Debug.LogError(reject.Message);
        })
            .Catch(e => Debug.LogError(e));
    }

    RequestHelper GetRequestHelper_Post(string path, Boo.Lang.Hash args)
    {
        var request = new RequestHelper
        {
            Uri = NetWorkDefine.baseUrl + path,
            Method = "POST",
            Timeout = 10,
            ContentType = "application/json",
            BodyString = LitJson.JsonMapper.ToJson(args),
            CertificateHandler = new BypassCertificate(),
            EnableDebug = true,
        };
        request.Headers.Add("Accept", "application/json");
        Debug.LogError(request.BodyString);
        return request;
    }

    RequestHelper GetRequestHelper_Post(string path, Dictionary<string, string> queries = null)
    {
        var request = new RequestHelper
        {
            Uri = NetWorkDefine.baseUrl + path,
            Method = "GET",
            Timeout = 10,
            Params = queries,
            ContentType = "application/json",
            CertificateHandler = new BypassCertificate(),
            EnableDebug = true,
        };
        request.Headers.Add("Accept", "application/json");
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
