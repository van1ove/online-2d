using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel, losePanel, victoryPanel; 
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

    private void Update()
    {
        
    }

    public void TurnGameOverPanel()
    {
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void DestroyPlayer(GameObject gameObject)
    {
        StartCoroutine(DeletePlayer(gameObject));
    }

    private IEnumerator DeletePlayer(GameObject gameObject)
    {
        yield return new WaitForSeconds(1.4f);
        PhotonNetwork.Destroy(gameObject);
    }
    public void OnReturnClick()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
}
