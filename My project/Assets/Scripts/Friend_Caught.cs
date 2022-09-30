using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Friend_Caught : MonoBehaviour
{
    public Friend_AI ai;
    public Tilemap map;
    public Transform[] door;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ai.Caught();
            Debug.Log("You Caught Me");
            foreach(var t in door)
            {
                map.SetTile(map.WorldToCell(t.position), null);
            }
            //Do something to open the way
        }
    }
}
