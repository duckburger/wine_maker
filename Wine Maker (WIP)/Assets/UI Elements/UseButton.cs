using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseButton : MonoBehaviour {

	Button useButton;
	private GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		useButton = GetComponent<Button>();
		gameManager = FindObjectOfType<GameManager>();
		useButton.onClick.AddListener(gameManager.selectedBuilding.GetComponent<BuildingActions>().HandleInteractions);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
