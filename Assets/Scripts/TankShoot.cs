using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    public GameObject Bullet;
    
    public Rigidbody bullet;
    public Transform FireStart;
    private Transform Artillery;
    // Start is called before the first frame update
    void Start()
    {
        Artillery = FireStart.parent;

    }

    public void Shoot()
    {
        Rigidbody rbBullet = Instantiate(bullet, FireStart.position, Artillery.rotation);
        rbBullet.velocity = 100.0f * Artillery.forward;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    
    }
}
