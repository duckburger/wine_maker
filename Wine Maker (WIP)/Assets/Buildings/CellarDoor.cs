using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellarDoor : MonoBehaviour {

	public float rotSpeed;

	[SerializeField] bool opening;
	[SerializeField] bool closing;

	[SerializeField] bool open;



	public void OnMouseOver()
	{
		
		if (Input.GetMouseButtonDown(1))
		{
			if (!open)
			{
				if (!opening)
					opening = true;
				return;
			}

			if (!closing)
				closing = true;
			return;
			
		}
	}



	// Use this for initialization
	void Start () {
		
	}



	

	// Update is called once per frame
	void Update () {
		if (opening)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,-55), rotSpeed);

			if (transform.rotation == Quaternion.Euler(0, 0, -55))
			{
				open = true;
				opening = false;
				return;
			}
		}

		if (closing)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotSpeed);

			if (transform.rotation == Quaternion.Euler(0, 0, 0))
			{
				open = false;
				closing = false;
				return;
			}

		}

	}
}
