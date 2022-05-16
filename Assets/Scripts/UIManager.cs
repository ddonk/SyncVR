using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }
    private int score;
    [SerializeField] private TMP_Text _scoreText;

    public void UpdateScore()
    {
        Singleton.score++;
        _scoreText.text = $"Score: {score}";
    }
    
    private void Awake()
    {
        if (Singleton != null && Singleton != this) 
        { 
            Destroy(this); 
        } 
        else 
        {
            Debug.Log("Init UI Manager Singleton");
            Singleton = this;
            _scoreText.text = $"Score: 0";
        }
    }
}
