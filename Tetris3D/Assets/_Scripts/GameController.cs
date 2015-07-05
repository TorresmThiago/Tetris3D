using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public Board_Grid Grid;
    public Spawner spawner;

    private int[, ,] board;

	void Start () {
        board = Grid.genGrid();
	}
	
	void Update () {
	
	}
}
