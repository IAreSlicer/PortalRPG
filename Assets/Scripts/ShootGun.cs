using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    public float bulletSpeed = 30;
    public float lifeTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Fire();
        }
    }

    private void Fire() 
    {
        GameObject bullet = Instantiate(bulletPrefab);

        bullet.transform.position = bulletSpawn.position;

        Vector3 rotation = bulletSpawn.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x + 90, transform.eulerAngles.y, rotation.z + 90);

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(bullet);
    }
}
