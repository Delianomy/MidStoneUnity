using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GAME : MonoBehaviour
{
    [Header("Switching scenes")]
    [SerializeField] PlayerBody player;
    [SerializeField] InterScene interSceneData;
    [SerializeField] TextMeshProUGUI timerHud;
    [SerializeField] TextMeshProUGUI scoreHud;
    [SerializeField] TextMeshProUGUI killHud;
    [SerializeField] TextMeshProUGUI roomHud;
    float elapsedTime = 0;
    public int score = 0;
    public int killCount = 0;
    public int room = 1;

    [Header("Switching rooms")]
    [SerializeField] GameObject exitPortal;

    // Start is called before the first frame update
    private void Awake() {
        exitPortal.SetActive(false);
        player.currenthp = interSceneData.playerHealth;
        elapsedTime = interSceneData.totalTime;
        score = interSceneData.score;
        killCount = interSceneData.killCount;
        room = interSceneData.room;
    }



    // Update is called once per frame
    void Update()
    {
        //Hud stuff
        elapsedTime += Time.deltaTime;
        int hour = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerHud.text = string.Format("{0:00}:{1:00}:{2:00}", hour, minutes, seconds);

        scoreHud.text = "Score " + score;
        killHud.text = "Kill " + killCount;
        roomHud.text = "Room " + room;
    }

    public void ChangeScene(string sceneName) {
        interSceneData.playerHealth = player.currenthp;
        interSceneData.totalTime = elapsedTime;
        interSceneData.score = score;
        interSceneData.killCount = killCount;
        if (sceneName != "Result"){
            interSceneData.room += 1;
        }
        SceneManager.LoadScene(sceneName);
    }

    public void OpenExitPortal() {
        exitPortal.SetActive(true);
    }
}
