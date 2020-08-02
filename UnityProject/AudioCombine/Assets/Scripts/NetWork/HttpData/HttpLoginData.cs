using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpLoginData : HttpData
{
    public static string portPath
    {
        get => ApiPath.LOGIN;
    }

    public static string portMethod
    {
        get { return "POST"; }
    }

    public class DataSend
    {
        public string loginName = "loginName";
        public string password = "password";
    }

    public class DataReceive
    {
        public string retCode { get; set; }
        public string retResp { get; set; }
    }
}
