using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource asBGM;

    public AudioSource asPlayerAttack;
    public AudioSource asBossAttack;
    public AudioSource asBossHit;

    public AudioSource asBossDead;
    public AudioSource asPlayerDead;

    public AudioSource asBossVoice;

    public AudioClip[] acPlayerAttack = new AudioClip[3];
    public AudioClip[] acBossAttack = new AudioClip[3];
    public AudioClip[] acBossHit = new AudioClip[3];

    public AudioClip[] acBossDead= new AudioClip[3];
    public AudioClip[] acPlayerDead = new AudioClip[3];

    public AudioClip[] acBossVoice = new AudioClip[3];

    private void Awake()
    {
        if (AudioManager.Instance == null)
            AudioManager.Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        asBGM.Play();
    }

    // 플레이어가 공격하는 효과음
    public void PlayPlayerAttack()
    {
        asPlayerAttack.clip = acPlayerAttack[Random.Range(0, acPlayerAttack.Length)];
        asPlayerAttack.Play();
    }

    // 보스가 공격
    public void PlayBossAttack()
    {
        asBossAttack.clip = acBossAttack[Random.Range(0, acBossAttack.Length)];
        asBossAttack.Play();
    }

    // 보스 피격 시 멘트
    public void PlayBossHit()
    {
        asBossHit.clip = acBossHit[Random.Range(0, acBossHit.Length)];
        asBossHit.Play();
    }

    // 게임 승리
    public void PlayBossDead()
    {
        asBossDead.clip = acBossDead[Random.Range(0, acBossDead.Length)];
        asBossDead.Play();
    }

    // 게임 패배
    public void PlayPlayerDead()
    {
        asPlayerDead.clip = acPlayerDead[Random.Range(0, acPlayerDead.Length)];
        asPlayerDead.Play();
    }

    // 보이스
    public void PlayBossVoice()
    {
        asBossVoice.clip = acBossVoice[Random.Range(0, acBossVoice.Length)];
        asBossVoice.Play();
    }
}
