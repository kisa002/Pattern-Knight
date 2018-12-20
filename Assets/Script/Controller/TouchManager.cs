using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : Singleton<TouchManager>
{
    public Camera mainCamera;

    private void Update()
    {
        if (GameManager.Instance.m_GamteState != GameState.CanTouch)
            return;

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction
                                                 , 100f
                                                 , 1 << LayerMask.NameToLayer(Constant.Layer_Note));
            if (hit)
            {
                NoteCtrl touchedNote = hit.transform.GetComponent<NoteCtrl>();
                Debug.Log("Touch : " + touchedNote.name);
                NoteManager.Instance.CheckNote(touchedNote);
                return;
            }

            NoteManager.Instance.MoveLastChain(
                mainCamera.ScreenToWorldPoint(Input.mousePosition));

            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("MouseButtonUp");
            NoteManager.Instance.MatchingNotes();
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
