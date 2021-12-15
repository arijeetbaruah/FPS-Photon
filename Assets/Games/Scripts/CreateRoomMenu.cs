using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Photon.Pun;

public class CreateRoomMenu : MonoBehaviour
{
    public const string playerKey = "PlayerName";
    [SerializeField] private TMP_InputField playerNameInput;

    private string roomID;

    private void Start()
    {
        string playerName = PlayerPrefs.GetString(playerKey);
        playerNameInput.text = playerName;
    }

    public void SetRoomID(string roomID)
    {
        this.roomID = roomID;
    }

    public string GetRoomID()
    {
        return roomID;
    }

    public void SetPlayerName(string value)
    {
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerKey, value);
    }
}
