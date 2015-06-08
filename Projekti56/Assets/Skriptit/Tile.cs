using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public bool clicked = false;

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
// hit.transform.GetComponent[tile]

// jos clicked = true
// transform.Find("Frame").SetActive(true);

// if (asfjaf) { .... } else { .... }