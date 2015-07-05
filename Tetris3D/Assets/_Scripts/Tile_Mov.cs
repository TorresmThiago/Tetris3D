using UnityEngine;
using System.Collections;

public class Tile_Mov : MonoBehaviour {

    public Board_Grid Grid;
    public Spawner spawner;

    private int[, ,] board;

    void eraseInGrid(int[, ,] grid, int facing) { 
        foreach (Transform child in transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, actualRow, actualColumn] = 0;
        }
    }

    void appearInGrid (int[, ,] grid, int facing) {
        foreach (Transform child in transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, actualRow, actualColumn] = 1;
        }
    }

    bool canMoveVertical(int[, ,] grid, int facing) {
        eraseInGrid(grid, facing);

        foreach (Transform child in transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int desiredRow = actualRow--;
            if (grid[facing, desiredRow, actualColumn] == 1) {
                return false;
            }
        }

        appearInGrid(grid, facing);
        return true;
    }

	IEnumerator MovVertical (){ //EDIT
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z);
		yield return new WaitForSeconds (0.25f);
		StartCoroutine (MovVertical ());
	}

    bool canMoveHorizontal(int[, ,] grid, int facing, string direction) {
        eraseInGrid(grid, facing);

        foreach (Transform child in transform) { 
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int desiredColumn = direction == "left" ? desiredColumn = actualColumn-- : desiredColumn = actualColumn++;
            if (grid[facing, actualRow, desiredColumn] == 1) {
                return false;
            }
        }

        appearInGrid(grid, facing);
        return true;
    }

    void MovHorizontal() { //EDIT
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