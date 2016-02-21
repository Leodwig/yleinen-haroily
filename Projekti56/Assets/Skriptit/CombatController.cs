using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour {
    public static CombatController instance;

    void Start() {
        instance = this;
    }

    public void Fight(Unit attacker, Unit defender) {

        defender.GetComponent<Unit>().currentHealth = defender.GetComponent<Unit>().currentHealth - attacker.GetComponent<Unit>().attack * 1.5 * attacker.GetComponent<Unit>().currentHealth / 100;
        attacker.GetComponent<Unit>().currentHealth = attacker.GetComponent<Unit>().currentHealth - defender.GetComponent<Unit>().attack * defender.GetComponent<Unit>().currentHealth / 100;

    }

}
