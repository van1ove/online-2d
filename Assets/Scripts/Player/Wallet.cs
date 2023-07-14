using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private TextMeshProUGUI _walletTMP;
    private int _coinsAmount;
    private PhotonView _photonView;
    private void Start()
    {
        _coinsAmount = 0;
        _photonView = GetComponent<PhotonView>();
        _walletTMP = GameObject.FindGameObjectWithTag("Wallet").GetComponent<TextMeshProUGUI>();
    }

    [PunRPC]
    public void TakeCoin()
    {
        if (!_photonView.IsMine)
            return;

        _coinsAmount++;
        _walletTMP.text = _coinsAmount.ToString();
    }
}
