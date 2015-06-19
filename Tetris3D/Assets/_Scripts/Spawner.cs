using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;

	public void spawnNext(){
		int index = Random.Range (0, groups.Length);
		Instantiate (groups [index], gameObject.transform.position, Quaternion.identity);
	}

	void Start () {
		spawnNext ();
	}

	void Update () {
	
	}
}
