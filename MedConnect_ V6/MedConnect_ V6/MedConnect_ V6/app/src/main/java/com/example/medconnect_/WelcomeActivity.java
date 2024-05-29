package com.example.medconnect_;

import android.content.Intent;
import android.os.Bundle;
import android.os.StrictMode;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;
//import android.widget.Toolbar;
import androidx.appcompat.widget.Toolbar;

import androidx.activity.EdgeToEdge;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.GravityCompat;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.drawerlayout.widget.DrawerLayout;

import com.google.android.material.navigation.NavigationView;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class WelcomeActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener {

    TextView N_P;

    DrawerLayout drawerLayout;
    NavigationView navigationView;
    Toolbar toolbar;
    Connection connection;

    DatabaseManager conBD;


    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_welcome);

        //pt baza de date
        conBD=new DatabaseManager();


        //preiau date din pagina precedenta
        Intent intent = getIntent();
        String _cnp = intent.getStringExtra("cnp");

        N_P = findViewById(R.id.TextNumePrenume);

        //iau numele si prenumele din baza de date
        String sql = "SELECT * FROM Pacienti WHERE cnp = ?";

        try {
            connection = conBD.connectionClass();
            PreparedStatement ps = connection.prepareStatement(sql);
            ps.setString(1, _cnp);
            ResultSet rs = ps.executeQuery();

            if (rs.next())
            {
                // Procesarea rezultatelor și afișarea lor în elementele vizuale corespunzătoare
                String _nume=rs.getString("Nume");
                String _prenume=rs.getString("Prenume");

                String nume_prenume = _nume + " " + _prenume;
                N_P.setText(nume_prenume);
            }

        } catch (SQLException e)
        {
            Toast.makeText(getApplicationContext(), "Catch exception welcome", Toast.LENGTH_SHORT).show();
            throw new RuntimeException(e);
        }

        //Toolbar MENU
        drawerLayout = findViewById(R.id.drawerlayout);
        navigationView = findViewById(R.id.navigationview);
        toolbar = findViewById(R.id.toolbar);

        //toolbar.setTitle(nume_prenume);


        navigationView.bringToFront();
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, R.string.navigation_open, R.string.navigation_close);
        drawerLayout.addDrawerListener(toggle);
        toggle.syncState();

        navigationView.setNavigationItemSelectedListener(this);

        drawerLayout.closeDrawer(GravityCompat.START);
    }

    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem menuItem)
    {
        Intent intent = getIntent();
        String _cnp = intent.getStringExtra("cnp");
        return ToolbarManager.onNavigationItemSelected(menuItem, WelcomeActivity.this,  _cnp);
    }

}

