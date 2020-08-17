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
        m_HttpHandler.GetAK(loginName, (data) =>
        {
            string ak = data.data.ak;
            if (string.IsNullOrEmpty(ak)) return;
            //Debug.LogError("AK:" + ak);
            string encrpyPassword = EncryptUtil.Md532(EncryptUtil.Md532(password) + ak);
            //Debug.LogError("pwd:" + encrpyPassword);
            m_HttpHandler.Login(loginName, encrpyPassword, (loginData) =>
            {
                if (callback != null && IsResponseOK(loginData.retCode))
                    callback(loginData);
            });
        });

    }


    private bool IsResponseOK(string code)
    {
        return code.Equals("0000");
    }


}

