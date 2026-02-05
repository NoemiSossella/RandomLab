using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTaps;
    public bool allowButtonHold;
    private int bulletsLeft, bulletsShots;

    //bools
    private bool shooting, readyToShoot, reloading;

    //reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Inpunt.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.foward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            if (rayHit.collider.CompareTag("Enemy")
            {
                rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
            }
        }
        
        bulletsLeft--;
        invoke("ResetShot", timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {

    }
}
