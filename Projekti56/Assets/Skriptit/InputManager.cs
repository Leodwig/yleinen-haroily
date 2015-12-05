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

            // Katsotaan mihin osutaan klikkauksella
            if (Physics.Raycast(ray, out hit)) {

                //Osutaanko tileen
                if (hit.transform.GetComponent<Tile>()) {

                    //Jos muistissa on aiemmin klikattu ruutu, ja nyt kyseessä on uusi highlightattu ruutu (potentiaalinen liikkumisruutu)
                    if (selected && hit.transform.GetComponent<Tile>().isHighlighted()) {

                        //Jos edellisessä valitussa tilessä on liikkumiskykyinen yksikkö ja kohderuutu on vapaa
                        if (selected.GetComponent<Tile>().unitInTile != null) {
                            if (selected.GetComponent<Tile>().unitInTile.GetComponent<Unit>().CanMove() && hit.transform.GetComponent<Tile>().unitInTile == null) {

                                //Liikutetaan yksikkö uuteen tileensä ja poistetaan highlightit kaikista ruuduista
                                selected.GetComponent<Tile>().unitInTile.GetComponent<Unit>().Move(hit.transform.gameObject);
                                selected = null;
                                UnselectAll();
                            }
                        //Muutoin
                        } else {
                            //Deselektoidaan kaikki vanhat tilet ja valitaan klikattu tile (tile sitten itse highlightaa kaikki naapurinsa)
                            UnselectAll();
                            selected = hit.transform.gameObject;
                            selected.GetComponent<Tile>().Click();
                        }
                    //Muutoin
                    } else {
                        //Sama deselektointi
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