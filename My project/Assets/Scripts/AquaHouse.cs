using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaHouse : MonoBehaviour
{
    float startTime;
    Animator thisHouse;
    // Start is called before the first frame update
    void Start()
    {
        
        startTime = Random.Range(0.0f, 3f);
        thisHouse = GetComponent<Animator>();
        //Debug.Log(startTime);
        StartCoroutine(WaitForSeconds(startTime));
    }

    IEnumerator WaitForSeconds(float timing)
    {
//        Debug.Log(timing);
        yield return new WaitForSeconds(timing);
        StartGlowing();
    }

    private void StartGlowing()
    {
        thisHouse.SetBool("StartGlowing", true);
    }
}
