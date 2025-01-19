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



using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        // Poți adăuga un efect de moarte sau distrugere aici
        Destroy(gameObject);
    }
}




using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 100f;
    public int damage = 20;

    public ParticleSystem gunFireEffect; // Efectul de foc al armei
    public AudioSource gunSound; // Sunetul armelor

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Fire1 este în mod normal butonul stâng al mouse-ului
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Efectul de foc
        gunFireEffect.Play();
        gunSound.Play();

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Verifică dacă am lovit un inamic
            if (hit.collider.CompareTag("Enemy"))
            {
                // Aplică damage
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
}
