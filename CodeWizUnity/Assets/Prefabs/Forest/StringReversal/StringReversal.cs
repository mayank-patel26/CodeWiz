using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StringReversal : MonoBehaviour
{
    int levelNumber=3;
    [SerializeField]
    GameObject[] runes;
    int[] n = { 5,8,12};
    int difficulty;
    [SerializeField] Transform parent;
    private GameObject runeObj;
    [SerializeField] private GameObject correctWord;
    char[] correct;
    [SerializeField] private GameObject stringReversalPanel;
    bool start = false;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject mainPlayer;
    private void Start()
    {
        APIConnections.FetchLevel(levelNumber);
        DynamicDifficulty.getinitialN();
    }
    private void Update()
    {
        if (start)
        {
            start = false;
            startGame();
        }    
           
    }
    void startGame()
    {
        DynamicDifficulty.startTimer();
        runeObj = Instantiate(runes[difficulty], parent);
        System.Random rand = new System.Random();
        string word = RandomString(n[difficulty]);
        //char[] wordShuffle = Shuffle(word).ToCharArray();
        char[] wordShuffle = word.ToCharArray();
        Array.Reverse(wordShuffle);
        correct = word.ToCharArray();
        correctWord.GetComponent<TMP_Text>().text = word;
        for (int i = 0; i < n[difficulty]; i++)
        {
            Transform child = runeObj.transform.GetChild(i);
            child.GetChild(0).GetComponent<TMP_Text>().text = wordShuffle[i].ToString();
        }
    }
    string Shuffle(string word)
    {
        char[] chars = new char[word.Length];
        System.Random rand = new System.Random(10000);
        int index = 0;
        while (word.Length > 0)
        { // Get a random number between 0 and the length of the word. 
            int next = rand.Next(0, word.Length - 1); // Take the character from the random position 
                                                      //and add to our char array. 
            chars[index] = word[next];                // Remove the character from the word. 
            word = word.Substring(0, next) + word.Substring(next + 1);
            ++index;
        }
        return new string(chars);
    }
    public void shutDialogue(GameObject dialogue)
    {
        dialogue.SetActive(false);
        stringReversalPanel.SetActive(true);
        start = true;
    }
    string RandomString(int length)
    {
        string alphabet = "abdfhijklmnopqrstuvwxyz";
        System.Random num = new System.Random();
        string randomString = new string(
            alphabet
                .ToCharArray()
                .OrderBy(s => (num.Next(2) % 2) == 0)
                .Take(length)
                .ToArray()
            );
        return randomString;
    }
    public void check()
    {
        
        int c = 0;
        for (int i = 0; i < n[difficulty]; i++)
        {
            Transform child = runeObj.transform.GetChild(i);
            //Debug.Log(child.GetChild(0).GetComponent<TMP_Text>().text[0]+"-"+correct[i]);
            Debug.Log(child.name);
            if(child.GetChild(0).GetComponent<TMP_Text>().text[0].Equals(correct[i]))
                c++;
        }
        if (c == n[difficulty])
        {
            long time = DynamicDifficulty.getTimeElapsed();
            APIConnections.makeLevelChanges(DynamicDifficulty.score,difficulty,time,levelNumber,0); 
            StartCoroutine(APIConnections.UpdateLevel(levelNumber));
            DynamicDifficulty.NextDifficulty((double)DynamicDifficulty.getTimeElapsed(), difficulty, 0);
            difficulty = DynamicDifficulty.currentDifficulty;
            //APIConnections.FetchLevel(levelNumber);
            //update score
            Destroy(runeObj);
            //if difficulty = 3 show success panel and go to next level, else next difficulty 
            if (difficulty == 3)
            {
                successPanel.SetActive(true);
                stringReversalPanel.SetActive(false);
            }    
            else
                start = true;
        }     
    }
    public void forest()
    {
        successPanel.SetActive(false);
        mainPlayer.SetActive(true);
        Invoke("graphLoad", 4);
    }
    public void graphLoad()
    {
        SceneManager.LoadScene("GraphColouring");
    }
}
