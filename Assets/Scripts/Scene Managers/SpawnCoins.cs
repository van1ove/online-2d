using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnCoins : MonoBehaviour
{    
    [SerializeField] private GameObject coin;
    [SerializeField] private float minX, minY, maxX, maxY;
    private static bool _keepSpawning;
    private void Start()
    {
        _keepSpawning = PhotonNetwork.CurrentRoom.PlayerCount > 1;
        StartCoroutine(SpawnCoin());
    }
    private IEnumerator SpawnCoin()
    {
        while (_keepSpawning)
        {
            yield return new WaitForSeconds(3f);
            Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(coin.name, pos, Quaternion.identity);
        }
    }

    public static void StopSpawning()
    {
        _keepSpawning = false;
    }
    
    
}
