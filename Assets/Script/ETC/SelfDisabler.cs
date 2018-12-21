using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisabler : MonoBehaviour {

	public float delay = 2;
	float leftDelay;

	private void OnEnable() {
		leftDelay = delay;
		Debug.Log("OnEnable");
	}
	
	// Update is called once per frame
	void Update () {
		if(leftDelay > 0)
		{
			leftDelay -= Time.deltaTime;
		}
		else
		{
			this.gameObject.SetActive(false);			
		}
	}
}
