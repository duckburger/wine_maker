using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StompingMinigame : MonoBehaviour {


	public float qsForThisStage;
	public GameObject stompingSliderGO;
	public Slider stompingTimer;
	public GameObject wineBucket;

	[SerializeField] ParticleSystem effector;
	[SerializeField] float stompTimer;
	[SerializeField] Sprite leftArrow;
	[SerializeField] Sprite rightArrow;
	[SerializeField] Sprite upArrow;
	[SerializeField] Sprite downArrow;
	[SerializeField] bool isPlaying = false;
	[SerializeField] GameObject arrowPrefab;
	[SerializeField] GameObject arrowToPressNow;
	[SerializeField] Sprite emptyStateImage;
	[SerializeField] Sprite fullStateImage;
	

	[SerializeField] List<GameObject> listOfArrows = new List<GameObject>();
	[SerializeField] List<StompingButton> listOfButtons = new List<StompingButton>();

	[SerializeField] bool hasPlayedOnce = false;
	private int amountOfButtonsToDisplay = 4;
	private PlayerMovement player;
	private Animator animator;
	private Inventory inventory;
	private float stompTimerMemory;



	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		animator = GetComponent<Animator>();

		stompTimerMemory = stompTimer;

		stompingTimer = stompingSliderGO.GetComponent<Slider>();
		stompingTimer.maxValue = stompTimer;
		stompingTimer.value = stompTimer;

		inventory = FindObjectOfType<Inventory>();

	}

	public void StartTheStompingMiniGame()
	{

		effector = wineBucket.GetComponentInChildren<ParticleSystem>();

		qsForThisStage = 0;
		isPlaying = true;
		animator.SetTrigger("isAppearing");
		player.isUsingSomething = true;
		if (!hasPlayedOnce)
		{
			GenerateANewButtonList();
			hasPlayedOnce = true;
			StartCoroutine(ChooseArrowToPress());
		} else if (hasPlayedOnce)
		{

			ActivateStompingButtons();
			StartCoroutine(ChooseArrowToPress());

		}


	}

	private void ActivateStompingButtons()
	{
		arrowProps[] listOfChildren = new arrowProps[4];
		listOfChildren = transform.GetComponentsInChildren<arrowProps>(true);
		foreach (arrowProps prop in listOfChildren)
		{
			print("activating the arrows back up");
			prop.gameObject.SetActive(true);
		}
	}

	public void StopTheStompingMiniGame()
	{
		isPlaying = false;
		player.isUsingSomething = false;
		ClearButtonList();
		StopAllCoroutines();
		animator.SetTrigger("isAppearing");
		qsForThisStage = 0;
		wineBucket.GetComponentInChildren<WineBucket>().isBeingUsed = false;
		wineBucket.GetComponentInChildren<WineBucket>().isEmpty = true;
	}

	IEnumerator ChooseArrowToPress()
	{
		if (arrowToPressNow != null)
		{
			arrowToPressNow.GetComponent<Animator>().SetBool("isBlinking", false);
		}
		arrowToPressNow = listOfArrows[UnityEngine.Random.Range(0, 4)];
		arrowToPressNow.GetComponent<Animator>().SetBool("isBlinking", true);
		print("Chose a new arrow");
		yield return new WaitForSeconds(1.2f);
		StopAllCoroutines();
		StartCoroutine(ChooseArrowToPress());
	}

	void ClearButtonList()
	{
		listOfButtons.Clear();
		for (int i = 0; i < amountOfButtonsToDisplay; i++)
		{
			
			arrowProps[] listOfChildren = transform.GetComponentsInChildren<arrowProps>();
			foreach (arrowProps prop in listOfChildren)
			{
				prop.gameObject.SetActive(false);
			}
			

			
		}
		return;
	}

	void GenerateANewButtonList()
	{
		listOfButtons = new List<StompingButton>();

		for (int i = 0; i < amountOfButtonsToDisplay; i++)
		{
			
			listOfButtons.Add(new StompingButton());
			switch (listOfButtons[i].keyCode)
			{
				case "up":
					listOfButtons[i].myCurrentImage = upArrow;
					break;
				case "down":
					listOfButtons[i].myCurrentImage = downArrow;
					break;
				case "left":
					listOfButtons[i].myCurrentImage = leftArrow;
					break;
				case "right":
					listOfButtons[i].myCurrentImage = rightArrow;
					break;
			}

			listOfArrows.Add(Instantiate(arrowPrefab, transform));
			listOfArrows[i].GetComponent<Image>().sprite = listOfButtons[i].myCurrentImage;
			listOfArrows[i].GetComponent<arrowProps>().myID = i;
			listOfArrows[i].GetComponent<arrowProps>().keyCode = listOfButtons[i].keyCode;
		}


	}
	
	// Update is called once per frame
	void Update () {


		if (isPlaying)
		{
			stompTimer -= Time.deltaTime;
			stompingTimer.value = stompTimer;

			if (stompTimer <= 0)
		{
			
			player.GetComponent<PlayerMovement>().myAnimator.SetTrigger("playerIdle");
			player.GetComponent<PlayerMovement>().isUsingSomething = false;

			effector.Stop();
			stompTimer = stompTimerMemory;
			wineBucket.GetComponent<WineBucket>().mySpriteRender.sprite = emptyStateImage;
			
			Item itemToAdd = inventory.AddItem("full_clay_jar", 1);

			inventory.lastAddedItem.GetComponent<ItemData>().myBottleInProgress.qualityScore = wineBucket.GetComponent<WineBucket>().qSToRemember + qsForThisStage;

			wineBucket.GetComponent<WineBucket>().qSToRemember = 0;

			StopTheStompingMiniGame();
			


		}

			if (Input.anyKeyDown)
			{
				if (Input.GetKeyDown(arrowToPressNow.GetComponent<arrowProps>().keyCode))
				{
					print("CORRECT PRESS!");
					if (qsForThisStage < 50)
						qsForThisStage++;
					StopAllCoroutines();
					StartCoroutine(ChooseArrowToPress());
				}
				else 
				{
					if (qsForThisStage >= 0)
						qsForThisStage--;
					StopAllCoroutines();
					StartCoroutine(ChooseArrowToPress());
				}
			}
			
		}
		
	}
}
