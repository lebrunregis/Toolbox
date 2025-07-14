using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CutsceneDirector : MonoBehaviour
{
    #region Publics

    #endregion


    #region Unity Api

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < playables.Count; i++)
        {
            playables[i].enabled = false;
        }
    }

    #endregion


    #region Main Methods
    public void PlayCutscene(int id)
    {
        playables[id].enabled = true;
        playables[id].Play();
    }

    public void StopCutscene(int id)
    {
        playables[id].Stop();
    }
    #endregion


    #region Utils

    #endregion


    #region Private and Protected
    public List<PlayableDirector> playables;
    #endregion
}
