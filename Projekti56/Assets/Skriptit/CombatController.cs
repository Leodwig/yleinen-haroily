using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour {
    public static CombatController instance;

    void Start() {
        instance = this;
    }

    public void Fight(Unit attacker, Unit defender) {

    }

}
