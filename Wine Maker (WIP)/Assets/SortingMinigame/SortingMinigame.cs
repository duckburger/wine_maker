using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingMinigame : MonoBehaviour {


	
	[SerializeField] float timeLeftToPlay;
	[SerializeField] float playTimer;
	[SerializeField] bool isPlaying = false;
	private Animator animator;
	private Inventory inventory;
	private PlayerMovement player;
	private SortingTable sortingTable;


	public List<GrapeButton> grapeButtons = new List<GrapeButton>();
	public float badGrapesRemoved;
	public float goodGrapesRemoved;
	public Sprite goodGrape;
	public Sprite badGrape;
	public Transform grapePanel;
	public GameObject grapeButton;
	public GameObject sortingSliderGO;
	public Slider sortingSlider;


	private int buttonAmount = 54;

	private void Start()
	{
		sortingTable = FindObjectOfType<SortingTable>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		animator = GetComponent<Animator>();
		playTimer = timeLeftToPlay;
		sortingSlider = sortingSliderGO.GetComponent<Slider>();
		sortingSlider.maxValue = timeLeftToPlay;
		sortingSlider.value = playTimer;
		inventory = FindObjectOfType<Inventory>();
		

	}

	



	public void StartTheSortingMiniGame()
	{

		if (inventory.CheckForItemInInventory("full_grape_basket_u"))
		{
			sortingTable.isBeingUsed = !sortingTable.isBeingUsed;

			animator.SetTrigger("isAppearing");
			if (!isPlaying)
			{
				isPlaying = true;
				inventory.RemoveItem("full_grape_basket_u", 1);
				PopulateTheField();
				sortingTable.isBeingUsed = false;

			}


			Debug.Log("The game has already started");
		}

		Debug.Log("You don't have a basket of unsorted grapes to sort!");
		


	}

	public void StopTheSortingMiniGame()
	{

		animator.SetTrigger("isAppearing");
	
		inventory.AddItem("full_grape_basket_s", 1);

	

		player.isUsingSomething = false;
		sortingTable.isBeingUsed = false;
	
		goodGrapesRemoved = 0;
		badGrapesRemoved = 0;


	}






	void PopulateTheField ()
	{
		player.isUsingSomething = true;
		grapeButtons = new List<GrapeButton>();
		GoodOrBadButton[] oldButtons = grapePanel.GetComponentsInChildren<GoodOrBadButton>();
		for (int j = 0; j < oldButtons.Length; j++)
		{
			Destroy(oldButtons[j].gameObject);
		}
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
			StopTheSortingMiniGame();
			playTimer = timeLeftToPlay;
		}

		
	}
}
