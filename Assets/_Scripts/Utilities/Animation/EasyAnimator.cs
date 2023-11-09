using System;
using UnityEngine;

public class EasyAnimator : EasyAnime
{
    [SerializeField]
    protected Animator _animator = null;

    private void Awake()
    {
        if (this._animator == null) this._animator = this.GetComponent<Animator>();
    }

    public Animator GetAnimation()
    {
        return this._animator;
    }

    public override void Play(string paramName, Action animeEnd)
    {
        // Set anime end callback
        this.SetAnimeEnd(animeEnd);

        if (this.HasAnime(paramName))
        {
            // Reset first to make sure is clear param set
            this._animator.ResetTrigger(paramName);

            // Play animation by param name
            this._animator?.SetTrigger(paramName);
        }
        // If cannot found param name just call end back directly
        else this.AnimeEnd();
    }

    public override bool HasAnime(string paramName)
    {
        return this._animator.ContainsParam(paramName);
    }
}
