using UnityEngine;
using System.Collections;

public class TimeManager : Singleton<TimeManager>
{
    public float m_TimerInterval = 0.1f;

    private float m_PlayerMaxTime = 0f;
    public float m_PlayerMaxTimeAttack = 15f;
    public float m_PlayerMaxTimeEvade = 10f;
    public float m_PlayerNowTime = 15f;

    private Coroutine m_PlayerTimer = null;

    public float m_BossMaxTime = 50f;

    public int m_BossMaxGauseCount = 20;
    public int m_BossGauseCount = 0;

    private Coroutine m_BossTimer = null;

    /// <summary>
    /// 일시 정지 시, 호출되는 '타이머 재시작'
    /// </summary>
    public void RestartTimer()
    {
        if (m_PlayerTimer != null)
        {
            if (m_PlayerTimer != null)
                StopCoroutine(m_PlayerTimer);

            m_PlayerNowTime = m_PlayerMaxTimeAttack;

            StartPlayerTimer();
        }
        else
            Debug.LogError("PlayerTimer is Nothing");

        if (m_BossTimer != null)
        {
            if (m_BossTimer != null)
                StopCoroutine(m_BossTimer);

            m_PlayerNowTime = m_PlayerMaxTimeAttack;

            StartBossTimer();
        }
        else
            Debug.LogError("BossTimer is Nothing");
    }

    /// <summary>
    /// 일시 정지 시, 호출되는 '타이머 일시정지'
    /// </summary>
    public void PauseTimer()
    {
        if (m_PlayerTimer != null)
            StopCoroutine(m_PlayerTimer);

        if (m_PlayerTimer != null)
            StopCoroutine(m_BossTimer);
    }

    #region Player Timer
    /// <summary>
    /// '플레이어 타이머' 시작 및 일시정지된 타이머 시작, 현재 시간이 변하지 않음
    /// </summary>
    public void StartPlayerTimer()
    {
        m_PlayerTimer = StartCoroutine(CorPlayerTimer());
    }

    /// <summary>
    /// '회피 타이머'를 '플레이어 타이머'로 변경
    /// </summary>
    public void ChangePlayerAttackTimer()
    {
        if (m_PlayerTimer != null)
            StopCoroutine(m_PlayerTimer);

        m_PlayerMaxTime = m_PlayerMaxTimeAttack;
        m_PlayerNowTime = m_PlayerMaxTimeAttack;

        // UI 매니저에게 '슬레이트' 이미지 교체를 해야 한다.

        m_PlayerTimer = StartCoroutine(CorPlayerTimer());
    }

    /// <summary>
    /// '플레이어 타이머'를 '회피 타이머'로 변경
    /// </summary>
    public void ChangePlayerEvadeTimer()
    {
        if (m_PlayerTimer != null)
            StopCoroutine(m_PlayerTimer);

        NoteManager.Instance.m_IsEvading = true;

        m_PlayerMaxTime = m_PlayerMaxTimeEvade;
        m_PlayerNowTime = m_PlayerMaxTimeEvade;

        // UI 매니저에게 '슬레이트' 이미지 교체를 해야 한다.

        m_PlayerTimer = StartCoroutine(CorPlayerTimer());
    }

    /// <summary>
    /// '플레이어 타이머'와 '회피 타이머' 역할. 타이머가 종료되면 자동으로 '플레이어 타이머'로 변경됨.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CorPlayerTimer()
    {
        while (m_PlayerNowTime >= 0)
        {
            m_PlayerNowTime -= m_TimerInterval;

            if (m_PlayerNowTime <= 0)
                break;

            UIManager.Instance.SetPlayerTimerText(m_PlayerNowTime);

            yield return new WaitForSeconds(m_TimerInterval);
        }

        m_PlayerMaxTime = m_PlayerMaxTimeAttack;
        m_PlayerNowTime = m_PlayerMaxTimeAttack;
        // UI 매니저에게 '슬레이트' 이미지 교체를 해야 한다.

        // 1번 초기화
        NoteManager.Instance.CheckAllNoteMatching();
    }

    /// <summary>
    /// '플레이어 타이머'의 현재 남은 시간
    /// </summary>
    /// <returns></returns>
    public float GetPlayerLeftTime()
    {
        return m_PlayerNowTime;
    }
    #endregion

    #region Boss Timer
    /// <summary>
    /// '보스 타이머' 시작 및 일시정지된 타이머 시작, 현재 시간이 변하지 않음
    /// </summary>
    public void StartBossTimer()
    {
        m_BossTimer = StartCoroutine(CorBossTimer());
    }

    /// <summary>
    /// '보스 타이머'. 사이클 당, 보스 공격 게이지를 1칸 채운다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CorBossTimer()
    {
        while (m_BossGauseCount <= m_BossMaxGauseCount)
        {
            yield return new WaitForSeconds(m_BossMaxTime / (float)m_BossMaxGauseCount);

            m_BossGauseCount++;
            UIManager.Instance.AddBossTimerGause();
        }

        m_BossGauseCount = 0;
        NoteManager.Instance.ClearFieldAndSlate(true);

        UIManager.Instance.ChangeSlate();
        ChangePlayerEvadeTimer();
    }

    public void StopBossTimer()
    {
        if (m_BossTimer != null)
            StopCoroutine(m_BossTimer);
    }
    #endregion
}
