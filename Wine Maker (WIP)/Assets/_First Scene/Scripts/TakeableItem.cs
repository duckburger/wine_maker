using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TakeableItem : MonoBehaviour {

	public int myItemID;

	//public Inventory inventory;

	// Use this for initialization
	void Start () {

		
		
	}

	/*private void OnTriggerStay2D(Collider2D collider)
	{

		if (collider.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				 if (inventory.AddItemToInventory(myItemID))
					Destroy(gameObject);
			}
		}
	}*/

	// Update is called once per frame
	void Update () {
		
	}
}
