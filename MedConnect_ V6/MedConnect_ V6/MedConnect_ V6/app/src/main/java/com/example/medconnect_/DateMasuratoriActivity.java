package com.example.medconnect_;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.StrictMode;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.drawerlayout.widget.DrawerLayout;

import com.google.android.material.navigation.NavigationView;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class DateMasuratoriActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener
{
    TextView N_P;
    TextView CNP;
    TextView temp;
    TextView umid;
    TextView frecv;
    Connection connection;
    Handler handler;

    DatabaseManager conBD;

    //toolbar
    DrawerLayout drawerLayout;
    NavigationView navigationView;
    Toolbar toolbar;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_date_masuratori);

        //pt baza de date
        conBD=new DatabaseManager();

        //Toolbar MENU
        drawerLayout = findViewById(R.id.drawerlayout);
        navigationView = findViewById(R.id.navigationview);
        toolbar = findViewById(R.id.toolbar);

        navigationView.bringToFront();
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, R.string.navigation_open, R.string.navigation_close);
        drawerLayout.addDrawerListener(toggle);
        toggle.syncState();
        navigationView.setNavigationItemSelectedListener(this);


        Intent intent = getIntent();
        String _cnp = intent.getStringExtra("cnp");

        //iau numele si prenumele din baza de date
        String sql_np = "SELECT * FROM pacienti WHERE cnp = ?";

        String _nume = null;
        String _prenume = null;
        try {
            connection = conBD.connectionClass();
            PreparedStatement ps_np = connection.prepareStatement(sql_np);
            ps_np.setString(1, _cnp);
            ResultSet rs_np = ps_np.executeQuery();

            if (rs_np.next())
            {
                _nume = rs_np.getString("nume");
                _prenume = rs_np.getString("prenume");
            }

        }
        catch (SQLException e) {
            Toast.makeText(getApplicationContext(), "Catch exception conexiune DATE", Toast.LENGTH_SHORT).show();
            throw new RuntimeException(e);
        }

        //preiau din xml
        N_P = findViewById(R.id.TextNumePrenume);
        String nume_prenume = _nume + " " + _prenume;
        N_P.setText(nume_prenume);

        temp = findViewById(R.id.temperatura);
        umid = findViewById(R.id.umiditate);
        frecv = findViewById(R.id.frecventa);

        handler = new Handler();

        // Rulează metoda de actualizare la fiecare 30 de secunde
        handler.postDelayed(updateData, 0);
    }

    // Runnable pentru actualizarea datelor
    private Runnable updateData = new Runnable() {
        @Override
        public void run() {
            updateDataFromDatabase(); // Actualizează datele
            handler.postDelayed(this, 10000); // Rulează din nou după 30 de secunde
        }
    };

    // Metoda pentru actualizarea datelor din baza de date și afișarea lor pe ecran
    private void updateDataFromDatabase()
    {
        conBD = new DatabaseManager();

        Intent intent = getIntent();
        String _cnp = intent.getStringExtra("cnp");

        String sql = "SELECT * FROM Date_senzori WHERE IDPacient = ? ORDER BY Timestamp DESC";

        Toast.makeText(getApplicationContext(), _cnp, Toast.LENGTH_SHORT).show();


        connection = conBD.connectionClass();

        if (connection == null)
        {
            Toast.makeText(getApplicationContext(), "Conexiune error", Toast.LENGTH_SHORT).show();
        }

        else
        {
            try {


                PreparedStatement ps = connection.prepareStatement(sql);
                ps.setString(1, _cnp);
                ResultSet rs = ps.executeQuery();

                if (rs.next())
                {
                    // Procesarea rezultatelor și afișarea lor în elementele vizuale corespunzătoare
                    float frecventa = rs.getFloat("FrecventaCardiaca");
                    float umiditate = rs.getFloat("Umiditate");
                    double temperatura = rs.getDouble("TemperaturaMax");

                    frecv.setText(String.valueOf(frecventa));
                    umid.setText(String.valueOf(umiditate));
                    temp.setText(String.valueOf(temperatura));
                }

            }
            catch (SQLException e)
            {
                Toast.makeText(getApplicationContext(), "Catch exception UPDATE DATE", Toast.LENGTH_SHORT).show();
                throw new RuntimeException(e);
            }
        }
    }

//    @Override
//    protected void onDestroy() {
//        super.onDestroy();
//        // Anulează task-ul de actualizare când activitatea este distrusă
//        handler.removeCallbacks(updateData);
//    }

    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem menuItem)
    {
        Intent intent = getIntent();
        String _cnp = intent.getStringExtra("cnp");
        return ToolbarManager.onNavigationItemSelected(menuItem, DateMasuratoriActivity.this,  _cnp);
    }

}