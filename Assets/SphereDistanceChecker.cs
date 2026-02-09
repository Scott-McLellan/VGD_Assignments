using UnityEngine;

public class SphereDistanceChecker : MonoBehaviour
{
    GameObject[] allSpheres;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allSpheres =  GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject sphere in allSpheres)
        {
            Debug.Log(sphere.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
