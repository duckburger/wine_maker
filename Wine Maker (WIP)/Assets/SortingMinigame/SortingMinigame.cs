using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingMinigame : MonoBehaviour {


	
	[SerializeField] float timeLeftToPlay;
	[SerializeField] float playTimer;
	public bool isPlaying = false;
	private Animator animator;
	private Inventory inventory;
	private PlayerMovement player;
	private SortingTable sortingTable;

	public Transform grapePanel;
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
		animator.SetTrigger("isAppearing");

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

	void PopulateTheField()
	{

	}

	void StopTheGame()
	{

	}







	// Update is called once per frame
	void Update()
	{
		
	}
}
