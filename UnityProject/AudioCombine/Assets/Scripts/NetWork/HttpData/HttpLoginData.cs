using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpLoginData : HttpData
{
    public static string portPath = ApiPath.LOGIN;

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
        public SubData data { get; set; }
    }

    public class SubData
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string loginToken { get; set; }
        public string userName { get; set; }
        public List<string> roles { get; set; }
    }
}
