using UnityEngine;

public abstract class GridCell : MonoBehaviour, IInteractable
{
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public bool IsOccupied { get; protected set; }
    
    protected SpriteRenderer sr;
    
    public virtual void Initialize(int x, int y){
        X = x;
        Y = y;
        sr = GetComponent<SpriteRenderer>();
        name = $"Cell_{x}_{y}";
    }
    
    public abstract void OnCharacterEnterRadius(Character character);
    public abstract void OnCharacterExitRadius(Character character);
    
    public void InteractWithCell(Character character){
        OnCharacterEnterRadius(character);
        PerformCellAction();
        OnCharacterExitRadius(character);
    }
    
    protected virtual void PerformCellAction() { }
}