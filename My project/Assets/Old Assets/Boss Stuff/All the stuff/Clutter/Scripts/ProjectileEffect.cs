using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    public float projectileEffectLifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeDelayTillDeath());    }

    IEnumerator TimeDelayTillDeath()
    {
        yield return new WaitForSeconds(projectileEffectLifeSpan);
        Destroy(gameObject);
    }
}
