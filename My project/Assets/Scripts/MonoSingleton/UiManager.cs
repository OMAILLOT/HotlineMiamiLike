using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField] CanvasGroup startPanel;
    [SerializeField] GameObject loosePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] List<Image> allBulletImageSort;
    [SerializeField] TextMeshProUGUI numberOfMagasinLeft;
    [SerializeField] TextMeshProUGUI enemyLeftNumber;

    public void OpenWinPanel()
    {
        PlayerController.Instance.isInMenu = true;
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void OpenLoosePanel()
    {
        PlayerController.Instance.isInMenu = true;
        loosePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        PlayerController.Instance.isInMenu = false;
        startPanel.alpha = 0;
        startPanel.interactable = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void DecreaseBulletOnUi(int index)
    {
        allBulletImageSort[index].enabled = false;
    }

    public void ReloadBulletUi(int numberOfMagasin)
    {
        foreach(Image bullet in allBulletImageSort)
        {
            bullet.enabled = true;
        }
        UpdateMagasinNumber(numberOfMagasin);
    }

    public void UpdateMagasinNumber(int numberOfMagasin)
    {
        numberOfMagasinLeft.text = numberOfMagasin.ToString();
    }

    public void UpdateEnemyAliveCounter(int enemyLeftNumber)
    {
        this.enemyLeftNumber.text = enemyLeftNumber.ToString();
    }
}
