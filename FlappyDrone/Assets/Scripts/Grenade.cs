using UnityEngine;

public class Grenade : MonoBehaviour {

    // Variables
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float deadZone = -7f;

    // References
    private ParticleSystem explosionParticles;

    private void Start()
    {
        explosionParticles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        explosionParticles.Play();
        Debug.Log("ÁÀÁÀÕ ÕÅÕÅÕÅ");
    }
}