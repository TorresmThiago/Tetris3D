using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public Board_Grid Grid;
    public Board_Rotate Board;
    public Spawner Spawner;
    public Tile_Mov TileManager;
    public GameObject[] groups_1;
    public GameObject[] groups_2;
    public GameObject[] Holders;
    private GameObject tiles;
    public int facing;
    private int index;
    private int[, ,] board;
    private float time;

    public int[, ,] getGrid() {
        return board;
    }

	void Awake () {
        board = Grid.genGrid();
		facing = gameObject.GetComponent<Board_Rotate>().getDirection();
    }

    void Start() {
        time = 0.7f;
        index = Random.Range(0, 4);
        int groupAdj = Spawner.GetComponent<Spawner>().spawnNext(groups_1, groups_2, index);
        tiles = GameObject.FindGameObjectWithTag("Piece");
        TileManager.StartCoroutine(TileManager.MovVertical(tiles, board, index, Holders,time));
        Destroy(GameObject.FindGameObjectWithTag("UsedHolder"));
    }

	void Update () {
        facing = gameObject.GetComponent<Board_Rotate>().getDirection();
        tiles = GameObject.FindGameObjectWithTag("Piece");
        TileManager.MovHorizontal(tiles, board, index);
        if (Input.GetKeyDown(KeyCode.Space)) {
            time = 0.01f;
            TileManager.StartCoroutine(TileManager.MovVertical(tiles, board, index, Holders, time));
        }
	}

    void LateUpdate() {
		for (int i = 0; i < 4; i++) {
			int[,,] a = Holders[i].GetComponent<HolderUpdate>().getGrid();
			for (int j = 0; j < board.GetLength(1); j++) {
				for (int k = 0; k < board.GetLength(2); k++) {
					board[i,j,k] = a[i,j,k];
				}
			}
		}
		
		if (tiles.tag == "UsedHolder") {
			Start();
        }
		TileManager.rotateObject(tiles, board, index);
    }
}