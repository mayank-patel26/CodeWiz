using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class StringReversal : MonoBehaviour
{
    [SerializeField]
    GameObject[] runes;
    int n;
    [SerializeField] Transform parent;
    private GameObject runeObj;
    [SerializeField] private GameObject correctWord;
    int baseN=5;
    System.Random rand;
    char[] correct;
    private void Start()
    {
        runeObj = Instantiate(runes[n],parent);
        rand= new System.Random();
        string word = RandomString(n + baseN);
        //char[] wordShuffle = Shuffle(word).ToCharArray();
        char[] wordShuffle = word.ToCharArray();
        Array.Reverse(wordShuffle);
        correct = word.ToCharArray();
        correctWord.GetComponent<TMP_Text>().text = word;
        for(int i=0;i<n+baseN;i++)
        {
            Transform child=runeObj.transform.GetChild(i);
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
        Debug.Log(new string(correct));
        for (int i = 0; i < n + baseN; i++)
        {
            Transform child = runeObj.transform.GetChild(i);
            //Debug.Log(child.GetChild(0).GetComponent<TMP_Text>().text[0]+"-"+correct[i]);
            Debug.Log(child.name);
            if(child.GetChild(0).GetComponent<TMP_Text>().text[0].Equals(correct[i]))
                c++;
            Debug.Log(c);
        }
        Debug.Log(c);
        if (c == n + baseN)
            Debug.Log("Success!");
    }
}
