using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

	public GameObject[] units;
	public Vector2[] coordinates;
	
	void Start () {
	
	for (int i = 0; i < units.Length; i++) {
			GameObject unit = units[i];
			
			// Instantioi unitin koordinaatteihin
			GameObject instantiatedUnit = (GameObject)Instantiate(unit, new Vector3(0,0,0), Quaternion.identity);
			
			// Groundcontrollerin taulukosta haetaan tile, johon unit ollaan laittamassa
			GameObject tile = GroundController.instance.tilesAsArray[(int)coordinates[i].x,(int)coordinates[i].y];
            
            instantiatedUnit.GetComponent<Unit>().SetTile(tile);
		}
}
}