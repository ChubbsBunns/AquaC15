using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
  //  public Animator gateAnim;
    public Minotaur minotaurAnim;
    // Start is called before the first frame update
    void Start()
    {
        minotaurAnim = FindObjectOfType<Minotaur>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // gateAnim.SetBool("GateClose", true);
        minotaurAnim.AngeryBoiAttack();
    }
}
