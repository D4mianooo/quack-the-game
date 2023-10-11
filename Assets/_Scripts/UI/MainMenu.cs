using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button createLobbyBtn;
    [SerializeField] private Button joinLobbyBtn;
    
    private void Awake() {
        createLobbyBtn.onClick.AddListener(CreateLobby);
        joinLobbyBtn.onClick.AddListener(JoinLobby);
    }
    private void CreateLobby(){
        string lobbyID = GenerateRandomID();
        SceneManager.LoadScene("LobbyScene");
        Debug.Log($"Created Lobby: {lobbyID} ");
    }
    private void JoinLobby(){
        string lobbyID = GenerateRandomID();
        Debug.Log($"Joined Lobby: {lobbyID} ");
    }

    private string GenerateRandomID()
    {
        System.Random random = new System.Random();
        string lobbyID = "QUACK";
        lobbyID += random.Next().ToString();
        return lobbyID;
    }
    private void LoadLobbyScene(string lobbyID){
        SceneManager.LoadScene("LobbyScene");
    }
}
