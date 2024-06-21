using RaceNS;
using UnityEngine;

namespace LevelPartsNS
{
    public class EndPartTrigger : MonoBehaviour
    {
        [SerializeField]
        private Transform nextPartSpawnTransform;
        [SerializeField]
        private LayerMask proceedLayer;

        private LevelPartsContoller levelPartsSpawner;
        private bool isActive = true;
        private RCC_SceneManager rCC_SceneManager;

        public Transform NextPartSpawnTransform { get => nextPartSpawnTransform; }

        private void Awake()
        {
            rCC_SceneManager = FindAnyObjectByType<RCC_SceneManager>();
        }
        private void OnEnable()
        {
            isActive = true;
        }
        private void OnDisable()
        {
            isActive = true;
        }
        private void Start()
        {
            levelPartsSpawner = LevelPartsContoller.Instance;
        }
        private void OnTriggerEnter(Collider other)
        {
            if ((proceedLayer & (1 << other.gameObject.layer)) != 0 && isActive)
            {
                if (other.attachedRigidbody.GetComponent<RCC_CarControllerV3>() == rCC_SceneManager.activePlayerVehicle)
                {
                    levelPartsSpawner.SpawnNextPart();
                    isActive = false;
                }
            }
        }
    }
}
