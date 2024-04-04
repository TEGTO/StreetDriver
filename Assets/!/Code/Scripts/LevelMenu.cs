using RaceNS;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private GameObject levelFinishedText;
        [SerializeField]
        private TextMeshProUGUI timeText;
        [SerializeField]
        private string selectMenuName;

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
            levelRaceManager.OnGameFinish -= LevelFinished;
            levelRaceManager.OnGameStart -= StartGame;
        }
        private void Update()
        {
            if (!isGameFinished)
                timeText.text = ((int)levelRaceManager.Timer).ToString();
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
        }
        public void LevelFinished()
        {
            EnableMenu();
            isGameFinished = true;
            levelFinishedText.SetActive(true);
            if (rCC_UIDashboardDisplay)
                rCC_UIDashboardDisplay.displayType = RCC_UIDashboardDisplay.DisplayType.Off;
        }
        public void CloseMenu()
        {
            if (!isGameFinished)
            {
                menuPanel.SetActive(false);
                closeMenuPanel.SetActive(false);
                openMenuButton.SetActive(true);
            }
        }
        private void StartGame()
        {
            isGameFinished = false;
        }
    }
}
