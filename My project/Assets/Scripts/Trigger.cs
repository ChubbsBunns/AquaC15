using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public QuestGiver questGiver;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.CompareTag("Player"))
        {
            questGiver.OpenQuestWindow();
            Debug.Log("Player has entered");
        }
        Debug.Log("This isnt a player");
    }
}
