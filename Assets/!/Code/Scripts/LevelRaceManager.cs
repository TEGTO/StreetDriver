using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaceNS
{
    public class LevelRaceManager : MonoBehaviour
    {
        public static LevelRaceManager Instance { get; private set; }

        [SerializeField]
        private float startTime = 20;
        [SerializeField]
        private float timeForPoint = 10;
        [SerializeField]
        private float minPointSpawnChance = 50;
        [SerializeField]
        private float timeToMaxDifficulty = 300;

        public Action OnGameStart;
        public Action OnGameFinish;

        private float raceStartTime;

        public float LeftTime { get; private set; }
        public float DifficultyCoefficient { get => (Time.fixedTime - raceStartTime) / timeToMaxDifficulty; }
        public HashSet<RacePoint> RacePoints { get; private set; } = new HashSet<RacePoint>();
        public bool IsRaceStarted { get; private set; }
        public Action<RacePoint> OnPlayerCrossPoint { get; set; }
        public Action<RacePoint> OnNonPlayerCrossPoint { get; set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        private void OnDestroy()
        {
            Instance = null;
        }
        private void Start()
        {
            LeftTime = startTime;
        }
        private void Update()
        {
            if (LeftTime > 0 && IsRaceStarted)
                LeftTime -= Time.deltaTime;
            else if (LeftTime < 0)
                FinishGame();
        }
        public void CrossedPoint(RacePoint racePoint)
        {
            racePoint.DisablePoint();
            LeftTime += timeForPoint;
            OnPlayerCrossPoint?.Invoke(racePoint);
        }
        public void CrossedPointNoNPlayer(RacePoint racePoint)
        {
            racePoint.DisablePoint();
            OnNonPlayerCrossPoint?.Invoke(racePoint);
        }
        public void AddPoint(RacePoint racePoint)
        {
            if (SpawnRacePointByDifficulty())
            {
                RacePoints.Add(racePoint);
                racePoint.EnablePoint();
            }
        }
        public void StartRace()
        {
            OnGameStart?.Invoke();
            IsRaceStarted = true;
            raceStartTime = Time.fixedTime;
        }
        private void FinishGame()
        {
            OnGameFinish?.Invoke();
            IsRaceStarted = false;
            LeftTime = 0;
        }
        private bool SpawnRacePointByDifficulty() => Mathf.Lerp(100, minPointSpawnChance, DifficultyCoefficient) > UnityEngine.Random.Range(0, 100);
    }
}
