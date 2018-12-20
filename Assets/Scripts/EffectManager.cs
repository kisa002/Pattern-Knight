using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public Animator[] hitEffect = new Animator[8];

    private void Awake()
    {
        if (EffectManager.Instance == null)
            EffectManager.Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayHitEffect(Random.Range(1, 8));
            //PlayHitEffect(1);
        }
    }

    public void PlayHitEffect(int count)
    {
        Debug.Log("E");
        for (int i = 0; i < count; i++)
        {
            hitEffect[i].gameObject.SetActive(true);
            hitEffect[i].transform.position = new Vector3(Random.Range(-1, 2), Random.Range(2, 4), -5);
        }
    }
}
