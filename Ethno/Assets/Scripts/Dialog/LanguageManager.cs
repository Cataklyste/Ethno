using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LanguageManager : MonoBehaviour {

    [SerializeField]
    private List<string> words;

    private List<int> wordsValues;

    [SerializeField]
    private GameObject asiatique;
    [SerializeField]
    private GameObject pirate;
    [SerializeField]
    private GameObject amerindien;
    [SerializeField]
    private GameObject inuit;

	// Use this for initialization
	void Start ()
    {
        wordsValues = new List<int>();

        for (int i = 0; i < words.Count ; i++)
        {
            string[] splitValues = words[i].Split(' ');

            wordsValues.Add(1 << (int.Parse(splitValues[0]) - 1) |
							1 << (int.Parse(splitValues[1]) - 1) | 
							1 << (int.Parse(splitValues[2]) - 1) | 
							1 << (int.Parse(splitValues[3]) - 1));
        }

        for (int i = 0; i < 16; i++)
        {
            int randomValue = wordsValues[Random.Range(0, wordsValues.Count)];
            if (i/4 == 0)
                asiatique.GetComponent<Language>().words.Add(randomValue);
            else if (i/4 == 1)
                pirate.GetComponent<Language>().words.Add(randomValue);
            else if (i/4 == 2)
                amerindien.GetComponent<Language>().words.Add(randomValue);
            else if (i/4 == 3)
                inuit.GetComponent<Language>().words.Add(randomValue);

            wordsValues.Remove(randomValue);
        }
	}
}
