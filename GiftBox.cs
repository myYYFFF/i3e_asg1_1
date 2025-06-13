using UnityEngine;

public class GiftBox : MonoBehaviour
{
    public GameObject coin;
    public int numberOfCoins = 5;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            for (int i = 0; i < numberOfCoins; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfCoins;
                Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 0.5f;
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.2f, 0) + offset;

                Instantiate(coin, spawnPosition, Quaternion.identity);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
