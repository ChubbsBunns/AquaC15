using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public bool one;
    public bool two;
    public int yay;
    // Start is called before the first frame update
    void Start()
    {
        if (one && two)
        {
            yay = Random.Range(1, 3);
        }
        else if (one == true && two == false)
        {
            yay = 1;
        }
        else if (one == false && two == true)
        {
            yay = 2;
        }
        else
        {
            Debug.Log("pikapisucka");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
