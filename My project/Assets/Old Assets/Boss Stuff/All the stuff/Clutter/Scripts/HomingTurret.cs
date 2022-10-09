using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingTurret : MonoBehaviour
{
    public GameObject bullet;
    public int timeBetweenBulletInstantiation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator generateBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(timeBetweenBulletInstantiation);
        StartCoroutine(generateBullet());
    }
}
