using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Enemy_Player_Check : MonoBehaviour
{
    [SerializeField] Ground_Enemy_AI grnd_Enemy;
    // Start is called before the first frame update
    void Start()
    {
        grnd_Enemy = GetComponentInParent<Ground_Enemy_AI>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
                    Debug.Log("1") ;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected") ;
            grnd_Enemy.followEnabled = true;
            grnd_Enemy.testThing(other.transform);
                        Debug.Log("Set follow enabled to true") ;
            grnd_Enemy.SetTarget(other.transform);
                        Debug.Log("set target to player") ;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            grnd_Enemy.followEnabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
