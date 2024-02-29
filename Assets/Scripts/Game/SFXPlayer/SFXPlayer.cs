using UnityEngine;

public enum SFXClip
{
    None = 0,
    CollectLetter = 10,
    Kick = 20,
    NutCracking = 30,
    TailSlap = 40,
    Stunned = 50
}

public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip collectLetter;

    [SerializeField]
    AudioClip kick;

    [SerializeField]
    AudioClip nutCracking;

    [SerializeField]
    AudioClip tailSlap;

    [SerializeField]
    AudioClip stunned;

    [SerializeField]
    GameObject audioSourcePrefab;

    public GameObject InstantiateAudioPlayer(Transform transform)
    {
        return Instantiate(audioSourcePrefab, gameObject.transform);
    }

    public void PlaySFXByType(SFXClip clip, GameObject go)
    {
        var audioSourceGO = InstantiateAudioPlayer(go.transform);
        var audioSource = audioSourceGO.GetComponent<AudioSource>();

        audioSourcePrefab.transform.position = go.transform.position;
        audioSource.clip = GetAudioClipByType(clip);
        audioSource.Play();

        float duration = audioSource.clip.length + 0.5f;

        Destroy(audioSourceGO, duration);
    }

    private AudioClip GetAudioClipByType(SFXClip clip)
    {
        AudioClip audioClip = null;

        switch (clip)
        {
            case SFXClip.None:
                audioClip = null;
                break;
            case SFXClip.CollectLetter:
                audioClip = collectLetter;
                break;
            case SFXClip.Kick:
                audioClip = kick;
                break;
            case SFXClip.NutCracking:
                audioClip = nutCracking;
                break;
            case SFXClip.TailSlap:
                audioClip = tailSlap;
                break;
            case SFXClip.Stunned:
                audioClip = stunned;
                break;
        }

        return audioClip;
    }
}
