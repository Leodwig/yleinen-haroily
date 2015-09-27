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
                    foreach (GameObject obj in gc.tilesAsArray) {
                        obj.transform.GetComponent<Tile>().Unclick();
                    }
                    selected = hit.transform.gameObject;
                    selected.GetComponent<Tile>().Click();
                }
            }
        }
    }
}