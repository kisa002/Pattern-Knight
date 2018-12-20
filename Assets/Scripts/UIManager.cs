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

    public GameObject slatePlayer, slateBoss;

    public Image m_SlateGauge;
    public Image[] imgBossSlates = new Image[9];
    public Sprite[] sprSlates = new Sprite[9];

    [Header("Damage Hud Text")]
    public Text[] dmgTexts;
    public Text[] m_DamageTextShadows;
    public int m_DamageTextSize = 60;

    string talkTitle = "대마왕";
    string[] talkContext = { "음냐음냐음...", "가소로운 것", "숨을 수 없는 공포를 맞이하라", "겨우 이정도로 나를 물리치려 하다니", "더... 더... 더 강하게 공격해보거라!", "지옥이 그대를 기다린다", "공허 그 너머로 너를 데려가주마", "솔로인가? 그렇다면 목숨만은 살려주지", "커플이라? 우주 최강의 고통을 안겨주마", "나는 아직 배가 고프다", "어떠한 공포를 선사해줄까", "아잉 때리지마여" };

    private void Awake()
    {
        if (UIManager.Instance == null)
            UIManager.Instance = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < dmgTexts.Length; i++)
        {
            dmgTexts[i].fontSize = m_DamageTextSize;
            m_DamageTextShadows[i].fontSize = m_DamageTextSize;
        }
    }

    private void Start()
    {
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
    
    public void BossAttack()
    {
        // 보스가 플레이어에게 공격하는 함수
        // 여기다가 보스 족보 보여주면 될듯

        ShowSlateBoss();
    }
    #endregion

    public void ShowSlatePlayer()
    {
        slatePlayer.SetActive(true);
        slateBoss.SetActive(false);
    }

    public void ShowSlateBoss()
    {
        slatePlayer.SetActive(false);
        slateBoss.SetActive(true);
    }

    public void ResetSlateGauge()
    {
        m_SlateGauge.fillAmount = 0;
    }

    public void IncreaseGauge()
    {
        //imgSlatGuage.fillAmount += 0.105f;

        if (m_SlateGauge.fillAmount == 0)
            m_SlateGauge.fillAmount = 0.097f;
        else if (m_SlateGauge.fillAmount == 0.097f)
            m_SlateGauge.fillAmount = 0.215f;
        else if (m_SlateGauge.fillAmount == 0.215f)
            m_SlateGauge.fillAmount = 0.336f;
        else if (m_SlateGauge.fillAmount == 0.336f)
            m_SlateGauge.fillAmount = 0.454f;
        else if (m_SlateGauge.fillAmount == 0.454f)
            m_SlateGauge.fillAmount = 0.57f;
        else if (m_SlateGauge.fillAmount == 0.57f)
            m_SlateGauge.fillAmount = 0.69f;
        else if (m_SlateGauge.fillAmount == 0.69f)
            m_SlateGauge.fillAmount = 0.806f;
        else if (m_SlateGauge.fillAmount == 0.806f)
            m_SlateGauge.fillAmount = 0.927f;
        else if (m_SlateGauge.fillAmount == 0.927f)
            m_SlateGauge.fillAmount = 1;
        else
        {
            //꽉찬거임
        }

    }

    public void StatOn()
    {
        imgStat.sprite = sprStatOn;
    }

    public void StatOff()
    {
        imgStat.sprite = sprStatOff;
    }
    
    public void ShowDmg(int dmg, Vector3 pos)
    {
        for (int i = 0; i < 8; i++)
        {
            if(!dmgTexts[i].IsActive())
            {
                dmgTexts[i].gameObject.SetActive(true);
                dmgTexts[i].rectTransform.position = Camera.main.WorldToScreenPoint(pos);
                dmgTexts[i].text = dmg.ToString();

                dmgTexts[i].GetComponent<TextDmgController>().StartAnimation();
                break;
            }
        }
    }

    [Header("Slate")]
    public Ui_SlateCtrl m_SlateCtrl;

    public void ChangeSlate() { m_SlateCtrl.ChangeSlate(); }
}