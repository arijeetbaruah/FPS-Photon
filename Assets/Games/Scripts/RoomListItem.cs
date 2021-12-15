using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using UnityEngine.Events;

public class RoomListItem : MonoBehaviour
{
    public RoomInfo roomInfo;
    public UnityEvent<RoomInfo> OnClickEvent;

    public void OnClick()
    {
        OnClickEvent.Invoke(roomInfo);
    }
}
