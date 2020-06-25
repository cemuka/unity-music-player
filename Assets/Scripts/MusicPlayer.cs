using UnityEngine;
using System.Collections;
using System;

public class MusicPlayer : MonoBehaviour
{
    public Transform notificationParent;
    public AudioSource audioSource;
    public float notificationDuration;

    private MusicPlayerResources _resources;
    private Notification _notification;
    private int _index;
    private bool _notificationActive;
    private float _timer;
    private IEnumerator _notificationTimerCoroutine;
    
    private void Start()
    {
        _index = -1;
        _notificationActive = false;
        _resources = Resources.Load<MusicPlayerResources>("MusicPlayerResources"); 

        var notificationPrefab = Instantiate(Resources.Load<GameObject>("Notification"), notificationParent);
        _notification = notificationPrefab.GetComponent<Notification>();
        _notification.Init(NextTrack);

        _notificationTimerCoroutine = NotificationSlideOutCoroutine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_resources.nextKey))
        {
            NextTrack();
        }

        if (Input.GetKeyDown(_resources.previousKey))
        {
            PreviousTrack();
        }
    }

    private void NextTrack()
    {
        if (_index == _resources.trackList.Length - 1)
        {
            _index = 0;
        }
        else
        {
            _index ++;
        }

        var track = _resources.trackList[_index];

        audioSource.Stop();
        audioSource.PlayOneShot(track.clip);
        
        _notification.SetTrackInfo(track.name, track.artist, track.album, track.albumCover);
        if (_notificationActive == false)
        {
            _notification.SlideIn();
            _notificationActive = true;
        }
        NotificationTimer();

        
    }

    private void PreviousTrack()
    {
        if (_index == 0 || _index < 0)
        {
            _index = _resources.trackList.Length - 1;
        }
        else
        {
            _index --;
        }

        var track = _resources.trackList[_index];

        audioSource.Stop();
        audioSource.PlayOneShot(track.clip);
        
        _notification.SetTrackInfo(track.name, track.artist, track.album, track.albumCover);
        if (_notificationActive == false)
        {
            _notification.SlideIn();
            _notificationActive = true;
        }
        NotificationTimer();
    }

    private void NotificationTimer()
    {
        StopCoroutine(_notificationTimerCoroutine);
        _notificationTimerCoroutine = NotificationSlideOutCoroutine();
        StartCoroutine(_notificationTimerCoroutine);   
    }

    private IEnumerator NotificationSlideOutCoroutine()
    {
        _timer = notificationDuration;
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }

        _notification.SlideOut();
        _notificationActive = false;

        yield return null;
    }
}