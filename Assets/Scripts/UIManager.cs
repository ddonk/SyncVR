using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton { get; private set; }
    private int _score;
    
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _againButton;
    [SerializeField] private Button _startButton;
    public void UpdateScore()
    {
        Singleton._score++;
        _scoreText.text = $"Score: {_score}";
    }
    
    public void HandleDeath()
    {
        _scoreText.text = $"You hit a branch, your score was: {_score}.\n Do you want to play again?";
        _againButton.gameObject.SetActive(true);
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
            _scoreText.text = $"Score: {_score}";

            //Load Main Menu Again
            _againButton.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(0);
            });
            Singleton = this;
        }
    }
}