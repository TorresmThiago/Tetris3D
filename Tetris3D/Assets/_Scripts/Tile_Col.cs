using UnityEngine;
using System.Collections;

public class Tile_Col : MonoBehaviour {

	public Spawner spawner;
	
	void OnCollisionStay(Collision col){
		if(col.gameObject.tag == "Board"){
			this.transform.parent = col.gameObject.transform;
			foreach(Transform child in transform){
				child.gameObject.tag = "Board";
				gameObject.tag = "Board";
			}
			spawner.spawnNext();
			Destroy(gameObject.GetComponent<Rigidbody>());
			Destroy(gameObject.GetComponent<Tile_Mov>());
			
		}
	}
	
	void AdjustToInt(){
		int adjustPosX = Mathf.RoundToInt(transform.position.x);
		int adjustPosY = Mathf.RoundToInt(transform.position.y);
		gameObject.transform.position = new Vector3 (adjustPosX, adjustPosY, transform.position.z);
	}

}
