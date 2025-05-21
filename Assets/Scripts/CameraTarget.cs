using UnityEngine;

public class FightingGameCamera : MonoBehaviour
{
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    public float minZoom = 50f;  
    public float maxZoom = 100f; 
    public float zoomSpeed = 2f;
    public float followSpeed = 5f;
    public Vector3 cameraOffset = new Vector3(0, 1, 1.59f); 

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1")?.transform;
        player2 = GameObject.FindGameObjectWithTag("Player2")?.transform;

        if (player1 == null || player2 == null) return;

        Vector3 midPoint = (player1.position + player2.position) / 2f;
        midPoint.z -= 5f;
        transform.position = Vector3.Lerp(transform.position, midPoint + cameraOffset, followSpeed * Time.deltaTime);

        float distance = Vector3.Distance(player1.position, player2.position);
        float targetZoom = Mathf.Clamp(distance, minZoom, maxZoom);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, zoomSpeed * Time.deltaTime);
    }
}
