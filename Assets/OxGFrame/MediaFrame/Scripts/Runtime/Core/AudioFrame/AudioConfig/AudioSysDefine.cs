﻿using System;
using UnityEngine;

namespace OxGFrame.MediaFrame.AudioFrame
{
    public enum SourceType
    {
        Audio,
        Streaming,
        Url
    }

    public enum MixerGroupSourceType
    {
        Assign,
        Find
    }

    public enum SoundType
    {
        Sole,
        SoundEffect
    }

    [Serializable]
    public class AudioType
    {
        public SoundType soundType = SoundType.SoundEffect;
        [Range(0, 256)] public int priority = 128;

        public AudioType(SoundType soundType = SoundType.SoundEffect, int priority = 128)
        {
            this.soundType = soundType;
            this.priority = priority;
        }
    }

    public class AudioSysDefine
    {
        public static readonly string AUDIO_MANAGER_NAME = "AudioManager";

        /**
         * url_cfg format following
         * audio_urlset http://localhost/audio/
         * # => comment
         */
    }
}