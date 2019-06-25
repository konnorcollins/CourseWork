// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System.Collections.Generic;
using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// Play sounds
    /// </summary>
    public class SoundController : MonoBehaviour
    {
        private Dictionary<AudioClip, SoundItem> _soundItems;

        public SoundController()
        {
            _soundItems = new Dictionary<AudioClip, SoundItem>();
        }

        public void PlaySound(AudioClip clip)
        {
            if (clip == null) return;

            SoundItem sound;
            if (_soundItems.Count == 0)
            {
                sound = new SoundItem(gameObject.AddComponent<AudioSource>(), clip);
                _soundItems.Add(clip, sound);
                sound.PlaySound();
                return;
            }

            if (_soundItems.TryGetValue(clip, out sound))
            {
                sound.PlaySound();
            }
            else
            {
                sound = new SoundItem(gameObject.AddComponent<AudioSource>(), clip);
                _soundItems.Add(clip, sound);
                sound.PlaySound();
            }
        }
    }
}
