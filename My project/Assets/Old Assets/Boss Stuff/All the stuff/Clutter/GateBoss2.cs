using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBoss2 : MonoBehaviour
{
    public Animator gateAnim;
    public Swordsman swordman;


    // Start is called before the first frame update
    void Start()
    {
        swordman = FindObjectOfType<Swordsman>();
        gateAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TESTEST");
        gateAnim.SetBool("GateClose", true);
        swordman.StartAttacking();
    }

}
