using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;

	public int spawnNext(GameObject[] pieces){
		int index = Random.Range (0, pieces.Length);
        Instantiate(pieces [index]);
        return index;
	}

    public void adjToGrid(GameObject group, int facing, int[, ,] grid) {
        foreach (Transform child in group.transform) { 
            int child_row = Mathf.FloorToInt(child.transform.position.x); 
            int child_column = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, child_row, child_column] = 1;
        }
    }
}