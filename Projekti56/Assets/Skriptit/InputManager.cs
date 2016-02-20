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
                        Unit ownUnit = selected.GetComponent<Tile>().unitInTile.GetComponent<Unit>();
                        Unit targetUnit = null;
                        if (hit.transform.GetComponent<Tile>().unitInTile) {
                            targetUnit = hit.transform.GetComponent<Tile>().unitInTile.GetComponent<Unit>();
                        }

                        //Jos edellisessä valitussa tilessä on liikkumiskykyinen yksikkö ja kohderuutu on vapaa
                        if (ownUnit != null) {
                            // Jos yksikkö kuuluu kontrolloivaan tiimiin
                            if (ownUnit.GetTeam() == Teams.instance.currentTeam) {
                                if (targetUnit == null) {
                                    if (ownUnit.canMove) {
                                        //Liikutetaan yksikkö uuteen tileensä ja poistetaan highlightit kaikista ruuduista
                                        ownUnit.Move(hit.transform.gameObject);
                                        selected = null;
                                        UnselectAll();
                                        ownUnit.canMove = false;
                                    }
                                    // Jos siinä ruudussa olikin jo joku niin valitaan se
                                } else if (targetUnit != null) {
                                    if (ownUnit.GetTeam() != targetUnit.GetTeam()) {
                                        if (ownUnit.canFight) {
                                            CombatController.instance.Fight(ownUnit, targetUnit);
                                            ownUnit.canFight = false;
                                            Debug.Log("FIGHT! Between " + ownUnit.name + " (team "+ownUnit.GetTeam() + ") and " + targetUnit.name + " (team "+ownUnit.GetTeam() + ")");
                                        }
                                    } else {
                                        //Deselektoidaan kaikki vanhat tilet ja valitaan klikattu tile (tile sitten itse highlightaa kaikki naapurinsa)
                                        Select(hit);
                                    }
                                }
                                //Muutoin
                            } else {
                                //Deselektoidaan kaikki vanhat tilet ja valitaan klikattu tile (tile sitten itse highlightaa kaikki naapurinsa)
                                Select(hit);
                            }
                        }
                        //Muutoin
                    } else {
                        //Sama deselektointi
                        Select(hit);
                    }

                }
            }
        }
    }

    private void Select(RaycastHit hit) {
        UnselectAll();
        selected = hit.transform.gameObject;
        selected.GetComponent<Tile>().Click();
    }

    private void UnselectAll() {
        foreach (GameObject obj in gc.tilesAsArray) {
            obj.transform.GetComponent<Tile>().Unclick();
        }
    }
}