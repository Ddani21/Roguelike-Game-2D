using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreTable : MonoBehaviour{
    // Start is called before the first frame update
    public static HighScoreTable instance;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    int counter = 0;
    private void Awake() {
        instance = this;
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        



        
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        if ( highscores == null) {
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(100,"CMK");
            AddHighscoreEntry(50,"CMD");
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(0,"NoOne");
            AddHighscoreEntry(0,"NoOne");

            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);    
        }
        //Sortare
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++){
            for(int j = i +1 ; j < highscores.highscoreEntryList.Count; j++){
                
                if( highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score){
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }

            }
        }
        highscoreEntryTransformList = new List<Transform>();
        
        foreach(HighscoreEntry highscoreEntry in highscores.highscoreEntryList){
            CreateHighscoreEntryTransform(highscoreEntry,entryContainer,highscoreEntryTransformList);
            counter++;
            if( counter == 10){
                break;
            }
        }



    }
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList){
        float templateHeight = 20f;
        
        Transform entryTransform =Instantiate(entryTemplate,container);//luam referinta
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();//luam transform 
        entryRectTransform.anchoredPosition = new Vector2(0,-templateHeight* transformList.Count);//punem in ordine separate obiectele noi;
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch(rank){
            default: 
                rankString = rank + "TH";
            break;
                
            case 1: rankString ="1ST";

            break;

            case 2: rankString = "2ND";

            break;
                
            case 3: rankString = "3RD";

            break;
        }
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();
            
        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;


        transformList.Add(entryTransform);
    }
    public void AddHighscoreEntry(int score, string name){
        //Creaza un entry
        HighscoreEntry highscoreEntry = new HighscoreEntry {score = score, name = name};

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if ( highscores == null) {

            highscores = new Highscores(){
                highscoreEntryList = new List<HighscoreEntry>()
            };

        }
        highscores.highscoreEntryList.Add(highscoreEntry);//Adaugam entry-ul nou
        
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    private class Highscores{
        public List<HighscoreEntry> highscoreEntryList;
    }
    [System.Serializable]
    private class HighscoreEntry {
        public int score;
        public string name;
    }

}
