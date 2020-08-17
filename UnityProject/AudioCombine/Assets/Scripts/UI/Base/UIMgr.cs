using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    private static UIMgr s_Instane;

    public static UIMgr S
    {
        get
        {
            // if (s_Instane == null)
            // {
            //     s_Instane = this;
            // }
            return s_Instane;
        }
    }

    [SerializeField] private Canvas m_CannvasRoot;


    private void Awake()
    {
        s_Instane = this;
    }


    public BasePanel OpenPanel(string panelRes)
    {
        var prefab = Resources.Load(panelRes) as GameObject;
        var panelGO = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, m_CannvasRoot.transform);
        panelGO.transform.localPosition = Vector3.zero;
        return panelGO.GetComponent<BasePanel>();
    }
}
