package com.damytech.appwidget;

import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.widget.Toast;

public class WidgetIntentReceiver extends BroadcastReceiver {

	@Override
	public void onReceive(Context context, Intent intent)
    {
		if(intent.getAction().equals("com.damytech.intent.action.RUN_PROGRAM"))
        {
            runProgram(context);
		}
	}

    private void runProgram(Context context)
    {
        try
        {
            Intent intent = new Intent();
            intent.setAction(Intent.ACTION_MAIN);
            intent.addCategory(Intent.CATEGORY_LAUNCHER);
            intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            PackageManager pm = context.getPackageManager();
            Intent launch = pm.getLaunchIntentForPackage("com.damytech.YiTong");
            String strClass = launch.getComponent().getClassName();
            intent.setComponent(new ComponentName("com.damytech.YiTong", strClass));
            context.startActivity(intent);
        }
        catch (Exception ex)
        {
            Toast.makeText(context, ex.getMessage(), Toast.LENGTH_LONG).show();
        }
    }
}
