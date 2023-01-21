using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2 : MonoBehaviour
{
    [SerializeField]
    private GameObject gates;
    [SerializeField]
    private UnityEngine.UI.Image countdown;

    public static bool isActivated;

    const float startTime = 10;

    private float countdownTime;

    private float passedTime;
    public static string status;

    private AudioSource sound;

    void Start()
    {
        this.passedTime = 0;
        this.countdownTime = startTime;

        CheckPoint2.status = "In progress.";
        CheckPoint2.isActivated = false;

        this.sound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(CheckPoint2.isActivated == true){

            countdownTime -= Time.deltaTime;

            if(countdownTime < 0){
                FailCheckpoint();
            }
            else{
                this.passedTime += Time.deltaTime;
                GameStat.Checkpoint2Fill = countdown.fillAmount = countdownTime / startTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        this.PassCheckpoint();
        if(GameMenu.SoundEnabled){
            this.sound.volume = GameMenu.SoundValue;
            this.sound.Play();
        }
    }

    private void PassCheckpoint(){
        CheckPoint2.status = $"Passed time: {this.passedTime:F1} s Bonus +50000";

        gates.gameObject.SetActive(false);

        countdown.gameObject.SetActive(false);

        this.gameObject.SetActive(false);

        GameStat.PassCheckpoint2(true);
        GameStat.ScoreForCheckpoint();
    }
    private void FailCheckpoint(){
        CheckPoint2.status = "Failed.";
        GameStat.PassCheckpoint2(false);
        this.gameObject.SetActive(false);
    }
}
