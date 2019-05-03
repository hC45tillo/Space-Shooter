using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool activateSpeedBoost = false;
    public bool activateShield = false;

    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private float fireRate = 0.25f;

    private float canFire = 0.0f;

    [SerializeField]
    private float speed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            this.Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > canFire)
        {
            if (canTripleShot)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            canFire = Time.time + fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float speedBoost = this.activateSpeedBoost ? 2.0f : 1;
        transform.Translate(Vector3.right * (speed * speedBoost) * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * (speed * speedBoost) * verticalInput * Time.deltaTime);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 12)
        {
            transform.position = new Vector3(-12, transform.position.y, 0);
        }
        else if (transform.position.x < -12)
        {
            transform.position = new Vector3(12, transform.position.y, 0);
        }
    }

    // 0 = tripleshoot; 1 = speed boost; 2 = shield;
    public void PowerupOn(int powerupID)
    {
        switch(powerupID)
        {
            case 0:
                canTripleShot = true;
                break;
            case 1:
                activateSpeedBoost = true;
                break;
            case 2:
                activateShield = true;
                break;
        }
        StartCoroutine(PowerupDown(powerupID));
    }

    // 0 = tripleshoot; 1 = speed boost; 2 = shield;
    public IEnumerator PowerupDown(int powerupID)
    {
        yield return new WaitForSeconds(5.0f);
        switch (powerupID)
        {
            case 0:
                canTripleShot = false;
                break;
            case 1:
                activateSpeedBoost = false;
                break;
            case 2:
                activateShield = false;
                break;
        }

    }
  
}
