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

    public void IncreaseGauge()
    {
        //imgSlatGuage.fillAmount += 0.105f;

        if (imgSlatGuage.fillAmount == 0)
            imgSlatGuage.fillAmount = 0.097f;
        else if (imgSlatGuage.fillAmount == 0.097f)
            imgSlatGuage.fillAmount = 0.215f;
        else if (imgSlatGuage.fillAmount == 0.215f)
            imgSlatGuage.fillAmount = 0.336f;
        else if (imgSlatGuage.fillAmount == 0.336f)
            imgSlatGuage.fillAmount = 0.454f;
        else if (imgSlatGuage.fillAmount == 0.454f)
            imgSlatGuage.fillAmount = 0.57f;
        else if (imgSlatGuage.fillAmount == 0.57f)
            imgSlatGuage.fillAmount = 0.69f;
        else if (imgSlatGuage.fillAmount == 0.69f)
            imgSlatGuage.fillAmount = 0.806f;
        else if (imgSlatGuage.fillAmount == 0.806f)
            imgSlatGuage.fillAmount = 0.927f;
        else if (imgSlatGuage.fillAmount == 0.927f)
            imgSlatGuage.fillAmount = 1;
        else
        {
            //꽉찬거임
        }
       
    }

    public void ChangeSlate()
    {
        NoteManager.Instance.ChangeMonsterNotes();

        for (int i = 0; i < 8; i++)
        {
            imgSlates[i].sprite = sprSlates[(int)NoteManager.Instance.m_MonsterNotes[i] - 1];
        }
    }
}
