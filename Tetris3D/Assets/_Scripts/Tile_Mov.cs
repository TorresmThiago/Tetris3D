using UnityEngine;
using System.Collections;

public class Tile_Mov : MonoBehaviour {

    public Board_Grid Grid;
    public Spawner spawner;

    private int[, ,] board;

	IEnumerator MovVertical (){
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z);
		yield return new WaitForSeconds (0.25f);
		StartCoroutine (MovVertical ());
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
        board = Grid.genGrid();
	}

	void Update () {
		MovHorizontal ();
	}
}