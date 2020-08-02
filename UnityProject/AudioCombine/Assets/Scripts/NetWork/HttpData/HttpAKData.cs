using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpAKData : HttpData
{
    public static string portPath = ApiPath.AK;

    public static string portMethod
    {
        get { return "GET"; }
    }


    public class DataSend
    {
        public string cgid = "cgid";
    }

    public class DataReceive
    {
        public string retCode { get; set; }
        public string retResp { get; set; }
        public SubData data { get; set; }
    }

    public class SubData
    {
        public string ak { get; set; }
    }
}
