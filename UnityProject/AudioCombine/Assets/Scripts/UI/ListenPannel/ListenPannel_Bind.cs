using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using GFrame;

public class ListenPannel_Bind : MonoBehaviour
{
    [SerializeField] private GameObject m_ObjUnBind;
    [SerializeField] private Button m_BtnBind;

    [SerializeField] private GameObject m_ObjBinding;
    [SerializeField] private GameObject m_ObjBinded;

    private void Awake()
    {
        m_BtnBind.onClick.AddListener(OnClickBind);

        EventSystem.S.Register(EventID.OnPushTestStart, OnPushTestStart);
        EventSystem.S.Register(EventID.OnPushTestEnded, OnPushTestEnded);
    }

    void Start()
    {
        OnPushTestEnded(0);
    }

    private void OnClickBind()
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

    private void OnPushTestStart(int key, params object[] args)
    {
        m_ObjUnBind.SetActive(false);
        m_ObjBinding.SetActive(true);
        m_ObjBinded.SetActive(false);
    }

    private void OnPushTestEnded(int key, params object[] args)
    {
        m_ObjBinding.SetActive(false);
        bool isbinded = DataRecord.S.GetBool(Define.SAVEKEY_PUSHTEST, false);
        //Debug.LogError("Isbinded" + isbinded);
        m_ObjBinded.SetActive(isbinded);
        m_ObjUnBind.SetActive(!isbinded);
    }

}
