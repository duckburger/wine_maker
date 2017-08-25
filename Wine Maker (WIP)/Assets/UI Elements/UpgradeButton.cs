using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

	Button upgradeButton;
	GameManager gameManager;

	// Use this for initialization
	void Start () {
		upgradeButton = GetComponent<Button>();
		gameManager = FindObjectOfType<GameManager>();
		upgradeButton.onClick.AddListener(gameManager.UpgradeBuildingInCurrentSlot);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
