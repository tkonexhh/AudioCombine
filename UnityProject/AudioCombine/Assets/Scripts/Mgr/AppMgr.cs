using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFrame;
using LitJson;
using Qarth;

public class AppMgr : MonoBehaviour
{
    private static AppMgr s_Instane;

    public static AppMgr S
    {
        get
        {
            // if (s_Instane == null)
            // {
            //     s_Instane = this;
            // }
            return s_Instane;
        }
    }

    private bool m_IsOpenSubsides;
    public bool isOpenSubsides
    {
        get
        {
            m_IsOpenSubsides = PlayerPrefs.GetInt(Define.SAVEKEY_OPENSUBSIDES, 0) == 1;
            return m_IsOpenSubsides;
        }
        set
        {
            m_IsOpenSubsides = value;
            PlayerPrefs.SetInt(Define.SAVEKEY_OPENSUBSIDES, m_IsOpenSubsides ? 1 : 0);
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
