using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginRegister : MonoBehaviour
{
    public InputField myUsername, myPassword;
    public GameObject loginPanel;
    public Text infoText;

    public string playFabId;
    public static LoginRegister instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Register();
    }

    public void Register()
    {
        // Register Request erstellen
        RegisterPlayFabUserRequest registerRequest = new RegisterPlayFabUserRequest
        {
            // Parameter übergeben
            Username = myUsername.text,
            Password = myPassword.text,
            DisplayName = myUsername.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterResult, OnPlayFabError);
    }

    public void Login()
    {
        // Login Request erstellen
        LoginWithPlayFabRequest loginWithPlayFabRequest = new LoginWithPlayFabRequest
        {
            // Daten für die Anmeldung (login)
            Username = myUsername.text,
            Password = myPassword.text
        };

        // API Request für Login
        PlayFabClientAPI.LoginWithPlayFab(loginWithPlayFabRequest, OnLoginResult, OnPlayFabError);
    }

    private void OnLoginResult(LoginResult obj)
    {
        print("Angemeldet als: " + obj.PlayFabId);
        // PlayFabId zwischenspeichern
        playFabId = obj.PlayFabId;
        loginPanel.SetActive(false);
        // Spielerdaten abrufen
        PlayerInfo.instance.OnLoggedIn();
        // Nächste Scene laden
        SceneManager.LoadScene("MiniGame");
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        print(obj.ErrorMessage);
        string myOutput = "";

        switch (obj.Error)
        {
            case PlayFabErrorCode.UsernameNotAvailable:
                myOutput = "Benutzername nicht verfügbar";
                break;

            case PlayFabErrorCode.InvalidUsernameOrPassword:
                myOutput = "Benutzername oder Password falsch!";
                break;
        }

        // Text auf info text anzeigen
        infoText.text = myOutput;
    }

    private void OnRegisterResult(RegisterPlayFabUserResult obj)
    {
        print(obj.PlayFabId);
    }
}