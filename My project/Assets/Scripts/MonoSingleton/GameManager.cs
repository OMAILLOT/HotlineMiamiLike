using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] int totalEnemyCount;
    void Awake()
    {
        Time.timeScale = 0.2f;
        PlayerController.Instance.Init();
        UiManager.Instance.UpdateEnemyAliveCounter(totalEnemyCount);
    }

    public void TotalEnemyReduce()
    {
        totalEnemyCount--;
        UiManager.Instance.UpdateEnemyAliveCounter(totalEnemyCount);
        if (totalEnemyCount == 0)
        {
            UiManager.Instance.OpenWinPanel();
        }
    } 
}
