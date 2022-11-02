using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float wait;
    void Start()
    {
        StartCoroutine(destroyVFX());
    }

    IEnumerator destroyVFX()
    {
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }

}
