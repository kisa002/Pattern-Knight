using UnityEngine;
using System.Collections;

public class NoteCtrl : MonoBehaviour
{
    public SpriteRenderer m_Renderer;

    public NoteType m_Type = NoteType.Undefine;
    public GameObject touchBorder = null;
    public Sprite[] noteSprites = new Sprite[9];
    public GameObject particle;

    public void Active(NoteType type)
    {
        m_Type = type + 1;

        gameObject.SetActive(true);
        touchBorder.SetActive(false);

        m_Renderer.sprite = noteSprites[(int)type];
    }

    public void Touched()
    {
        touchBorder.SetActive(true);
    }

    public void UnTouched()
    {
        touchBorder.SetActive(false);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        touchBorder.SetActive(false);
    }
}
