using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : Singleton<TouchManager>
{
    public Camera mainCamera;
    public bool m_IsMatchFail, m_IsPressing = false;

    private void Update()
    {
        if (GameManager.Instance.m_GamteState != GameState.CanTouch)
            return;

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            m_IsPressing = true;
            return;
        }

        if (Input.GetMouseButton(0) && m_IsPressing)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction
                                                 , 100f
                                                 , 1 << LayerMask.NameToLayer(Constant.Layer_Note));
            if (hit)
            {
                NoteCtrl touchedNote = hit.transform.GetComponent<NoteCtrl>();
                //Debug.Log("Touch : " + touchedNote.name);
                NoteManager.Instance.CheckNote(touchedNote);
                return;
            }

            NoteManager.Instance.MoveLastChain(
                mainCamera.ScreenToWorldPoint(Input.mousePosition));

            return;
        }

        if (Input.GetMouseButtonUp(0) && m_IsPressing)
        {
            Debug.Log("MouseButtonUp");

            m_IsPressing = false;

            // 3번 초기화
            if (NoteManager.Instance.CheckAllNoteMatching())
                TimeManager.Instance.ChangePlayerAttackTimer();
            return;
        }
#else
        Touch touch = Input.GetTouch(0);

        switch (touch.phase) {
            case TouchPhase.Began:
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.transform.CompareTag(Constant.TAG_CRYSTAL)) {
                    chainList.Add(hit.transform.GetComponent<CellCtrl>());
                }
                break;
            case TouchPhase.Moved:
                break;
            case TouchPhase.Ended:
                break;
        }
#endif

    }

}
