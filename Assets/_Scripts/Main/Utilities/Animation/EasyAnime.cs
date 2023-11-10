using System;
using UnityEngine;

public abstract class EasyAnime : MonoBehaviour
{
    protected Action _animeEnd = null;

    public abstract void Play(string name, Action animeEnd);

    public abstract bool HasAnime(string name);

    protected void SetAnimeEnd(Action animeEnd)
    {
        this._animeEnd = animeEnd;
    }

    /// <summary>
    /// Anime event name (Set anime event on clip called AnimeEnd)
    /// </summary>
    protected virtual void AnimeEnd()
    {
        this._animeEnd?.Invoke();
        this._animeEnd = null;

        Debug.Log($"<color=#b6ff75>[EasyAnime] Root: {this.transform.root.name} trigger AnimeEnd event function</color>");
    }
}
