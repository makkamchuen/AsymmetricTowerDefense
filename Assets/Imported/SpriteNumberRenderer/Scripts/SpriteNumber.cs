using UnityEngine;
using System.Collections;

public class SpriteNumber : MonoBehaviour {


	
	public Sprite[] numbers;
	[UnityEngine.HideInInspector]
	public SpriteRenderer render;

	// Use this for initialization
	void Awake ()
	{
		//used so that we can use the Bounds function
		render = this.GetComponent<SpriteRenderer> ();
		render.sprite = numbers [0]; //temporarily set the number to 0
	}


	public void SetNumber( int number)
	{
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = numbers [number];
	}
}
