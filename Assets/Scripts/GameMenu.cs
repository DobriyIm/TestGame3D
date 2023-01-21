using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class GameMenu : MonoBehaviour
{

    public static bool SoundEnabled {get; private set;}
    public static float SoundValue {get; private set;}

    private static GameObject MenuContent;
    private static TMPro.TextMeshProUGUI MenuMessage;
    private static TMPro.TextMeshProUGUI MenuButtonTitle;
    private static TMPro.TextMeshProUGUI MenuStatistic;

    private static UnityEngine.UI.Button mainButton;
    private static UnityEngine.UI.Button restartButton;

    private AudioSource music;

    private bool musicEnabled;
    private float musicValue;

    private const string preferenceFilename = "Pref.json";


    void Start()
    {
        GameStat.LoadScore();

        MenuContent = GameObject.Find(nameof(MenuContent));
        MenuMessage = GameObject.Find(nameof(MenuMessage)).GetComponent<TMPro.TextMeshProUGUI>();
        MenuButtonTitle = GameObject.Find(nameof(MenuButtonTitle)).GetComponent<TMPro.TextMeshProUGUI>();
        MenuStatistic = GameObject.Find(nameof(MenuStatistic)).GetComponent<TMPro.TextMeshProUGUI>();
        mainButton = GameObject.Find("MenuButton").GetComponent<UnityEngine.UI.Button>();
        restartButton = GameObject.Find("RestartButton").GetComponent<UnityEngine.UI.Button>();

        this.music = this.GetComponent<AudioSource>();

        if(MenuContent.activeInHierarchy) GameMenu.Show(MenuMessage.text, MenuButtonTitle.text);


        var SoundToggle = GameObject.Find("SoundEnabled").GetComponent<UnityEngine.UI.Toggle>();
        var SoundSlider= GameObject.Find("SoundVolume").GetComponent<UnityEngine.UI.Slider>();

        var MusicToggle = GameObject.Find("MusicEnabled").GetComponent<UnityEngine.UI.Toggle>();
        var MusicSlider= GameObject.Find("MusicVolume").GetComponent<UnityEngine.UI.Slider>();

        if(this.LoadPreference()){
            SoundToggle.isOn = SoundEnabled;
            SoundSlider.value = SoundValue;

            MusicToggle.isOn = musicEnabled;
            MusicSlider.value = musicValue;

        }
        else{
            SoundEnabled = SoundToggle.isOn;
            SoundValue = SoundSlider.value;

            musicEnabled = MusicToggle.isOn;
            musicValue = MusicSlider.value;
        }

    }

    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!GameStat.IsEnd){
                if(MenuContent.activeInHierarchy) GameMenu.Hide();
                else GameMenu.Show();
            }
        }
    }

    private void OnDestroy() {
        this.SavePreference();
    }

    #region EventHandler

    public void MenuButtonClick(){
        GameMenu.Hide();
    }

    #region  Music
    public void MusicToogleChanged(bool value){
        musicEnabled = value;
        UpdateMusicState();
    }
    public void MusicValueChanged(float value){
        musicValue = value;
        UpdateMusicState();
    }
    private void UpdateMusicState(){
        this.music.volume = musicValue;
        if(musicEnabled){
            if(!this.music.isPlaying)
                this.music.Play();
        }
        else this.music.Pause();
    }
    #endregion
    #region  Sound
    public void SoundToogleChanged(bool value){
        SoundEnabled = value;
    }
    public void SounValueChanged(float value){
        SoundValue = value;
    }
    #endregion
    #region  PrefSaving
    private void SavePreference(){
        string str = $"{{ \"music\":\"{musicEnabled}\"," + 
        $" \"musicVolume\":\"{musicValue}\"," +
        $" \"sound\":\"{SoundEnabled}\"," + 
        $" \"soundVolume\":\"{SoundValue}\"}}";

        System.IO.File.WriteAllText(preferenceFilename, str);
    }
    private bool LoadPreference(){
        if(System.IO.File.Exists(preferenceFilename)){
            try{
                string str = System.IO.File.ReadAllText(preferenceFilename);
                JObject json = JObject.Parse(str);

                musicEnabled = json["music"].Value<bool>();
                musicValue = json["musicVolume"].Value<float>();
                if(musicValue > 1) musicValue /= 10000000;

                SoundEnabled = json["sound"].Value<bool>();
                SoundValue = json["soundVolume"].Value<float>();
                if(SoundValue > 1) SoundValue /= 10000000;
                return true;
            }
            catch(System.Exception ex){
                Debug.Log(ex.Message);
            }
        }
        return false;
    }
    #endregion
    #endregion

    #region MemuVisible

    public static void Show(string gameMessage = "Game paused",string buttonText = "Resume"){

        MenuContent.SetActive(true);

        MenuMessage.text = gameMessage;
        MenuButtonTitle.text = buttonText;
        MenuStatistic.text = $"Game Statistics: "
            + $"\n - Time in game: {GameStat.GameTime:F1} s."
            + $"\n - Checkpoint 1: {CheckPoint1.status}."
            + $"\n - Checkpoint 2: {CheckPoint2.status}."
            + $"\n - Checkpoint 3: {CheckPoint3.status}."
            + $"\n - Score: {(int)(GameStat.score * 100)}"
            + $"\n - Bests Scores: 1st - {(GameStat.bestScores[0])},"
            + $"\n 2nd - {(GameStat.bestScores[1])}, 3rd - {(GameStat.bestScores[2])}";
        Time.timeScale = 0f;

        if(GameStat.IsEnd){
           GameMenu.mainButton.gameObject.SetActive(false);
           GameMenu.restartButton.gameObject.SetActive(true);
        }
        else{
            GameMenu.mainButton.gameObject.SetActive(true);
           GameMenu.restartButton.gameObject.SetActive(false);
        }
    }
    public static void Hide(){
        MenuContent.SetActive(false);
        Time.timeScale = 1f;
    }

    public static void GameEndShow(){
        


    }
    
    #endregion
}
