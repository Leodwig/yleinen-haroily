using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	// Yksikölle ominainen liikkumisnopeus
	public int movement = 1;
    public int attackRange = 1;
    public int attack = 20;
    public int defense = 10;
    public double maxHealth = 100;
    public double currentHealth = 100;

    public bool canMove = true;
    public bool canFight = true;

    private GameObject currentTile;

    private TeamController team;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move(GameObject newTile) {
        SetTile(newTile);
    }

    public void SetTile(GameObject tile) {
        if (currentTile) {
            currentTile.GetComponent<Tile>().unitInTile = null;
        }
        currentTile = tile;
        tile.GetComponent<Tile>().unitInTile = this.gameObject;
        this.transform.position = tile.transform.position + new Vector3(0, 0, -0.2f);
    }

    public void SetTeam(TeamController team) {
        this.team = team;
    }

    public TeamController GetTeam() {
        return team;
    }

}
