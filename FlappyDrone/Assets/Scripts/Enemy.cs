using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float deadZone = -5f;

    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {           
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlaySoundEffect("Hitmarker");
        Destroy(gameObject);
    }

    public void PlaySoundEffect(string soundName)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(soundName);
        }
        else
        {
            Debug.LogWarning("SoundManager не найден. Ќе удалось воспроизвести звук: " + soundName);
        }
    }
}
