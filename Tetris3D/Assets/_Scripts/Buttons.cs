using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

    public int scene;
    public static bool clicked = false;

    void OnMouseOver()
    {
        gameObject.renderer.material.color = Color.red;
    }

    void OnMouseExit()
    {
        gameObject.renderer.material.color = Color.green;
    }

    void OnMouseDown()
    {
        clicked = true;
        StartCoroutine(Timer());
    }

    void Movimentation()
    {
        if (!clicked)
        {
            transform.Rotate(1, 0, 0);
        }
        if (clicked)
        {
            transform.Rotate(-10 , 0, 0);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        Application.LoadLevel(scene);
        StartCoroutine(Timer());
    }
	
	void Update () 
    {
        Movimentation();    
	}
}
