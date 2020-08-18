using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpPushData : HttpData
{
    public static string portPath = ApiPath.PUSH;
    public static string portMethod = "GET";

    public class DataSend
    {
        public string loginToken = "loginToken";
    }

    public class DataReceive
    {
        public string retCode { get; set; }
        public string retResp { get; set; }
        public SubData data { get; set; }
    }

    public class SubData
    {
    }
}