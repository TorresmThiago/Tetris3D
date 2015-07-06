using UnityEngine;
using System.Collections;

public class Tile_Mov : MonoBehaviour {

    public int[, ,] board;
    public int boardFacing = 0;

    public void eraseInGrid(int[, ,] grid, int facing, GameObject group) {
        foreach (Transform child in group.transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, actualRow, actualColumn] = 0;
        }
    }

    public void appearInGrid (int[, ,] grid, int facing, GameObject group) {
        foreach (Transform child in group.transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, actualRow, actualColumn] = 1;
        }
    }

    public bool canRotate(int[, ,] grid, int facing, string direction, GameObject group) {
        //Erase the group position in grid so it won't hit itself
        eraseInGrid(grid, facing, group);
        
        //Rotate the group itself
        if (direction == "left"){
            group.transform.Rotate(0, 0, 90);
        } else{
            group.transform.Rotate(0, 0, -90);
        }

        //Loop that goes through the position in desired rotation checking if it's empty 
        foreach (Transform child in group.transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            if (grid[facing, actualRow, actualColumn] == 1) {
                
                //If it hits something, rotate back to the original position, draws it back in the grid and returns false (Not allowing the rotation)
                if (direction == "left") {
                    group.transform.Rotate(0, 0, -90);
                } else {
                    group.transform.Rotate(0, 0, 90);
                }

                appearInGrid(grid, facing, group);
                return false;

            }
        }

        //If it goes through the entire loop without hiting anything, rotates back to the original position, draws it back in the grid and returns true (Allowing rotation)
        if (direction == "left") {
            group.transform.Rotate(0, 0, -90);
        } else {
            group.transform.Rotate(0, 0, 90);
        }

        appearInGrid(grid, facing, group);
        return true;
    }

    public bool canMoveVertical(int[, ,] grid, int facing, GameObject group) {
        eraseInGrid(grid, facing, group);
        foreach (Transform child in group.transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int desiredRow = actualRow + 1;
            if (grid[facing, desiredRow, actualColumn] == 1) {
                appearInGrid(grid, facing, group);
                return false;
            }
        }

        appearInGrid(grid, facing, group);
        return true;
    }

    public bool canMoveHorizontal(int[, ,] grid, int facing, string direction, GameObject group) {
        eraseInGrid(grid, facing, group);

        foreach (Transform child in group.transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int desiredColumn;
            if (direction == "left") {
                desiredColumn = actualColumn - 1;
            } else {
                desiredColumn = actualColumn + 1;
            }
            if (grid[facing, actualRow, desiredColumn] == 1) {
                appearInGrid(grid, facing, group);
                return false;
            }
        }

        appearInGrid(grid, facing, group);
        return true;
    }

    public void rotateObject(GameObject group, int[, ,] grid, int facing) {
        if (Input.GetKeyDown(KeyCode.Z) && canRotate(grid, facing, "left", group)) {
            eraseInGrid(grid, facing, group);
            group.transform.Rotate(0, 0, -90);
            foreach (Transform child in group.transform) {
                child.transform.Rotate(0, 0, 90);
            }
            appearInGrid(grid, facing, group);
        } else if (Input.GetKeyDown(KeyCode.X) && canRotate(grid, facing, "right", group)) {
            eraseInGrid(grid, facing, group);
            group.transform.Rotate(0, 0, 90);
            foreach (Transform child in group.transform) {
                child.transform.Rotate(0, 0, -90);
            }
            appearInGrid(grid, facing, group);
        }
    }

    public IEnumerator MovVertical (GameObject group, int[, ,] grid, int facing){
        yield return new WaitForSeconds(0.5f);
        if (canMoveVertical(grid, facing, group)) {
            eraseInGrid(grid, facing, group);
            group.transform.position = new Vector3(group.transform.position.x, group.transform.position.y - 1, group.transform.position.z);
            appearInGrid(grid, facing, group);
        } else {
            group.tag = "Board";
            foreach (Transform child in group.transform) {
                child.tag = "Board";
            }
        }
		
		StartCoroutine (MovVertical (group, grid, facing));
	}

    public void MovHorizontal(GameObject group, int[, ,] grid, int facing) {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && canMoveHorizontal(grid, facing, "left", group)) {
            eraseInGrid(grid, facing, group);
            group.transform.position = new Vector3(group.transform.position.x - 1, group.transform.position.y, group.transform.position.z);
            appearInGrid(grid, facing, group);

        } else if (Input.GetKeyDown(KeyCode.RightArrow) && canMoveHorizontal(grid, facing, "right", group)) {
            eraseInGrid(grid, facing, group);
            group.transform.position = new Vector3(group.transform.position.x + 1, group.transform.position.y, group.transform.position.z);
            appearInGrid(grid, facing, group);
        }
    }
}