using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlateController : MonoBehaviour
{
    public static SlateController Instance;

    public Image imgSlatGuage;

    public Image[] imgSlates = new Image[9];
    public Sprite[] sprSlates = new Sprite[9];

    private void Awake()
    {
        if (SlateController.Instance == null)
            SlateController.Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseGauge();
            ChangeSlate();
        }
    }

    public void IncreaseGauge()
    {
        imgSlatGuage.fillAmount += 0.105f;
    }

    public void ChangeSlate()
    {
        NoteManager.Instance.ChangeMonsterNotes();

        for (int i = 0; i < 8; i++)
        {
            //Debug.Log("index: " + (int)NoteManager.Instance.m_MonsterNotes[i]);
            imgSlates[i].sprite = sprSlates[(int)NoteManager.Instance.m_MonsterNotes[i] - 1];
        }
    }
}
