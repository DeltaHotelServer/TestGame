using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using System;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardCanvas;
    public string leaderboardName;

    public GameObject[] leaderboardEntries;

    // Start is called before the first frame update
    void Start()
    {
        leaderboardCanvas.SetActive(false);
        DisplayLeaderboard();
        //SetLeaderboardEntry(42);
    }

    public void DisplayLeaderboard()
    {
        // Request um Daten des Leaderboards zu erhalten
        GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest()
        {
            StatisticName = leaderboardName
        };

        // Request per API versenden
        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest, OnLeaderboardSuccess, OnPlayFabError);
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        print("Error: " + obj.ErrorMessage);
    }

    private void OnLeaderboardSuccess(GetLeaderboardResult obj)
    {
        // Aktualisiere das Leaderboard
        UpdateLeaderboardUI(obj.Leaderboard);
    }

    void UpdateLeaderboardUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        print("Leaderboard aktualisieren");
        leaderboardCanvas.SetActive(true);

        print("Leaderboard Count: " + leaderboard.Count);

        for (int i = 0; i < leaderboardEntries.Length; i++)
        {
            if (i < leaderboard.Count)
            {
                leaderboardEntries[i].SetActive(true);

                // Eintrag befindet sich im Leaderboard
                print(leaderboard[i].DisplayName);
                print(leaderboard[i].StatValue);
                leaderboardEntries[i].transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = leaderboard[i].DisplayName;
                leaderboardEntries[i].transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = leaderboard[i].StatValue.ToString();
            }
            else
            {
                leaderboardEntries[i].SetActive(false);
            }

        }
    }

    public void SetLeaderboardEntry(int newScore)
    {
        // Cloud Script Request
        ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
        {
            FunctionName = "UpdateHighscore",
            FunctionParameter = new { score = -newScore }
        };

        // Übermittlung an den Server
        PlayFabClientAPI.ExecuteCloudScript(request, OnEntrySuccess, OnPlayFabError);
    }

    private void OnEntrySuccess(ExecuteCloudScriptResult obj)
    {
        print("Ergebnis im Leaderboard abgespeichert");
    }
}