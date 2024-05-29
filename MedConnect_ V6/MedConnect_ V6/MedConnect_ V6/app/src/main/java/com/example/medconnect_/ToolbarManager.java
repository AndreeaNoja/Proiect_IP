package com.example.medconnect_;

import static androidx.core.content.ContextCompat.startActivity;

import android.app.ActionBar;
import android.app.Activity;
import android.content.Intent;
import android.view.MenuItem;
import android.widget.Toolbar;

import androidx.core.view.GravityCompat;

public class ToolbarManager
{
    public static void setupToolbar(Activity activity, Toolbar toolbar) {
        activity.setActionBar(toolbar);
        ActionBar actionBar = activity.getActionBar();
    }

    public static boolean onNavigationItemSelected(MenuItem menuItem, Activity activity, String _cnp)
    {
        int id = menuItem.getItemId();
        if (id == R.id.acasa)
        {
            startNewActivity(activity, HomeActivity.class, _cnp);
            return true;
        }
        else if (id == R.id.calendar)
        {
            startNewActivity(activity, CalendarActivity.class, _cnp);
            return true;
        }
        else if (id == R.id.recomandari)
        {
            startNewActivity(activity, RecomandariActivity.class, _cnp);
            return true;
        }
        else if (id == R.id.avertizari)
        {
            startNewActivity(activity, AvertizariActivity.class, _cnp);
            return true;
        }
        else if (id == R.id.date_masuratori)
        {
            startNewActivity(activity, DateMasuratoriActivity.class, _cnp);
            return true;
        }

        else if (id == R.id.logout)
        {
            startNewActivity(activity, LogoutActivity.class, _cnp);
            return true;
        }

        else if (id == R.id.meniu)
        {
            startNewActivity(activity, WelcomeActivity.class,  _cnp);

            return true;
        }

        else if (id == R.id.signal)
        {
            startNewActivity(activity, SignalActivity.class,  _cnp);

            return true;
        }

        return false;

    }


    private static void startNewActivity(Activity activity, Class<?> cls, String _cnp)
    {
        Intent intent = new Intent(activity, cls);
        intent.putExtra("cnp", _cnp);
        activity.startActivity(intent);
    }
}

