using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class StompingButton {


	public Sprite myCurrentImage;
	public string keyCode;
	


	private string[] buttonCodes = new string[] { "up", "down", "left", "right" };




	public StompingButton ()
	{
		keyCode = buttonCodes[Random.Range(0, 3)];
		
		
	}

}
