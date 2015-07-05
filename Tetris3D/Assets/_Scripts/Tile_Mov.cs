using UnityEngine;
using System.Collections;

public class Tile_Mov : MonoBehaviour {

    public int[, ,] board;
    public GameController gameController;
    public Board_Rotate boardRotation;
    public int boardFacing = 0;
    public bool hasStoped = false; 

    public int[, ,] getGrid() {
        return board;
    }

    public void eraseInGrid(int[, ,] grid, int facing) { 
        foreach (Transform child in transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, actualRow, actualColumn] = 0;
        }
    }

    public void appearInGrid (int[, ,] grid, int facing) {
        foreach (Transform child in transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, actualRow, actualColumn] = 1;
        }
    }

    public int[, ,] setGrid(int[, ,] grid) {
        return grid;
    }

    public bool canRotate(int[, ,] grid, int facing, string direction) {
        //Erase the group position in grid so it won't hit itself
        eraseInGrid(grid, facing);
        
        //Rotate the group itself
        if (direction == "left"){
            transform.Rotate(0, 0, 90);
        } else{
            transform.Rotate(0, 0, -90);
        }

        //Loop that goes through the position in desired rotation checking if it's empty 
        foreach (Transform child in transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            if (grid[facing, actualRow, actualColumn] == 1) {
                
                //If it hits something, rotate back to the original position, draws it back in the grid and returns false (Not allowing the rotation)
                if (direction == "left") {
                    transform.Rotate(0, 0, -90);
                } else {
                    transform.Rotate(0, 0, 90);
                }

                appearInGrid(grid, facing);
                return false;

            }
        }

        //If it goes through the entire loop without hiting anything, rotates back to the original position, draws it back in the grid and returns true (Allowing rotation)
        if (direction == "left") {
            transform.Rotate(0, 0, -90);
        } else {
            transform.Rotate(0, 0, 90);
        }

        appearInGrid(grid, facing);
        return true;
    }

    public bool canMoveVertical(int[, ,] grid, int facing) {
        eraseInGrid(grid, facing);

        foreach (Transform child in transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int desiredRow = actualRow--;
            if (grid[facing, desiredRow, actualColumn] == 1) {
                appearInGrid(grid, facing);
                return false;
            }
        }

        appearInGrid(grid, facing);
        return true;
    }

    public bool canMoveHorizontal(int[, ,] grid, int facing, string direction) {
        eraseInGrid(grid, facing);

        foreach (Transform child in transform) {
            int actualColumn = Mathf.FloorToInt(child.transform.position.x);
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int desiredColumn = direction == "left" ? desiredColumn = actualColumn-- : desiredColumn = actualColumn++;
            if (grid[facing, actualRow, desiredColumn] == 1) {
                appearInGrid(grid, facing);
                return false;
            }
        }

        appearInGrid(grid, facing);
        return true;
    }

    public void rotateObject() {
        if (Input.GetKeyDown(KeyCode.Z) && canRotate(board, boardFacing, "left")) {
            eraseInGrid(board, boardFacing);
            transform.Rotate(0, 0, -90);
            appearInGrid(board, boardFacing);
        } else if (Input.GetKeyDown(KeyCode.X) && canRotate(board, boardFacing, "right")) {
            eraseInGrid(board, boardFacing);
            transform.Rotate(0, 0, 90);
            appearInGrid(board, boardFacing);
        }
    }

    public IEnumerator MovVertical (){
        yield return new WaitForSeconds(0.25f);
        if (canMoveVertical(board, boardFacing)) {
            eraseInGrid(board, boardFacing);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z);
            appearInGrid(board, boardFacing);
        } else {
            gameObject.tag = "Board";
            foreach (Transform child in transform) {
                child.tag = "Board";
            }
            hasStoped = true;
        }
		
		StartCoroutine (MovVertical ());
	}

    public void MovHorizontal() { 
		if (Input.GetKeyDown (KeyCode.LeftArrow) && canMoveHorizontal(board, boardFacing, "left")) {
            eraseInGrid(board, boardFacing);
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
            appearInGrid(board, boardFacing);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && canMoveHorizontal(board, boardFacing, "right")) {
            eraseInGrid(board, boardFacing);
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
            appearInGrid(board, boardFacing);
        }
	}

    void Start() {
        board = gameController.getGrid();
        boardFacing = boardRotation.isFacing; 
        StartCoroutine(MovVertical());
    }

    void Update() {
        boardFacing = boardRotation.isFacing;
        rotateObject();
        MovHorizontal();
    }
}