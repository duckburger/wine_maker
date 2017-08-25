using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeActions : MonoBehaviour
{

	private SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start()
	{
		mySpriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			mySpriteRenderer.material.shader = Shader.Find("Sprites/Diffuse");

		}
	}


	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{

			if (collider.gameObject.CompareTag("Player"))
			{
				mySpriteRenderer.material.shader = Shader.Find("Sprites/Default");

			}
		}
	}
}

