using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{

    private void Awake()
    {
        OnInit();
    }



    protected virtual void OnInit()
    {

    }


    protected void ClosePanel()
    {
        GameObject.Destroy(gameObject);
    }

}
