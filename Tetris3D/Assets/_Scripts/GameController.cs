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
		facing = gameObject.GetComponent<Board_Rotate>().getDirection();
    }

    void Start() {
        int groupAdj = Spawner.GetComponent<Spawner>().spawnNext(groups, facing);
        Spawner.GetComponent<Spawner>().adjToGrid(groups[groupAdj], facing, board);
        tiles = GameObject.FindGameObjectWithTag("Piece");
        TileManager.StartCoroutine(TileManager.MovVertical(tiles, board, facing));
    }

	void Update () {
        facing = gameObject.GetComponent<Board_Rotate>().getDirection();
        tiles = GameObject.FindGameObjectWithTag("Piece");
        TileManager.MovHorizontal(tiles, board, facing);
        TileManager.rotateObject(tiles, board, facing);
	}

    void LateUpdate() {
        if (tiles.tag == "Board")
			Start ();
    }
}