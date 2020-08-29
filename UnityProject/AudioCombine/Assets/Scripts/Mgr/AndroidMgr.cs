using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using GFrame;
using Qarth;

public class AndroidMgr : MonoBehaviour
{
    private static AndroidMgr s_Instance;

    public static AndroidMgr S
    {
        get
        {
            return s_Instance;
        }
    }

    public void SetJPushAlias(string alias)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.CallStatic("SetAlias", currentActivity, alias);
#endif
    }

    void Awake()
    {
        s_Instance = this;
#if UNITY_ANDROID && !UNITY_EDITOR
        //调用Java注册Listener  
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("setListener", new AndroidListener());
        UnityEngine.Debug.LogError("Awake");
#endif
    }
}

public class AndroidListener : AndroidJavaProxy
{
    //所对应的类名
    public AndroidListener() : base("com.unity3d.player.IPushListener")
    {
    }

    public void onAliasOperatorResult(string msg)//和java中接口同名
    {
        UnityEngine.Debug.LogError("JIGUANG-Example" + msg);

        msg = msg.Replace("{", "");
        msg = msg.Replace("}", "");
        msg = msg.Replace(" ", "");
        var values = Helper.String2ListString(msg, ",");
        for (int i = 0; i < values.Count; i++)
        {
            var steps = Helper.String2ListString(values[i], "=");
            // Debug.LogError(steps[0]);
            if (steps[0].Equals("errorCode"))
            {
                // Debug.LogError(steps[0]);
                int errorCode = int.Parse(steps[1]);
                // Debug.LogError("----" + errorCode);
                if (errorCode != 0)
                {
                    AndroidMgr.S.StartCoroutine(SetAlias());
                }
                else if (errorCode == 0)
                {
                    string loginToken = DataRecord.S.GetString(Define.SAVEKEY_LOGINTOKEN, "");
                    //Debug.LogError("Unity --onAliasOperatorResult" + loginToken);
                    if (!string.IsNullOrEmpty(loginToken))
                    {
                        ServerMgr.S.PushTest(loginToken, (data) =>
                        {
                            //Debug.LogError("Unity --PushTest Success" + data.retResp);
                        });
                    }
                }
                break;
            }
        }

        //{alias='huakai0512', tags=null, checkTag='null', errorCode=0, tagCheckStateResult=false, isTagCheckOperator=false, sequence=0, mobileNumber=null}
        // var message = LitJson.JsonMapper.ToObject<JPushMessage>(msg);
        // if (message != null)
        // {
        //     if (message.errorCode != 0)
        //     {
        //         AndroidMgr.S.StartCoroutine(SetAlias());

        //         //测试一下是否能收到通知
        //         string loginToken = DataRecord.S.GetString(Define.SAVEKEY_LOGINTOKEN, "");
        //         Debug.LogError("Unity --onAliasOperatorResult" + loginToken);
        //         if (!string.IsNullOrEmpty(loginToken))
        //         {
        //             ServerMgr.S.PushTest(loginToken, (data) =>
        //             {
        //                 Debug.LogError("Unity --PushTest Success" + data.retResp);
        //             });
        //         }

        //     }
        // }
    }

    public void processCustomMessage(string extra)
    {
        UnityEngine.Debug.LogError("JIGUANG-Example" + extra);
        if (ListenPannel.S != null)
        {
            UnityEngine.Debug.LogError("JIGUANG-Example2" + extra);
            //ListenPannel.S.PlayAudio(123.23f);
            extra = extra.Replace("\"", "");
            extra = extra.Replace("{", "");
            extra = extra.Replace("}", "");
            Debug.LogError("Demo" + extra);
            var lstExtra = Helper.String2ListString(extra, ":");
            if (lstExtra.Count <= 1) return;
            string cashStr = lstExtra[1];
            Debug.LogError("Demo" + cashStr);
            float cash = -1;
            if (float.TryParse(cashStr, out cash))
            {
                ListenPannel.S.PlayAudio(cash);
            }

        }
    }

    IEnumerator SetAlias()
    {
        yield return new WaitForSeconds(2.0f);
        string username = DataRecord.S.GetString(Define.SAVEKEY_USERNAME, "");
        if (!string.IsNullOrEmpty(username))
        {
            AndroidMgr.S.SetJPushAlias("huakai" + username.ToLower());
        }

    }
}


//JPushMessage{alias='demo', tags=null, checkTag='null', errorCode=0, tagCheckStateResult=false, isTagCheckOperator=false, sequence=0, mobileNumber=null}
public class JPushMessage
{
    public string alias = "123";
    public string tags = "123";
    public string checkTag = "123";
    public int errorCode = -1;
    public bool tagCheckStateResult = false;
    public bool isTagCheckOperator = false;
    public int sequence = -1;

}

public class MessageExtra
{
    public string amount;
}


