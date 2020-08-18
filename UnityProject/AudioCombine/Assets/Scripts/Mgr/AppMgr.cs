using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFrame;

public class AppMgr : MonoBehaviour
{
    private static AppMgr s_Instane;

    public AppMgr S
    {
        get
        {
            if (s_Instane == null)
            {
                s_Instane = this;
            }
            return s_Instane;
        }
    }

    private void Awake()
    {
        s_Instane = this;
        gameObject.AddComponent<AndroidMgr>();
    }


    private void Start()
    {
        string usernameSave = DataRecord.S.GetString(Define.SAVEKEY_USERNAME, "");
        if (string.IsNullOrEmpty(usernameSave))
        {
            UIMgr.S.OpenPanel("Panels/LoginPanel");
        }
        else
        {
            UIMgr.S.OpenPanel("Panels/ListenPannel");
        }

    }
}
