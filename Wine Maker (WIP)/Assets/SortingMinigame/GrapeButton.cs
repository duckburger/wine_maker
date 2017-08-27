using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeButton {

	public ButtonValue buttonValue;
	public int buttonSlot;
	public Sprite buttonImage;
	public int myID;

	private SortingMinigame sortingMinigameController = GameObject.Find("SortingMinigame").GetComponent<SortingMinigame>();


	public enum ButtonValue
	{
		Good,
		Bad,
		Max
	}

	public ButtonValue PickItem()
	{
		ButtonValue valueOfButton = (ButtonValue)Random.Range(0, (int)ButtonValue.Max);
		return valueOfButton;
	}
	
	public GrapeButton()
	{
		this.buttonValue = PickItem();
		
		if (buttonValue == ButtonValue.Good)
		{
			buttonImage = sortingMinigameController.goodGrape;
		} else if (buttonValue == ButtonValue.Bad)
		{
			buttonImage = sortingMinigameController.badGrape;

		}
	}


}
