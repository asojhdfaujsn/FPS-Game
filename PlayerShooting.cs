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
