using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAME : MonoBehaviour
{
    [SerializeField] PlayerBody player;
    [SerializeField] InterScene interSceneData;
    float time = 0;
    public int score = 0;
    public int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        player.currenthp = interSceneData.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 
    }

    void ChangeScene(string sceneName) {
        interSceneData.playerHealth = player.currenthp;
        interSceneData.totalTime = time;
        interSceneData.score = score;
        interSceneData.killCount = killCount;
        SceneManager.LoadScene(sceneName);
    }
}
