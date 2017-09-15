using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteText : MonoBehaviour {

	public Color color = new Color(0.8f, 0.8f, 0.1f, 0.0f);
	public float scrollingVelocity = 0.02f;  // scrolling velocity
	public float duration = 1.5f; // time to die
	public float alpha;
	

	


	// Use this for initialization
	void Start () {
		
		gameObject.GetComponent<Text>().color = color; // set text color
		alpha = 2;

	}
	
	// Update is called once per frame
	void Update () {

		if (alpha > 0)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + scrollingVelocity * Time.deltaTime, 0);
			alpha -= Time.deltaTime / duration;
			gameObject.GetComponent<Text>().color = new Color (color.r, color.g, color.b, alpha);
		}
		else
		{
			Destroy(gameObject); // text vanished - destroy itself
		}

	}
}
