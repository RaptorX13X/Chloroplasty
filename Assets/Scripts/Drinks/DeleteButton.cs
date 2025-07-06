using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public void OnClicked()
    {
        if (MixingStation.Instance.amountInShaker == 0) return;
        
        MixingStation.Instance.PourOut();
    }
}
