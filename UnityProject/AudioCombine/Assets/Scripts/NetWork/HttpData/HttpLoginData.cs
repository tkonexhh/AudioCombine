using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpLoginData : HttpData
{
    public override string portPath
    {
        get => ApiPath.AK;
    }

    public override string portMethod
    {
        get { return "GET"; }
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
