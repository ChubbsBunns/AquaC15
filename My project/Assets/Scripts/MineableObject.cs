using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableObject : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;  //Object to spawn when mined like a coin
    [SerializeField] Sprite[] sprites;          //Sprites to change to when mining
    [SerializeField] float maxHorizontalForce;
    [SerializeField] float minHorizontalForce;
    [SerializeField] float maxVerticalForce;
    [SerializeField] float minVerticalForce;

    //Called by player when mining this object, to be ovewritten by other classes to perform different behaviours
    public virtual void Mine()  
    {
        //Change the sprite of the object
        Rigidbody2D rb = Instantiate<GameObject>(objectToSpawn, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        float sideForce = Random.Range(minHorizontalForce, maxHorizontalForce);
        float upForce = Random.Range(minVerticalForce, maxVerticalForce);
        rb.AddForce(new Vector2(sideForce, upForce), ForceMode2D.Force);
    }
}
