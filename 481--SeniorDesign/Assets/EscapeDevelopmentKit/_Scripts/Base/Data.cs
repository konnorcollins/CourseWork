// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// Save/get data to/from player prefs. 
    /// </summary>
    public static class Data
    {
        private static string _lastEndedLevelStr = "EscapeModules_Data_LastEndedScene";
        private static string _isOffSoundStr = "EscapeModules_Data_IsOffSound";
        private static string _volumeStr = "EscapeModules_Data_Volume";
        private static string _isHintsLimitedStr = "EscapeModules_Data_IsHintsLimited";
        private static string _numberOfHintsStr = "EscapeModules_Data_NumberOfHits";
        private static string _currentLevelStr = "EscapeModules_Data_CurrentLevel";
        private static string _currentLevelNameStr = "EscapeModules_Data_CurrentLevelName";
        public static bool IsHintsForCurrentLevel = false;
        public static void Init()
        {
            _numberOfHints = PlayerPrefs.HasKey(_numberOfHintsStr) ? PlayerPrefs.GetInt(_numberOfHintsStr) : 0;
            _isHintsLimited = (PlayerPrefs.HasKey(_isHintsLimitedStr) ? PlayerPrefs.GetInt(_isHintsLimitedStr) : 0) == 1;
            _isOffSound = false;
            _volume = PlayerPrefs.HasKey(_volumeStr) ? PlayerPrefs.GetFloat(_volumeStr) : 1;
            _numberOfHints = PlayerPrefs.HasKey(_numberOfHintsStr) ? PlayerPrefs.GetInt(_isHintsLimitedStr) : -1;
            _lastEndedLevel = PlayerPrefs.HasKey(_lastEndedLevelStr) ? PlayerPrefs.GetString(_lastEndedLevelStr) : "";
        }

        private static bool _isOffSound;
        public static bool IsOffSound
        {
            get { return _isOffSound; }
            set
            {
                PlayerPrefs.SetInt(_isOffSoundStr, value ? 1 : 0);
                _isOffSound = value;
            }
        }

        private static float _volume;
        public static float Volume
        {
            get { return _volume; }
            set
            {
                PlayerPrefs.SetFloat(_volumeStr, value);
                _volume = value;
            }
        }

        private static int _numberOfHints;
        public static int NumberOfHits
        {
            get { return _numberOfHints; }
            private set
            {
                if (IsHintsForCurrentLevel)
                    PlayerPrefs.SetInt(_numberOfHintsStr + _currentLevelName, value);
                else
                    PlayerPrefs.SetInt(_numberOfHintsStr, value);
                _numberOfHints = value;
            }
        }

        private static bool _isHintsLimited;
        public static bool IsHintsLimited
        {
            get { return _isHintsLimited; }
            private set
            {
                if (IsHintsForCurrentLevel)
                    PlayerPrefs.SetInt(_isHintsLimitedStr + _currentLevelName, value ? 1 : 0);
                else
                    PlayerPrefs.SetInt(_isHintsLimitedStr, value ? 1 : 0);

                _isHintsLimited = value;
            }
        }

        public static void IncrementHints()
        {
            ++NumberOfHits;
        }

        public static void IncrementHints(int count)
        {
            NumberOfHits += count;
        }

        public static void DecrementHints()
        {
            --NumberOfHits;
            Mathf.Clamp(NumberOfHits, 0, 100);
        }

        public static void DecrementHints(int decr)
        {
            NumberOfHits -= decr;
            Mathf.Clamp(NumberOfHits, 0, 100);
        }

        private static string _lastEndedLevel;
        public static string LastEndedLevel
        {
            get { return _lastEndedLevel; }
            set
            {
                PlayerPrefs.SetString(_lastEndedLevelStr, value);
                _lastEndedLevel = value;
            }
        }

        private static int _currentLevel;
        public static int CurrentLevel
        {
            get { return _currentLevel; }
            set
            {
                PlayerPrefs.SetInt(_currentLevelStr, value);
                _currentLevel = value;
            }
        }
        private static string _currentLevelName;
        public static string CurrentLevelName
        {
            get { return _currentLevelName; }
            set
            {
                PlayerPrefs.SetString(_currentLevelNameStr, value);
                _currentLevelName = value;
            }
        }

        public static void EndCurrentLevel()
        {
            LastEndedLevel = _currentLevelName;
        }

        public static void SetHints(bool isHintsLimited, int numberOfHints)
        {
            if (IsHintsForCurrentLevel)
            {
                IsHintsLimited = isHintsLimited;
                NumberOfHits = numberOfHints;
            }
            else
            {
                if (!PlayerPrefs.HasKey(_isHintsLimitedStr))
                {
                    IsHintsLimited = isHintsLimited;
                }
                if (!PlayerPrefs.HasKey(_numberOfHintsStr))
                {
                    NumberOfHits = numberOfHints;
                }
            }
        }
    }
}