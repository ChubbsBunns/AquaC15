using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastEffect : MonoBehaviour
{
    public int damageBlastDealsToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController_2 playerGameObject = collision.gameObject.GetComponent<PlayerController_2>();
            playerGameObject.TakeDamage(damageBlastDealsToPlayer);
        }
    }
}
