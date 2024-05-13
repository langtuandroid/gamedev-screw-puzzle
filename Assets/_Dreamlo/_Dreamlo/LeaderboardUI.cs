using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WS.Script.GameManagers;
using Zenject;

public class LeaderboardUI : MonoBehaviour
{
    [Inject] private SoundManager _soundManager;
    public static LeaderboardUI Instance;
    public GameObject leaderboardContainer;

    public GameObject[] players;        //store the 10 players
    public Text[] playerRank;       //player rank text
    public Text[] playerName;   //player rank name
    public Text[] playerScores; //player rank score

    [Header("MY RANK")]
    public Text myRank;     ////owner rank text
    public Text myName; //player name text
    public Text myScores;   //player score text

    enum gameState
    {
        waiting,
        running,
        enterscore,
        leaderboard
    };
    gameState gs;

    dreamloLeaderBoard dl;

    private void Awake()
    {
        Instance = this;
        leaderboardContainer.SetActive(false);
    }

    public void OpenLeaderboard(bool open)
    {
        _soundManager.Click();
        leaderboardContainer.SetActive(open);
        StartCoroutine(ShowLeaderboardCo());
    }

    // Use this for initialization
    IEnumerator ShowLeaderboardCo()
    {
        while (ValueStorage.PlayerName == "xxx")      //wait user input player name
        {
            yield return null;
        }

        // get the reference here...
        this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        this.gs = gameState.waiting;

        //StartCoroutine(GetPlayerScoresCo());
        SubmitScore();      //try submit score when start the Menu scene
        
        for (int i = 0; i < playerScores.Length; i++)   
        {
            playerName[i].text = "---";     //set all text ---
            playerRank[i].text = "---";
            playerScores[i].text = "---";
        }
    }

    public void SubmitScore()       
    {
        //Debug.LogError(gs);
        if (gs == gameState.waiting)
            StartCoroutine(SubmitScoreCo());        //call submit score when the player name is exist
    }

    public void SubmitScoreNew()
    {
        //Debug.LogError("SubmitScoreNew");
        dl = null;
        gs = gameState.waiting;

        this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        this.gs = gameState.waiting;

        //StartCoroutine(GetPlayerScoresCo());
        SubmitScore();      //try submit score when start the Menu scene

        for (int i = 0; i < playerScores.Length; i++)
        {
            playerName[i].text = "---";     //set all text ---
            playerRank[i].text = "---";
            playerScores[i].text = "---";
        }
    }

    IEnumerator SubmitScoreCo()
    {
        this.gs = gameState.running;        //set current state is submiting score
        
        dl.AddScore(ValueStorage.PlayerName, ValueStorage.BestResult);       //send player score to server

        yield return null;      //wait a frame
        //dl.LoadScores();
        List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();        //get the list from 1 -> 10

        while (scoreList.Count == 0) { scoreList = dl.ToListHighToLow(); yield return null; }       //wait until get the player data

        //Debug.LogError(scoreList.Count);
        for (int i = 0; i < playerScores.Length; i++)
        {
            players[i].gameObject.SetActive(false);     //disable the player, only enable it when exist the data of this position
            if (i < (scoreList.Count - 1))
            {
                playerName[i].text = scoreList[i].playerName;       //show player ranking, name and score
                playerRank[i].text = (i + 1) + "";
                playerScores[i].text = scoreList[i].score + "";
                players[i].gameObject.SetActive(true);      //enable the player
            }
        }

        bool inTop100 = false;
        for (int k = 0; k < scoreList.Count; k++)
        {
            if (scoreList[k].playerName == ValueStorage.PlayerName)      //compare if the current player is the owner, then show the owner data
            {
                myRank.text = (k + 1) + "";
                myName.text = scoreList[k].playerName;
                myScores.text = scoreList[k].score + "";

                inTop100 = true;
            }
        }

        if (!inTop100)
        {
            myRank.text = "100+";
            myName.text = ValueStorage.PlayerName;
            myScores.text = ValueStorage.BestResult + ""    ;
        }


        //Debug.LogError("XXX");
        this.gs = gameState.waiting;        //waiting for next submit score
    }
}
