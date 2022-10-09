using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePortal : MonoBehaviour
{
    public string nextSceneToLoad;
    public BlackScreen blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        blackScreen = FindObjectOfType<BlackScreen>();
        Debug.Log("asasdsdd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asd");
        if (collision.tag == "Player")
        {
            
            blackScreen.SetSceneName(nextSceneToLoad);
            blackScreen.StartLoadSceneAnimation();
        }
            
    }
}
