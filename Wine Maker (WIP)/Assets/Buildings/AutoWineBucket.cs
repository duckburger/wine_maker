using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWineBucket : BuildingActions, IBottleproducer {



	[SerializeField] ParticleSystem effector;
	[SerializeField] float juiceTimer;
	[SerializeField] int wineBottlesProduced;
	[SerializeField] bool isBeingUsed = false;

	private Animator animator;
	private float juicingTimerMemory;
	private GameObject player;
	private Vector2 playerLastPos;
	private InventoryManager inventoryManager;



	// Use this for initialization
	void Start () {
		juicingTimerMemory = juiceTimer;
		player = GameObject.FindGameObjectWithTag("Player");
		inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
		animator = GetComponent<Animator>();
	}



	public override void HandleInteractions ()
	{
		if (!isBeingUsed && Vector2.Distance(player.transform.position, transform.position) <= 0.5f)
		{
			juiceTimer = juicingTimerMemory;
			isBeingUsed = true;
			effector.Play();
			animator.SetTrigger("autoStompingTrigger");
		
		}
	}

	

	public void ProduceBottles(int producedBottles)
	{
		inventoryManager.CurrentBottles += producedBottles;

	}



	// Update is called once per frame
	void Update () {

		

		juiceTimer -= Time.deltaTime;

		if (juiceTimer <= 0 && isBeingUsed)
		{
			isBeingUsed = false;
			

			effector.Stop();
			juiceTimer = juicingTimerMemory;
			animator.SetTrigger("autoIdleTrigger");

			ProduceBottles(wineBottlesProduced);

		}


	}
}
