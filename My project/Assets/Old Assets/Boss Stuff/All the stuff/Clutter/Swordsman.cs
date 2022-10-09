using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : MonoBehaviour
{
    public PlayerController_2 player;
    public SwordEnemy swordEnemy;

    public Animator animSwordEnemy;

    public int damageIDealToPlayer;

    public int attackIndex;

    [Header("(Attack 1) Sword Charge Attack")]
    public float maxDistanceOfSword = 1f;
    public float speedOfSword;
    public RaycastHit2D pointHit;
    public RaycastHit2D currentPointHit;
    public Vector3 pointToMoveTo;
    public bool attackSwordLargeAiming;
    public bool swordMoving = false;
    public LayerMask maskSwordHit;
    public bool SwordGoBack;

    public Transform SwordOriginalPosition;

    [Header("(Attack 2) Disappear and appear attack")]
    public Transform placeholderPosition;
    public GameObject poof;
    //wait for charge up value must be the same as the animation time for the poofAppearing
    public float WaitForChargeUp;
    public float WaitForAppearance;
    public GameObject poofAppearing;
    public PlayerAreaCheckMaster playerCheck;
    public Vector3 appearHere = new Vector3 (0,0,0);
    public float displacementValue;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController_2>();
        playerCheck = FindObjectOfType<PlayerAreaCheckMaster>();
        swordEnemy = FindObjectOfType<SwordEnemy>();
        animSwordEnemy = GetComponent<Animator>();
        attackIndex = Random.Range(1, 3);


    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (attackSwordLargeAiming)
        {
            Vector3 vectorDirectionToPlayer = player.transform.position - swordEnemy.transform.position;
            Debug.DrawRay(swordEnemy.transform.position, vectorDirectionToPlayer);
            Quaternion swordRotation = Quaternion.LookRotation(Vector3.forward, vectorDirectionToPlayer);
            swordEnemy.transform.rotation = swordRotation;
        }


    }

    private void FixedUpdate()
    {
        if (swordMoving)
        {
            swordEnemy.gameObject.transform.position = Vector2.MoveTowards(swordEnemy.gameObject.transform.position, pointToMoveTo, speedOfSword);
        }

        if (SwordGoBack)
        {
            swordEnemy.gameObject.transform.position = Vector2.MoveTowards(swordEnemy.gameObject.transform.position, SwordOriginalPosition.position, speedOfSword);
        }
    }

    public void StartAttacking()
    {
        animSwordEnemy.SetBool("StartAttacking", true);
    }

    public void AimSword()
    {
        attackSwordLargeAiming = true;
        Debug.Log("asgfagd");
    }

    public void StopAimSword()
    {
        attackSwordLargeAiming = false;
    }

    public void SwordBigCharge()
    {
        StopAimSword();
        if (!swordMoving)
        {
            pointHit = Physics2D.Raycast(swordEnemy.transform.position, player.transform.position - swordEnemy.transform.position, maxDistanceOfSword, maskSwordHit);
            pointToMoveTo = new Vector3 (pointHit.transform.position.x, pointHit.transform.position.y, pointHit.transform.position.z);
        }
        swordMoving = true;
    }

    public void SwordHasReturned()
    {
        animSwordEnemy.SetBool("SwordReturnNow", true);
    }


    public void ResetParameters()
    {
        SwordGoBack = false;
        animSwordEnemy.SetBool("SwordReturnNow", false);
        swordMoving = false;
    }

    public void GoToPlaceholderPosition()
    {
        Instantiate(poof, transform.position, Quaternion.identity);
        transform.position = placeholderPosition.position;
        StartCoroutine(StartTheAttack());
    }

    IEnumerator StartTheAttack()
    {
        playerCheck.GetTheData();
        Debug.Log(playerCheck.rightCheck);
        playerCheck.GetAttackIndex();
        if (playerCheck.DoIGoLeftOrRightIndex == 1)
        {
            appearHere = new Vector3(player.transform.position.x - displacementValue, player.transform.position.y, player.transform.position.z);
        }
        else
        {
            appearHere = new Vector3(player.transform.position.x + displacementValue, player.transform.position.y, player.transform.position.z);
        }
        Instantiate(poofAppearing, appearHere, Quaternion.identity);
        yield return new WaitForSeconds(WaitForChargeUp);
        AppearAttack();
    }

    public void AppearAttack()
    {
        transform.position = new Vector3 ( appearHere.x , appearHere.y + 1, appearHere.z);
        animSwordEnemy.SetBool("AppearAttack", true);
    }

    public void SetAppearAttackToFalse()
    {
        animSwordEnemy.SetBool("AppearAttack", false);

    }

    public void SetDisappearingAttackToFalse()
    {
        animSwordEnemy.SetBool("DisappearingAttack", false);
    }

    public void ChangeAttack()
    {
        if (attackIndex == 1)
        {
            animSwordEnemy.SetBool("SwordChargeAttack", true);
            animSwordEnemy.SetBool("DisappearingAttack", false);
            Debug.Log("this ones working");
            attackIndex = 2;
        }
        else if(attackIndex == 2)
        {
            animSwordEnemy.SetBool("DisappearingAttack", true);
            animSwordEnemy.SetBool("SwordChargeAttack", false);
            Debug.Log("this ones working 2");
            attackIndex = 1;
        }
    }

}
