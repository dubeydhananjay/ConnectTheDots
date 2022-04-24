using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Text text;

    //Set this button with level no and save it.
    public void SetAction(int levelNo)
    {
        text.text = string.Format("Level{0}", levelNo);
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            PlayerPrefs.SetInt(GameConstants.LEVEL, levelNo);
            ScreenNavigation.NavigateScene(GameConstants.GAMESCENE);
        });
        
    }
}
