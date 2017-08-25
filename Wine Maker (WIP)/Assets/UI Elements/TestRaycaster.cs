using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycaster : MonoBehaviour {

	float range = 5000f;
	public LayerMask layerMask;

	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		


		if (Input.GetMouseButtonDown(1))
		{
			OnRightClick();
		}
	}

	void OnRightClick()
	{


		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (hit.collider != null)
		{
			print("The raycast hit " + hit.collider.gameObject); 
		}

	}
}
