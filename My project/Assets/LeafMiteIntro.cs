using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMiteIntro : MonoBehaviour
{
    [SerializeField] private GameObject brightSpark;
    [SerializeField] private Transform[] placeToDropBrightSparks;
    [SerializeField] private Patrol_Object helicopter;
    // Update is called once per frame
    void DropBrightSparks()
    {
        foreach (Transform i in placeToDropBrightSparks)
        {
            Instantiate(brightSpark, i.position, Quaternion.identity);
        }
    }

    void flyToLeft()
    {
        helicopter.animChange(1);
    }

    void flyToRight()
    {
        helicopter.animChange(0);
    }
}
