using UnityEngine;
using System.Collections;

public class Tile_Mov : MonoBehaviour {

	IEnumerator MovVertical (){
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z);
		yield return new WaitForSeconds (0.25f);
		StartCoroutine (MovVertical ());
	}


	void rotate(){
		if (Input.GetKeyDown (KeyCode.Z)) {
			gameObject.transform.Rotate (0, 0, -90);
		} else if (Input.GetKeyDown (KeyCode.X)) {
			gameObject.transform.Rotate (0, 0, 90);
		}
	}

	void MovHorizontal(){
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
		} else if(Input.GetKeyDown (KeyCode.RightArrow)){
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
		}
	}

	void Start () {
		StartCoroutine (MovVertical ());
	}

	void Update () {
		rotate ();
		MovHorizontal ();
	}
}