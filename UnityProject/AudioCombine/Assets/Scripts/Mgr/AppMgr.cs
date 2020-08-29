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
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        gameObject.AddComponent<AndroidMgr>();

        //设置只允许横屏旋转
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToLandscapeLeft = false;
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
