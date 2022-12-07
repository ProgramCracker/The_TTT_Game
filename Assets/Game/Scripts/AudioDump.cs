using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDump : MonoBehaviour
{
    [SerializeField] AudioClip _winAudio;
    [SerializeField] AudioClip _loseAudio;
    [SerializeField] AudioClip _tieAudio;

    [SerializeField] AudioSource _source;

    public void Tie()
    {
        _source.clip = _tieAudio;
    }

    public void Win()
    {
        _source.clip = _winAudio;
    }

    public void Lose()
    {
        _source.clip = _loseAudio;
    }
}
