using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using System;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardCanvas;

    private void Start()
    {
        leaderboardCanvas.SetActive(false);
        DisplayLeaderboard();
        SetLeaderboardEntry(-20);
    }

    public void DisplayLeaderboard()
    {
        GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest()
        {
            StatisticName = "Highscore"
        };

        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest, OnLeaderboardSuccess, OnPlayFabError);
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        print("Error: " + obj.ErrorMessage);
    }

    private void OnLeaderboardSuccess(GetLeaderboardResult obj)
    {
        UpdateLeaderboardUI(obj.Leaderboard);
    }

    void UpdateLeaderboardUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        print("leaderboard success");
        leaderboardCanvas.SetActive(true);
    }

    public void SetLeaderboardEntry(int newScore)
    {
        ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
        {
            FunctionName = "UpdateHighscore",
            FunctionParameter = new { score = newScore }
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnEntrySuccess, OnPlayFabError);
    }

    private void OnEntrySuccess(ExecuteCloudScriptResult obj)
    {
        print("Ergebnis im Leaderboard abgespeichert");
    }
}