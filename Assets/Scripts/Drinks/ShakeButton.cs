using UnityEngine;

public class ShakeButton : MonoBehaviour
{
    public void OnClicked()
    {
        if (MixingStation.Instance.amountInShaker < 3) return;
        
        MixingStation.Instance.FinishDrink();
    }
}
