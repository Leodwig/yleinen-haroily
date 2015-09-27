using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	private bool clicked = false;
    private bool selected = false;

	// Tarvittava movement, että yksikkö voi siirtyä tileen
	public int movementCost;
	
	// Kertoo, voiko yksikkö siirtyä tileen
	public bool moveable = false;
	
	// Update is called once per frame
	void Update () {
		if (clicked) {
            if (!selected) {
                SelectThis();
                HighlightNeighbours();
                selected = true;
            }
		}
	}

	private void HighlightNeighbours () {
		// Naapureiden highlight
		GameObject[] tileObjects = GroundController.instance.GetNeighbours(gameObject);
		Tile[] tiles = new Tile[tileObjects.Length];
        for (int i = 0; i < tileObjects.Length; i++) {
            tileObjects[i].GetComponent<Tile>().HighlightThis();
        }
    }

    private void UnhighlightNeighbours() {
        GameObject[] tileObjects = GroundController.instance.GetNeighbours(gameObject);
        Tile[] tiles = new Tile[tileObjects.Length];
        for (int i = 0; i < tileObjects.Length; i++) {
            tileObjects[i].GetComponent<Tile>().UnselectThis();
        }
    }

    private void SelectThis() {
        transform.Find("Frame").gameObject.SetActive(true);
        transform.Find("Frame").gameObject.GetComponent<Animator>().enabled = true;
    }

    private void HighlightThis () {	// Highlightausfunktio
		transform.Find("Frame").gameObject.SetActive(true);
		transform.Find("Frame").gameObject.GetComponent<Animator>().enabled = false;
	}

    private void UnselectThis() {
        transform.Find("Frame").gameObject.SetActive(false);
    }

    public void Click() {
        clicked = true;
    }

    public void Unclick() {
        clicked = false;
        selected = false;
        UnselectThis();
        UnhighlightNeighbours();
    }
}