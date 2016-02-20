using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	private bool clicked = false;
    private bool selected = false;
    private bool highlighted = false;
    public GameObject unitInTile;

    // Tarvittava movement, että yksikkö voi siirtyä tileen
    public int movementCost = 1;
	
	// Kertoo, voiko yksikkö siirtyä tileen
	public bool moveable = false;
	
	// Update is called once per frame
	void Update () {
		if (clicked) {
            if (!selected) {
                SelectThis();
                if (unitInTile != null) {
                    HighlightNeighbours(unitInTile.GetComponent<Unit>().movement);
                } else {
                    //HighlightNeighbours();
                }
            }
		}
	}

	private GameObject[] HighlightNeighbours () {
		// Naapureiden highlight
		GameObject[] tileObjects = GroundController.instance.GetNeighbours(gameObject);
        for (int i = 0; i < tileObjects.Length; i++) {
            if (!tileObjects[i].GetComponent<Tile>().isHighlighted() && !tileObjects[i].GetComponent<Tile>().isSelected()) {
                tileObjects[i].GetComponent<Tile>().HighlightThis();
            }
        }
        return tileObjects;
    }

    public void HighlightNeighbours (int range) {
        if (range > 0) {
            GameObject[] neighbours = HighlightNeighbours();
            for (int i = 0; i < neighbours.Length; i++) {
                neighbours[i].GetComponent<Tile>().HighlightNeighbours(range - movementCost);
            }
        }
    }

    private void UnhighlightNeighbours() {
        GameObject[] tileObjects = GroundController.instance.GetNeighbours(gameObject);
        for (int i = 0; i < tileObjects.Length; i++) {
            tileObjects[i].GetComponent<Tile>().UnselectThis();
        }
    }

    private void SelectThis() {
        selected = true;
        transform.Find("Frame").gameObject.SetActive(true);
        transform.Find("Frame").gameObject.GetComponent<Animator>().enabled = true;
        // unitInTile.GetComponent<Unit>().selected = true;
    }

    private void HighlightThis () {	// Highlightausfunktio
        highlighted = true;
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
        highlighted = false;
        clicked = false;
        selected = false;
        UnselectThis();
        UnhighlightNeighbours();
    }

    public bool isHighlighted() {
        return highlighted;
    }

    public bool isSelected() {
        return selected;
    }
}