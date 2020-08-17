using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenPannel : BasePanel
{
    private static ListenPannel s_Instance;
    public static ListenPannel S
    {
        get
        {
            return s_Instance;
        }
    }

    [SerializeField] AudioCombine m_AudioCombine;
    protected override void OnInit()
    {
        s_Instance = this;
    }

    public void PlayAudio(float cash)
    {
        m_AudioCombine.Init();
        m_AudioCombine.PlayPriceAudio(cash);
    }
}
