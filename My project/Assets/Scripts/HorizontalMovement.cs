using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField]
    public GameObject lightningImage;
    public bool lightning_alive;
    public Transform positionImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player has entered");
            Instantiate(lightningImage, positionImage.position, Quaternion.identity);
        }
        Debug.Log("This isnt a player");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
