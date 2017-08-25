using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

	[SerializeField] float currentBalance;
	[SerializeField] int currentBottles;
	[SerializeField] int pricePerBottle;

	// Use this for initialization
	void Start() {
		DontDestroyOnLoad(gameObject);
	}

	public float CurrentBalance {
		 get {
			return currentBalance;
		}

		set
		{
			currentBalance = value;
		}

		}

	public int CurrentBottles
	{
		get
		{
			return currentBottles;
		}

		set
		{
			currentBottles = value;
		}

	}

	public void SellAllBottles()
	{
		currentBalance += currentBottles * pricePerBottle;
		currentBottles = 0;
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateBalance(float amount)
	{
		currentBalance += amount;
		return;
	}
}
