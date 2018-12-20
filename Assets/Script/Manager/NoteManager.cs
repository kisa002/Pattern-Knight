using UnityEngine;
using System.Collections.Generic;

public enum NoteType
{
    Undefine,
    RedArrow0,
    YellowArrow1,
    BlueArrow2,
    RedSword3,
    YellowSword4,
    BlueSword5,
    RedAxe6,
    YellowAxe7,
    BlueAxe8,
}

[AddComponentMenu("LineRenderer")]
public class NoteManager : Singleton<NoteManager>
{
    public List<NoteCtrl> m_TouchChainNotes = new List<NoteCtrl>();
    public List<NoteCtrl> m_FieldNotes = new List<NoteCtrl>();

    public int m_ChainCount = 0;
    public LineRenderer m_ChainRenderer;

    public List<NoteType> m_MonsterNotes = new List<NoteType>();

    public Vector2 m_StartPos = Vector2.zero;

    public bool m_IsEvading = false;

    public void Awake()
    {
        if (m_ChainRenderer == null)
            m_ChainRenderer = GetComponent<LineRenderer>();
    }

    public void InitChain(NoteCtrl note)
    {
        Debug.Log("Start Note Chain");

        m_ChainCount = 1;

        m_ChainRenderer.positionCount = 2;

        m_StartPos = note.transform.position;
        transform.position = note.transform.position;

        m_ChainRenderer.SetPosition(0, Vector2.zero);
        m_ChainRenderer.SetPosition(1, Vector2.zero);

        m_TouchChainNotes.Add(note);

        note.Touched();
    }

    public void CheckNote(NoteCtrl note)
    {
        // 체인된 노트가 최초 체인 노트일 경우
        if (m_TouchChainNotes.Count == 0)
        {
            if (note.m_Type != m_MonsterNotes[0])
            {
                TouchManager.Instance.m_IsPressing = false;
                ClearFieldAndSlate(false);
                return;
            }

            InitChain(note);
            return;
        }

        // 마지막 체인 노트와 현재 체인된 노트가 동일한 경우
        if (m_TouchChainNotes[m_ChainCount - 1] == note)
        {
            // 아무 것도 하지 않음
            return;
        }

        // 체인된 노트와 몬스터 현재 패턴 노트와 다를 경우
        // !! 패턴 입력 실패 !!
        if (note.m_Type != m_MonsterNotes[m_ChainCount])
        {
            Debug.Log("Pattern Failed, [" + m_ChainCount +
                "] touch : " + note.m_Type + " mob : " + m_MonsterNotes[m_ChainCount]);

            if (m_IsEvading)
            {
                GameManager.Instance.OnGameOver();
                return;
            }

            ClearFieldAndSlate(false);
            return;
        }

        m_TouchChainNotes.Add(note);
        AddChainedNote(note);
    }

    public void AddChainedNote(NoteCtrl note)
    {
        m_ChainRenderer.SetPosition(m_ChainCount, (Vector2)note.transform.position - m_StartPos);

        m_ChainCount++;

        m_ChainRenderer.positionCount++;
        m_ChainRenderer.SetPosition(m_ChainCount, (Vector2)note.transform.position - m_StartPos);

        note.Touched();

        // 8개 패턴 모드 매칭되는 경우
        if (m_ChainCount == 8)
        {
            TouchManager.Instance.m_IsPressing = false;

            DoAttack();

            if (m_IsEvading)
            {
                m_IsEvading = false;
                TimeManager.Instance.StartBossTimer();
            }

            ClearFieldAndSlate(false);
        }
    }

    public void MoveLastChain(Vector3 pos)
    {
        if (m_TouchChainNotes.Count != 0)
        {
            //m_ChainRenderer.SetPosition(m_ChainCount, (Vector2)pos - m_StartPos);
        }
    }

    //public void RemoveChainedNote() { }

    ///// <summary>
    ///// 연결된 노트가 족보와 틀릴 때, 체인을 지우고, 연결된 노트 리스트를 비운다.
    ///// </summary>
    //public void ClearChainNotse()
    //{
    //    m_ChainIdx = 0;
    //    m_ChainRenderer.positionCount = 1;

    //    for (int i = 0; i < m_TouchChainNotes.Count; i++)
    //    {
    //        m_TouchChainNotes[i].UnTouched();
    //    }
    //    m_TouchChainNotes.Clear();
    //}

    ///// <summary>
    ///// 연결된 노트가 족보와 동일하고 마우스를 땔 때, 체인 연결된 노트를 파괴한다.
    ///// </summary>
    //public void DestroyChainNotes()
    //{
    //    m_ChainIdx = 0;
    //    m_ChainRenderer.positionCount = 1;

    //    for (int i = 0; i < m_TouchChainNotes.Count; i++)
    //    {
    //        m_TouchChainNotes[i].Destroy();
    //    }
    //}

    public bool CheckAllNoteMatching()
    {
        // 회피 타이머 상태
        if (m_IsEvading)
        {
            GameManager.Instance.OnGameOver();
            return false;
        }

        Debug.Log("Check All Note Matching");
        if (CompareNotes() > m_MinusMatchCount)
        {
            // 데미지 처리
            DoAttack();
        }

        ClearFieldAndSlate(false);

        return true;
    }

    /// <summary>
    /// 필드 노트를 섞고, 몬스터 패턴을 바꾸고, 체인 노트를 초기화한다.
    /// </summary>
    public void ClearFieldAndSlate(bool isChangeTimer)
    {
        m_ChainCount = 0;
        m_ChainRenderer.positionCount = 1;

        ShakeFieldNotes();
        SlateController.Instance.ChangeSlate();

        m_TouchChainNotes.Clear();

        if (isChangeTimer == false)
            TimeManager.Instance.ChangePlayerAttackTimer();
    }

    private int CompareNotes()
    {
        if (m_TouchChainNotes.Count == 9)
            return 0;


        int matchedNoteCount = 0;
        for (int i = 0; i < m_TouchChainNotes.Count; i++)
        {
            if (m_TouchChainNotes[i].m_Type == m_MonsterNotes[i])
                matchedNoteCount++;
            else
                break;
        }
        Debug.Log("Mathcing Note Count : " + matchedNoteCount);
        return matchedNoteCount;
    }

    /// <summary>
    /// 새로운 몬스터의 패턴(슬레이트)를 가져옵니다.
    /// </summary>
    public void ChangeMonsterNotes()
    {
        NoteType[] notes = SlateInfoLoader.Instance.GetSlateInfo().Notes;

        m_MonsterNotes.Clear();

        for (int i = 0; i < notes.Length; i++)
            m_MonsterNotes.Add(notes[i] + 1);
    }

    /// <summary>
    /// 필드 노트 9개의 타입을 중복되지 않게 섞습니다.
    /// </summary>
    public void ShakeFieldNotes()
    {
        int[] randArray = new int[9];
        bool isSame;

        for (int i = 0; i < 9; i++)
        {
            while (true)
            {
                randArray[i] = Random.Range(0, 9);
                isSame = false;

                for (int j = 0; j < i; j++)
                {
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            m_FieldNotes[i].Active((NoteType)randArray[i]);
        }
    }

    [Header("Damage")]
    public float m_BaseDamage = 50f;
    public int m_MinusMatchCount = 2;

    public float m_MaxDamageRatio = 0.25f;
    public float m_MinDamageRatio = 0.15f;

    public float m_MaxBonusTime = 2.5f;
    public float m_MinBonusTime = 1.0f;

    public int[] m_ComboSteps = new int[3]{2, 5, 7};
    public float[] m_ComboDamageRatios = new float[3]{ 1.0f, 1.5f, 2.0f };
    public float m_ComboDamageRatio = 1f;

    public float m_LeftTime = 0f;
    public float m_NowDamage = 0f;

    public List<AttackMissile> m_Missiles = new List<AttackMissile>();
    private List<AttackMissile> m_ActiveMissiles = new List<AttackMissile>();

    public void DoAttack()
    {
        m_LeftTime = TimeManager.Instance.GetPlayerLeftTime();
        m_NowDamage = GetTotalDamage(m_LeftTime);

        GameManager.Instance.OnDontTouch();

        for (int i = 0; i < m_ChainCount; i++)
        {
            m_ActiveMissiles.Add(m_Missiles[i]);
            m_Missiles[i].Active(Mathf.FloorToInt(m_NowDamage / m_ChainCount)
                , m_TouchChainNotes[i].transform.position);
        }
    }

    public void OnAttacked(AttackMissile missile)
    {
        if (m_ActiveMissiles.Contains(missile) == false)
            return;

        m_ActiveMissiles.Remove(missile);

        if(m_ActiveMissiles.Count == 0)
        {
            GameManager.Instance.OnCanTouch();
        }
    }

    /// <summary>
    /// 노트 별로 낱개로 데미지 처리할 경우
    /// </summary>
    /// <param name="time">남은 시간</param>
    /// <returns>노트 1개에 해당하는 데미지</returns>
    public float GetDamage(float time)
    {
        // 콤보 배율 구하기
        float comboRatio = 1f;

        for (int i = 0; i < m_ComboSteps.Length; i++)
            if (m_ChainCount > m_ComboSteps[i])
                comboRatio = m_ComboDamageRatios[i];

        // 최소 배율, 최대 배율 구하기
        float minDamage
            = m_BaseDamage * m_MinDamageRatio * (m_MinBonusTime + time) * comboRatio;
        float maxDamage
            = m_BaseDamage * m_MaxDamageRatio * (m_MaxBonusTime + time) * comboRatio;

        // 실제 데미지
        float realDamage = Random.Range(minDamage, maxDamage);

        Debug.Log("ComboRatio : " + comboRatio + ", Hit : 1, Damage : " + realDamage);

        return realDamage;
    }

    /// <summary>
    /// 맞춘 노트 수만큼 총 데미지 처리할 경우
    /// </summary>
    /// <param name="time">남은 시간</param>
    /// <returns>총 데미지</returns>
    public float GetTotalDamage(float time)
    {
        // 콤보 배율 구하기
        float comboRatio = 1f;

        for (int i = 0; i < m_ComboSteps.Length; i++)
            if (m_ChainCount > m_ComboSteps[i])
                comboRatio = m_ComboDamageRatios[i];

        // 실제 Hit 수 구하기
        int realHit = m_ChainCount - 2;
        if (realHit <= 0)
        {
            Debug.Log("Hit chain 3 exclusive");
            return 0f;
        }

        // 최소 배율, 최대 배율 구하기
        float minDamage
            = m_BaseDamage * m_MinDamageRatio * (m_MinBonusTime + time) * comboRatio;
        float maxDamage
            = m_BaseDamage * m_MaxDamageRatio * (m_MaxBonusTime + time) * comboRatio;

        // 실제 총 데미지
        float totalDamage = 0f;
        for (int i = 0; i < realHit; i++)
            totalDamage += Random.Range(minDamage, maxDamage);

        Debug.Log("ComboRatio : " + comboRatio + ", Hit : " + realHit + ", Total Damage : " + totalDamage);
        return Mathf.Floor(totalDamage);
    }
}
