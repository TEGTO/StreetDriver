using AICars;
using UnityEngine;

namespace RaceNS
{
    public class RaceStarter : MonoBehaviour
    {
        [SerializeField]
        private GameObject mobileUi;

        private LevelRaceManager levelRaceManager;
        private AICarsManager aICarsManager;

        private void Start()
        {
            mobileUi.SetActive(false);
        }
        public void StartRace()
        {
            GetInstances();
            mobileUi.SetActive(true);
            levelRaceManager.StartRace();
            aICarsManager.EnableAICars();
        }
        private void GetInstances()
        {
            levelRaceManager = LevelRaceManager.Instance;
            aICarsManager = AICarsManager.Instance;
        }
    }
}
