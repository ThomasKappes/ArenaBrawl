using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;
    public GameObject battleButton;
    public GameObject cancelButton;

    // Initialize Singleton in main menu scene
    private void Awake() {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Test connection to Photon server
    public override void OnConnectedToMaster() {
        Debug.Log("Player has connected to the Photon master server!");
        battleButton.SetActive(true);
    }

    // Starts a multiplayer session from the main menu
    public void OnBattleButtonClicked() {
        Debug.Log("Battle button was clicked");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    // Automatically called when the connection to a random server failed
    // When there is no existing room and this error occurs a new room will be created via CreateRoom()
    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("Tried to join a random game but failed. There must be no open games available");
        CreateRoom();
    }

    // Creates a new room with a randomly generated room name
    void CreateRoom() {
        Debug.Log("Create Room button was clicked");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        // Room name will be random to prevent duplicate rooms (probably a better way to do this ..)
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    // Will be executed when the player successfully joined a room
    public override void OnJoinedRoom() {
        Debug.Log("Room joined successfully");
    }

    // Will be called when the room creation failed -> a new room with another random name will be created
    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
        base.OnCreateRoomFailed(returnCode, message);
        CreateRoom();
    }

    // Allows the user to stop the matchmaking process
    public void OnCancelButtonClicked() {
        Debug.Log("Cancel button was clicked");
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
