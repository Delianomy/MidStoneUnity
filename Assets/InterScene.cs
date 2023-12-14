using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InterScene", menuName = "Special Sauce")]
public class InterScene : ScriptableObject{
    public float playerHealth = 150;
    public float totalTime = 0;
    public int score = 0;
    public int killCount = 0;
    public int rooms = 0;
}
