using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    private void Awake()
    {
        instance = this;
    }

    /* 
     * Var for loading screen */
    public GameObject loadingScreen;
    public TMP_Text loadingText;

    /* 
     * Var for button menu */
    public GameObject menuButtons;

    /* 
     * Var for create room screen */
    public GameObject createRoomScreen;
    public TMP_InputField roomNameInput;

    /* 
     * Var for joined room screen */
    public GameObject roomScreen;
    public TMP_Text roomNameText;

    /* 
     * Var for error screen */
    public GameObject errorScreen;
    public TMP_Text errorText;

    /* 
     * Start is called before the first frame update */
    void Start()
    {
        CloseMenus();

        loadingScreen.SetActive(true);
        loadingText.text = "Connecting To Network ...";

        PhotonNetwork.ConnectUsingSettings();
    }
    
    /* 
     * Deactive Menu Canvas */
    void CloseMenus()
    {
        loadingScreen.SetActive(false);
        menuButtons.SetActive(false);
        createRoomScreen.SetActive(false);
        roomScreen.SetActive(false);
        errorScreen.SetActive(false);
    }

    /* 
     * First time connecting to server */
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        loadingText.text = "Joining Lobby ...";
    }

    /* 
     * When joining a Room */
    public override void OnJoinedLobby()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    /* 
     * When succesfully create a room */
    public void OpenRoomCreate()
    {
        CloseMenus();
        createRoomScreen.SetActive(true);
    }

    /* 
     * Creating room function */
    public void CreateRoom()
    {
        if(!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 8;

            PhotonNetwork.CreateRoom(roomNameInput.text, options);

            CloseMenus();
            loadingText.text = "Creating Room ...";
            loadingScreen.SetActive(true);
        }
    }

    /* 
     * Set name when creating room */
    public override void OnJoinedRoom()
    {
        CloseMenus();
        roomScreen.SetActive(true);

        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    /* 
     * When game has already have a room name */
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Failed To Create Room: " + message;
        CloseMenus();
        errorScreen.SetActive(true);
    }

    /* 
     * Closing error screen */
    public void CloseErrorScreen()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    /* 
     * Leaving room function */
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CloseMenus();
        loadingText.text = "Leaving Room";
        loadingScreen.SetActive(true);
    }

    /* 
     * When leaving room */
    public override void OnLeftRoom()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }
}
