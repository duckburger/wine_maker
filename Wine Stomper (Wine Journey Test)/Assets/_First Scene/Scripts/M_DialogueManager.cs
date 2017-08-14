using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_DialogueManager : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;
	public Sprite imageOfSpeaker;
	public GameObject dialogBoxImageOfSpeaker;
	public GameObject spokeLast;
	public Animator animator;
	public Queue<string> currentSentences;
	public GameObject spokeLastMemory;


	

	private Player player;
	private CameraUIManager cameraUIManager;


	// Use this for initialization
	void Start()
	{
		currentSentences = new Queue<string>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		//cameraUIManager = FindObjectOfType<CameraUIManager>();

	}

	public void StartDialogue(Dialogue dialogue, GameObject speakerGameObject)
	{
		spokeLastMemory = speakerGameObject;
		player.isActive = false;
		animator.SetBool("isOpen", true);
		nameText.text = dialogue.name;

		if (dialogue.characterImage != null)
		{
			imageOfSpeaker = dialogue.characterImage;
			dialogBoxImageOfSpeaker.GetComponent<Image>().sprite = imageOfSpeaker;
		}

		currentSentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			currentSentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (currentSentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = currentSentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}


	void EndDialogue()
	{
		spokeLast = spokeLastMemory;

		Debug.Log("End of conversation");
		animator.SetBool("isOpen", false);
		dialogBoxImageOfSpeaker.GetComponent<Image>().sprite = null;
		player.isActive = true;

		if (spokeLast.name == "Father")
		{
			cameraUIManager.sceneEnding = true;
		}


	}


}
