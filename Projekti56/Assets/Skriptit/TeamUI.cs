using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeamUI : MonoBehaviour {

	void Update () {
        gameObject.GetComponent<Text>().text = Teams.instance.currentTeam.teamName;
	}
}
