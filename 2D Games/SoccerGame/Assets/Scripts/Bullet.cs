using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
     [SerializeField] float m_Speed;   
     [SerializeField] int damage;
 
     private Rigidbody2D rb;
 
     void Awake()
     {
         rb = GetComponent<Rigidbody2D>();
     }
     void Start()
     {
        rb = GetComponent<Rigidbody2D>();
     }

     //moves the bullet forward depending on the direction of the car
     void Update(){
        rb.velocity = rb.GetRelativeVector(Vector2.up * m_Speed);
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(gM.destroyAllBullets){
            Destroy(gameObject);
        }
     }

     //Depending on what the bullet collides with, it either destroys itself or decreases the health of the other player.
     //I also check for accidental friendly fire, and if the bullet is fired from the blue car it cannot damage the blue car.
     void OnCollisionEnter2D(Collision2D other){
        BlueCar blueCar = GameObject.FindObjectOfType<BlueCar>();
        RedCar redCar = GameObject.FindObjectOfType<RedCar>();
        if (other.gameObject.tag == "Ball"){
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall"){
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "BlueCar"){
            if(tag == "RedBullet"){
                blueCar.Health -= damage;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "RedCar"){
            if(tag == "BlueBullet"){
                redCar.Health -= damage;
            }
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "BlueBullet" || other.gameObject.tag == "RedBullet"){
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

}
