using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInZoomOutColliders : MonoBehaviour
{
    CutSceneManager CutSceneMng;

    public bool ZoomIn;
    public bool ZoomOut;
    // Start is called before the first frame update
    void Start()
    {
        if (ZoomIn && ZoomOut)
        {
            Debug.LogError("This ZoomInZoomOutCollider at position" + transform.position + "has both ZoomInAndOut true, please resolve conflict");
        }
        else if (ZoomIn != true && ZoomOut != true)
        {
           Debug.LogError("This ZoomInZoomOutCollider at position" + transform.position + "has both ZoomInAndOut false, please resolve conflict");
        }
        CutSceneMng = FindObjectOfType<CutSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I sense player");
            if (ZoomIn)
            {
                Debug.Log("Zoom In");
                CutSceneMng.ZoomInNow = true;
            }
            else if (ZoomOut)
            {
                Debug.Log("Zoom Out");
                CutSceneMng.ZoomOutNow = true;
            }
        }        
    }
}

