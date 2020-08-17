using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        username = "20200705";
        pwd = "etJkgmPm";
        ServerMgr.S.Login(username, pwd, (dataReceive) =>
        {
            //TODO
            //设置alias
            Debug.LogError("code:" + dataReceive.retCode);
            //保存登陆状态
            ClosePanel();
            //设置alias
            Debug.LogError("huakai" + username.ToLower());
            AndroidMgr.S.SetJPushAlias("huakai" + username.ToLower());
            UIMgr.S.OpenPanel("Panels/ListenPannel");

        });
    }
}
