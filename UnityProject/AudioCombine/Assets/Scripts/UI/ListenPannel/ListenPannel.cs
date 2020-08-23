using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using Qarth;

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

    [Header("测试")]
    [SerializeField] private InputField m_InputText;
    [SerializeField] private Button m_BtnText;
    protected override void OnInit()
    {
        s_Instance = this;

        //PlayAudio(2.0f);
        //PlayAudio(2003.4f);


        m_BtnText.onClick.AddListener(() =>
        {
            string s = m_InputText.text;
            float value = 0;
            if (float.TryParse(s, out value))
            {
                PlayAudio(value);
            }

        });
    }

    public void PlayAudio(float cash)
    {
        if (cash < 0.01 || cash > 99999)
        {
            Debug.LogError("Out of Range");
            return;
        }

        m_AudioCombine.Init();
        var audioPrice = new AudioPrice(cash);
        m_AudioCombine.AddAudioPrice(audioPrice);
    }
}
