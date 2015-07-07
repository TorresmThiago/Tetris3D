using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;

	public int spawnNext(GameObject[] pieces, int facing){
		int index = Random.Range (0, pieces.Length);
        if (facing == 0) {
            Vector3 position = new Vector3(6,-2,-11.5f); 
            Instantiate(pieces[index], position, Quaternion.identity);
        } else if (facing == 2) {
            Vector3 position = new Vector3(6, -2, -.5f);
            Instantiate(pieces[index], position, Quaternion.identity);
        }
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