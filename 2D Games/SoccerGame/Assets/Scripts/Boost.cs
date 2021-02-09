using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Boolean boostActive;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        boostActive = true;
    }

    //whenever a car collides with the boost we want to fill that car's boost level, then we want to hide the boost pad
    private void OnTriggerEnter2D(Collider2D other){
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        BlueCar blueCar = GameObject.FindObjectOfType<BlueCar>();
        RedCar redCar = GameObject.FindObjectOfType<RedCar>();
        if(boostActive){
            if(other.tag == "BlueCar"){
                blueCar.carBoostLevel = 10;
                StartCoroutine(hideBoostPad());
            }
            if(other.tag == "RedCar"){
                redCar.carBoostLevel = 10;
                StartCoroutine(hideBoostPad());
            }
        }
    }

    //This Coroutine disables the spriteRenderer for the boost pad that was ran over, 
    //and then waits for 5 seconds before setting the spriteRenderer active again
    public IEnumerator hideBoostPad(){
        spriteRenderer.enabled = false;
        boostActive = false;
        yield return new WaitForSeconds(5);
        spriteRenderer.enabled = true;
        boostActive = true;
    }
}

