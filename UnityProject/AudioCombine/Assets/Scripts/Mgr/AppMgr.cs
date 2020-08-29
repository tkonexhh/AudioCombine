using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFrame;
using LitJson;
using Qarth;

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

        // string str = "{alias='huakai0512', tags=null, checkTag='null', errorCode=0, tagCheckStateResult=false, isTagCheckOperator=false, sequence=0, mobileNumber=null}";
        // str = str.Replace("{", "");
        // str = str.Replace("}", "");
        // str = str.Replace(" ", "");
        // var values = Helper.String2ListString(str, ",");
        // for (int i = 0; i < values.Count; i++)
        // {
        //     // Debug.LogError(values[i]);
        //     var steps = Helper.String2ListString(values[i], "=");
        //     Debug.LogError(steps[0]);
        //     if (steps[0].Equals("errorCode"))
        //     {
        //         Debug.LogError(steps[0]);
        //         int errorCode = int.Parse(steps[1]);
        //         Debug.LogError("----" + errorCode);
        //         break;
        //     }
        // }

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
