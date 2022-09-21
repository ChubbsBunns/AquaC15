using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Trigger_Point : MonoBehaviour
{
    public int Trigger_Point_Index;
    public CreiddyIntro creiddy;
    private void Start()
    {
        creiddy = FindObjectOfType<CreiddyIntro>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Player"))
        {
            creiddy.Move_To_Next_Point(Trigger_Point_Index);
        }
    }

}
