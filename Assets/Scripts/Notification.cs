using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    public TMP_Text trackName;
    public TMP_Text artistName;
    public TMP_Text albumtName;
    public Image albumCoverImage;
    public Button skipButton;

    public Animator animator;

    public void Init(UnityAction onSkip)
    {
        skipButton.onClick.AddListener(onSkip);
    }

    public void SetTrackInfo(string name, string artist, string album, Sprite albumCover)
    {
        trackName.text = name;
        artistName.text = artist;
        albumtName.text = album;
        albumCoverImage.sprite = albumCover;
    }

    public void SlideIn()
    {
        animator.SetTrigger("SlideIn");
    }

    public void SlideOut()
    {
        animator.SetTrigger("SlideOut");
    }
}