using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int leftPlayerScore;
    private int rightPlayerScore;
    [SerializeField] private int roundTime;
    private float startTime = 0;
    public bool roundActive = false;
    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;
    [SerializeField] private TextMeshProUGUI roundTimeText;
    [SerializeField] private GameObject leftWinText;
    [SerializeField] private GameObject rightWinText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject tiedGameText;
    [SerializeField] private GameObject endGameBackground;
    public bool destroyAllBullets;
    public int LeftPlayerScore{
        get{
            return leftPlayerScore;
        }
        set{
            
            this.leftPlayerScore = value;
            this.leftScoreText.text = (value/2).ToString();
        }
    }
    public int RightPlayerScore{
        get{
            return rightPlayerScore;
        }
        set{
            
            this.rightPlayerScore = value;
            ;
            this.rightScoreText.text = (value/2).ToString();
        }
    }

    
    void Start(){
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        startTime = Time.time;
        setTimeDisplay(roundTime);
        roundActive = true;
        leftWinText.SetActive(false);
        rightWinText.SetActive(false);
        restartButton.SetActive(false);
        tiedGameText.SetActive(false);
        endGameBackground.SetActive(false);
        destroyAllBullets = false;

    }

    //Alex Hart on Youtube is responsible for creating the if statement involving the time, 
    //but not setting the win conditions or tie active depending on the score
    //https://www.youtube.com/watch?v=CYNSY7U1kIM
    void Update(){
        if(Time.time - startTime < roundTime){
            float timeElapsed = Time.time - startTime;
            setTimeDisplay(roundTime - timeElapsed);
        }
        else{
            setTimeDisplay(0);
            if(LeftPlayerScore > RightPlayerScore){
                leftWinText.SetActive(true);
                endGameBackground.SetActive(true);
            }
            if(RightPlayerScore > LeftPlayerScore){
                rightWinText.SetActive(true);
                endGameBackground.SetActive(true);
            }
            if(LeftPlayerScore == RightPlayerScore){
                tiedGameText.SetActive(true);
                endGameBackground.SetActive(true);
            }
            roundActive = false;
            restartButton.SetActive(true);
        }
        Debug.Log(destroyAllBullets);
    }

    //Alex Hart on Youtube is responsible for creating the setTimeDisplay()
    //https://www.youtube.com/watch?v=CYNSY7U1kIM
    private void setTimeDisplay(float timeDisplay){
        roundTimeText.text = roundTimeDisplay(timeDisplay);
    }

    //Alex Hart on Youtube is responsible for creating the roundTimeDisplay()
    //https://www.youtube.com/watch?v=CYNSY7U1kIM
    private string roundTimeDisplay(float time){
        int secondsToShow = Mathf.CeilToInt(time);
        int seconds = secondsToShow % 60;
        string secondsDisplay = "0";
        if(seconds < 10){
            secondsDisplay = "0" + seconds.ToString();
        }else{
             secondsDisplay = seconds.ToString();
        }
        int minutes = (secondsToShow - seconds) / 60;
        return minutes.ToString() + " : " + secondsDisplay;
    }

    //Increases the score depending on which goal the ball hit.
    public IEnumerator IncreaseScore(string goal){
        BlueCar blueCar = GameObject.FindObjectOfType<BlueCar>();
        RedCar redCar = GameObject.FindObjectOfType<RedCar>();
        Ball ball = GameObject.FindObjectOfType<Ball>();
        if(roundActive){
            if(goal.Equals("left")){
                LeftPlayerScore = leftPlayerScore + 1;
                destroyAllBullets = true;
                Debug.Log("left scored a point");
            }
            if(goal.Equals("right")){
                RightPlayerScore = rightPlayerScore + 1;
                destroyAllBullets = true;
                Debug.Log("right scored a point");
            }
            blueCar.returnToStartPos();
            redCar.returnToStartPos();
            ball.returnToStartPos();
        }
        yield return new WaitForSeconds(0.5f);
        destroyAllBullets = false;
    }

    public void NewGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
