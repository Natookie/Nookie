using UnityEngine;

public interface IMineable
{
    int Health { get; }
    void TakeDamage(int damage);
    void OnMined();
    GameObject GetGameObject();
}