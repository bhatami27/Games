using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlueCar : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float accelerateForce;
    [SerializeField] private float turningForce;
    [SerializeField] private float boostAccelerateForce;
    public float AccelerateForce{
        get{
            return accelerateForce;
        }
    }
    private float startAccelerateForce; 
    private float speed;
    private float turningAmount;
    private float direction;
    private Quaternion carStartDirection;
    private bool isCarBoosting;
    public float carBoostLevel;
    private float carBoostRate = 5;
    private Vector3 startPos;  
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI boostText;
    private int health;

    public int Health{
        get{
            return health;
        }
        set{
            this.health = value;
            healthText.text = "Health: " + value.ToString();
            if(health <= 0){
                this.health = 0;
                healthText.text = "Health: " + value.ToString();
                returnToStartPos();
            }
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        carStartDirection = transform.rotation;
        carBoostLevel = 0;
        isCarBoosting = false;
        startAccelerateForce = AccelerateForce;
        Health = 100;
    }
    public void FixedUpdate()
    {
        carController();
    }

    public void Update(){
        carBoost();
        boostText.text = "Boost: " + Mathf.CeilToInt(carBoostLevel).ToString();
    }

    //Alexander Zotov on Youtube is responsible for creating the movement in carController()
    //https://www.youtube.com/watch?v=Od_f6Y_OfYI&feature=emb_logo
    private void carController(){
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(gM.roundActive){
            turningAmount = - Input.GetAxis("2nd Player Horizontal");
            speed = Input.GetAxis("2nd Player Vertical") * accelerateForce;
            direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
            rb.rotation += turningAmount * turningForce * rb.velocity.magnitude * direction;
            rb.AddRelativeForce(Vector2.up * speed);
            rb.AddRelativeForce(- Vector2.right * rb.velocity.magnitude * turningAmount / 2);
        }
    }

    //carBoost() checks if the player is holding the left shift key and if they have boost available we set our normal acceleration to the 
    //boostAccelerateForce to increase our car speed.
    private void carBoost(){
        if(Input.GetKey("left shift") && carBoostLevel > 0){
            isCarBoosting = true;
            if(isCarBoosting == true){
                accelerateForce = boostAccelerateForce;
                carBoostLevel -= carBoostRate * Time.deltaTime;
            }
        }else{
            isCarBoosting = false;
            accelerateForce = startAccelerateForce;
        }
        if(carBoostLevel <= 0){
            carBoostLevel = 0;
            isCarBoosting = false;
        }
        
    }

    //returnToStartPos() is what handles the respawning for the blue car whenever it runs out of health or a goal is scored.
    public void returnToStartPos(){
        transform.position = startPos;
        transform.rotation = carStartDirection;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        isCarBoosting = false;
        carBoostLevel = 0;
        Health = 100;
    }
     
}
