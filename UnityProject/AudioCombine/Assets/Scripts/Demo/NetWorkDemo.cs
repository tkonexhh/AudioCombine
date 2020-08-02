using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkDemo : MonoBehaviour
{
    private void Awake()
    {
        Application.runInBackground = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        ServerMgr.S.GetAK(null);
        // ServerMgr.S.Login("123", "123123", null);
    }


}
