using AICars;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RaceNS
{
    public class RandomCarsLocation : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> positions;
        [SerializeField]
        private float spawnAfterSeconds = 0.1f;

        private List<RCC_CarControllerV3> cars = new List<RCC_CarControllerV3>();

        private void Start()
        {
            GetCars();
            StartCoroutine(SpawnAfter());
        }
        private void GetCars()
        {
            RCC_SceneManager rCC_SceneManager = FindAnyObjectByType<RCC_SceneManager>();
            cars.Add(rCC_SceneManager.activePlayerVehicle);
            AICarsManager.Instance.AICarControllers.ForEach(x => cars.Add(x.GetComponent<RCC_CarControllerV3>()));
        }
        private void SetRandomPosition()
        {
            System.Random random = new System.Random();
            positions = positions.OrderBy(x => random.Next()).ToList();
            for (int i = 0; i < cars.Count; i++)
                RCC.Transport(cars[i], positions[i].position, positions[i].rotation);
        }
        private IEnumerator SpawnAfter()
        {
            yield return new WaitForSeconds(spawnAfterSeconds);
            SetRandomPosition();
        }
    }
}