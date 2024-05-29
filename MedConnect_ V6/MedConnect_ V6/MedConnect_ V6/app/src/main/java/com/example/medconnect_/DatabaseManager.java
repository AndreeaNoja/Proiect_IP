package com.example.medconnect_;

import android.annotation.SuppressLint;
import android.os.StrictMode;
import android.util.Log;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class DatabaseManager {

    Connection con;

    @SuppressLint("NewApi")
    public Connection connectionClass()
    {
        StrictMode.ThreadPolicy threadPolicy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(threadPolicy);

        Connection connection=null;
        String ConnectionURL=null;

        try{
            Class.forName("net.sourceforge.jtds.jdbc.Driver");

            ConnectionURL="jdbc:jtds:sqlserver://wearabledbserver.database.windows.net:1433;DatabaseName=WearableAzureDB;user=admin_IP@wearabledbserver;password=Atmin34#;encrypt=true;trustServerCertificate=false;hostNameInCertificate=*.database.windows.net;loginTimeout=30;";
            connection= DriverManager.getConnection(ConnectionURL);


        }
        catch(SQLException se)
        {
            Log.e("error here 1: ", se.getMessage());
        }

        catch (ClassNotFoundException e)
        {
            Log.e("error here 2: ", e.getMessage());
        }

        catch(Exception e) {

            Log.e("error here 3: ", e.getMessage());

        }
        return connection;
    }
}
