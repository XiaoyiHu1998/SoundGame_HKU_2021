using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //public int winScore;
    public int score { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementScore(int scoreIncrement)
    {
        score += scoreIncrement;
    }
}
