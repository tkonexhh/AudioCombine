using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpTestData : HttpData
{
    public static string portPath = ApiPath.TESTPUSH;
    public static string portMethod = "GET";

    public class DataSend
    {
        public string jPushMsg = "jPushMsg";
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