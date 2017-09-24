using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour {

	public float scrollSpeed;

	private Rigidbody2D myRigidBody;
	private SortingMinigame sortingMinigameController;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		sortingMinigameController = FindObjectOfType<SortingMinigame>();
		myRigidBody.velocity = new Vector2(-scrollSpeed, 0);
	}
	



	// Update is called once per frame
	void Update () {

		if (!sortingMinigameController.isPlaying)
		{
			myRigidBody.velocity = Vector2.zero;

		} else if (sortingMinigameController.isPlaying)
		{
			myRigidBody.velocity = new Vector2(-scrollSpeed, 0);
		}



	}
}
