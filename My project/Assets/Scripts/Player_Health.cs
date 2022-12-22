using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Player Sprite Variables")]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material flashRedMaterial;
    [SerializeField] float timeFlashRed;
    [SerializeField] float immunityOpacity;

    Color color;
    List<Image> heartImages = new List<Image>();
    SpriteRenderer playerSpriteRenderer;
    int currentHeartIndex;                          //The currently healthy heart that is to be damaged next
    bool hitImmunity = false;                       //Disables taking damage after recently taking damage
    private void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        Material newMaterial = new Material(defaultMaterial);
        playerSpriteRenderer.material = newMaterial;
        defaultMaterial = newMaterial;
        color = defaultMaterial.color;
        //Spawn hearts
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

    public void SwitchOffHearts()
    {
        heartsUIParent.SetActive(false);
    }

    public void SwitchOnHearts()
    {
        heartsUIParent.SetActive(true);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            RestoreHearts(100);
            return;
        }
        StartCoroutine(FlashRed());
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

    IEnumerator FlashRed()
    {
        hitImmunity = true;
        playerSpriteRenderer.material = flashRedMaterial;
        yield return new WaitForSeconds(timeFlashRed);
        playerSpriteRenderer.material = defaultMaterial;
        color.a = immunityOpacity;
        defaultMaterial.color = color;
        yield return new WaitForSeconds(hitImmunityTime);
        color.a = 1;
        defaultMaterial.color = color;
        hitImmunity = false;
    }
}
