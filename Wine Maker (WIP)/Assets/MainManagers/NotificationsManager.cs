using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour {

	
	public GameObject textNote;
	public float durationOfNotification;
	public float timer;

	private Vector2 textStartPos;
	private Vector2 textEndPos;
	private Text text;

	public Color colorMemory;
	public bool liveNotificationActive;
	public float moveSpeed;

	// Use this for initialization
	void Start()
	{
		textStartPos = transform.position;
		textEndPos = new Vector2(transform.position.x, transform.position.y + 10);
		text = textNote.GetComponent<Text>();
		textNote.SetActive(false);
	}

	public void StartSpawningText(string textOfNotification)
	{
		if (!liveNotificationActive)
		{
			textNote.SetActive(true);
			text.color = colorMemory;
			text.text = textOfNotification;
			liveNotificationActive = true;
			timer = 0;

		} if (liveNotificationActive)
		{
			textNote.SetActive(false);
			textNote.transform.position = this.transform.position;
			textNote.SetActive(true);
			text.color = colorMemory;
			text.text = textOfNotification;
			timer = 0;
		}
		
		

	}

	void ResetText()
	{
		textNote.SetActive(false);
		textNote.transform.position = textStartPos;
		timer = 0;
	}


	
	
	// Update is called once per frame
	void Update () {

		if (liveNotificationActive)
		{
			textNote.transform.position = new Vector3(textNote.transform.position.x, textNote.transform.position.y + (moveSpeed * Time.deltaTime), 0);
			timer += Time.deltaTime;
			

			if (timer > durationOfNotification - 1)
			{
				
				text.color = Color.Lerp(text.color, Color.clear, 0.02f);
			}

			if (textNote.transform.position.y > Screen.height / 6)
			{
				liveNotificationActive = false;
			}
			
		} else if (!liveNotificationActive)
		{
			ResetText();
		}
	}
}
