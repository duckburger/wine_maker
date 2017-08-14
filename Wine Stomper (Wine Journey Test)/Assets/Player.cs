using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool isActive = false;

	[SerializeField] float movementSpeed;
	private Rigidbody2D myRigidBody;
	private float x;
	private float y;

	

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame


	private void FixedUpdate()
	{
		x = Input.GetAxisRaw("Horizontal");
	}

	void Update()
	{

		if (Input.GetButton("Horizontal") && isActive)
		{
			if (x > 0.7f || x < -0.7f)
			{

				myRigidBody.velocity = new Vector2(x * movementSpeed, myRigidBody.velocity.y);

			}



		} else
		{
			myRigidBody.velocity = Vector2.zero;

		}
	}
	}

