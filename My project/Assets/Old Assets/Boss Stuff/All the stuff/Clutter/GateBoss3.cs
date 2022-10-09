using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBoss3 : MonoBehaviour
{
    public Animator gateAnim;
    public BossKing bossKing;
    public Animator bossKingAnim;

    // Start is called before the first frame update
    void Start()
    {
        gateAnim = GetComponent<Animator>();
        bossKing = FindObjectOfType<BossKing>();
        bossKingAnim = bossKing.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        gateAnim.SetBool("GateClose", true);
        bossKingAnim.SetBool("Start", true);
    }
}
