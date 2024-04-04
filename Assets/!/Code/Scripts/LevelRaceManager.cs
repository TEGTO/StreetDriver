using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RaceNS
{
    public class LevelRaceManager : MonoBehaviour
    {
        public static LevelRaceManager Instance { get; private set; }

        [SerializeField]
        private List<RacePoint> racePoints;
        [SerializeField]
        private float startTime = 20;
        [SerializeField]
        private float timeForPoint = 10;

        private bool isRaceStarted = false;

        public Action OnGameStart;
        public Action OnGameFinish;

        public RacePoint CurrentPoint { get; private set; }
        public float Timer { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        private void Start()
        {
            racePoints.ForEach(x => x.DisablePoint());
            Timer = startTime;
        }
        private void Update()
        {
            if (Timer > 0 && isRaceStarted)
                Timer -= Time.deltaTime;
            else if (Timer < 0)
                FinishGame();
        }
        public void CrossedPoint(RacePoint racePoint)
        {
            if (racePoint == CurrentPoint)
            {
                racePoints.Remove(racePoint);
                racePoint.DisablePoint();
                Timer += timeForPoint;
                if (racePoints.Count == 0)
                    FinishGame();
                else
                {
                    CurrentPoint = racePoints.FirstOrDefault();
                    CurrentPoint.EnablePoint();
                }
            }
        }
        public void StartRace()
        {
            CurrentPoint = racePoints.FirstOrDefault();
            CurrentPoint.EnablePoint();
            OnGameStart?.Invoke();
            isRaceStarted = true;
        }
        private void FinishGame()
        {
            OnGameFinish?.Invoke();
            isRaceStarted = false;
            Timer = 0;
        }
    }
}
