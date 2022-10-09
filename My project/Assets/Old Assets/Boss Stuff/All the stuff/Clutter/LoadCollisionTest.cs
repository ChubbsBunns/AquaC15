using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCollisionTest : MonoBehaviour
{
    public Animator gateAnim;
    public Minotaur AngeryBoi;
    private void Start()
    {
        AngeryBoi = FindObjectOfType<Minotaur>();
        //     gateAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //          gateAnim.SetBool("GateClose", true);
            AngeryBoi.AngeryBoiAttack();
            Debug.Log("Yay");
        }
    }
}