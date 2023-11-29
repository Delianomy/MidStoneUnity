using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTheNextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    bool levelWon = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) {
            Debug.Log("Door open");
            levelWon = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && levelWon == true) {
            SceneManager.LoadScene("Scene2");
            Debug.Log("Won");
        }
    }
}
