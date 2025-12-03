using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Button restartButton;
    
    void Start(){
        restartButton.onClick.AddListener(RestartGame);
        gameOverPanel.SetActive(false);
    }
    
    public void ShowGameOver(int finalScore){
        finalScoreText.text = $"Final Score: {finalScore}";
        gameOverPanel.SetActive(true);
    }
    
    void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}