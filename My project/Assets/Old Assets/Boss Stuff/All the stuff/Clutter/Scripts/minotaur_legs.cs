using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minotaur_legs : MonoBehaviour
{
    public Transform poof_effect_location;
    public GameObject poof_effect;
    public Transform minotaur_transform;
    public bool left;
    public GameObject shockwave;
    public float shockwave_speed;
    public int left_or_right;
    public shockwave current_shockwave;
    // Start is called before the first frame update
    public void InstantiatePoofHere()
    {
        //check player's position relative to minotaur
        PlayerController_2 player = FindObjectOfType<PlayerController_2>();
        if (player.transform.position.x > minotaur_transform.position.x)
        {
            left = true;
        }
        else
        {
            left = false;
        }
        //create shockwave
        Instantiate(shockwave, poof_effect_location.position, Quaternion.identity);
        current_shockwave = FindObjectOfType<shockwave>();
        current_shockwave.move(left);
        Instantiate(poof_effect, poof_effect_location.position, Quaternion.identity);
        Debug.Log("Help la");
    }
}
