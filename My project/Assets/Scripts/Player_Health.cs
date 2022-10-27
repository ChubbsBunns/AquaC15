using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [Header("Behaviour Variables")]     
    [SerializeField] int hearts = 3;                //How many hearts the player starts with
    [SerializeField] float hitImmunityTime;         //How long the player is immune after taking damage
    [SerializeField] float onHitShakeIntensity;
    [SerializeField] float onHitShakeDuration;

    [Header("UI Variables")]
    [SerializeField] Sprite healthyHeart;
    [SerializeField] Sprite damagedHeart;
    [SerializeField] GameObject heartsUIParent;     //The parent of the heart images, use of horizontal layout group for equal spacing

    List<Image> heartImages = new List<Image>();    
    int currentHeartIndex;                          //The currently healthy heart that is to be damaged next
    bool hitImmunity = false;                       //Disables taking damage after recently taking damage
    private void Start()
    {
        //Spawn hearts and 
        for(int i = 0; i < hearts; ++i)
        {
            GameObject newHeart = new GameObject("Heart" + i.ToString());
            newHeart.transform.parent = heartsUIParent.transform;
            heartImages.Add(newHeart.AddComponent<Image>());
            heartImages[i].sprite = healthyHeart;
            heartImages[i].preserveAspect = true;
        }
        currentHeartIndex = hearts - 1;
    }

    //Called by whatever damages the player
    //Removes one heart and starts hit immunity
    public void TakeDamage()
    {
        if(currentHeartIndex < 0 || hitImmunity) { return; }
        CinemachineCameraShake.instance.ShakeCamera(onHitShakeIntensity, onHitShakeDuration);
        heartImages[currentHeartIndex].sprite = damagedHeart;
        --currentHeartIndex;
        if(currentHeartIndex < 0)
        {
            //Respawn
            //Reset player position and stuff
            RestoreHearts(100);
            return;
        }
        hitImmunity = true;
        Invoke(nameof(StopImmunity), hitImmunityTime);
    }

    //Called when respawning to restore all the hearts
    public void RestoreHearts(int num)
    {
        while(num-- > 0 && currentHeartIndex < heartImages.Count - 1)
        {
            ++currentHeartIndex;
            heartImages[currentHeartIndex].sprite = healthyHeart;
        }
    }

    //To be called after a time using Invoke to disable hit immunity
    void StopImmunity()
    {
        hitImmunity = false;
    }


}
