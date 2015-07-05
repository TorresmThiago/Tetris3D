using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public Board_Grid Grid;
    public Board_Rotate Board;
    public Spawner Spawner;
    public Tile_Mov TileManager;
    public GameObject[] groups;
    private GameObject tiles;
    public int facing;

    private int[, ,] board;

    public int[, ,] getGrid() {
        return board;
    }

	void Awake () {
        board = Grid.genGrid();
        facing = Board.isFacing;
    }

    void Start() {
        int groupAdj = Spawner.GetComponent<Spawner>().spawnNext(groups);
        Spawner.GetComponent<Spawner>().adjToGrid(groups[groupAdj], facing, board);
    }

	void Update () {
        facing = Board.isFacing;
        tiles = GameObject.FindGameObjectWithTag("Piece");
	}

    void LateUpdate() {
        if (tiles.GetComponent<Tile_Mov>().hasStoped) {
            Destroy(tiles.GetComponent<Tile_Mov>());
        }
    }
}