using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GFrame;

public class LoginPanel : BasePanel
{
    [SerializeField] private InputField m_InputUsername;
    [SerializeField] private InputField m_InputPasswword;
    [SerializeField] private Button m_BtnLogin;


    protected override void OnInit()
    {
        m_BtnLogin.onClick.AddListener(OnClickLogin);
    }


    private void OnClickLogin()
    {
        //  ServerMgr.S.Login("20200705", "etJkgmPm", null);
        if (string.IsNullOrEmpty(m_InputUsername.text) || string.IsNullOrEmpty(m_InputPasswword.text))
        {
            Debug.LogError("请输入用户名密码");
            return;
        }
        string username = m_InputUsername.text;
        string pwd = m_InputPasswword.text;
        // username = "20200705";
        // pwd = "etJkgmPm";
        ServerMgr.S.Login(username, pwd, (dataReceive) =>
        {
            ClosePanel();
            AndroidMgr.S.SetJPushAlias("huakai" + username.ToLower());

            UIMgr.S.OpenPanel("Panels/ListenPannel");
            DataRecord.S.SetString(Define.SAVEKEY_USERNAME, username);
            string loginToken = dataReceive.data.loginToken;
            //Debug.LogError("Unity --loginToken" + loginToken);
            DataRecord.S.SetString(Define.SAVEKEY_LOGINTOKEN, loginToken);
            DataRecord.S.Save();
            // ServerMgr.S.PushTest(loginToken, (data) =>
            // {
            //     Debug.LogError(data.retResp);
            // });
        });
    }
}
