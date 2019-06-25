// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// Set AudioClip to AudioSound and play it
    /// </summary>
    public class SoundItem
    {
        private AudioSource _audioSource;

        public SoundItem(AudioSource audio, AudioClip clip)
        {
            if (audio == null) { Debug.LogError("AudioSource is null"); return; }
            if (clip == null) { Debug.LogError("AudioClip is null"); return; }
            _audioSource = audio;
            _audioSource.clip = clip;
        }

        public void PlaySound()
        {
            _audioSource.Stop();
            _audioSource.Play();
        }

        public void StopPlay()
        {
            _audioSource.Stop();
        }

        public bool IsPlaying()
        {
            return _audioSource.isPlaying;
        }
    }
}
