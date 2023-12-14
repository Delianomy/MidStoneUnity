using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    InterScene sceneData;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI rooms;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI kills;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int hour = Mathf.FloorToInt(sceneData.totalTime / 3600);
        int minutes = Mathf.FloorToInt(sceneData.totalTime / 60);
        int seconds = Mathf.FloorToInt(sceneData.totalTime % 60);
        time.text = "Total Time " + string.Format("{0:00}:{1:00}:{2:00}", hour, minutes, seconds);
        rooms.text = "Rooms Cleared " + sceneData.room;
        score.text = "Score " + sceneData.score;
        kills.text = "Kills " + sceneData.killCount;
    }

    private void ResetData() {
        sceneData.playerHealth = 250.0f;
        sceneData.killCount = 0;
        sceneData.totalTime = 0;
    }
}
