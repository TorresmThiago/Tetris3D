using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public Board_Grid Grid;
    public Board_Rotate Board;
    public Spawner Spawner;
    public Tile_Mov TileManager;
    public GameObject[] groups_1;
    public GameObject[] groups_2;
    private GameObject tiles;
    public int facing;
    private int index;
    private int[, ,] board;

    public int[, ,] getGrid() {
        return board;
    }

	void Awake () {
        board = Grid.genGrid();
		facing = gameObject.GetComponent<Board_Rotate>().getDirection();
    }

    void Start() {
        index = Random.Range(0, 4);
        int groupAdj = Spawner.GetComponent<Spawner>().spawnNext(groups_1, groups_2, index);
        if (index == 1 || index == 3) {
            Spawner.GetComponent<Spawner>().adjToGrid(groups_2[groupAdj], index, board);
        } else {
            Spawner.GetComponent<Spawner>().adjToGrid(groups_1[groupAdj], index, board);
        }
        tiles = GameObject.FindGameObjectWithTag("Piece");
        TileManager.StartCoroutine(TileManager.MovVertical(tiles, board, index));
    }

	void Update () {
        facing = gameObject.GetComponent<Board_Rotate>().getDirection();
        tiles = GameObject.FindGameObjectWithTag("Piece");
        TileManager.MovHorizontal(tiles, board, index);
        TileManager.rotateObject(tiles, board, index);
	}

    void LateUpdate() {
        if (tiles.tag == "Board")
			Start ();
    }
}