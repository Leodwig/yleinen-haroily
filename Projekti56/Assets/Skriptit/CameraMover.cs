using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
	public float speed = 1;
	
	void Update() {
		if(Input.GetKey(KeyCode.RightArrow)) {
			transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
	    }
		if(Input.GetKey(KeyCode.DownArrow)) {
			transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
		}
	}
}
