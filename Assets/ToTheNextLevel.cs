using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTheNextLevel : MonoBehaviour
{
    [SerializeField] GAME gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            gameManager.ChangeScene("Room_1");
            gameManager.score += 100;
        }
    }
}
