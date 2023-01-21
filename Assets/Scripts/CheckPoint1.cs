using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint1 : MonoBehaviour
{
    [SerializeField]
    private GameObject gates;
    [SerializeField]
    private UnityEngine.UI.Image countdown;

    const float startTime = 5;

    private float countdownTime;

    private float passedTime;
    public static string status;

    private AudioSource sound;

    void Start()
    {
        this.passedTime = 0;
        this.countdownTime = startTime;

        CheckPoint1.status = "In progress.";
        this.sound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        this.countdownTime -= Time.deltaTime;
        
        
        if(countdownTime < 0){
            FailCheckpoint();
        }
        else{

            this.passedTime += Time.deltaTime;

            GameStat.Checkpoint1Fill = countdown.fillAmount = countdownTime / startTime;
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        PassCheckpoint();
        if(GameMenu.SoundEnabled){
            this.sound.volume = GameMenu.SoundValue;
            this.sound.Play();
        }
    }

    private void PassCheckpoint(){
        gates.gameObject.SetActive(false);
        CheckPoint1.status = $"Passed time: {this.passedTime:F1} s. Bonus +50000";
        countdown.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        GameStat.PassCheckpoint1(true);
        GameStat.ScoreForCheckpoint();
    }
    private void FailCheckpoint(){
        CheckPoint1.status = "Failed.";
        GameStat.PassCheckpoint1(false);
        this.gameObject.SetActive(false);
    }

    
}
