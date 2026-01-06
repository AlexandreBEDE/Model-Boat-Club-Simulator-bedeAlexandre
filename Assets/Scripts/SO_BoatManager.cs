using UnityEngine;

[CreateAssetMenu(fileName = "SO_BoatManager", menuName = "Scriptable Objects/SO_BoatManager")]

public class SO_BoatManager : ScriptableObject
{
    [SerializeField]
    private float width = 16f;

    [SerializeField]
    private float length = 9f;

    [SerializeField]
    [Range(0, 300)]
    private int SpawningCount;

    [SerializeField]
    [Range(0, 10)]
    private float maxSpeed = 6f;

    [SerializeField]
    [Range(0.1f, 45f)]
    private float steeringSpeed = 4.5f;

    [SerializeField]
    [Range(.01f, .5f)]
    private float maxForce = .03f;

    [SerializeField]
    [Range(1, 10)]
    private float neighborhoodRadius = 4f;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float separationRadius = 2.4f;

    [SerializeField]
    [Range(0, 3)]
    private float separationAmount = 1.1f;

    [SerializeField]
    [Range(0, 3)]
    private float cohesionAmount = 0.3f;

    [SerializeField]
    [Range(0, 3)]
    private float alignmentAmount = 0.5f;

    public float Width
    {
               get { return width; }
    }
    
    public float Length
    {
        get { return length; }
    }

    public int Spawning_Count
    {
        get { return SpawningCount; }
    }

    public float Max_Speed
    {
        get { return maxSpeed; }
    }   
    public float Steering_Speed
    {
        get { return steeringSpeed; }
    }
    public float Max_Force
    {
        get { return maxForce; }
    }
    public float Neighborhood_Radius
    {
        get { return neighborhoodRadius; }
    }
    public float Separation_Radius
    {
        get { return separationRadius; }
    }
    public float Separation_Amount
    {
        get { return separationAmount; }
    }
    public float Cohesion_Amount
    {
        get { return cohesionAmount; }
    }
    public float Alignment_Amount
    {
        get { return alignmentAmount; }
    }


}
