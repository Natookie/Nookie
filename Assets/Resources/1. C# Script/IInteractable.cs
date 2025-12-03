using UnityEngine;

public interface IInteractable
{
    void OnCharacterEnterRadius(Character character);
    void OnCharacterExitRadius(Character character);
}