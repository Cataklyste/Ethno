using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Language : MonoBehaviour
{
    
    public List<int> words;

    public int salut;
    public int insulte;
    public int oui;
    public int non;

    public int answerSalut;
    public int answerInsulte;
    public int answerOui;
    public int answerNon; 

	void Start ()
    {
        List<int> tmp = new List<int>(words);

        salut = tmp[Random.Range(0, tmp.Count - 1)];
        tmp.Remove(salut);
		insulte = tmp[Random.Range(0, tmp.Count - 1)];
        tmp.Remove(insulte);
		oui = tmp[Random.Range(0, tmp.Count - 1)];
        tmp.Remove(oui);
		non = tmp[Random.Range(0, tmp.Count - 1)];
        tmp.Remove(non);

        tmp = new List<int>(words);

		answerSalut = tmp[Random.Range(0, tmp.Count - 1)];
        tmp.Remove(answerSalut);
		answerInsulte = tmp[Random.Range(0, tmp.Count - 1)];
        tmp.Remove(answerInsulte);
		answerOui = tmp[Random.Range(0, tmp.Count - 1)];
        tmp.Remove(answerOui);
		answerNon = tmp[Random.Range(0, tmp.Count - 1)];
        tmp.Remove(answerNon);
	}

	public int getIndexAnswer(int index)
	{
		if(index == salut)
			return 0;
		else if (index == insulte)
			return 1;
		else if (index == oui)
			return 2;
		else if (index == non)
			return 3;
		return 0;
	}

	public int getAnswer(int question)
	{
		if (question == salut)
			return answerSalut;
		else if (question == oui)
			return answerOui;
		else if (question == non)
			return answerNon;
		else if (question == insulte)
			return answerInsulte;

		return 0;
	}

    public bool PlayerAnswerMatch(int question, int answer)
    {
        if (question == salut)
            return answerSalut == answer;
        else if (question == insulte)
            return answerInsulte == answer;
        else if (question == oui)
            return answerOui == answer;
        else if (question == non)
            return answerNon == answer;

        return false;
    }
}
