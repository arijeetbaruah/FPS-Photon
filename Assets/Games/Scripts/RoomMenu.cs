using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;

using TMPro;

public class RoomMenu : MonoBehaviour
{
    [SerializeField] private RoomListItem roomListItem;
    [SerializeField] private Transform content;

    public UnityEvent<RoomInfo> OnClickEvent;

    private List<RoomListItem> roomInstanceList = new List<RoomListItem>();

    public void UpdateRooms(List<RoomInfo> roomList)
    {
        foreach(RoomListItem item in roomInstanceList)
        {
            Destroy(item.gameObject);
        }
        roomInstanceList.Clear();
        foreach(RoomInfo roomInfo in roomList)
        {
            var instance = Instantiate(roomListItem, content);
            instance.GetComponentInChildren<TextMeshProUGUI>().SetText(roomInfo.Name);
            instance.roomInfo = roomInfo;
            instance.OnClickEvent.AddListener(OnClick);
            roomInstanceList.Add(instance);
        }
    }

    public void OnClick(RoomInfo roomInfo)
    {
        OnClickEvent.Invoke(roomInfo);
    }
}
