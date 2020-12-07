using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class TimeLineController : MonoBehaviour {
    [SerializeField] PlayableDirector _winConditionPlayableDirector;

    public void PlayWinConditionAnimation() {
        _winConditionPlayableDirector.Play();
    }

    public void StopWinConditionAnimation() {
        _winConditionPlayableDirector.Stop();
    }
}
