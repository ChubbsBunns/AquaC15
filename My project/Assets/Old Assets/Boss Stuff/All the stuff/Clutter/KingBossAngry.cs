using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingBossAngry : MonoBehaviour
{
    public void LoadNextKingBossScene()
    {
        SceneManager.LoadScene("5");
    }
}
