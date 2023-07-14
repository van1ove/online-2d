using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel, losePanel, victoryPanel;
    [SerializeField] private TextMeshProUGUI playerName, coinsAmount;
    public static GameManager Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gamePanel.SetActive(true);
        losePanel.SetActive(false);
        victoryPanel.SetActive(false);
    }

    public void TurnGameOverPanel()
    {
        
        gamePanel.SetActive(false);
        if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
        {
            losePanel.SetActive(true);
        }
        else
        {
            victoryPanel.SetActive(true);
        }
    }
    public void OnReturnClick()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
    
    public void DestroyPlayer(GameObject gameObject)
    {
        StartCoroutine(DeletePlayer(gameObject));
    }

    private IEnumerator DeletePlayer(GameObject gameObject)
    {
        yield return new WaitForSeconds(1.4f);
        PhotonNetwork.Destroy(gameObject);
        CheckAmount();
    }

    private void CheckAmount()
    {
        Debug.Log("check");
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1) return;
        Victory();
    }

    private void Victory()
    {
        Debug.Log("victory");
        gamePanel.SetActive(false);
        losePanel.SetActive(false);
        victoryPanel.SetActive(true);
    }
}
