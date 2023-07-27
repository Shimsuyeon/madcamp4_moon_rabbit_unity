package com.defaultcompany.myproject;

import android.widget.Toast;
import com.unity3d.player.UnityPlayer;

public class UnityToast {
    public static void showToast(final String message) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                Toast.makeText(UnityPlayer.currentActivity, message, Toast.LENGTH_SHORT).show();
            }
        });
    }
}