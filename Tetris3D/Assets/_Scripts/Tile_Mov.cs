using UnityEngine;
using System.Collections;

public class Tile_Mov : MonoBehaviour {

	public Spawner spawner;
	private bool isMoving = true;

	IEnumerator MovVertical (){
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, (int)gameObject.transform.position.y - 1, gameObject.transform.position.z);
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (MovVertical ());
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Board"){
			this.transform.parent = col.gameObject.transform;
			gameObject.tag = "Board";
			spawner.spawnNext();
			Destroy(rigidbody);
			Destroy(gameObject.GetComponent<Tile_Mov>());
		}
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