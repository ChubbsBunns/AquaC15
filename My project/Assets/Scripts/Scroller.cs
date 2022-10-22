using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public float Speed;

    public bool startAscending;
    private float offset;
    [SerializeField] private  Material mat;



    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }


    void Update()
    {
        if (startAscending)
        {
            offset += (Time.deltaTime * Speed);
            Debug.Log(offset);
            mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
            Debug.Log(mat.GetTextureOffset("_MainTex"));
        }
    }
}