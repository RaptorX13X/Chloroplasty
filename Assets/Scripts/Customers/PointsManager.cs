using System;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private int stars = 3;
    [SerializeField] private GameObject[] starImages;

    public static PointsManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void LosePoints()
    {
        stars -= 1;
        starImages[stars].SetActive(false);
        if (stars == 0)
        {
            SequenceManager.instance.OnLoss();
        }
    }
}
