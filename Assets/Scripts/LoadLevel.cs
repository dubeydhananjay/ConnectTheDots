using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class LoadLevel : MonoBehaviour
{
    //Load level from the json after fetching from Levels.txt.
    private void Start()
    {
        string path = "Assets/Levels.txt";
        string jsonString = File.ReadAllText(path);
        JSONObject levelsObject = (JSONObject)JSON.Parse(jsonString);
        int levelNo = PlayerPrefs.GetInt(GameConstants.LEVEL);
        string levelName = string.Format("Level{0}", levelNo);

        for (int i = 0; i < levelsObject["Levels"][levelName].AsArray.Count; i++)
        {
            string nodeName = levelsObject["Levels"][levelName].AsArray[i]["ColorNode"];
            string tileName = levelsObject["Levels"][levelName].AsArray[i]["TileParent"];
            HandleNode(nodeName, tileName);
        }
    }

    private void HandleNode(string nodeName, string tileName)
    {
        ColorNode node = GameObject.Find(nodeName).GetComponent<ColorNode>();
        node.SetTileParent(tileName);
    }

}
