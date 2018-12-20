using UnityEngine;
using System.Collections;

public class NoteCtrl : MonoBehaviour
{
    public SpriteRenderer m_Renderer;

    public NoteType m_Type = NoteType.Undefine;
    public GameObject touchBorder = null;
    public Sprite[] noteSprites = new Sprite[9];

    public void Active(NoteType type)
    {
        m_Type = (NoteType)Random.Range(0, 9);

        gameObject.SetActive(true);

        m_Renderer.sprite = noteSprites[(int)m_Type];
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
