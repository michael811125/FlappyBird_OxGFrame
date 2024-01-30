using System;
using UnityEngine;

public class EasyAnimation : EasyAnime
{
    [SerializeField]
    protected Animation _animation = null;

    private void Awake()
    {
        if (this._animation == null) this._animation = this.GetComponent<Animation>();
    }

    public Animation GetAnimation()
    {
        return this._animation;
    }

    public override void Play(string animeName, Action animeEnd)
    {
        // Set anime end callback
        this.SetAnimeEnd(animeEnd);

        if (this.HasAnime(animeName))
        {
            // Play animation by anime name
            this._animation?.Play(animeName);
        }
        // If cannot found anime name just call end back directly
        else this.AnimeEnd();
    }

    public override bool HasAnime(string animeName)
    {
        return this._animation.GetClip(animeName) != null;
    }
}
