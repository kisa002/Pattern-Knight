using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int currentHP, maxHP;

    public int ad;

    bool isDead = false;

    private void Start()
    {
        currentHP = maxHP;
        UIManager.Instance.InitBoss(currentHP, maxHP);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            HitDmg(Random.Range(100, 500));
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
        }
    }
    #endregion
}