using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour, IDragable
{
    [Header("TWEAKS")]
    [SerializeField] private float miningRadius;
    [SerializeField] private int miningPower;
    [SerializeField] private float miningInterval;

    [Header("REFERENCES")]
    public CircleCollider2D miningCollider;
    
    private IMineable currentMineable;
    private Coroutine miningCoroutine;
    private Vector3 originalPosition;

    private bool isDragging = false;

    public bool IsMining { get; private set;}
    public int TotalOreMined { get; private set;  }

    void Awake(){
        originalPosition = transform.position;
    }

    public void OnDragStart(){
        isDragging = true;
        StopMining();
    }

    public void OnDrag(Vector3 position){
        if(CanBeDragged()) transform.position = position;
    }

    public void OnDragEnd(){
        isDragging = false;
        SnapToGrid();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, miningRadius);
        foreach(var hitCollider in hitColliders){
            IMineable mineable = hitCollider.GetComponent<IMineable>();

            if(mineable != null){
                StartMining(mineable);
                break;
            }
        }
    }

    public bool CanBeDragged() => !IsMining;
    public void StartMining(IMineable mineable){
        if(currentMineable != null) return;

        StopMining();
        currentMineable = mineable;
        IsMining = true;

        miningCoroutine = StartCoroutine(MiningRoutine());        
    }
    public void StopMining(){
        if(miningCoroutine != null){
            StopCoroutine(miningCoroutine);
            miningCoroutine = null;
        }

        IsMining = false;
        currentMineable = null;
    }
    IEnumerator MiningRoutine(){
        while(currentMineable != null && currentMineable.GetGameObject().activeSelf){
            yield return new WaitForSeconds(miningInterval);

            if(currentMineable != null){
                currentMineable.TakeDamage(miningPower);
                TotalOreMined++;
                break;
            }
        }

        StopMining();
    }

    void SnapToGrid(){
         Vector3 snappedPosition = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            0
        );
        transform.position = snappedPosition;
    }

    void OnTriggerEnter2D(Collider2D other){ if(!isDragging && other.TryGetComponent<IMineable>(out var mineable)) StartMining(mineable); }
    void OnTriggerExit2D(Collider2D other){ if(currentMineable != null && other.gameObject == currentMineable.GetGameObject()) StopMining(); }
}
