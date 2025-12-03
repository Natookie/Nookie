using UnityEngine;

public class OreCell : GridCell, IMineable
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Color[] oreColors = { Color.gray, Color.yellow, Color.red };
    [SerializeField] private GameObject oreParticles;
    
    public int Health { get; private set; }
    public OreType Type { get; private set; }
    
    public enum OreType { Stone, Gold, Diamond }
    
    public override void Initialize(int x, int y){
        base.Initialize(x, y);
        Health = maxHealth;
        
        Type = (OreType)Random.Range(0, 3);
        UpdateVisual();
    }
    
    public void TakeDamage(int damage){
        Health -= damage;
        UpdateVisual();
        
        if(Health <= 0) OnMined();
    }
    
    public void OnMined(){
        if(oreParticles != null) Instantiate(oreParticles, transform.position, Quaternion.identity);
        
        GameManager.Instance.AddScore(GetOreValue());
        gameObject.SetActive(false);
    }
    
    public GameObject GetGameObject() => gameObject;
    
    public override void OnCharacterEnterRadius(Character character){
        sr.color = Color.red;
        character.StartMining(this);
    }
    
    public override void OnCharacterExitRadius(Character character){
        sr.color = oreColors[(int)Type];
        character.StopMining();
    }
    
    protected override void PerformCellAction(){
        //Mining action happens through TakeDamage
    }
    
    void UpdateVisual(){
        if(sr != null) sr.color = oreColors[(int)Type];
    }
    
    int GetOreValue(){
        return Type switch
        {
            OreType.Stone => 1,
            OreType.Gold => 5,
            OreType.Diamond => 20,
            _ => 1
        };
    }
}