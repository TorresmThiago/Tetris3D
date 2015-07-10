using UnityEngine;
using System.Collections;

public class Board_Grid : MonoBehaviour {

    /* 
      
      Three-dimensional array with specified dimensions [Page, Row, Column]
     
      [0,0,0]  [0,0,1]  [0,0,2]  |  [1,0,0]  [1,0,1]  [1,0,2]
                                 | 
      [0,1,0]  [0,1,1]  [0,1,2]  |  [1,1,0]  [1,1,1]  [1,1,2]
                                 |
      [0,2,0]  [0,2,1]  [0,2,2]  |  [1,2,0]  [1,2,1]  [1,2,2]
     
     */

    private int[, ,] grid;

	public int[, ,] genGrid(){
        grid = new int[4, 24, 12];
        grid = setGrid(grid);
        return grid;
	}

    private int[, ,] setGrid(int [, ,]Grid) {
        for (int i = 0; i < Grid.GetLength(0); i++) {
            for (int j = 0; j < Grid.GetLength(1); j++) {
                for (int k = 0; k < Grid.GetLength(2); k++) {
                    if (((j == 0) || (k == 0)) || ((j == Grid.GetLength(1) - 1) || (k == Grid.GetLength(2) - 1))) {
                        Grid[i, j, k] = 1;
                    } else {
                        Grid[i, j, k] = 0;
                    }
                }
            }
        }

        return Grid;

    }

	public string heyGrid(int [, ,]Grid, int facing) {

		string a = "";

		for (int i = 0; i < Grid.GetLength(0); i++) {
			for (int j = 0; j < Grid.GetLength(1); j++) {
				for (int k = 0; k < Grid.GetLength(2); k++) {
					if(i == facing)
						a = a + " " + Grid [i,j,k];
				}
				a = a + " - Quebra de Linha - ";
			}
			a = a + " - Troca de face - ";
		}
		
		return a;
		
	}
}
