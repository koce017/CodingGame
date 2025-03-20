using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public float movementSpeed = 100f;

    public float zoomSensitivity = 50f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.position += movementSpeed * Time.deltaTime * transform.up;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.position -= movementSpeed * Time.deltaTime * transform.up;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.position -= movementSpeed * Time.deltaTime * transform.right;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.position += movementSpeed * Time.deltaTime * transform.right;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            transform.position += movementSpeed * Time.deltaTime * transform.forward;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            transform.position -= movementSpeed * Time.deltaTime * transform.forward;   

        var axis = Input.GetAxis("Mouse ScrollWheel");
        
        if (axis != 0)
            transform.position += axis * zoomSensitivity * transform.forward;
    }

}
