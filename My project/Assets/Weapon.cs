using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Ground_Enemy_AI ai;
    public GameObject laserPrefab;
    public bool isLeft = true;
    public bool shouldRaycast = false;

    // Update is called once per frame
    void Update()
    {
        Flip();
        if (shouldRaycast) {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
            if (hitInfo)
            {
                Player_Health player = hitInfo.transform.GetComponent<Player_Health>();
                if (player != null)
                {
                    Shoot();
                }
            }
        }
    }

    void Flip()
    {
        if (!ai.facingLeft && isLeft) {
            firePoint.Rotate(0f, 180f, 0f);
            isLeft = false;
        } else if (ai.facingLeft && !isLeft) {
            firePoint.Rotate(0f, 180f, 0f);
            isLeft = true;
        }
    }

    void Shoot()
    {
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
    }
}
