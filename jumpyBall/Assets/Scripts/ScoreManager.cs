using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score=0;
    // Start is called before the first frame update
    void Awake()
    {
        MakeSingleton();
    }

   void Start()
    {
        AddScore(0);
    }
    void MakeSingleton() {

        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


    }

   

   public void AddScore(int amount)
    {
        score += amount;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", score);

        print(score);


    }

    public void ResetScore() {
        score = 0;
    }
}
