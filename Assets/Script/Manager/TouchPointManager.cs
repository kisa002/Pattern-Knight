using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPointManager : MonoBehaviour {
	public Transform touchPoint;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			touchPoint.gameObject.SetActive(true);
		}

		if(Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 pos = ray.origin;
			pos.z = 0f;
			touchPoint.transform.position = pos;
		}

		if(Input.GetMouseButtonUp(0))
		{
			touchPoint.gameObject.SetActive(false);
		}
		
	}
}
