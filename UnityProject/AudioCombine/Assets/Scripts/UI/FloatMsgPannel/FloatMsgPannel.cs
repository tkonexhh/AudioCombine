using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GFrame;
using DG.Tweening;

public class FloatMsgPannel : BasePanel
{
    private static FloatMsgPannel s_Instance;
    public static FloatMsgPannel S
    {
        get
        {
            if (s_Instance == null)
            {
                var basePanel = UIMgr.S.OpenPanel("Panels/FloatMsgPannel");
                s_Instance = basePanel as FloatMsgPannel;
            }
            return s_Instance;
        }
    }

    [SerializeField] private Text m_TxtMsg;


    public void ShowMsg(string msg)
    {
        var go = GameObject.Instantiate(m_TxtMsg.gameObject, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(transform);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        var text = go.GetComponent<Text>();
        text.text = msg;
        text.transform.DOLocalMoveY(200, 1.0f).OnComplete(() =>
        {
            Destroy(go);
        });
        m_TxtMsg.text = msg;
    }
}
