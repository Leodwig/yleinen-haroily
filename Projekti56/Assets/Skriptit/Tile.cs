using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public bool clicked = false;
	
	// Tarvittava movement, että yksikkö voi siirtyä tileen
	public int movementCost;
	
	// Kertoo, voiko yksikkö siirtyä tileen
	public bool moveable = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (clicked) {
			transform.Find("Frame").gameObject.SetActive(true);
			HighlightNeighbours();
		}
		else {
			transform.Find("Frame").gameObject.SetActive(false);
		}
		
		
	}

	private void HighlightNeighbours () {
		// Naapureiden highlight
		GameObject[] tileObjects = GroundController.instance.GetNeighbours(gameObject);
		Tile[] tiles = new Tile[tileObjects.Length];
		for (int i = 0; i < tileObjects.Length; i++) {
			tiles[i] = tileObjects[i].GetComponent<Tile> ();
		}
		Debug.Log(tiles.Length);
		foreach (Tile tile in tiles) {
			tile.HighlightThis();
		}
	}
	
	public void HighlightThis () {	// Highlightausfunktio
		transform.Find("Frame").gameObject.SetActive(true);
		transform.Find("Frame").gameObject.GetComponent<Animator>().enabled = false;
	}
}