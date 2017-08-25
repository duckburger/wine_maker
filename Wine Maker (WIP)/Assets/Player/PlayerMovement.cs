using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	[SerializeField] private float jumpStrength;
	[SerializeField] private float minJumpDistanceFromGround;


	private Rigidbody2D myRigidBody;


	public bool isUsingSomething = false;
	public Animator myAnimator;
	public LayerMask layerMask;
	
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		
	}

	private void Update()
	{
		if (Input.GetKey("space") && IsGrounded())
		{
				myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpStrength);

		}
	}

	private void FixedUpdate()
	{
		if (!isUsingSomething)
		{
			HandleMovement();
			myRigidBody.isKinematic = false;
		} else
		{
			myRigidBody.isKinematic = true;
			myRigidBody.velocity = new Vector2(0, 0);
		}

		
	}

	bool IsGrounded()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, minJumpDistanceFromGround, layerMask);
		if (hit.collider != null)
		{
			return true;
		}

		return false;
	}







	void HandleMovement()
	{
		var x = Input.GetAxisRaw("Horizontal");
		
		if (Input.GetButton("Horizontal"))
		{

			if (x > 0.7f || x < -0.7f)
			{
			
				myRigidBody.velocity = new Vector2(x * movementSpeed, myRigidBody.velocity.y);

			}

			


		}

		if (x < 0.5f && x > -0.5f) // This code HAS to be outside the check for buttons pressed, otherwise the velocity doesn't get updated (ALREADY DONE)
		{
			myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
		}

	
		







	}
}
