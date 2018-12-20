using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text textBossHP, textBossTime;
    public Text textPlayerTime;

    public Slider sliderBossHP;

    private void Awake()
    {
        if (UIManager.Instance == null)
            UIManager.Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartPlayerTimer(15);
        StartBossTimer(10);
    }

    #region Boss
    public void InitBoss(int currentHP, int maxHP)
    {
        SetBossHP(currentHP, maxHP);

        sliderBossHP.value = currentHP;
        sliderBossHP.maxValue = maxHP;
    }

    public void SetBossHP(int currentHP, int maxHP)
    {
        textBossHP.text = currentHP + "/" + maxHP;

        sliderBossHP.value = currentHP;
    }
    #endregion

    #region Timer
    public void StartPlayerTimer(int time)
    {
        StartCoroutine(CorPlayerTimer(time));
    }

    IEnumerator CorPlayerTimer(float time)
    {
        while(time >= 0)
        {
            for (int i = 10; i >= 0; i--)
            {
                time -= 0.1f;
                textPlayerTime.text = string.Format("{0:00.0}", time);
                textPlayerTime.transform.parent.GetComponent<Text>().text = textPlayerTime.text;

                //Debug.Log(time.ToString());

                if (time <= 0)
                    break;
                
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    public void StartBossTimer(float time)
    {
        StartCoroutine(CorBossTimer(time));
    }

    IEnumerator CorBossTimer(float time)
    {
        while (time >= 0)
        {
            for (int i = 10; i >= 0; i--)
            {
                time -= 0.1f;
                textBossTime.text = string.Format("{0:00.0}", time);

                if (time <= 0)
                    break;

                yield return new WaitForSeconds(.1f);
            }
        }
    }
    #endregion
}