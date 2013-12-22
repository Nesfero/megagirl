﻿using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
    public const string levelBitState = "levelBits";
    public const string levelPickupBitState = "levelPickupBits"; //for certain items that can't be picked up until complete restart

    private static bool mCheckpointActive = false;
    private static Vector3 mCheckpoint;
    private static string mLevelLoaded;

    /// <summary>
    /// Get the level that was loaded from stage
    /// </summary>
    public static string levelLoaded {
        get {
            return mLevelLoaded;
        }
    }

    public static bool isLevelComplete(string level) {
        return SceneState.instance.GetGlobalValue(level) == 1;
    }

    /// <summary>
    /// Determine if life up has been dropped, uses bit 30 in levelPickupBitState
    /// </summary>
    public static bool isLifeUpDropped {
        get { return SceneState.instance.CheckGlobalFlag(levelPickupBitState, 30); }
        set { SceneState.instance.SetGlobalFlag(levelPickupBitState, 30, value, false); }
    }

    public static void CheckpointApplyTo(Transform target) {
        if(mCheckpointActive) {
            target.position = mCheckpoint;
        }
    }

    public static void CheckpointSet(Vector3 pos) {
        mCheckpointActive = true;
        mCheckpoint = pos;
    }

    public static void CheckpointReset() {
        mCheckpointActive = false;
    }

    /// <summary>
    /// For specific level state and pickups, when exiting, call this
    /// </summary>
    public static void LevelStateReset() {
        SceneState.instance.SetGlobalValue(levelBitState, 0, false);
        SceneState.instance.SetGlobalValue(levelPickupBitState, 0, false);
    }

    public static void Complete() {
        SceneState.instance.SetGlobalValue(mLevelLoaded, 1, true);
    }

    void Awake() {
        mLevelLoaded = Application.loadedLevelName;
    }
}
