using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
