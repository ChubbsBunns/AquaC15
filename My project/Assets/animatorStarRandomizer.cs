using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorStarRandomizer : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(WaitForRandomSeconds());
    }

    IEnumerator WaitForRandomSeconds()
    {
        float num = Random.Range(0.3f, 4.0f);
        yield return new WaitForSeconds(num);
        anim.enabled = true;
    }
}
