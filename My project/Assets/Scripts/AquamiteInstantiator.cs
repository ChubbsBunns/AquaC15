using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamiteInstantiator : MonoBehaviour
{
    public AquaMite_Going_To_Work[] AquaMites;
    public float TimeToInstantiate;
    public int WhichMite;
    // Start is called before the first frame update
    void Start()
    {

        TimeToInstantiate = Random.Range(0.0f, 2f);
        //        int maxNumMites = AquaMites.Length();
        //        /WhichMite = Random.Range(0.0f, maxNumMites);
        StartCoroutine(WaitForSeconds(TimeToInstantiate));
    }

    IEnumerator WaitForSeconds(float timing)
    {
        Debug.Log(timing);
        yield return new WaitForSeconds(timing);

    }
}
