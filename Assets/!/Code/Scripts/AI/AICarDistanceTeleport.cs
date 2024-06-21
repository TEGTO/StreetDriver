using LevelPartsNS;
using RaceNS;
using System.Collections;
using UnityEngine;

namespace AICars
{
    public class AICarDistanceTeleport : MonoBehaviour
    {
        [SerializeField]
        private float farDistanceWhenTeleport = 1000f;
        [SerializeField]
        private float playerLeaderDistanceTeleport = 200f;
        [SerializeField]
        private float updateRate = 0.3f;

        private Transform playerTransform;
        private LevelPartsContoller levelPartsContoller;
        private LevelRaceManager levelRaceManager;
        private RCC_SceneManager rCC_SceneManager;
        private Quaternion startRotation;

        private void Start()
        {
            levelPartsContoller = LevelPartsContoller.Instance;
            startRotation = transform.rotation;
            StartCoroutine(CheckDistance());
        }
        private void OnEnable()
        {
            levelRaceManager = LevelRaceManager.Instance;
            levelRaceManager.OnPlayerCrossPoint += TeleportIfPlayerLeader;
        }
        private void OnDisable()
        {
            levelRaceManager.OnPlayerCrossPoint -= TeleportIfPlayerLeader;
        }
        private IEnumerator CheckDistance()
        {
            while (true)
            {
                if (!playerTransform)
                    GetTransforms();
                else
                    TryTeleport(farDistanceWhenTeleport);
                yield return new WaitForSeconds(updateRate);
            }
        }
        private void TeleportCloserToPlayer()
        {
            Transform teleportTransform = levelPartsContoller.GetTeleportTransform();
            transform.SetPositionAndRotation(teleportTransform.position, startRotation);
        }
        private void GetTransforms()
        {
            if (!rCC_SceneManager)
                rCC_SceneManager = FindAnyObjectByType<RCC_SceneManager>();
            if (rCC_SceneManager && rCC_SceneManager.activePlayerVehicle)
                playerTransform = rCC_SceneManager.activePlayerVehicle.transform;
        }
        private void TeleportIfPlayerLeader(RacePoint racePoint)
        {
            TryTeleport(playerLeaderDistanceTeleport);
        }
        private void TryTeleport(float distance)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) >= distance)
                TeleportCloserToPlayer();
        }
    }
}