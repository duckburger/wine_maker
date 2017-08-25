using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject selectedBuilding;
	public GameObject selectedConstructionSlot;

	private GameObject[] allAvaialableBuildingSlots;
	private CameraUIManager cameraUIManager;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		cameraUIManager = FindObjectOfType<CameraUIManager>();
	}


	public void DeleteBuidlingFromCurrentSlot ()
	{
		foreach (Transform child in selectedConstructionSlot.transform)
		{
			GameObject.Destroy(child.gameObject);
			selectedBuilding = null;
			
		}
		cameraUIManager.menuSpawned = false;
	}

	public void UpgradeBuildingInCurrentSlot ()
	{

		if (selectedConstructionSlot.transform.childCount > 0)
		{

			if (selectedBuilding.GetComponent<Upgradability>().nextLevelOfThisBuilding != null)
			{
				GameObject nextUpgradeMemory = selectedBuilding.GetComponent<Upgradability>().nextLevelOfThisBuilding;

				foreach (Transform child in selectedConstructionSlot.transform)
				{
					GameObject.Destroy(child.gameObject);
				}

				selectedBuilding = Instantiate(nextUpgradeMemory, selectedConstructionSlot.transform.position, Quaternion.identity, selectedConstructionSlot.transform);
				cameraUIManager.menuSpawned = false;
			}
			else
			{
				return;
			}
		}
			

		
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
