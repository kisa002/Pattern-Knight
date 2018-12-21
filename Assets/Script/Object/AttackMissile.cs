using UnityEngine;
using System.Collections;

public class AttackMissile : MonoBehaviour
{
    public Vector3 m_BossPos;
    public Vector3 m_FromPos, m_ToPos;
    private float m_MoveTime;

    public int m_Damage = 0;

    private Coroutine m_CorMover;

    public void Awake()
    {
        m_BossPos = Camera.main.WorldToScreenPoint(GameObject.Find("BOSS").transform.position);
        gameObject.SetActive(false);
    }

    public void Active(int damage, Vector3 pos)
    {
        if (GameManager.Instance.m_IsTitle)
            return;

        m_FromPos = transform.position = Camera.main.WorldToScreenPoint(pos);

        m_ToPos = m_BossPos;
        m_ToPos.x += Random.Range(-150f, 150f);
        m_ToPos.y += Random.Range(-150f, 150f);

        m_MoveTime = Random.Range(0.7f, 1.0f);

        m_Damage = damage;

        gameObject.SetActive(true);

        m_CorMover = StartCoroutine(CorMove());
    }

    private IEnumerator CorMove()
    {
        float nowTime = 0f;
        while (nowTime < m_MoveTime)
        {
            yield return new WaitForFixedUpdate();

            transform.position = Vector3.Lerp(m_FromPos, m_ToPos, nowTime / m_MoveTime);
            nowTime += Time.fixedDeltaTime;
        }

        HitBoss();
    }

    private void HitBoss()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(transform.position);
        BossController.Instance.HitDmg(m_Damage, worldPos);
        EffectManager.Instance.PlayHitEffect(worldPos);
        NoteManager.Instance.OnAttacked(this);
        Disable();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
