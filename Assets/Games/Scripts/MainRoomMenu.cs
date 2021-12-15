using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class MainRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private TextMeshProUGUI playerNamePrefab;
    [SerializeField] private TextMeshProUGUI playerSizeTxt;
    [SerializeField] private Transform playerContent;

    Dictionary<Player, TextMeshProUGUI> playerNames = new Dictionary<Player, TextMeshProUGUI>();

    public void UpdateRoomName()
    {
        roomName.SetText(PhotonNetwork.CurrentRoom.Name);
        playerNames.Clear();

        foreach (var player in PhotonNetwork.PlayerList)
        {
            TextMeshProUGUI playerName = Instantiate(playerNamePrefab, playerContent);
            playerName.SetText(player.NickName);
            playerNames.Add(player, playerName);
        }
        PlayerSize();
    }

    public void PlayerSize()
    {
        playerSizeTxt.SetText($"Player ({PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers})");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("Player Enter");

        TextMeshProUGUI playerName = Instantiate(playerNamePrefab, playerContent);
        playerName.SetText(newPlayer.NickName);
        playerNames.Add(newPlayer, playerName);

        PlayerSize();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Destroy(playerNames[otherPlayer].gameObject);
        playerNames.Remove(otherPlayer);

        PlayerSize();
    }
}
