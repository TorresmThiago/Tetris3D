using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public int facing;
    public Board_Grid Grid;
    public Board_Rotate Board;
    public GameObject spawner;
    public GameObject[] groups;
    private GameObject tiles;

    private int[, ,] board;

    public int[, ,] getGrid() {
        return board;
    }

	void Awake () {
        board = Grid.genGrid();
        facing = Board.isFacing;
    }

    void Start() {
        int groupAdj = spawner.GetComponent<Spawner>().spawnNext(groups);
        spawner.GetComponent<Spawner>().adjToGrid(groups[groupAdj], facing, board);
    }

	void Update () {
        facing = Board.isFacing;
        tiles = GameObject.FindGameObjectWithTag("Piece");
	}

    void LateUpdate() {
        board = tiles.GetComponent<Tile_Mov>().getGrid();
        if (tiles.GetComponent<Tile_Mov>().hasStoped) {
            Destroy(tiles.GetComponent<Tile_Mov>());
        }
    }
}