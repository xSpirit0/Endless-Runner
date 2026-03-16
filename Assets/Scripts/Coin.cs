using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 50;
    public float rotateSpeed = 200f;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin collected! Adding: " + value);

            if (scoreManager != null)
                scoreManager.AddScore(value);

            AudioManager.instance.PlaySound(AudioManager.instance.coinSound);

            Destroy(gameObject);
        }
    }
}