using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AquaMiteColourController : MonoBehaviour
{
    SpriteRenderer sr;

    public int[] color_R_variance;
    public int whichAquaMiteSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        whichAquaMiteSprite = (int)Random.Range(0.0f, color_R_variance.Length);
        //whichAquaMiteSprite = (int)Random.Range(0.0f, 255.0f);
        sr.color= new Color(0.0f, (float)color_R_variance[whichAquaMiteSprite], 255.0f, 255.0f) ;

        Debug.Log("WhichAquaMiteSprite Colour" + whichAquaMiteSprite);

        
        Debug.Log(sr.color);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
