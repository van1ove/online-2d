using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnCoins : MonoBehaviour
{    
    [SerializeField] private GameObject coin;
    [SerializeField] private float minX, minY, maxX, maxY;
    private void Start()
    {
        StartCoroutine(SpawnCoin());
    }

    private IEnumerator SpawnCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(coin.name, pos, Quaternion.identity);
        }
    }
}
