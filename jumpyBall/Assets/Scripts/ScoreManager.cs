using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private Text scoreTXT;
    public int score=0;
    // Start is called before the first frame update
    void Awake()
    {
        scoreTXT = GameObject.Find("ScoreTxt").GetComponent<Text>();
        MakeSingleton();
    }

   void Start()
    {
        AddScore(0);
    }
    private void Update()
    {
        if (scoreTXT == null) {

            scoreTXT = GameObject.Find("ScoreTxt").GetComponent<Text>();
            AddScore(0);
        }
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

        scoreTXT.text = score.ToString();

        print(score);


    }

    public void ResetScore() {
        score = 0;
    }
}
