using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    public CubeSpawnerAndScore gameManager;

    void OnCollisionEnter(Collision collision)
    {
        CheckHit(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        CheckHit(other.gameObject);
    }

    void CheckHit(GameObject hitObject)
    {
        // Si entra en contacto con las manos o el stick antiguo, suma y rompe
        if (hitObject.CompareTag("Stick") || hitObject.name.ToLower().Contains("hand") || hitObject.GetComponent<Rigidbody>() != null)
        {
            gameManager.AddScore(1);
            Destroy(gameObject);
        }
    }
}