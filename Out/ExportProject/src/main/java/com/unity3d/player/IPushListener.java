package com.unity3d.player;

/**
 * Created by xuhonghua on 2020/8/17.
 */

public interface IPushListener {
      void onAliasOperatorResult(String msg);
      void processCustomMessage(String msg);
}
