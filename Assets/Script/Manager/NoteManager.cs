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

    public int m_ChainIdx = 0;
    public LineRenderer m_ChainRenderer;

    public List<NoteType> m_MonsterNotes = new List<NoteType>();

    public Vector2 m_StartPos = Vector2.zero;

    public void Awake()
    {
        if (m_ChainRenderer == null)
            m_ChainRenderer = GetComponent<LineRenderer>();
    }

    public void InitChain(NoteCtrl note)
    {
        m_ChainIdx = 0;

        m_ChainRenderer.positionCount = 2;
        m_ChainRenderer.SetPosition(0, note.transform.position);
        m_ChainRenderer.SetPosition(1, note.transform.position);

        transform.position = note.transform.position;

        m_TouchChainNotes.Add(note);

        note.Touched();
    }

    public void CheckNote(NoteCtrl note)
    {
        if (m_TouchChainNotes.Count == 0)
        {
            InitChain(note);
            return;
        }

        if (m_TouchChainNotes[m_ChainIdx] == note)
        {

            return;
        }

        m_TouchChainNotes.Add(note);
        AddChainedNote(note);
    }

    public void AddChainedNote(NoteCtrl note)
    {
        m_ChainRenderer.SetPosition(m_ChainIdx + 1, (Vector2)note.transform.position);

        m_ChainIdx++;

        m_ChainRenderer.positionCount++;
        m_ChainRenderer.SetPosition(m_ChainIdx + 1, (Vector2)note.transform.position);

        note.Touched();
    }

    public void MoveLastChain(Vector3 pos)
    {
        if (m_TouchChainNotes.Count != 0)
        {
            m_ChainRenderer.SetPosition(m_ChainIdx + 1, (Vector2)pos);
        }
    }

    //public void RemoveChainedNote() { }

    /// <summary>
    /// 연결된 노트가 족보와 틀릴 때, 체인을 지우고, 연결된 노트 리스트를 비운다.
    /// </summary>
    public void ClearChainNotse()
    {
        m_ChainIdx = 0;
        m_ChainRenderer.positionCount = 1;

        for (int i = 0; i < m_TouchChainNotes.Count; i++)
        {
            m_TouchChainNotes[i].UnTouched();
        }
        m_TouchChainNotes.Clear();
    }

    /// <summary>
    /// 연결된 노트가 족보와 동일하고 마우스를 땔 때, 체인 연결된 노트를 파괴한다.
    /// </summary>
    public void DestroyChainNotes()
    {
        m_ChainIdx = 0;
        m_ChainRenderer.positionCount = 1;

        for (int i = 0; i < m_TouchChainNotes.Count; i++)
        {
            m_TouchChainNotes[i].Destroy();
        }
    }

    public void MatchingNotes()
    {
        if (CompareNotes() == 0)
        {
            ClearChainNotse();
        }
        else
        {
            DestroyChainNotes();
        }
    }

    private int CompareNotes()
    {
        ChangeMonsterNotes();

        if (m_TouchChainNotes.Count == 9)
            return 0;

        int matchedNoteCount = 0;
        for (int i = 0; i < m_TouchChainNotes.Count; i++)
        {
            if (m_TouchChainNotes[i].m_Type == m_MonsterNotes[i])
                matchedNoteCount++;
            else
                return 0;
        }


        return matchedNoteCount;
    }

    public void ChangeMonsterNotes()
    {
        NoteType[] notes = SlateInfoLoader.Instance.GetSlateInfo().Notes;

        m_MonsterNotes.Clear();

        for (int i = 0; i < notes.Length; i++)
            m_MonsterNotes.Add(notes[i] + 1);

        // change ui
    }
}
