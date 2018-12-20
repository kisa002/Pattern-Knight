using UnityEngine;

public enum GameState
{
    Undefine,
    Idle,
    CanTouch,
    None,
}

public class GameManager : Singleton<GameManager>
{
    public GameState m_GamteState = GameState.Idle;

    public void Start()
    {
        SlateInfoLoader.Instance.SaveOrLoad();

        NoteManager.Instance.ChangeMonsterNotes();

        m_GamteState = GameState.CanTouch;
    }
}
