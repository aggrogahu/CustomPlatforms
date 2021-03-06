using System;

using BeatSaberMarkupLanguage.Attributes;

using UnityEngine;

using static CustomFloorPlugin.GlobalCollection;
using static CustomFloorPlugin.Utilities.Logging;


namespace CustomFloorPlugin.UI {


    /// <summary>
    /// Interface between the UI and the remainder of the plugin<br/>
    /// Abuses getters to inline showing values, and setters to perform relevant actions<br/>
    /// Not intended to hold extensive logic!<br/>
    /// Why does this need to be a <see cref="PersistentSingleton{T}"/>? I don't know.<br/>
    /// Why is everything here public? I don't know... -.-
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812:Avoid unistantiated internal classes", Justification = "Instantiated by Unity")]
    internal class Settings : PersistentSingleton<Settings> {


        /// <summary>
        /// Hover hint of load-custom-scripts
        /// </summary>
        [UIValue("LoadingCustomScriptsText")]
        public const string loadingCustomScriptsText = "Loading Custom Scripts \nUse this at your own risk! \nOnly use scripts of trusted sources!";

        /// <summary>
        /// Hover hint for use-in-360
        /// </summary>
        [UIValue("UseIn360Text")]
        public const string useIn360Text = "Toggle if Custom Platforms is used in 360�-Levels \n!Not supported!";


        /// <summary>
        /// Hover hint of use-in-multiplayer
        /// </summary>
        [UIValue("UseInMultiplayerText")]
        public const string useInMultiplayerText = "Toggle if Custom Platforms is used in Multiplayer \n!Not supported!";


        /// <summary>
        /// Determines if the feet icon is shown even if the platform would normally hide them<br/>
        /// Forwards the current choice to the UI, and the new choice to the plugin
        /// </summary>
        [UIValue("always-show-feet")]
        public static bool AlwaysShowFeet {
            get {
                if (_AlwaysShowFeet == null) {
                    _AlwaysShowFeet = CONFIG.GetBool("Settings", "AlwaysShowFeet", false, true);
                }
                return _AlwaysShowFeet.Value;
            }
            set {
                if (value != _AlwaysShowFeet.Value) {
                    CONFIG.SetBool("Settings", "AlwaysShowFeet", value);
                    _AlwaysShowFeet = value;
                }
            }
        }
        private static bool? _AlwaysShowFeet;


        /// <summary>
        /// Should the heart next to the logo be visible?<br/>
        /// Forwards the current choice to the UI, and the new choice to the plugin
        /// </summary>
        [UIValue("show-heart")]
        public static bool ShowHeart {
            get {
                if (_ShowHeart == null) {
                    _ShowHeart = CONFIG.GetBool("Settings", "ShowHeart", true, true);
                }
                return _ShowHeart.Value;
            }
            set {
                if (value != _ShowHeart.Value) {
                    CONFIG.SetBool("Settings", "ShowHeart", value);
                    _ShowHeart = value;
                    ShowHeartChanged(value);
                }
            }
        }
        private static bool? _ShowHeart;
        internal static event Action<bool> ShowHeartChanged = delegate (bool value) {
            Log("ShowHeart value changed. Notifying listeners.\nNew value: " + value);
        };


        /// <summary>
        /// Should this Plugin load CustomScripts?
        /// Forwards the current choice to the UI, and the new choice to the plugin
        /// </summary>
        [UIValue("load-custom-scripts")]
        public static bool LoadCustomScripts {
            get {
                if (_LoadCustomScripts == null) {
                    _LoadCustomScripts = CONFIG.GetBool("Settings", "LoadCustomScripts", false, true);
                }
                return _LoadCustomScripts.Value;
            }
            set {
                if (value != _LoadCustomScripts.Value) {
                    CONFIG.SetBool("Settings", "LoadCustomScripts", value);
                    _LoadCustomScripts = value;
                    PlatformManager.Reload();
                }
            }
        }
        private static bool? _LoadCustomScripts;


        /// <summary>
        /// Should this Plugin spawn a Custom Platform in 360�-Levels?
        /// Forwards the current choice to the UI, and the new choice to the plugin
        /// </summary>
        [UIValue("use-in-360")]
        public static bool UseIn360 {
            get {
                if (_UseIn360 == null) {
                    _UseIn360 = CONFIG.GetBool("Settings", "UseIn360", false, true);
                }
                return _UseIn360.Value;
            }
            set {
                if (value != _UseIn360.Value) {
                    CONFIG.SetBool("Settings", "UseIn360", value);
                    _UseIn360 = value;
                }
            }
        }
        private static bool? _UseIn360;


        /// <summary>
        /// Should this Plugin spawn a Custom Platform in Multiplayer?
        /// Forwards the current choice to the UI, and the new choice to the plugin
        /// </summary>
        [UIValue("use-in-multiplayer")]
        public static bool UseInMultiplayer {
            get {
                if (_UseInMultiplayer == null) {
                    _UseInMultiplayer = CONFIG.GetBool("Settings", "UseInMultiplayer", false, true);
                }
                return _UseInMultiplayer.Value;
            }
            set {
                if (value != _UseInMultiplayer.Value) {
                    CONFIG.SetBool("Settings", "UseInMultiplayer", value);
                    _UseInMultiplayer = value;
                }
            }
        }
        private static bool? _UseInMultiplayer;


        /// <summary>
        /// This is a wrapper for Beat Saber's player data structure.<br/>
        /// </summary>
        internal static PlayerData PlayerData {
            get {
                if (_PlayerData == null) {
                    PlayerDataModel[] playerDataModels = Resources.FindObjectsOfTypeAll<PlayerDataModel>();
                    if (playerDataModels.Length >= 1) {
                        _PlayerData = Resources.FindObjectsOfTypeAll<PlayerDataModel>()[0].playerData;
                    }
                }
                return _PlayerData;
            }
        }
        private static PlayerData _PlayerData;


        /// <summary>
        /// Call this Method before using <see cref="PlayerData"/> again to Update it
        /// </summary>
        internal static void UpdatePlayerData() {
            _PlayerData = null;
        }
    }
}