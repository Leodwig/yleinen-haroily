using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public bool clicked = false;
	
	// Tarvittava movement, että yksikkö voi siirtyä tileen
	public int movementCost;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (clicked == true) {
			transform.Find("Frame").gameObject.SetActive(true);
		}
		else {
			transform.Find("Frame").gameObject.SetActive(false);
		}
	}
}
