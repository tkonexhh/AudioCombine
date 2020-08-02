using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;

public class NetWorkDemo : MonoBehaviour
{
    private void Awake()
    {
        Application.runInBackground = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError(EncryptUtil.Md532("etJkgmPm"));
        ServerMgr.S.Login("20200705", "etJkgmPm", null);
        // ServerMgr.S.Login("123", "123123", null);
    }


}
