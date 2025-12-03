using UnityEngine;

public class BaseCell : GridCell
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.green;
    
    public override void Initialize(int x, int y){
        base.Initialize(x, y);
        sr.color = normalColor;
    }
    
    public override void OnCharacterEnterRadius(Character character){
        sr.color = highlightColor;
    }
    
    public override void OnCharacterExitRadius(Character character){
        sr.color = normalColor;
    }
}