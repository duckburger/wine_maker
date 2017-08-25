using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour {

	Button deleteButton;
	GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		deleteButton = GetComponent<Button>();
		deleteButton.onClick.AddListener(gameManager.DeleteBuidlingFromCurrentSlot);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
