using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackScreen : MonoBehaviour
{
    public string sceneName;
    public Animator blackScreenAnim;

    public GameObject retryButton;
    // Start is called before the first frame update
    void Start()
    {
        blackScreenAnim = GetComponent<Animator>();
        blackScreenAnim.SetBool("LoadSceneNow", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneLul()
    {
        Debug.Log("help");
        SceneManager.LoadScene(sceneName);
    }

    public void SetSceneName(string SceneNameToLoad)
    {
        sceneName = SceneNameToLoad;
    }

    public void StartLoadSceneAnimation()
    {
        Debug.Log("yay");
        blackScreenAnim.SetBool("LoadSceneNow", true);
    }

    public void RetryButton()
    {
        retryButton.SetActive(true);
    }

    public void FadeToBlackOni()
    {
        blackScreenAnim.SetBool("FadeToBlack", true);
    }
}
