using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraUIManager : MonoBehaviour {

	public Image fadeImage;
	public float fadeSpeed;
	public DialogueTrigger dialogueTrigger;
	public CameraUIManager cameraUIManager;
	public bool sceneEnding;
	

	private Player player;
	private bool sceneStarting = true;
	private bool isFirstScene;
	private DialogueManager dialogueManager;


	private void Awake()
	{
		fadeImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
		dialogueManager = FindObjectOfType<DialogueManager>();


		
		
	}

	private void Start()
	{
		player = FindObjectOfType<Player>();
	}



	private void Update()
	{
		if (sceneStarting)
		{
			FadeIn();
		}

		if (sceneEnding)
		{
			FadeOut();
		}
	}




	void FadeIn()
	{
		fadeImage.color = Color.Lerp(fadeImage.color, Color.clear, fadeSpeed * Time.deltaTime);

	
		if (fadeImage.color.a < 0.05f && SceneManager.GetActiveScene().buildIndex == 0)
		{
			print(fadeImage.color);
			fadeImage.color = Color.clear;
			fadeImage.enabled = false;
			sceneStarting = false;
			dialogueTrigger.TriggerDialogue();
		} else if (fadeImage.color.a < 0.05f)
		{
			print(fadeImage.color);
			fadeImage.color = Color.clear;
			fadeImage.enabled = false;
			sceneStarting = false;
		}

	}

	void FadeOut()
	{
		
		if (dialogueManager.spokeLast.name == "Father")
		{
			fadeImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
			Debug.Log("Fading out with " + dialogueManager.spokeLast.name + "as the name");
			fadeImage.enabled = true;
			fadeImage.color = Color.Lerp(fadeImage.color, new Color (0,0,0,1), fadeSpeed * Time.deltaTime);

			if (fadeImage.color == Color.black)
			{
				SceneManager.LoadScene(1);
	
			}
		}
		
		
	}

}
