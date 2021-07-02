using System.Collections;
using UnityEngine;

public class NumberExample : MonoBehaviour
{
	[SerializeField]
	private NumberRenderer signLevel;
	private MapManager mapManager;

	// Use this for initialization
	void Start()
	{
		mapManager = GetComponent<MapManager>();
		//This is the main render function. Give this fuction the number you want to render.
		signLevel.RenderNumber(mapManager.mapNumber);
	}

	public void RefreshSign()
	{
		signLevel.RenderNumber(mapManager.mapNumber);
	}
	/* 
		// Update is called once per frame
		void Update()
		{
			signLevel.RenderNumber(mapManager.mapNumber); //Render the score
		} */
}