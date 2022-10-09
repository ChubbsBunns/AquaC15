using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNameHolder : MonoBehaviour
{
    public string currentSceneName;
    public BlackScreen blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        blackScreen = FindObjectOfType<BlackScreen>();
        blackScreen.SetSceneName(currentSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
