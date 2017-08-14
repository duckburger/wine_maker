using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		this.GetComponent<DialogueTrigger>().TriggerDialogue();
	}
}
