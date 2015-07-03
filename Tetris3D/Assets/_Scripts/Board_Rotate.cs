using UnityEngine;
using System.Collections;

public class Board_Rotate : MonoBehaviour {

	private float rotLeft;
	private float rotRight;
	private bool isRotating;
	private string direction;

	void Swing(float rot)
	{   
		Quaternion newRotation = Quaternion.AngleAxis (rot, Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, 0.1f);   
	}

	void FixAngle(string direction){

		if (direction == "left") {
			switch ((int)transform.localEulerAngles.y) {
			case 91:
				transform.rotation = Quaternion.Euler(0, 90, 0);
				break;
			case 181:
				transform.rotation = Quaternion.Euler(0, 180, 0);
				break;
			case 271:
				transform.rotation = Quaternion.Euler(0, 270, 0);
				break;
			case 1:
				transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			default:
				break;
			}
		} else if (direction == "right") {
			switch ((int)transform.localEulerAngles.y) {
			case 89:
				transform.rotation = Quaternion.Euler(0, 90, 0);
				break;
			case 179:
				transform.rotation = Quaternion.Euler(0, 180, 0);
				break;
			case 269:
				transform.rotation = Quaternion.Euler(0, 270, 0);
				break;
			case 359:
				transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			default:
				break;
			}
		}
	}

	void Rotate(string dir){
		if (dir == "left") {
			Swing (rotLeft);
		} else if(dir == "right") {
			Swing(rotRight);
		}
	}

	void checkAngle(){
		switch ((int)transform.localEulerAngles.y) {
		case 0:
			rotLeft = -90;
			rotRight = 90;
			isRotating = false;	
			break;
		case 90:
			rotLeft = 0;
			rotRight = -180;
			isRotating = false;
			break;			
		case 180:
			rotLeft = 90;
			rotRight = 270;
			isRotating = false;	
			break;
		case 270:
			rotLeft = 180;
			rotRight = -360;
			isRotating = false;	
			break;
		default:
			break;
		}
	}
	
	void Update (){

		if (!isRotating) {
			if (Input.GetKey (KeyCode.C)) {
				direction = "left";
				isRotating = true;
			} else if (Input.GetKey (KeyCode.V)) {
				direction = "right";
				isRotating = true;
			}
		} else if(isRotating) {
			Rotate(direction);
			FixAngle(direction);
			checkAngle();
		}
	}

}
