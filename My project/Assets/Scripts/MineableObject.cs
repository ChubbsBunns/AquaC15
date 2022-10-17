using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableObject : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;          //Object to spawn when mined like a coin
    [SerializeField] Sprite[] sprites;                  //Sprites to change to when mining
    [SerializeField] Sprite[] glowSprites;              //GlowSprites to change to when mining
    [SerializeField] float maxHorizontalForce;
    [SerializeField] float minHorizontalForce;
    [SerializeField] float maxVerticalForce;
    [SerializeField] float minVerticalForce;
    [SerializeField] int numItemPerMine = 1;            //Number of items that 
    [SerializeField] SpriteRenderer spriteRenderer;     //Sprite renderer for mine graphic
    [SerializeField] SpriteRenderer glowSpriteRenderer; //Sprite renderer for glow of mine graphic

    int spriteIndex = 0;

    //Called by player when mining this object, to be ovewritten by other classes to perform different behaviours
    public virtual void Mine()  
    {
        
        for (int i = 0; i < numItemPerMine; ++i)
        {
            Rigidbody2D rb = Instantiate<GameObject>(objectToSpawn, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            float sideForce = Random.Range(minHorizontalForce, maxHorizontalForce);
            float upForce = Random.Range(minVerticalForce, maxVerticalForce);
            rb.AddForce(new Vector2(sideForce, upForce), ForceMode2D.Force);
        }

        //Change the sprite of the object
        ++spriteIndex;
        if(spriteIndex >= sprites.Length)
        {
            Destroy(this.gameObject);
        }
        else
        {
            spriteRenderer.sprite = sprites[spriteIndex];
            glowSpriteRenderer.sprite = glowSprites[spriteIndex];
        }
    }
}
