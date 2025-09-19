using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
   
    [SerializeField] private List<ClipData> audioClipDataList;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayAudio(AudioType audioType)
    {
        ClipData clipData = audioClipDataList.Where(t=> t.type == audioType).FirstOrDefault();
        if (clipData != null)
        {
            audioSource.PlayOneShot(clipData.clip);
        }
    }
}

public enum AudioType
{
    NONE = 0,
    CARD_FLIP = 1,
    CARD_MATCHED = 2,
    CARD_UNMATCHED = 3,
    GAME_LOSE = 4,
    LEVEL_DONE = 5,
    BUTTON_CLICK = 6,
}

[System.Serializable]
public class ClipData
{
    public AudioType type;
    public AudioClip clip;

}
