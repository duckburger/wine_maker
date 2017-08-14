using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	
	private GameObject target;


	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
		
	
	}
	

	private void LateUpdate()
	{
		transform.position = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y + 0.25f, -10f);
	}
}
