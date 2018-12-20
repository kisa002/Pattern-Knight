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
        m_BossPos = GameObject.Find("BOSS").transform.position;
        gameObject.SetActive(false);
    }

    public void Active(int damage, Vector3 pos)
    {
        m_FromPos = transform.position = pos;

        m_ToPos = m_BossPos;
        m_ToPos.x += Random.Range(-0.5f, 0.5f);
        m_ToPos.y += Random.Range(-1.0f, 1.0f);

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
        BossController.Instance.HitDmg(m_Damage, transform.position);
        EffectManager.Instance.PlayHitEffect(transform.position);
        NoteManager.Instance.OnAttacked(this);
        Disable();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
