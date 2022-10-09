using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptForCollision : MonoBehaviour
{
    public Animator gateAnim;
    public Swordsman swordman;
    private void Start()
    {
        swordman = FindObjectOfType<Swordsman>();
   //     gateAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
  //          gateAnim.SetBool("GateClose", true);
            swordman.StartAttacking();
            Debug.Log("Yay");
        }
    }
}
