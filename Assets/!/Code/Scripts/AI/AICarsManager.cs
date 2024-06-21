using RaceNS;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AICars
{
    public class AICarsManager : MonoBehaviour
    {
        public static AICarsManager Instance { get; protected set; }

        [SerializeField]
        private float startSpeedLimit = 100;
        [SerializeField]
        private float maxSpeedLimit = 300;
        [SerializeField]
        private float updateRate = 0.3f;

        private LevelRaceManager levelRaceManager;

        public List<RCC_AICarController> AICarControllers { get; private set ; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            GetAICars();
        }
        private void Start()
        {
            levelRaceManager = LevelRaceManager.Instance;
            StartCoroutine(SetAICarsDifficulty());
        }
        private void GetAICars()
        {
            AICarControllers = FindObjectsByType<RCC_AICarController>(FindObjectsSortMode.None).ToList();
            DisableAICars();
        }
        public void EnableAICars()
        {
            if (AICarControllers != null)
                AICarControllers.ForEach(x => x.enabled = true);
        }
        public void DisableAICars()
        {
            if (AICarControllers != null)
                AICarControllers.ForEach(x => x.enabled = false);
        }
        private IEnumerator SetAICarsDifficulty()
        {
            SetCarsLimitSpeed();
            while (true)
            {
                foreach (var car in AICarControllers)
                {
                    car.limitSpeed = true;
                    car.maximumSpeed = Mathf.Lerp(startSpeedLimit, maxSpeedLimit, levelRaceManager.DifficultyCoefficient);
                }
                yield return new WaitForSeconds(updateRate);
            }
        }
        private void SetCarsLimitSpeed() => AICarControllers.ForEach(x => x.limitSpeed = true);
    }
}