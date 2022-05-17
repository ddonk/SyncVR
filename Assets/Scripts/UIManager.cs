using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }
    private int score;
    
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _againButton;
    [SerializeField] private Button _startButton;
    public void UpdateScore()
    {
        Singleton.score++;
        _scoreText.text = $"Score: {score}";
    }

    public void HandleDeath()
    {
        _scoreText.text = $"You hit a branch, your score was: {score}.\n Do you want to play again?";
        _againButton.gameObject.SetActive(true);
    }

    private void HandleStart()
    {
        _scoreText.text = $"Score: {score}";
        PlayerCharacterController._characterController.enabled = true;
        _startButton.gameObject.SetActive(false);
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
            _scoreText.text = "Try to avoid the branches that are on the ground!";
            _startButton.onClick.AddListener(delegate
            {
                HandleStart();
            });
            
            _againButton.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
            Singleton = this;
        }
    }
}
