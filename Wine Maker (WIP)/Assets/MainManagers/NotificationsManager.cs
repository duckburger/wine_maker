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
	private List<GameObject> allTextNotes = new List<GameObject>();
	public Queue<string> allStringsToDisplay = new Queue<string>();
	[SerializeField] bool currentlySpawningText;
	

	public Color colorMemory;
	public float moveSpeed;

	// Use this for initialization
	void Start()
	{
		textStartPos = transform.position;
		textEndPos = new Vector2(transform.position.x, transform.position.y + 10);

		for (int i = 0; i < 3; i++)
		{
			allTextNotes.Add(Instantiate(textNote, transform.position, Quaternion.identity, transform));
			allTextNotes[i].SetActive(false);
		}
		
		
	}

	public void StartSpawningText(string textOfNotification)
	{

		if (allStringsToDisplay.Count >= 3)
		{
			allStringsToDisplay.Clear();
		}

		allStringsToDisplay.Enqueue(textOfNotification);

		foreach (GameObject notes in allTextNotes)
		{
			if (notes.active = false)
			{

			}
		}


		

		
	}




	
	
	// Update is called once per frame
	void Update () {

		
	}
}
