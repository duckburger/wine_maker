using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroTextPanel : MonoBehaviour {

	//public Scene1Blurbs scene1Blurbs;


	private Image backgroundImage;
	//private DialogueManager dialogueManager;
	private string[] stringsToDisplay;
	private Animator animator;

	public Text titleText;
	public Text bodyText;

	

	// Use this for initialization
	void Start () {
		backgroundImage = GetComponent<Image>();

		animator = GetComponent<Animator>();

		//dialogueManager = FindObjectOfType<DialogueManager>();

		//stringsToDisplay = scene1Blurbs.textToShow;

		WriteTheIntro();

	}

	// Update is called once per frame
	void Update () {

		

	}


	public void WriteTheIntro ()
	{
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			titleText.text = "";
			bodyText.text = "";

			
			StopAllCoroutines();
			StartCoroutine(TypeTitle(stringsToDisplay[0]));
			StartCoroutine(TypeSentence(stringsToDisplay[1]));



		}
	
	}

	IEnumerator TypeSentence(string sentence)
	{
		
		foreach (char letter in sentence.ToCharArray())
		{
			
			bodyText.text += letter;
			yield return new WaitForSeconds(.01f);
		}
	}

	IEnumerator TypeTitle(string sentence)
	{

		foreach (char letter in sentence.ToCharArray())
		{
			
			titleText.text += letter;
			yield return new WaitForSeconds(.01f);
		}
	}


}
