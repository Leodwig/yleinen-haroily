using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	private GameObject selected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit)) {
				// Tähän settiä
				Debug.Log(hit.transform.name);
				
				selected.GetComponent<Tile>().clicked = false;
				selected = hit.gameObject;
				selected.GetComponent<Tile>().clicked = true;
			}
		}
	}
}