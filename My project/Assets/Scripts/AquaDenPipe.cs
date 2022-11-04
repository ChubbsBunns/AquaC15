using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaDenPipe : MonoBehaviour
{
    float timeBetweenAscends;
    public float lower_bound = 2f;
    public float upper_bound= 4f;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(WaitThenSpawn());
    }

    IEnumerator WaitThenSpawn()
    {
        timeBetweenAscends = Random.Range(lower_bound, upper_bound); 
        yield return new WaitForSeconds(timeBetweenAscends);
        anim.SetBool("AquamiteAscend" , true);
    }

    void SetAscendToFalse()
    {
        anim.SetBool("AquamiteAscend" ,false);
        StartCoroutine(WaitThenSpawn());
    }
}
