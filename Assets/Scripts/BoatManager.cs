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


    private List<BoatAutoPilot> boatsList = new List<BoatAutoPilot>();
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
        for (int i = 0; i < BoatPara.Spawning_Count; i++)
        {
            CreateBoat(Vector3.zero);
        }
    }

    //private void SpawnBoat(Vector3 worldPosition, Quaternion worldRotation)
    //{
    //    // Le bateau qu'on va instancier
    //    GameObject boatToInstanciate = GetRandomBoat();

    //    // Créer une instance (qu'on attache directement en enfant de notre transform)
    //    GameObject boatInstance = Instantiate(boatToInstanciate, worldPosition, worldRotation, transform);

    //    // Rajouter cette instance à notre liste d'instances
    //    boatsInstances.Add(boatInstance);
    //}
    private Vector3 GetRandomWorldPosition()
    {
        float x = Random.Range(-BoatPara.Width * 0.5f, BoatPara.Width * 0.5f);
        float z = Random.Range(-BoatPara.Length * 0.5f, BoatPara.Length * 0.5f);

        return transform.position + new Vector3(x, 0f, z);
    }
    private void CreateBoat(Vector3 WorldPosition)
    {
        int randomIndex = Random.Range(0, BoatPrefab.Length);
        GameObject boat = BoatPrefab[randomIndex];

        GameObject boatInstance = Instantiate(boat, transform);
        boatInstance.name = boat.name;
        boatInstance.transform.position = GetRandomWorldPosition();

        boatsInstances.Add(boatInstance);
        boatsList.Add(boatInstance.GetComponent<BoatAutoPilot>());
    }
    

    private void LateUpdate()
    {
        BorderPatrol();
    }

    private void BorderPatrol()
    {
        // On vérifie que nos bateaux sont dans la zone de jeu
        // On les téléporte au côté opposé s'ils en sortent
        int boatCount = boatsList.Count;
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