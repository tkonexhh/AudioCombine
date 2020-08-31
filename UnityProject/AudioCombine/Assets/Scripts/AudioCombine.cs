using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class AudioCombine : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;

    [SerializeField] private AudioClip m_StartAudio1;
    [SerializeField] private AudioClip m_StartAudio2;
    [SerializeField] private AudioClip m_StartAudio;
    [SerializeField] private AudioClip m_DotAudio;
    [SerializeField] private AudioClip m_EndAudio;
    [SerializeField] private AudioClip[] m_NumAudios;
    [SerializeField] private AudioClip[] m_UnitAudios;
    [SerializeField] private AudioClip[] m_UnitFloatAudios;

    [SerializeField] private float m_Cash;

    private List<int> m_Num = new List<int>();
    private string[] unit = new string[] { "", "十", "百", "千", "万" };
    private string[] unitfloat = new string[] { "毛", "分" };

    private List<AudioClip> m_PrepareAudios = new List<AudioClip>();

    private Queue<AudioPrice> m_AudioQueues = new Queue<AudioPrice>();
    private AudioPrice m_CurrentAudioPrice = null;
    private bool m_Isplaying = false;
    public void Init()
    {
        if (m_AudioSource == null)
            m_AudioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void AddAudioPrice(AudioPrice audioPrice)
    {
        m_AudioQueues.Enqueue(audioPrice);

        PlayNext();
    }

    private void PlayNext()
    {
        if (m_AudioQueues.Count > 0 && !m_Isplaying)
        {
            m_CurrentAudioPrice = m_AudioQueues.Dequeue();
            PlayPriceAudio(m_CurrentAudioPrice.cash);
        }
    }


    private void PlayPriceAudio(float cash)
    {
        m_PrepareAudios.Clear();

        //只能处理0.01-99999之间的数字
        if (cash < 0.01 || cash > 99999)
        {
            Debug.LogError("Out of Range");
            return;
        }

        m_Num.Clear();

        m_PrepareAudios.Add(m_StartAudio1);
        m_PrepareAudios.Add(m_StartAudio2);
        m_PrepareAudios.Add(m_StartAudio);

        int part_int = (int)cash;
        HandleInteger(part_int);

        //处理小数部分
        float part_float = cash - (int)cash;
        if (part_float > 0)
        {
            HandleFloat(part_float);
        }

        StartCoroutine(Audio(m_PrepareAudios));
    }

    private void HandleInteger(int cash)
    {
        int n = cash;
        int part_int = n;

        int length = GetLength(n);

        if (part_int == 100 || part_int == 1000 || part_int == 10000)
        {
            m_PrepareAudios.Add(m_NumAudios[1]);
            m_PrepareAudios.Add(m_UnitAudios[length - 1]);
            return;
        }

        // 获取n的位数，调用<1>的函数                     
        int power = (int)Mathf.Pow((float)10, (float)length - 1);  //  获取n最高位数字需要除模的数字   
        int temp;

        while (power != 0)
        {
            temp = n / power;   //    此数位数上的数字
            n = n % power;    //   下一个需要除的数字
            power = power / 10;    //    每次除10的指数

            m_Num.Add(temp);
        }

        if (part_int >= 10 && part_int <= 19)
        {
            Debug.LogError("frame");
            m_PrepareAudios.Add(m_UnitAudios[1]);
            if (m_Num[1] > 0)
            {
                m_PrepareAudios.Add(m_NumAudios[m_Num[1]]);
            }

            m_PrepareAudios.Add(m_UnitAudios[0]);
            return;
        }

        for (int i = 0; i < m_Num.Count; i++)
        {
            //Debug.LogError(m_Num[i] + unit[length - i - 1]);
            if (m_Num[i] == 0)
            {
                //TODO 如果上一个已经是0的话 这次就不用加了
                //if (i == m_Num.Count - 1)
                if (m_Num[i - 1] == 0)
                {
                    //个位不加0
                }
                else
                    m_PrepareAudios.Add(m_NumAudios[0]);
            }
            else
            {
                m_PrepareAudios.Add(m_NumAudios[m_Num[i]]);
                //Debug.LogError("Audio:" + i);
                m_PrepareAudios.Add(m_UnitAudios[length - i - 1]);
            }
        }
    }

    private void HandleFloat(float flo)
    {
        flo = float.Parse(flo.ToString("#0.00"));

        if (flo > 0)
        {
            m_Num.Clear();
            flo *= 100;
            int n = (int)flo;
            int length = GetLength(n);
            int power = (int)Mathf.Pow((float)10, (float)length - 1);
            int temp;
            while (power != 0)
            {
                temp = n / power;   //    此数位数上的数字
                n = n % power;    //   下一个需要除的数字
                power = power / 10;    //    每次除10的指数

                m_Num.Add(temp);
            }

            for (int i = 0; i < 2; i++)
            {
                //Debug.LogError(m_Num[i] + unitfloat[i]);
                if (i == 1 && m_Num[i] == 0)
                {
                    continue;
                }

                if (m_Num[i] == 0)
                {
                    m_PrepareAudios.Add(m_NumAudios[0]);
                }
                else
                {
                    m_PrepareAudios.Add(m_NumAudios[m_Num[i]]);
                    m_PrepareAudios.Add(m_UnitFloatAudios[i]);
                }
            }
        }
    }

    private int GetLength(int place)
    {
        int i = 0;

        while (place != 0)
        {
            place = place / 10;//每次除以10
            i++;//统计循环次数
        }

        return i;
    }

    IEnumerator Audio(List<AudioClip> audios)
    {
        m_Isplaying = true;
        m_AudioSource.Pause();
        var tempAudios = new List<AudioClip>(audios.ToArray());
        for (int i = 0; i < tempAudios.Count; i++)
        {
            if (tempAudios[i] == null) continue;
            m_AudioSource.clip = tempAudios[i];
            m_AudioSource.Play();
            yield return new WaitForSeconds(tempAudios[i].length);
        }
        m_AudioSource.Pause();

        m_Isplaying = false;
        StartCoroutine(DelayPlayNext());
    }

    IEnumerator DelayPlayNext()
    {
        yield return new WaitForSeconds(1.0f);

        PlayNext();
    }

}


public class AudioPrice
{
    public float cash { set; get; }

    public AudioPrice(float cash)
    {
        this.cash = cash;
    }



}
