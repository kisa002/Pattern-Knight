using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_SlateCtrl : MonoBehaviour
{
    [Header("Slate")]
    public Sprite m_EvadeSlate, m_AttackSlate;
    public Image m_Renderer;

    [Header("Damage Ratio")]
    public List<GameObject> m_DamageRatioText = new List<GameObject>();

    [Header("Player Attack Icon")]
    public GameObject m_PlayerAttackOnIcon;
    public GameObject m_PlayerAttackIconParent;

    [Header("Boss Attack")]
    public GameObject m_BossAttackOffIcon;
    public Image m_BossAttackIconBorder;

    public void Start() { m_Renderer.sprite = m_AttackSlate; }

    public void ChangeSlate()
    {
        if (m_Renderer.sprite == m_EvadeSlate)
            ChangeAttackSlate();
        else
            ChangeEvadeSlate();
    }

    private void ChangeEvadeSlate()
    {
        m_Renderer.sprite = m_EvadeSlate;
        SwitchDamageRatioText(false);
        m_PlayerAttackIconParent.SetActive(false);
        m_BossAttackOffIcon.SetActive(false);
        //m_BossAttackIconBorder.enabled = true;
    }

    private void ChangeAttackSlate()
    {
        m_Renderer.sprite = m_AttackSlate;
        SwitchDamageRatioText(true);
        m_PlayerAttackIconParent.SetActive(true);
        m_BossAttackOffIcon.SetActive(true);
        //m_BossAttackIconBorder.enabled = false;
    }

    public void SwitchPlayerAttackIcon(bool isOn)
    {
        m_PlayerAttackOnIcon.SetActive(isOn);
    }

    public void SwitchDamageRatioText(bool isOn)
    {
        for (int i = 0; i < m_DamageRatioText.Count; i++)
            m_DamageRatioText[i].SetActive(isOn);
    }
}
