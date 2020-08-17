package com.unity3d.player;

import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.content.res.Configuration;
import android.support.multidex.MultiDex;
import android.util.Log;

import cn.jpush.android.api.JPushInterface;


//import com.ugame.sdkbox.UGameMultiApp;

/**
 * Created by Administrator on 2019/5/5.
 */

public class UnityApplication extends Application {
    public static Activity mUnityActivity;
    @Override
    public void onCreate() {
        super.onCreate();
        JPushInterface.setDebugMode(true);
        JPushInterface.init(this);

        Log.e("package",getPackageName());

    }

    @Override
    public void onTerminate() {
        super.onTerminate();
    }

    @Override
    protected void attachBaseContext(Context base) {
        super.attachBaseContext(base);
        MultiDex.install(base);
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
    }
}
