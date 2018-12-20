﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour {

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void Update () {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("HitEffects"))
        {
            Debug.Log("XXX");
            gameObject.SetActive(false);
        }
	}
}