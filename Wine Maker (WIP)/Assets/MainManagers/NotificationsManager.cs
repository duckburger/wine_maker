using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour {

	
	public GameObject textNote;

	public Color colorMemory;
	public Transform player;


	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		

		
		
	}

	public void StartSpawningText(string textOfNotification)
	{
		var x = player.position.x;
		var y = player.position.y + 0.5f;

		var currentNotification = Instantiate(textNote,  new Vector3(x, y + Random.Range(0.2f, 0.5f)), Quaternion.identity);
		currentNotification.GetComponent<Text>().text = textOfNotification;

	}



	// Update is called once per frame
	void Update () { 
		
	}
}
