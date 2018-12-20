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

    public AudioClip[] acPlayerAttack = new AudioClip[3];
    public AudioClip[] acBossAttack = new AudioClip[3];
    public AudioClip[] acBossHit = new AudioClip[3];

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

    public void PlayPlayerAttack()
    {
        asPlayerAttack.clip = acPlayerAttack[Random.Range(0, acPlayerAttack.Length)];
        asPlayerAttack.Play();
    }

    public void PlayBossAttack()
    {
        asBossAttack.clip = acBossAttack[Random.Range(0, acBossAttack.Length)];
        asBossAttack.Play();
    }

    public void PlayBossHit()
    {
        asBossHit.clip = acBossHit[Random.Range(0, acBossHit.Length)];
        asBossHit.Play();
    }
}
