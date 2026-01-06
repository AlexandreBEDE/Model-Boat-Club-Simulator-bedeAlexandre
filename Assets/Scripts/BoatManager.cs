using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    private static BoatManager singleton = null;
    public SO_BoatManager BoatPara;

    [SerializeField]
    private GameObject[] BoatPrefab = new GameObject[0];
    public static BoatManager Singleton
    {
        get
        {
            return singleton;
        }

        private set
        {
            singleton = value;
        }
    }

    

    [SerializeField]
    private GameObject boatHouseA = null;

    [SerializeField]
    private GameObject boatHouseB = null;

    [SerializeField]
    private GameObject boatHouseC = null;


    private List<GameObject> boatsInstances = new List<GameObject>();

    private void Awake()
    {
        singleton = this;
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }

    private void Start()
    {
        // On génère un nombre de bateau au départ.
        for (int i = 0; i < BoatPara.Spawning_Count; i++)
        {
            // On choisi une position et une orientation au hasard dans la zone de jeu.
            Vector3 randomPosition = new Vector3((Random.value - 0.5f) * BoatPara.Width, 0f, (Random.value - 0.5f) * BoatPara.Length);
            Quaternion randomRotation = Quaternion.Euler(0f, Random.value * 360f, 0f);
            SpawnBoat(randomPosition, randomRotation);
        }
    }

    private void SpawnBoat(Vector3 worldPosition, Quaternion worldRotation)
    {
        // Le bateau qu'on va instancier
        GameObject boatToInstanciate = GetRandomBoat();

        // Créer une instance (qu'on attache directement en enfant de notre transform)
        GameObject boatInstance = Instantiate(boatToInstanciate, worldPosition, worldRotation, transform);

        // Rajouter cette instance à notre liste d'instances
        boatsInstances.Add(boatInstance);
    }

    private GameObject GetRandomBoat()
    {
        GameObject randomBoat = null;

        // On prend une variable aléatoire entre 0.000 et 1.000
        float randomValue = Random.value;
        if (randomValue < 0.333f)
        {
            randomBoat = boatHouseA;
        }
        else if (randomValue < 0.666f)
        {
            randomBoat = boatHouseB;
        }
        else
        {
            randomBoat = boatHouseC;
        }
        // TODO Je ne vais pas rajouter un "if..else" pour chaque nouveau bateau ?!
        // Il devrait y avoir un moyen de réunir mes Prefab dans une liste et
        // d'en choisir un selon sa place dans la liste...

        return randomBoat;
    }

    private void LateUpdate()
    {
        BorderPatrol();
    }

    private void BorderPatrol()
    {
        // On vérifie que nos bateaux sont dans la zone de jeu
        // On les téléporte au côté opposé s'ils en sortent
        int boatCount = boatsInstances.Count;
        for (int i = 0; i < boatCount; i++)
        {
            GameObject boatInstance = boatsInstances[i];
            Vector3 localPosition = boatInstance.transform.localPosition;
            bool positionHasChanged = false;

            // Left border?
            if (localPosition.x < -BoatPara.Width * 0.5f)
            {
                localPosition.x += BoatPara.Width;
                positionHasChanged = true;
            }
            // Right border?
            else if (localPosition.x > BoatPara.Width * 0.5f)
            {
                localPosition.x -= BoatPara.Width;
                positionHasChanged = true;
            }

            // Top border?
            if (localPosition.z > BoatPara.Length * 0.5f)
            {
                localPosition.z -= BoatPara.Length;
                positionHasChanged = true;
            }
            // Bottom border?
            else if (localPosition.z < -BoatPara.Length * 0.5f)
            {
                localPosition.z += BoatPara.Length;
                positionHasChanged = true;
            }

            if (positionHasChanged)
            {
                boatInstance.transform.localPosition = localPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        // Draw top border
        Gizmos.DrawWireCube(transform.position, new Vector3(BoatPara.Width, 0f, BoatPara.Length));
    }
}