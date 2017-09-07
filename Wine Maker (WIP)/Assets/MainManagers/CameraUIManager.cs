using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUIManager : MonoBehaviour {

	public Image fadeImage;
	public float fadeSpeed;
	public bool sceneEnding;
	public bool sceneStarting = true;
	public GameObject currentlyVisibleMenu;

	[SerializeField] GameObject inventoryPanel;
	[SerializeField] bool isInventoryOpen = false;

	public bool menuSpawned = false;
	public GameObject notificationsPanel;
	public delegate void OnRightMouseClick(GameObject currentlySelectedObject); // declare new delegate type
	public event OnRightMouseClick notifyMouseClickObservers; // notifies classes that the cameraUImanager detected a right click
	public List<Transform> trackedBuildings;
	[SerializeField] GameObject playerInteriorChecker;
	[SerializeField] LayerMask buildingLayerMask;
	[SerializeField] GameObject leftLandExtender;
	[SerializeField] GameObject rightLandExtender;
	[SerializeField] GameObject leftLandExtWarning;
	[SerializeField] GameObject rightLandExtWarning;



	private GameObject objectToLookAt;
	private GameObject player;
	private GameManager gameManager;
	private InventoryManager inventoryManager;
	
	private GameObject lastSelectedBuilding;
	private Shader oldShader;



	[SerializeField] GameObject buildingMenuPrefab;

	private void Awake()
	{
		fadeImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);

	}

	// Use this for initialization
	void Start () {
		inventoryPanel = GameObject.Find("Inventory Panel");
		player = GameObject.FindGameObjectWithTag("Player");
		objectToLookAt = player;
		gameManager = FindObjectOfType<GameManager>();
		inventoryManager = FindObjectOfType<InventoryManager>();
		notifyMouseClickObservers += OnRightClick;
		lastSelectedBuilding = null;
		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		
		if (sceneStarting)
		{
			FadeIn();
		}

		if (sceneEnding)
		{
			FadeOut();
		}


		if (Input.GetKeyDown(KeyCode.I))
		{

			if (!isInventoryOpen)
			{

				inventoryPanel.GetComponent<Animator>().SetTrigger("OpenInv");
				isInventoryOpen = !isInventoryOpen;

			}
			inventoryPanel.GetComponent<Animator>().SetTrigger("OpenInv");
			isInventoryOpen = !isInventoryOpen;
		}

		if (Input.GetMouseButtonDown(1))
		{
			notifyMouseClickObservers(gameManager.selectedBuilding);
			
		}
		
	}

	void FadeIn()
	{
		fadeImage.color = Color.Lerp(fadeImage.color, Color.clear, fadeSpeed * Time.deltaTime);

		if (fadeImage.color.a < 0.05f)
		{

			fadeImage.color = Color.clear;
			fadeImage.enabled = false;
			sceneStarting = false;
		}

	}

	void FadeOut()
	{


		fadeImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
		fadeImage.enabled = true;
		fadeImage.color = Color.Lerp(fadeImage.color, new Color(0, 0, 0, 1), fadeSpeed * Time.deltaTime);

	}

	// BELOW IS AN ALTERNATUVE WAY TO REVEAL BUILDING INTERIORS BASED ON RAYCASTS (IT HAS PERFORMANCE PROBLEMS, SO DISCARDED FOR NOW)
	//RaycastForInteriors();

	/*void RaycastForInteriors()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(playerInteriorChecker.transform.position, Vector2.zero, 100f, buildingLayerMask);

		for (int i = 0; i < hits.Length; i++)
		{
			Transform currentHit = hits[i].transform;

			if (hits[i].transform.gameObject.CompareTag("Building") && hits[i].transform.gameObject.GetComponent<BuildingActions>().hasInterior)
			{
				Renderer currentRend = currentHit.transform.GetComponent<Renderer>();


				if (currentRend)
				{
					
					trackedBuildings.Add(currentRend.transform);
					currentRend.enabled = false;
					
				}

			}
		}

		//Clean the list of objects that are in the list but not currently hit.
		for (int j = 0; j< trackedBuildings.Count; j++)
		{
			bool isHit = false;

			print("Set ishit to false");
			//Check every object in the list against every hit
			for (int i = 0; i < hits.Length; i++)
			{
				if (hits[i].transform == trackedBuildings[j])
				{
					isHit = true;
				}
			}

			if (!isHit)
			{
				print("Set to default");
				Transform wasHidden = trackedBuildings[j];
				wasHidden.GetComponent<Renderer>().enabled = true;
				trackedBuildings.RemoveAt(j);
				
				
				j--;
			}




		}

	}*/

	void OnRightClick(GameObject currentlySelectedBuilding)
	{

		
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		
		if (!hit)
		{
			if (menuSpawned)
			{
				Destroy(currentlyVisibleMenu);
				currentlyVisibleMenu = null;

				menuSpawned = false;
				return;
			}
		} else { 

		

			//print("The raycast hit " + hit.collider.gameObject);
			if (hit.collider.gameObject.tag == "Building")
			{

				if (hit.collider.gameObject != lastSelectedBuilding)
				{
					gameManager.selectedBuilding = hit.collider.gameObject;
					lastSelectedBuilding = gameManager.selectedBuilding;
					gameManager.selectedConstructionSlot = hit.collider.transform.parent.gameObject;

					if (!menuSpawned)
					{
						Transform placeToInstantiateBuildingUI = gameManager.selectedBuilding.GetComponentInChildren<BuildingUISocket>().transform;

						currentlyVisibleMenu = Instantiate(buildingMenuPrefab, placeToInstantiateBuildingUI.transform.position, Quaternion.identity, placeToInstantiateBuildingUI.transform);
						menuSpawned = true;

					}
					else if (menuSpawned)
					{
						Destroy(currentlyVisibleMenu);
						currentlyVisibleMenu = null;

						menuSpawned = false;
						return;
					}

				} else if (hit.collider.gameObject.name == lastSelectedBuilding.name)
				{
					if (menuSpawned)
					{
						Destroy(currentlyVisibleMenu);
						currentlyVisibleMenu = null;

						menuSpawned = false;
						return;


					} else if (!menuSpawned)
					{
						Transform placeToInstantiateBuildingUI = gameManager.selectedBuilding.GetComponentInChildren<BuildingUISocket>().transform;

						currentlyVisibleMenu = Instantiate(buildingMenuPrefab, placeToInstantiateBuildingUI.transform.position, Quaternion.identity, placeToInstantiateBuildingUI.transform);
						menuSpawned = true;

					}
				}
			}

			if (hit.collider.gameObject.tag == "LandExtender" && inventoryManager.CurrentBalance >= 10000f)
			{
				inventoryManager.UpdateBalance(-10000f);
				Transform slotToInstantiateIn = hit.collider.gameObject.transform.parent;
				Destroy(hit.collider.gameObject);

				if (slotToInstantiateIn.position.x < FindObjectOfType<HomeActions>().transform.position.x)
				{
					GameObject leftExtendedLand = Instantiate(leftLandExtender, slotToInstantiateIn.transform.position, Quaternion.identity, slotToInstantiateIn);
					Instantiate(leftLandExtWarning, leftExtendedLand.GetComponentInChildren<ExtenderSlot>().transform.position, Quaternion.identity, leftExtendedLand.GetComponentInChildren<ExtenderSlot>().transform);

				} else if (slotToInstantiateIn.position.x > FindObjectOfType<HomeActions>().transform.position.x)
				{
					GameObject rightExtendedLand =  Instantiate(rightLandExtender, slotToInstantiateIn.transform.position, Quaternion.identity, slotToInstantiateIn);
					Instantiate(rightLandExtWarning, rightExtendedLand.GetComponentInChildren<ExtenderSlot>().transform.position, Quaternion.identity, rightExtendedLand.GetComponentInChildren<ExtenderSlot>().transform);
				}
			}

		}


	}





	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (objectToLookAt.transform.position.x, objectToLookAt.transform.position.y + 0.2f);
	}
}
