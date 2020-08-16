using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCombine : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
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

    private void Awake()
    {
        PlayPriceAudio(m_Cash);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayPriceAudio(m_Cash);
        }
    }

    public void PlayPriceAudio(float cash)
    {
        m_PrepareAudios.Clear();


        //只能处理0.01-99999之间的数字
        if (cash < 0.01 || cash > 99999)
        {
            Debug.LogError("Out of Range");
            return;
        }

        m_Num.Clear();

        //处理整数部分
        int n = (int)cash;
        int length = GetLength(n);                         // 获取n的位数，调用<1>的函数                     
        int power = (int)Mathf.Pow((float)10, (float)length - 1);  //  获取n最高位数字需要除模的数字   
        int temp;

        while (power != 0)
        {
            temp = n / power;   //    此数位数上的数字
            n = n % power;    //   下一个需要除的数字
            power = power / 10;    //    每次除10的指数

            m_Num.Add(temp);
        }

        //处理小数部分
        float flo = cash - (int)cash;
        flo = float.Parse(flo.ToString("#0.00"));
        Debug.LogError(flo);


        //m_PrepareAudios.Add(m_StartAudio);
        for (int i = 0; i < m_Num.Count; i++)
        {
            Debug.LogError(m_Num[i] + unit[i]);
            if (m_Num[i] == 0)
            {
                //TODO 如果上一个已经是0的话 这次就不用加了
                if (i == m_Num.Count - 1)
                {
                    //个位不加0
                }
                else
                    m_PrepareAudios.Add(m_NumAudios[0]);
            }
            else
            {
                m_PrepareAudios.Add(m_NumAudios[m_Num[i]]);
                m_PrepareAudios.Add(m_UnitAudios[i]);
            }
        }

        if (flo > 0)
        {
            m_PrepareAudios.Add(m_DotAudio);
            m_Num.Clear();
            flo *= 100;
            n = (int)flo;
            length = GetLength(n);
            power = (int)Mathf.Pow((float)10, (float)length - 1);

            while (power != 0)
            {
                temp = n / power;   //    此数位数上的数字
                n = n % power;    //   下一个需要除的数字
                power = power / 10;    //    每次除10的指数

                m_Num.Add(temp);
            }

            for (int i = 0; i < m_Num.Count; i++)
            {
                Debug.LogError(m_Num[i] + unitfloat[i]);
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

        if (flo == 0)
            m_PrepareAudios.Add(m_EndAudio);

        StartCoroutine(Audio(m_PrepareAudios));

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
    }
}
