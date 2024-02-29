using UnityEngine;
using UnityEngine.Playables;

public class WalnutSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject walnut;

    private PlayableDirector playableDirector;

    private float force = 10.0f;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
        
        SetRandomStartTime();
        playableDirector.Play();
    }

    private void SetRandomStartTime()
    {
        float randomTime = Random.Range(0.0f, (float)playableDirector.duration);

        playableDirector.initialTime = randomTime;
    }

    public void SpawnWalnut()
    {
        var spawnedWalnut = Instantiate(walnut, GetComponentInParent<Transform>());

        Vector3 randomForce = new Vector3(1.0f, 0, 1.0f);
        randomForce.x = Random.Range(-randomForce.x, randomForce.x);
        randomForce.z = Random.Range(-randomForce.z, randomForce.z);

        randomForce *= force;

        spawnedWalnut.GetComponent<Rigidbody>().AddRelativeForce(randomForce);
    }
}
