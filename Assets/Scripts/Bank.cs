using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    
    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    [SerializeField] TextMeshProUGUI displayBalance;

    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if (currentBalance < 0)
        {
            // Lose the Game;
            ReloadScene();
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = $"Gold : {currentBalance.ToString()}";
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
