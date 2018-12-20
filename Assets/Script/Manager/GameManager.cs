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

        SlateController.Instance.ChangeSlate();
        NoteManager.Instance.ShakeFieldNotes();

        TimeManager.Instance.StartPlayerTimer();
        TimeManager.Instance.StartBossTimer();

        m_GamteState = GameState.CanTouch;
    }
}
