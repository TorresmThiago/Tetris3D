using UnityEngine;
using System.Collections;

public class Board_Grid : MonoBehaviour {

    //Three-dimensional array with specified dimensions [Page, Row, Column]
    /*
     
      [0,0,0]  [0,0,1]  [0,0,2]  |  [1,0,0]  [1,0,1]  [1,0,2]
                                 | 
      [0,1,0]  [0,1,1]  [0,1,2]  |  [1,1,0]  [1,1,1]  [1,1,2]
                                 |
      [0,2,0]  [0,2,1]  [0,2,2]  |  [1,2,0]  [1,2,1]  [1,2,2]
     
     */

    private int[, ,] grid;
    
	private void genGrid(){
        grid = new int[4, 24, 12];
        setGrid(grid);
	}

    private void setGrid(int[, ,] Grid) {
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                for (int k = 0; k < grid.GetLength(2); k++) {
                    if(((j == 0) || (k == 0)) || ((j == grid.GetLength(1)) || (k == grid.GetLength(2)))){
                        grid[i,j,k] = 1;
                    } else {
                        grid[i,j,k] = 0;
                    }
                }
            }
        }
    }

    void Start() {
        genGrid();
    }
}
