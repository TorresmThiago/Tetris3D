using UnityEngine;
using System.Collections;

public class HolderUpdate : MonoBehaviour {

    public GameObject gameController;
    int[, ,] board;
    int[] line = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    int[] compare = new int[12];

    /* 
      
      Three-dimensional array with specified dimensions [Page, Row, Column]
     
      [0,0,0]  [0,0,1]  [0,0,2]  |  [1,0,0]  [1,0,1]  [1,0,2]
                                 | 
      [0,1,0]  [0,1,1]  [0,1,2]  |  [1,1,0]  [1,1,1]  [1,1,2]
                                 |
      [0,2,0]  [0,2,1]  [0,2,2]  |  [1,2,0]  [1,2,1]  [1,2,2]
     

    
	Transform[] allChildren = GetComponentsInChildren<Transform>();
	foreach (Transform child in allChildren) {
		// do whatever with child transform here
	}

	*/

    bool compareArray (int[]a, int[]b) {

        for(var i = 1; i < a.Length; i++) {
            if(a[i] != b[0]){
                return false;        
            }
        }

        return true;
    }

    void destroyHolder() { 
        foreach (Transform child in gameObject.transform) {
            if (child.transform.position.y < -50) {
                Destroy(child.gameObject);
            }
        }
    }
    
    void breakLine(GameObject holder, int facing, int line) {
        foreach (Transform child in gameObject.transform) {
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int actualColumn = new int();
            if (facing == 0 || facing == 2) {
                actualColumn = Mathf.FloorToInt(child.transform.position.x);
            } else if (facing == 1 || facing == 3) {
                actualColumn = Mathf.FloorToInt(child.transform.position.z);
            }

            if (board[facing, line, actualRow] == 1) {
                Destroy(child);
            }
        }
    }

    void checkGrid(int[, ,] grid) {
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                for (int k = 0; k < grid.GetLength(2); k++) {
                    compare[k] = grid[i, j, k];
                }
                if (compareArray(compare, line)) {
                    breakLine(gameObject, i, j);
                }
            }
        }
    }
        
    void Start() {
        //board = gameController.GetComponent<GameController>().getGrid();
    }

	void Update () {
        //checkGrid(board);

    }

    void LateUpdate() { 
    
    }
}
