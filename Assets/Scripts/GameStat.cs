using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class GameStat : MonoBehaviour
{
    private static bool isEnd;
    public static bool IsEnd{
        get => isEnd;
        set{
            isEnd = value;
        }
    }

    public static void RestartGame(){

        GameStat.UpdateBestScores();
        
        GameTime = 0;
        score = 1000;

        CheckPoint1.status = "In progress.";
        CheckPoint2.status = "In progress.";
        CheckPoint3.status = "In progress.";

        IsEnd = false;

        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    #region Clock
    private static TMPro.TextMeshProUGUI Clock;
    private static float _gameTime;
    public static float GameTime {
        get => _gameTime;
        set{
            _gameTime = value;
            UpdateTime();
        }
    }

    private static void UpdateTime(){
        int t = (int)_gameTime;

        Clock.text = $"{t / 3600 % 24:00}:{t / 60 % 60:00}:{t % 60:00}.{(int)((_gameTime - t) * 10):0}";

    }
    #endregion
    #region Score

    public static float []bestScores = new float[3];
    public static float score;
    private const string scoreFileName = "Score.json";
    private void UpdateScore(){
        score -= (Time.deltaTime * 8f);
    }
    public static void ScoreForCheckpoint(){
        score += 50;
    }
    public static void UpdateBestScores(){
        for(int i = 0; i < 3; i++){
            if((int)(GameStat.score * 100) > GameStat.bestScores[i]){
                if(i + 1 <= 2)
                    GameStat.bestScores[i+1] = GameStat.bestScores[i];
                GameStat.bestScores[i] = (int)(score * 100);
                break;
            }
        }
    }

    public static void SaveScore(){
        string str = $"{{ \"1st\": \"{GameStat.bestScores[0]}\", \"2nd\": \"{GameStat.bestScores[1]}\", \"3rd\": \"{GameStat.bestScores[2]}\" }}";

        System.IO.File.WriteAllText(scoreFileName, str);
    }
    public static void LoadScore(){
        if(System.IO.File.Exists(scoreFileName)){
                string str = System.IO.File.ReadAllText(scoreFileName);
                JObject json = JObject.Parse(str);

                GameStat.bestScores[0] = json["1st"].Value<float>();
                GameStat.bestScores[1] = json["2nd"].Value<float>();
                GameStat.bestScores[2] = json["3rd"].Value<float>();
        }
        else{
            for(int i = 0; i < 3; i++)
                GameStat.bestScores[i] = 0;
        }
    }
    #endregion
    #region CheckPoint1

    private static UnityEngine.UI.Image imageCheckpoint1;
    private static float _checkpoint1Fill;
    public static float Checkpoint1Fill{
        get => _checkpoint1Fill;
        set{
            _checkpoint1Fill = value;
            UpdateCheckpoint1();
        }
    }
    private static void UpdateCheckpoint1()
    {
        if(Checkpoint1Fill >= 0 && Checkpoint1Fill <= 1){
            imageCheckpoint1.fillAmount = Checkpoint1Fill;
        }
    }
    public static void PassCheckpoint1(bool status){
        Checkpoint1Fill = 1;
        imageCheckpoint1.color = status ? Color.green : Color.red;
    }

    #endregion
    #region CheckPoint2

    private static UnityEngine.UI.Image imageCheckpoint2;
    private static float _checkpoint2Fill;
    public static float Checkpoint2Fill{
        get => _checkpoint2Fill;
        set{
            _checkpoint2Fill = value;
            UpdateCheckpoint2();
        }
    }
    private static void UpdateCheckpoint2()
    {
        if(Checkpoint2Fill >= 0 && Checkpoint2Fill <= 1){
            imageCheckpoint2.fillAmount = Checkpoint2Fill;
        }
    }
    public static void PassCheckpoint2(bool status){
        Checkpoint2Fill = 1;
        imageCheckpoint2.color = status ? Color.green : Color.red;
    }

    #endregion
    #region CheckPoint3

    private static UnityEngine.UI.Image imageCheckpoint3;
    private static float _checkpoint3Fill;
    public static float Checkpoint3Fill{
        get => _checkpoint3Fill;
        set{
            _checkpoint3Fill = value;
            UpdateCheckpoint3();
        }
    }
    private static void UpdateCheckpoint3()
    {
        if(Checkpoint3Fill >= 0 && Checkpoint3Fill <= 1){
            imageCheckpoint3.fillAmount = Checkpoint3Fill;
        }
    }
    public static void PassCheckpoint3(bool status){
        Checkpoint3Fill = 1;
        imageCheckpoint3.color = status ? Color.green : Color.red;
    }


    #endregion

    void Start()
    {
        score = 1000;

        GameStat.isEnd = false;

        GameStat.Clock = GameObject.Find("Clock").GetComponent<TMPro.TextMeshProUGUI>();
        GameStat.imageCheckpoint1 = GameObject.Find("ImageCheckpoint1").GetComponent<UnityEngine.UI.Image>();
        GameStat.imageCheckpoint2 = GameObject.Find("ImageCheckpoint2").GetComponent<UnityEngine.UI.Image>();
        GameStat.imageCheckpoint3 = GameObject.Find("ImageCheckpoint3").GetComponent<UnityEngine.UI.Image>();

        
    }

    private void LateUpdate() {
        GameStat.GameTime += Time.deltaTime;
        UpdateScore();
    }
    
}
