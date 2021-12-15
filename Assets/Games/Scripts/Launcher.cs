using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Header("Menu Object")]
    [SerializeField] private TextMeshProUGUI loadingTxt;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private CreateRoomMenu createRoomPanel;
    [SerializeField] private RoomMenu joinRoomPanel;
    [SerializeField] private MainRoomMenu roomMenu;

    private void Start()
    {
        loadingPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        roomMenu.gameObject.SetActive(false);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(false);
        loadingTxt.SetText("Connection To Server");

        joinRoomPanel.OnClickEvent.AddListener(JoinRoom);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        loadingTxt.SetText("Joinning Lobby");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joinned Lobby");

        loadingPanel.SetActive(false);
        createRoomPanel.gameObject.SetActive(false);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
        loadingTxt.SetText("");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joinned Room");

        roomMenu.UpdateRoomName();

        loadingPanel.SetActive(false);
        createRoomPanel.gameObject.SetActive(false);
        roomMenu.gameObject.SetActive(true);
        joinRoomPanel.gameObject.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");

        loadingPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        roomMenu.gameObject.SetActive(false);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(false);
    }

    public void OpenJoinRoom()
    {
        loadingPanel.SetActive(false);
        createRoomPanel.gameObject.SetActive(false);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).UserId == PhotonNetwork.LocalPlayer.UserId)
        {            
            PhotonNetwork.LoadLevel("Game");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        joinRoomPanel.UpdateRooms(roomList);
    }

    public void OpenCreateRoom()
    {
        loadingPanel.SetActive(false);
        createRoomPanel.gameObject.SetActive(true);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    public void JoinRoom(RoomInfo roomInfo)
    {
        print("hi");
        loadingPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(false);
        createRoomPanel.gameObject.SetActive(false);

        PhotonNetwork.JoinRoom(roomInfo.Name);
    }

    public void CreateRoom()
    {
        loadingPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(false);
        createRoomPanel.gameObject.SetActive(false);

        PhotonNetwork.CreateRoom(createRoomPanel.GetRoomID(), new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
    }

    public void ReturnBtn()
    {
        loadingPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(false);
        createRoomPanel.gameObject.SetActive(false);
    }

    public void ExitRoom()
    {
        loadingPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        roomMenu.gameObject.SetActive(false);
        joinRoomPanel.gameObject.SetActive(false);
        createRoomPanel.gameObject.SetActive(false);

        PhotonNetwork.LeaveRoom();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
