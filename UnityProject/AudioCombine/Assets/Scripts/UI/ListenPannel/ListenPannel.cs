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
    [SerializeField] Toggle m_ToggleSubsides;
    [Header("测试")]
    [SerializeField] private InputField m_InputText;
    [SerializeField] private Button m_BtnText;
    protected override void OnInit()
    {
        s_Instance = this;

        m_BtnText.onClick.AddListener(() =>
        {
            string s = m_InputText.text;
            float value = 0;
            if (float.TryParse(s, out value))
            {
                PlayAudio(value);
            }

        });

        m_ToggleSubsides.isOn = AppMgr.S.isOpenSubsides;
        m_ToggleSubsides.onValueChanged.AddListener((isOn) =>
        {
            AppMgr.S.isOpenSubsides = isOn;
        });


    }

    public void PlayAudio(float cash, float subside = 0)
    {
        if (cash < 0.01 || cash > 99999)
        {
            Debug.LogError("Out of Range");
            return;
        }

        m_AudioCombine.Init();
        var audioPrice = new AudioPrice(cash, subside);
        m_AudioCombine.AddAudioPrice(audioPrice);
    }
}
