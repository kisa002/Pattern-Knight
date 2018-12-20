using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Singleton<BossController>
{
    public static BossController Instance;

    public int currentHP, maxHP;

    public int ad;

    bool isDead = false;

    public Animator animator;

    private void Awake()
    {
        if (BossController.Instance == null)
            BossController.Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentHP = maxHP;
        UIManager.Instance.InitBoss(currentHP, maxHP);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            HitDmg(Random.Range(1000, 3000));

        if (Input.GetKeyDown(KeyCode.A))
            PlayAnimAttackWait();

        if (Input.GetKeyDown(KeyCode.S))
            PlayAnimAttack();

        if (Input.GetKeyDown(KeyCode.D))
            AudioManager.Instance.PlayBossVoice();
    }

    #region HP
    public int GetHP()
    {
        return currentHP;
    }

    public void HitDmg(int dmg)
    {
        currentHP -= dmg;
        UIManager.Instance.SetBossHP(currentHP, maxHP);

        AudioManager.Instance.PlayBossHit();

        if (currentHP <= 0 && !isDead)
        {
            // Dead
            currentHP = 0;

            isDead = true;
            AudioManager.Instance.PlayBossDead();

            PlayAnimAttack();
        }
    }
    #endregion

    public void PlayAnimAttackWait()
    {
        animator.SetTrigger("AttackWait");
    }

    public void PlayAnimAttack()
    {
        animator.SetTrigger("Attack");
    }
}