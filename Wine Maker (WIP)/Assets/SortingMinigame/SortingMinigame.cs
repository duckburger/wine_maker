using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingMinigame : MonoBehaviour {


	
	[SerializeField] float timeLeftToPlay;
	[SerializeField] float playTimer;
	[SerializeField] bool isPlaying = false;
	private Animator animator;


	public float qsForThisStage;
	public List<GrapeButton> grapeButtons = new List<GrapeButton>();
	public float badGrapesRemoved;
	public float goodGrapesRemoved;
	public Sprite goodGrape;
	public Sprite badGrape;
	public Transform grapePanel;
	public GameObject grapeButton;
	public Slider sortingSlider;


	private int buttonAmount = 54;

	private void Start()
	{
		animator = GetComponent<Animator>();
		playTimer = timeLeftToPlay;
		sortingSlider = GameObject.Find("SortingTimer").GetComponent<Slider>();
		sortingSlider.maxValue = timeLeftToPlay;
		sortingSlider.value = playTimer;

		

	}

	void CalculateTheFinalQSForThisStage (float badGrapesRemoved, float goodGrapesRemoved)
	{
		float badGrapesValAsPerc = (badGrapesRemoved / 25) * 100;
		float goodGrapesValAsPerc = (goodGrapesRemoved / 25) * 100;


		float totalValForThisStage = badGrapesValAsPerc - goodGrapesRemoved;

		

		qsForThisStage = (25.0f / 100.0f)*totalValForThisStage;

		animator.SetTrigger("isAppearing");



	}



	public void StartTheSortingMiniGame()
	{

		animator.SetTrigger("isAppearing");
		if (!isPlaying)
		{
			isPlaying = true;
			PopulateTheField();
			

		}


		Debug.Log("The game has already started");


	}

	




	void PopulateTheField ()
	{
		for (int i = 0; i < buttonAmount; i++)
		{ 
			GrapeButton newGrapeButton = new GrapeButton(); 
			grapeButtons.Add(new GrapeButton());
			grapeButtons[i].myID = i;
			GameObject buttonToAdd = Instantiate(grapeButton, grapePanel);
			buttonToAdd.GetComponent<GoodOrBadButton>().valueOfThis = newGrapeButton.buttonValue;
			buttonToAdd.GetComponent<GoodOrBadButton>().buttonID = grapeButtons[i].myID;
			buttonToAdd.GetComponent<Image>().sprite = newGrapeButton.buttonImage;	
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (isPlaying)
		{
			playTimer -= Time.deltaTime;
			sortingSlider.value = playTimer;

		}

		if (playTimer <= 0)
		{
			isPlaying = false;
			
		}

		
	}
}
