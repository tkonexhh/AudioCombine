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
        UnityEngine.Debug.LogError("Demo" + msg);
        var message = LitJson.JsonMapper.ToObject<JPushMessage>(msg);
        if (message != null)
        {
            if (message.errorCode != 0)
            {
                AndroidMgr.S.StartCoroutine(SetAlias());

                //测试一下是否能收到通知
                string loginToken = DataRecord.S.GetString(Define.SAVEKEY_LOGINTOKEN, "");
                if (!string.IsNullOrEmpty(loginToken))
                {
                    ServerMgr.S.PushTest(loginToken, (data) =>
                    {

                    });
                }

            }
        }
    }

    public void processCustomMessage(string extra)
    {
        UnityEngine.Debug.LogError("Demo" + extra);
        if (ListenPannel.S != null)
        {
            //ListenPannel.S.PlayAudio(123.23f);
            extra = extra.Replace("\"", "");
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
        AndroidMgr.S.SetJPushAlias("huakai20200705");
    }
}


//JPushMessage{alias='demo', tags=null, checkTag='null', errorCode=0, tagCheckStateResult=false, isTagCheckOperator=false, sequence=0, mobileNumber=null}
public class JPushMessage
{
    public string alias;
    public string tags;
    public string checkTag;
    public int errorCode;
}

public class MessageExtra
{
    public string amount;
}


