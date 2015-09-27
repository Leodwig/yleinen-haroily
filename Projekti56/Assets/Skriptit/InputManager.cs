using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private GameObject selected;

    private GroundController gc;

    void Start() {
        gc = GroundController.instance;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.GetComponent<Tile>()) {
                    if (selected && hit.transform.GetComponent<Tile>().isHighlighted()) {
                        if (selected.GetComponent<Tile>().unitInTile != null) {
                            if (selected.GetComponent<Tile>().unitInTile.GetComponent<Unit>().CanMove()) {
                                selected.GetComponent<Tile>().unitInTile.GetComponent<Unit>().Move(hit.transform.gameObject);
                                selected.GetComponent<Tile>().unitInTile = null;
                                selected = null;
                                UnselectAll();
                            }
                        }
                    } else {
                        UnselectAll();
                        selected = hit.transform.gameObject;
                        selected.GetComponent<Tile>().Click();
                    }
                }
            }
        }
    }

    private void UnselectAll() {
        foreach (GameObject obj in gc.tilesAsArray) {
            obj.transform.GetComponent<Tile>().Unclick();
        }
    }
}