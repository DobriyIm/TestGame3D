using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint3 : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image countdown;

    public static bool isActivated;


    const float startTime = 15;

    private float countdownTime;

    private float passedTime;
    public static string status;

    private AudioSource sound;

    void Start()
    {
        this.passedTime = 0;
        this.countdownTime = startTime;

        CheckPoint3.status = "In progress.";
        CheckPoint3.isActivated = false;

        this.sound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(CheckPoint3.isActivated == true){

            this.countdownTime -= Time.deltaTime;

            if(countdownTime < 0){
                FailCheckpoint();
            }
            else{
                this.passedTime += Time.deltaTime;
                GameStat.Checkpoint3Fill = countdown.fillAmount = countdownTime / startTime;
            }
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
        CheckPoint3.status = $"Passed time: {this.passedTime:F1} s Bonus +50000";
        GameStat.IsEnd = true;
        GameStat.PassCheckpoint3(true);
        GameStat.ScoreForCheckpoint();

        GameStat.UpdateBestScores();
        GameMenu.Show("Game finished.", "Again.");
        
        GameStat.SaveScore();
    }
    private void FailCheckpoint(){
        CheckPoint3.status = "Failed.";
        GameStat.PassCheckpoint3(false);
    }
}
