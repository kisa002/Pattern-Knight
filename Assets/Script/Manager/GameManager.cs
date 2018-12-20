using UnityEngine;

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

    public void Start()
    {
        SlateInfoLoader.Instance.SaveOrLoad();

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
