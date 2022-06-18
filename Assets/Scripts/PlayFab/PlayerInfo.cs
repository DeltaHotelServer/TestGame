using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnLoggedIn()
    {
        // Request für das Spielerprofil
        GetPlayerProfileRequest getPlayerProfileRequest = new GetPlayerProfileRequest
        {
            PlayFabId = LoginRegister.instance.playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowDisplayName = true
            }
        };

        // Spielerprofil Request per API versenden
        PlayFabClientAPI.GetPlayerProfile(getPlayerProfileRequest, OnProfileSuccess, OnPlayFabError);
    }

    private void OnProfileSuccess(GetPlayerProfileResult obj)
    {
        print("Display Name: " + obj.PlayerProfile.DisplayName);
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        print(obj.ErrorMessage);
    }
}