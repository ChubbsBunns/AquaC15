using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSparks : MonoBehaviour
{

    [SerializeField] private GameObject spark;
    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;

    void generateSparks()
    {
        Instantiate(spark, position1.position, Quaternion.identity);
        Instantiate(spark, position2.position, Quaternion.identity);
    }
}
