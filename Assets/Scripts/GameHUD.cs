using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private Button backButton;
    private void Start()
    {
        backButton.onClick.AddListener(() => ScreenNavigation.NavigateScene(GameConstants.MENUSCENE));
    }
}
