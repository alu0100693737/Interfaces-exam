using UnityEngine;
using System.Collections;

public class change : MonoBehaviour {
    public int contador = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (contador == 0)
            {
                this.transform.position = new Vector3(-7.7f, 2f, 2.6f);
                contador++;
            }
            else if(contador == 1)
            {
                transform.position = new Vector3(-10f, 2f, 0f);
                contador++;
            }
            else if(contador ==2)
            {
                transform.position = new Vector3(-7.7f, 2f, -2.7f);
                contador++;
            }
            else if(contador == 3)
            {
                transform.position = new Vector3(-5f, 2f, 0f);
                contador = 0;
            }

        }
	
	}
}
