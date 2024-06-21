using RaceNS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityMethodsNS;

namespace UINS
{
    public class LevelMenu : OnEnableMethodAfterStart
    {
        [SerializeField]
        private GameObject menuPanel;
        [SerializeField]
        private GameObject openMenuButton;
        [SerializeField]
        private GameObject closeMenuPanel;
        [SerializeField]
        private GameObject levelFinishedPanel;
        [SerializeField]
        private TextMeshProUGUI timeText;
        [SerializeField]
        private string selectMenuName;
        [SerializeField]
        private List<GameObject> disableElementsOnPanelOpen;

        private bool isGameFinished = false;
        private RCC_UIDashboardDisplay rCC_UIDashboardDisplay;
        private LevelRaceManager levelRaceManager;

        protected override void OnEnableAfterStart()
        {
            rCC_UIDashboardDisplay = FindAnyObjectByType<RCC_UIDashboardDisplay>();
            levelRaceManager = LevelRaceManager.Instance;
            levelRaceManager.OnGameStart += StartGame;
            levelRaceManager.OnGameFinish += LevelFinished;
        }
        private void OnDisable()
        {
            if(levelRaceManager)
            {
                levelRaceManager.OnGameFinish -= LevelFinished;
                levelRaceManager.OnGameStart -= StartGame;
            }
        }
        private void Update()
        {
            if (!isGameFinished)
                timeText.text = ((int)levelRaceManager.LeftTime).ToString();
            else
                timeText.text = "";
        }
        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        public void ToCarSelectMenu() => SceneManager.LoadScene(selectMenuName);
        public void EnableMenu()
        {
            closeMenuPanel.SetActive(true);
            menuPanel.SetActive(true);
            openMenuButton.SetActive(false);
            disableElementsOnPanelOpen.ForEach(x => x.SetActive(false));
        }
        public void LevelFinished()
        {
            isGameFinished = true;
            levelFinishedPanel.SetActive(true);
            menuPanel.SetActive(false);
            openMenuButton.SetActive(false);
            closeMenuPanel.SetActive(false);
            if (rCC_UIDashboardDisplay)
                rCC_UIDashboardDisplay.displayType = RCC_UIDashboardDisplay.DisplayType.Off;
        }
        public void CloseMenu()
        {
            menuPanel.SetActive(false);
            closeMenuPanel.SetActive(false);
            openMenuButton.SetActive(true);
            disableElementsOnPanelOpen.ForEach(x => x.SetActive(true));
        }
        private void StartGame()
        {
            isGameFinished = false;
        }
    }
}
