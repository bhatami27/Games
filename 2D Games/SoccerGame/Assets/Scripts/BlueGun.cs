using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGun : MonoBehaviour
{
    [SerializeField] private GameObject m_Projectile;
    [SerializeField] private float fireRate;
    [SerializeField] private float lastBulletFired; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        //this is where we handle the automatic fire for the gun. 
        if(Input.GetButton("Jump")){
            if (Time.time - lastBulletFired > 1 / fireRate){
                GameObject currentBullet;
                lastBulletFired = Time.time;
                currentBullet = Instantiate(m_Projectile, transform.position, transform.rotation);
                currentBullet.tag = "BlueBullet";
            }
        }
    }
}