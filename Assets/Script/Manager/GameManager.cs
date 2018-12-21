using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GameState
{
    Undefine,
    Idle,
    CanTouch,
    Wait,
    DontTouch,
    GameOver,
    None,
}

public class GameManager : Singleton<GameManager>
{
    public GameState m_GameState = GameState.Idle;
    public bool m_IsTitle = true;

    public void Start()
    {
        StartCoroutine(LoadData());

        //SlateController.Instance.ChangeSlate();
        //NoteManager.Instance.ShakeFieldNotes();

        //TimeManager.Instance.StartPlayerTimer();
        //TimeManager.Instance.StartBossTimer();

        //m_GameState = GameState.CanTouch;
    }

    public IEnumerator LoadData()
    {
        SlateInfoLoader.Instance.SaveOrLoad();

        yield return null;

        LoadTitle();
    }

    public float m_TitleFillRatio = 0.47f, m_TitleFillLastRatio = 0.33f;
    public float m_TitleCurtainCallTime = 0.5f;
    public Image m_TitleCurtain;

    public void LoadTitle()
    {
        // 1. boss 무적
        BossController.Instance.m_IsDead = true;

        // 2. 일회용 족보 세팅
        SlateController.Instance.ChangeSlate();

        // 3. 필드 노트 세팅
        NoteManager.Instance.ShakeFieldNotes();

        StartCoroutine(CorTitleCurtainCall());

    }

    public IEnumerator CorTitleCurtainCall()
    {
        float nowTime = 0f;
        while (nowTime < m_TitleCurtainCallTime)
        {
            m_TitleCurtain.fillAmount
                = Mathf.Lerp(m_TitleFillRatio, m_TitleFillLastRatio
                            , nowTime / m_TitleCurtainCallTime);

            yield return new WaitForFixedUpdate();

            nowTime += Time.fixedDeltaTime;
        }

        m_GameState = GameState.CanTouch;
    }

    public void EndTitle()
    {
        // 1. 터치 잠금
        m_GameState = GameState.DontTouch;

        // 2. Boss 무적 취소
        BossController.Instance.m_IsDead = false;

        StartCoroutine(CorTitleEnding());
    }

    public GameObject m_TitleLogoBG, m_TitleSlate;
    public Image m_TitleLogoBar, m_TitleLogo;

    Color color_alpha = new Color(0, 0, 0, 0);

    public IEnumerator CorTitleEnding()
    {
        float nowTime = 0f;

        m_TitleLogo.enabled = false;

        while (nowTime < m_TitleCurtainCallTime)
        {
            m_TitleCurtain.fillAmount
                = Mathf.Lerp(m_TitleFillLastRatio, 0.31f
                            , nowTime / m_TitleCurtainCallTime);

            m_TitleLogoBar.color = Color.Lerp(Color.white, color_alpha, nowTime / m_TitleCurtainCallTime);

            yield return new WaitForFixedUpdate();

            nowTime += Time.fixedDeltaTime;
        }

        m_TitleSlate.SetActive(false);
        m_TitleCurtain.gameObject.SetActive(false);
        m_TitleLogoBG.SetActive(false);

        SlateController.Instance.ChangeSlate();
        NoteManager.Instance.ShakeFieldNotes();

        TimeManager.Instance.StartPlayerTimer();
        TimeManager.Instance.StartBossTimer();

        m_GameState = GameState.CanTouch;
    }

    public void OnCanTouch()
    {
        m_GameState = GameState.CanTouch;
    }

    public void OnDontTouch()
    {
        m_GameState = GameState.DontTouch;
    }

    public void OnGameOver()
    {
        m_GameState = GameState.GameOver;
    }
}
