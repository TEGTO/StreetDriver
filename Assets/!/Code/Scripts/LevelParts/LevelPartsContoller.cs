using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace LevelPartsNS
{
    [Serializable]
    public class LevelPartPool
    {
        public GameObject LevelPartPrefab;
        public int PoolAmount;
    }
    public class LevelPartsContoller : MonoBehaviour
    {
        public static LevelPartsContoller Instance { get; protected set; }

        [SerializeField]
        private LevelPartPool[] levelPartsPrefabs;
        [SerializeField]
        private LevelPart startLevelPart;
        [SerializeField, Min(1)]
        private int partsStartShift = 3;
        [SerializeField, Min(2)]
        private int amountPartsAtMoment = 7;
        [SerializeField, Min(1)]
        private int spawnBarrierAfterParts = 2;
        [SerializeField]
        private GameObject barrierPref;

        private GameObject startBarrier;
        private GameObject endBarrier;
        private LevelPart currentLevelPart;
        private List<LevelPart> levelParts = new List<LevelPart>();
        private List<LevelPart> prevActiveLevelParts = new List<LevelPart>();
        private List<LevelPart> activeParts = new List<LevelPart>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        private void Start()
        {
            InstantiateLevelParts();
            ShuffleLevelParts();
            InitializeBarriers();
            SpawnFirstLocations();
        }
        private void InstantiateLevelParts()
        {
            foreach (var levelPart in levelPartsPrefabs)
            {
                for (int i = 0; i < levelPart.PoolAmount; i++)
                {
                    GameObject go = GameObject.Instantiate(levelPart.LevelPartPrefab, transform);
                    if (go.TryGetComponent(out LevelPart part))
                    {
                        go.SetActive(false);
                        go.transform.parent = transform;
                        levelParts.Add(part);
                    }
                }
            }
        }
        private void SpawnFirstLocations()
        {
            currentLevelPart = startLevelPart;
            activeParts.Add(startLevelPart);
            for (int i = 0; i < partsStartShift; i++)
                SpawnNextPart();
        }
        public void SpawnNextPart()
        {
            LevelPart newLevelPart = GetNewLevelPartFromPool();
            Transform newLevelPartTransform = currentLevelPart.EndPartTrigger.NextPartSpawnTransform;
            newLevelPart.gameObject.transform.SetPositionAndRotation(newLevelPartTransform.position, newLevelPartTransform.rotation);
            newLevelPart.gameObject.SetActive(true);
            activeParts.Add(newLevelPart);
            currentLevelPart = newLevelPart;
            DisableOldestLevelPart();
            SetStartBarrierTransform();
            SetEndBarrierTransform();
        }
        public Transform GetTeleportTransform()
        {
            int partIndex = spawnBarrierAfterParts;
            Transform teleportTransf = activeParts[partIndex].EndPartTrigger.transform;
            return teleportTransf;
        }
        private LevelPart GetNewLevelPartFromPool()
        {
            if (levelParts.Count == 0)
            {
                levelParts = prevActiveLevelParts.Where(x => !x.gameObject.activeInHierarchy).ToList();
                prevActiveLevelParts.RemoveAll(x => levelParts.Any(y => y == x));
            }
            LevelPart newLevelPart = levelParts[UnityEngine.Random.Range(0, levelParts.Count)];
            levelParts.Remove(newLevelPart);
            prevActiveLevelParts.Add(newLevelPart);
            return newLevelPart;
        }
        private void DisableOldestLevelPart()
        {
            if (activeParts.Count > amountPartsAtMoment)
            {
                LevelPart lastLevelPart = activeParts.First();
                lastLevelPart.gameObject.SetActive(false);
                activeParts.Remove(lastLevelPart);
            }
        }
        private void InitializeBarriers()
        {
            startBarrier = GameObject.Instantiate(barrierPref);
            endBarrier = GameObject.Instantiate(barrierPref);
            Transform barrierTran = startLevelPart.StartPartTrigger.transform;
            startBarrier.transform.SetPositionAndRotation(barrierTran.position, barrierTran.rotation);
        }
        private void SetStartBarrierTransform()
        {
            if (activeParts.Count >= amountPartsAtMoment)
            {
                int partIndex = spawnBarrierAfterParts - 1;
                Transform barrierTran = activeParts[partIndex].StartPartTrigger.transform;
                startBarrier.transform.SetPositionAndRotation(barrierTran.position, barrierTran.rotation);
            }
        }
        private void SetEndBarrierTransform()
        {
            Transform barrierTran = activeParts.Last().EndPartTrigger.transform;
            endBarrier.transform.SetPositionAndRotation(barrierTran.position, barrierTran.rotation);
        }
        private void ShuffleLevelParts()
        {
            Random random = new Random();
            levelParts = levelParts.OrderBy(x => random.Next()).ToList();
        }
    }
}
