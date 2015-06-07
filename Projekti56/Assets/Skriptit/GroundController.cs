using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	public GameObject basicGround;
	
	public int width = 10;
	public int height = 10;
	
	public float tileWidth = 0.5f;
	public float tileHeight = 0.5f;
	
	void Start () {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject tile = (GameObject)Instantiate(basicGround, new Vector3(i * tileWidth, j * tileHeight, 1), Quaternion.identity);
				tile.transform.parent = transform;
			}
		}
		transform.position = new Vector3(-(width*tileWidth)/2,-(height*tileHeight)/2,1);
	}
	
	void Update () {
	
	}
}
