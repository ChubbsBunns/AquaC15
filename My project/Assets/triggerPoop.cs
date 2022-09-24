using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPoop : MonoBehaviour
{
    [SerializeField] CircleMovement c;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            c.move = true;
        }
        Debug.Log("This isnt a player");
    }
}
