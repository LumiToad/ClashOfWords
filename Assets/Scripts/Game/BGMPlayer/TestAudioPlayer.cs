using UnityEngine;

public class TestAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip victoryBGM;

    private AudioSource audioSource;

    [SerializeField]
    private bool isNotDestroyedOnLoad = false;

    private void Start()
    {
        if (isNotDestroyedOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayVictoryBGM()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = victoryBGM;
        audioSource.loop = false;
        audioSource.Play();
    }
}
