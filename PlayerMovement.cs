using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;

    private float rotationX = 0;

    void Update()
    {
        // Mișcarea jucătorului
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        movement = transform.TransformDirection(movement); // Transformă mișcarea în direcția camerei
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Mișcarea camerei (privirea în jur)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotirea pe axa Y (orizontal)
        transform.Rotate(Vector3.up * mouseX * sensitivity);

        // Rotirea pe axa X (vertical)
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // Limitează mișcarea pentru a nu face capul prea sus sau jos
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
