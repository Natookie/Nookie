using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("REFERENCES")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Character character;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI oreCountText;
    
    private int score = 0;
    
    void Awake(){
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    void Start(){
        //INIT
        gridManager.GenerateGrid();
        UpdateUI();
    }
    
    void Update(){
        HandleCharacterDrag();
        UpdateUI();
    }
    
    public void AddScore(int points){
        score += points;
        UpdateUI();
    }
    
    void UpdateUI(){
        if(scoreText != null) scoreText.text = $"Score: {score}";
        if(oreCountText != null && character != null) oreCountText.text = $"Ore Mined: {character.TotalOreMined}";
    }
    
    void HandleCharacterDrag(){
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            if(hit.collider != null && hit.collider.gameObject == character.gameObject) character.OnDragStart();
        }
        
        if(Input.GetMouseButton(0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            character.OnDrag(mousePos);
        }
        
        if(Input.GetMouseButtonUp(0)){
            character.OnDragEnd();
        }
    }
}