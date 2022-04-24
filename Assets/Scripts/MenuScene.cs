using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    [SerializeField] private int levelsCount = 10;
    [SerializeField] private LevelButton levelButtonTemplate;

    //Generate levels.
    private void Start()
    {
        for(int i = 0; i < levelsCount; i++)
        {
            LevelButton levelButton = Instantiate(levelButtonTemplate);
            int level = i + 1;
            levelButton.SetAction(level);
            levelButton.transform.parent = levelButtonTemplate.transform.parent;
            levelButton.name = string.Format("Level{0}", level);
            levelButton.gameObject.SetActive(true);
        }
    }
}
