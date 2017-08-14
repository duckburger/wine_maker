using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

	public GameObject goArrow;

	private IntroFriend introFriend;
	private Player player;
	private CameraUIManager cameraUIManager;
	private DialogueManager dialogueManager;
	private bool hasRunFirstCutscene;
	private bool spawnedGoArrow;
	

	// Use this for initialization
	void Start () {
		introFriend = FindObjectOfType<IntroFriend>();
		player = FindObjectOfType<Player>();
		cameraUIManager = FindObjectOfType<CameraUIManager>();
		dialogueManager = FindObjectOfType<DialogueManager>();
		
		


	}
	
	// Update is called once per frame
	void Update () {
		if (!hasRunFirstCutscene)
		{
			RunFirstCutscene();
		}

		if (dialogueManager.spokeLast.name == "Friend" && !spawnedGoArrow)
		{
			Instantiate(goArrow, new Vector3(player.transform.position.x, player.transform.position.y), Quaternion.identity);
			spawnedGoArrow = true;
		}

	}


	public void RunFirstCutscene ()
	{
		if (dialogueManager.spokeLast != null)
		{
			if (Vector2.Distance(introFriend.transform.position, player.transform.position) > 0.3f && dialogueManager.spokeLast.name == cameraUIManager.gameObject.name)
			{
				introFriend.transform.Translate(Vector2.left * Time.deltaTime);
			}

			if (Vector2.Distance(introFriend.transform.position, player.transform.position) <= 0.3f && dialogueManager.spokeLast.name == cameraUIManager.gameObject.name)
			{
				introFriend.GetComponent<DialogueTrigger>().TriggerDialogue();
				hasRunFirstCutscene = true;
				
				
				

			}
		}

		
	}

	
}
