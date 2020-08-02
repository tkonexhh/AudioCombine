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
    private HttpHandler m_HttpHandler;

    public override void OnSingletonInit()
    {
        m_HttpHandler = new HttpHandler();
    }


    public void Login(string loginName, string password, Action<HttpLoginData.DataReceive> callback)
    {

        m_HttpHandler.GetAK((data) =>
        {
            string ak = data.data.ak;
            if (string.IsNullOrEmpty(ak)) return;
            Debug.LogError("AK:" + ak);
            //TODO MD5
            password = EncryptUtil.Md532(EncryptUtil.Md532(password) + ak);
            Debug.LogError(password);
            m_HttpHandler.Login(loginName, password, (loginData) =>
            {
                Debug.LogError(loginData.retCode);
            });
        });
        // var send = new HttpLoginData.DataSend();
        // Dictionary<string, string> qs = new Dictionary<string, string>();
        // qs.Add(send.loginName, loginName);
        // qs.Add(send.password, password);

        // RestClient.Request(GetRequestHelper_Post(ApiPath.LOGIN, qs)).Then(response =>
        // {
        //     Debug.LogError("response:" + response.Request);
        //     Debug.LogError("response:" + response.Text);
        //     if (response.StatusCode == 200 && string.IsNullOrEmpty(response.Error))
        //     {
        //         Debug.LogError("11111111");
        //         // var data = LitJson.JsonMapper.ToObject<HttpLoginData.DataReceive>(response.Text);
        //         // if (data != null && callback != null)
        //         // {
        //         //     if (data.code != 0)
        //         //     {
        //         //         Log.e("data.code  = " + data.code + "  missionId = " + missionId);// + "  mission_id= " + data.data.record.mission_id);
        //         //     }
        //         //     callback.Invoke(data);
        //         //     callback = null;
        //         // }
        //     }
        //     else
        //     {
        //         Debug.LogError("22222");
        //         if (callback != null)
        //         {
        //             callback.Invoke(null);
        //             callback = null;
        //         }
        //         Debug.LogError(response.StatusCode + " >>> " + response.Error);
        //     }
        // }, reject =>
        // {
        //     Debug.LogError("333333");
        //     if (callback != null)
        //     {
        //         callback.Invoke(null);
        //         callback = null;
        //     }
        //     Debug.LogError(reject.Message);
        // })
        //     .Catch(e => Debug.LogError(e));
    }


}

