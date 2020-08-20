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

        // PlayAudio(2.0f);
        // PlayAudio(2003.4f);
    }

    public void PlayAudio(float cash)
    {
        m_AudioCombine.Init();
        var audioPrice = new AudioPrice(cash);
        m_AudioCombine.AddAudioPrice(audioPrice);
    }
}
