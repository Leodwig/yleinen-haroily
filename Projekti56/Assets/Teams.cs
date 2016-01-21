using UnityEngine;
using System.Collections;

public class Teams : MonoBehaviour {

    public TeamController[] teams;

    public TeamController currentTeam;
    private int currentTeamId = 0;

    void Start() {
        currentTeam = teams[currentTeamId];
    }

    public void AdvanceTurn() {
        if (teams[currentTeamId+1]) {
            currentTeamId++;
        } else {
            currentTeamId = 0;
        }
        currentTeam = teams[currentTeamId];
    }

    public void SetTurn(int id) {
        if (teams[id]) {
            currentTeamId = id;
            currentTeam = teams[id];
        }
    }
	
}
