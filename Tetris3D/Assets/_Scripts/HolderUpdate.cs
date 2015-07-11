using UnityEngine;
using System.Collections;

public class HolderUpdate : MonoBehaviour {

    public GameObject gameController;
	public Tile_Mov tileMov;
    public int[, ,] board;
	public int direction;

    void breakLine(int[,,] board, int facing, int line) {
		int childCount = gameObject.transform.childCount;
		Transform[] children = gameObject.GetComponentsInChildren<Transform>();

		for(int i = 0; i <= childCount; i++) {

			if(((-1) * (Mathf.RoundToInt(children[i].transform.position.y + 0.5f))) == line){
				tileMov.GetComponent<Tile_Mov>().eraseInGrid(board, direction, children[i].gameObject);
				Destroy(children[i].gameObject);
			}

		}
    }

	void downLine(int[,,] board, int facing, int line) {
		int childCount = gameObject.transform.childCount;
		Transform[] children = gameObject.GetComponentsInChildren<Transform>();
		
		for(int i = 0; i < childCount; i++) {
			
			if(((-1) * (Mathf.RoundToInt(children[i].transform.position.y ))) == line){
				tileMov.GetComponent<Tile_Mov>().eraseInGrid(board, direction, children[i].gameObject);
				Destroy(children[i].gameObject);
				Destroy(children[i].gameObject);
			}
			
		}
	}

	public int [,,] getGrid(){
		return board;
	} 

    void checkGrid(int[, ,] grid, int facing) {

		bool fullLine;

		for (int j = 3; j < grid.GetLength(1) - 1; j++) {
			fullLine = true;
            for (int k = 1; k < grid.GetLength(2) - 1; k++) {
				if(grid[facing, j, k] == 0){
					fullLine = false;
				}
            }

			if(fullLine){
				print("FullLine");
				breakLine(grid, facing, j);
			}
        }
    }
        
    void Start() {
		board = gameController.GetComponent<GameController>().getGrid();
	}

	void LateUpdate () {
		board = gameController.GetComponent<GameController>().getGrid();
        checkGrid(board, direction);
    }
}
