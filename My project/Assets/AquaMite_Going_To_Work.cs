using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaMite_Going_To_Work : MonoBehaviour
{
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Random.Range(0.0f, 3f);

    }

    IEnumerator WaitForSeconds(float timing)
    {
        yield return new WaitForSeconds(timing);

    }

    private void StartGlowing()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
