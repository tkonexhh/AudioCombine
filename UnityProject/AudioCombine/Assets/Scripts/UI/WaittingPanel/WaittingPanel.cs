using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFrame;

public class WaittingPanel : BasePanel
{
    private static WaittingPanel s_Instance;

    public static WaittingPanel S
    {
        get
        {
            if (s_Instance == null)
            {
                var basePanel = UIMgr.S.OpenPanel("Panels/WaittingPannel");
                s_Instance = basePanel as WaittingPanel;
            }
            return s_Instance;
        }
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
