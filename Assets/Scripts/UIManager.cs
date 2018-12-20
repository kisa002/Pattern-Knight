using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text textBossHP, textBossTime;
    public Text textPlayerTime;

    public Text textTalkTitle, textTalkContext;

    public Slider sliderBossHP;

    public Image imgBossGuage;
    public Image imgStat;

    public Sprite sprStatOn, sprStatOff;

    string talkTitle = "대마왕";
    string[] talkContext = { "음냐음냐음...", "가소로운 것", "숨을 수 없는 공포를 맞이하라", "겨우 이정도로 나를 물리치려 하다니", "더... 더... 더 강하게 공격해보거라!", "지옥이 그대를 기다린다", "공허 그 너머로 너를 데려가주마", "솔로인가? 그렇다면 목숨만은 살려주지", "커플이라? 우주 최강의 고통을 안겨주마", "나는 아직 배가 고프다", "어떠한 공포를 선사해줄까", "아잉 때리지마여" };

    private void Awake()
    {
        if (UIManager.Instance == null)
            UIManager.Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //StartPlayerTimer(15);
        //StartBossTimer();

        StartRandomTalk();
    }

    #region Boss
    public void InitBoss(int currentHP, int maxHP)
    {
        sliderBossHP.maxValue = maxHP;
        sliderBossHP.value = maxHP;

        textBossHP.text = currentHP + "/" + maxHP;
    }

    public void SetBossHP(int currentHP, int maxHP)
    {
        textBossHP.text = currentHP + "/" + maxHP;
        textBossHP.transform.parent.GetComponent<Text>().text = currentHP + "/" + maxHP;

        sliderBossHP.value = currentHP;
    }
    #endregion

    #region Talk
    public void SetTalk(string title, string context)
    {
        textTalkTitle.text = title;
        textTalkContext.text = context;
    }

    public void StartRandomTalk()
    {
        StartCoroutine(CorRandomTalk());
    }

    IEnumerator CorRandomTalk()
    {
        SetTalk(talkTitle, talkContext[Random.Range(0, talkContext.Length)]);
        //textTalkContext.text = talkContext[Random.Range(0, talkContext.Length)];

        yield return new WaitForSeconds(2);

        StartRandomTalk();
    }
    #endregion

    #region Timer
    public void SetPlayerTimerText(float time)
    {
        textPlayerTime.text = string.Format("{0:00.0}", time);
        textPlayerTime.transform.parent.GetComponent<Text>().text = textPlayerTime.text;
    }

    //public void StartPlayerTimer(int time)
    //{
    //    StartCoroutine(CorPlayerTimer(time));
    //}

    //IEnumerator CorPlayerTimer(float time)
    //{
    //    while(time >= 0)
    //    {
    //        for (int i = 10; i >= 0; i--)
    //        {
    //            time -= 0.1f;
    //            textPlayerTime.text = string.Format("{0:00.0}", time);
    //            textPlayerTime.transform.parent.GetComponent<Text>().text = textPlayerTime.text;

    //            if (time <= 0)
    //                break;

    //            yield return new WaitForSeconds(.1f);
    //        }
    //    }
    //}

    private float _BossTimerFillRatio = 0.0415f;
    private float _BossTimerStartFillRatio = 0.86f;

    public void AddBossTimerGause()
    {
        imgBossGuage.fillAmount += _BossTimerFillRatio;
    }

    public void ResetBossTimerGause()
    {
        imgBossGuage.fillAmount = _BossTimerStartFillRatio;
    }

    //public void StartBossTimer()
    //{
    //    StartCoroutine(CorBossTimer());
    //}

    //IEnumerator CorBossTimer()
    //{
    //    float time = .5f;

    //    for (int i = 0; i < 20; i++)
    //    {
    //        imgBossGuage.fillAmount += 0.0415f;
    //        yield return new WaitForSeconds(time);
    //    }
    //}
    #endregion

    public void StatOn()
    {
        imgStat.sprite = sprStatOn;
    }

    public void StatOff()
    {
        imgStat.sprite = sprStatOff;
    }
}