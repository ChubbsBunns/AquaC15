using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamiteInstantiator : MonoBehaviour
{
    public GameObject[] AquaMites;
    public float TimeToInstantiate;
    public int WhichMite;

    
    // Start is called before the first frame update
    void Start()
    {

        
        //        int maxNumMites = AquaMites.Length();
        StartCoroutine(WaitForSeconds());
    }

    IEnumerator WaitForSeconds()
    {
        
        TimeToInstantiate = Random.Range(0.0f, 2f);
        Debug.Log(TimeToInstantiate);
        yield return new WaitForSeconds(TimeToInstantiate);
        InstantiateRunner();

    }

    void InstantiateRunner()
    {
        float maxNumMites = (float)AquaMites.Length;
        WhichMite = (int) Random.Range(0.0f, maxNumMites);
//        Debug.Log("WHICHMITE IS " + WhichMite);
        Instantiate(AquaMites[WhichMite], transform.position, transform.rotation);
//        Debug.Log("I HAVE PUT PUT A PERSON");
        StartCoroutine(WaitForSeconds());
    }
}
