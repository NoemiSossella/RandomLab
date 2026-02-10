using System.Linq.Expressions;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    // gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTaps;
    public bool allowButtonHold;
    private int bulletsLeft;
    private int bulletsShots;

    // bools
    private bool shooting;
    private bool readyToShoot;
    private bool reloading;

    // reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        // Aggiorna il testo delle munizioni se collegato
        if (ammoText != null)
        {
            ammoText.text = bulletsLeft + " / " + magazineSize;
        }
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetMouseButton(0);
        else shooting = Input.GetMouseButtonDown(0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        // shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShots = bulletsPerTaps;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        // RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            if (rayHit.collider.CompareTag("Enemy"))
            {
                // Esempio: se l'enemy ha un componente che gestisce il danno
                // var ai = rayHit.collider.GetComponent<ShootingAi>();
                // if (ai != null) ai.TakeDamage(damage);
            }
        }

        bulletsLeft--;
        bulletsShots--;

        // Permette il prossimo sparo dopo timeBetweenShooting
        Invoke(nameof(ResetShot), timeBetweenShooting);

        // Se ci sono più proiettili per tap, richiama Shoot dopo timeBetweenShots
        if (bulletsShots > 0 && bulletsLeft > 0)
        {
            Invoke(nameof(Shoot), timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        readyToShoot = true;
    }
}
