using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	// Peruslattian prefab
	public GameObject basicGround;
	
	public int width = 10;
	public int height = 10;
	
	public float tileWidth = 0.5f;
	public float tileHeight = 0.5f;
	
	// Erikoistilejen prefabit
	public GameObject[] tiles;
	// Erikoistilejen koordinaatit
	public Vector2[] coordinates;

	private float offset;
	public float offsetAmount;
	
	void Start () {
		// Asetetaan muuttujan alkuarvo
		offsetAmount = tileWidth * 0.4f;
		
		// Luo pohjatilet
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				
				// Vinoutus - katsotaan onko rivi parillinen vai pariton
				if(j%2==0) {
					offset = offsetAmount;
				} else {
					offset = 0f;
				}
				
				//Instantioi basicGround-objektin tilen koordinaatteihin (i,j)
				GameObject tile = (GameObject)Instantiate(basicGround, new Vector3((i * tileWidth) + offset, j * tileHeight, 1), Quaternion.identity);
				//Asettaa luodun tilen tämän lapseksi
				tile.transform.parent = transform;
			}
		}
		
		// Luo erikoistilet
		for (int i = 0; i < tiles.Length; i++) {
			GameObject tile = tiles[i];
			
			// Lasketaan tilen koordinaatit
			Vector3 coord = (Vector3)coordinates[i] + new Vector3(0,0,-0.1f);
			
			// Vinoutus
			if(coordinates[i].y%2==0) {
				offset = offsetAmount;
			} else {
				offset = 0f;
			}
			
			// Instantioi erikoistilen koordinaatteihin
			GameObject instantiatedTile = (GameObject)Instantiate(tile, new Vector3((coord.x * tileWidth) + offset, coord.y * tileHeight, coord.z), Quaternion.identity);
			// Asettaa luodun tilen tämän lapseksi
			instantiatedTile.transform.parent = transform;
		}
		
		// Keskittää kentän
		transform.position = new Vector3(-(width*tileWidth)/2,-(height*tileHeight)/2,1);
	}
}
