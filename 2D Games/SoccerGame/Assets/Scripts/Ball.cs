using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    //Checks if the round is over, and if that is true, 
    //then we return the ball back to the starting position so it cannot accidentally score a goal

    void Update()
    {
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(!gM.roundActive){
            returnToStartPos();
        }
    }

    //Whenever the ball collides with either the left or right goal we want to increase the score
    private void OnTriggerEnter2D(Collider2D other){
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(other.tag == "LeftGoal"){
            StartCoroutine(gM.IncreaseScore("right"));
        }
        if(other.tag == "RightGoal"){
            StartCoroutine(gM.IncreaseScore("left"));
        }
    }

    //returns the ball back to the starting position
    public void returnToStartPos(){
        transform.position = startPos;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
    
}
