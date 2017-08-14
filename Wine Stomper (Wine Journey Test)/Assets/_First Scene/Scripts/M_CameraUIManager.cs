using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class M_CameraUIManager : MonoBehaviour
{

	public Image fadeImage;
	public float fadeSpeed;
	public CameraUIManager cameraUIManager;
	public bool sceneEnding;


	private bool sceneStarting = true;



	private void Awake()
	{
		fadeImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
		



	}

	private void Start()
	{
		
		DontDestroyOnLoad(gameObject);
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

		 if (fadeImage.color.a < 0.05f)
		{
			
			fadeImage.color = Color.clear;
			fadeImage.enabled = false;
			sceneStarting = false;
		}

	}

	void FadeOut()
	{


		fadeImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
		fadeImage.enabled = true;
		fadeImage.color = Color.Lerp(fadeImage.color, new Color(0, 0, 0, 1), fadeSpeed * Time.deltaTime);





	}

}
