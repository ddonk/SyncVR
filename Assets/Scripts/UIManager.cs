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

    private void HandleStart()
    {
        _scoreText.text = $"Score: {_score}";
        PlayerCharacterController.CharacterController.enabled = true;
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
            _startButton.onClick.AddListener(HandleStart);
            
            //Will reload the game scene if you want to play again
            _againButton.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
            Singleton = this;
        }
    }
}