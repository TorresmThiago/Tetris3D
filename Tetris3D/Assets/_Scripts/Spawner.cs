using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;

    public int spawnNext(GameObject[] pieces_1, GameObject[] pieces_2, int facing) {
        int index = Random.Range(0, pieces_1.Length);
        if (facing == 0) {
            Vector3 position = new Vector3(6, -2, 11.5f);
            Instantiate(pieces_1[index], position, Quaternion.identity);
        } else if (facing == 1) {
            Vector3 position = new Vector3(.5f, -2, 6);
            Instantiate(pieces_2[index], position, Quaternion.identity);
        } else if (facing == 2) {
            Vector3 position = new Vector3(6, -2, .5f);
            Instantiate(pieces_1[index], position, Quaternion.identity);
        } else if (facing == 3) {
            Vector3 position = new Vector3(11.5f, -2, 6);
            Instantiate(pieces_2[index], position, Quaternion.identity);
        } 
        return index;
	}

    public void adjToGrid(GameObject group, int facing, int[, ,] grid) {
        foreach (Transform child in group.transform) {
            int child_row = new int();
            if (facing == 0 || facing == 2) {
                child_row = Mathf.FloorToInt(child.transform.position.x);
            } else if (facing == 1 || facing == 3) {
                child_row = Mathf.FloorToInt(child.transform.position.z); 
            }
            int child_column = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, child_row, child_column] = 1;
        }
    }
}