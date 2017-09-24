using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

	private BoxCollider2D conveyorCollider;
	private float conveyorBeltLength;

	// Use this for initialization
	void Start () {
		conveyorCollider = GetComponent<BoxCollider2D>();
		conveyorBeltLength = conveyorCollider.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x < -conveyorBeltLength)
		{
			RepositionBG();
		}
	}


	void RepositionBG()
	{
		Vector2 offset = new Vector2(conveyorBeltLength * 2, 0);
		transform.localPosition = (Vector2)transform.localPosition + offset;

	}
}
