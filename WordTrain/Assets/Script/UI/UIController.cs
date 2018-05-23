using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject RulesPanel;

	public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        MainMenuPanel.SetActive(true);
    }

    public void ToggleRules()
    {
        if (RulesPanel.activeSelf)
        {
            RulesPanel.SetActive(false);
        } else
        {
            RulesPanel.SetActive(true);
        }
    }
}
