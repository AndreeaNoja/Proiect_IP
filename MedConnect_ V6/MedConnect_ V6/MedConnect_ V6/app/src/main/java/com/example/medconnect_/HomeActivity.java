package com.example.medconnect_;

import android.content.Intent;
import android.os.Bundle;
import android.os.StrictMode;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
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

public class HomeActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener {

    Connection connection;  //baza de date
    TextView textNume;
    TextView textPrenume;
    TextView textCNP;
    TextView textVarsta;
    TextView textJudet;
    TextView textOras;
    TextView textStrada;
    TextView textNumar;
    TextView textBloc;
    TextView textApartament;
    TextView textNrTelefon;
    TextView textEmail;
    TextView textProfesie;
    TextView textLocMunca;
    TextView textMedic;

    //toolbar
    DrawerLayout drawerLayout;
    NavigationView navigationView;
    Toolbar toolbar;

    DatabaseManager conBD;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);

        //Toolbar MENU
        drawerLayout = findViewById(R.id.drawerlayout);
        navigationView = findViewById(R.id.navigationview);
        toolbar = findViewById(R.id.toolbar);

        navigationView.bringToFront();
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, R.string.navigation_open, R.string.navigation_close);
        drawerLayout.addDrawerListener(toggle);
        toggle.syncState();
        navigationView.setNavigationItemSelectedListener(this);
//
//        drawerLayout.closeDrawer(GravityCompat.START);


        //preiau date din pagina precedenta
        Intent intent = getIntent();
        String _cnp = intent.getStringExtra("cnp");

        // Inițializați TextView-urile din XML
        textNume = findViewById(R.id.text_nume);
        textPrenume = findViewById(R.id.text_prenume);
        textCNP = findViewById(R.id.text_cnp);
        textVarsta = findViewById(R.id.text_varsta);
        textJudet = findViewById(R.id.text_judet);
        textOras = findViewById(R.id.text_oras);
        textStrada = findViewById(R.id.text_strada);
        textNumar=findViewById(R.id.text_numar);
        textBloc = findViewById(R.id.text_bloc);
        textApartament = findViewById(R.id.text_apartament);
        textNrTelefon = findViewById(R.id.text_nr_telefon);
        textEmail = findViewById(R.id.text_email);
        textProfesie = findViewById(R.id.text_profesie);
        textLocMunca = findViewById(R.id.text_loc_munca);
        textMedic = findViewById(R.id.text_medic);


        //functie pentru incarcarea din baza de date a informatiilor
        loadUserProfile(_cnp);

    }



    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem menuItem)
    {
        Intent intent = getIntent();
        String _cnp = intent.getStringExtra("cnp");
        return ToolbarManager.onNavigationItemSelected(menuItem, HomeActivity.this,  _cnp);
    }

    private void loadUserProfile(String _cnp)
    {
        // SQL-ul pentru a obține informațiile utilizatorului din baza de date
        String sql = "SELECT * FROM Pacienti WHERE cnp = ?";

        conBD=new DatabaseManager();

        try {

            connection = conBD.connectionClass();

            // Pregătiți declarația SQL
            PreparedStatement ps = connection.prepareStatement(sql);
            ps.setString(1, _cnp);

            // Executați interogarea SQL
            ResultSet rs = ps.executeQuery();

            // Verificați dacă s-a găsit un rând în baza de date
            if (rs.next())
            {
                // Extrageți informațiile din rezultatul interogării și actualizați TextView-urile corespunzătoare
                textNume.setText(rs.getString("Nume"));
                textPrenume.setText(rs.getString("Prenume"));
                textCNP.setText(rs.getString("Cnp"));
                textVarsta.setText(String.valueOf(rs.getInt("varsta")));
                textJudet.setText(rs.getString("judet"));
                textOras.setText(rs.getString("oras"));
                textStrada.setText(rs.getString("strada"));

               // textNrBloc.setText(rs.getString("nr_bloc"));
                textNumar.setText(rs.getString("numar"));

                //verific daca campul care poate fi null ii nul sau nu
                String _bloc=rs.getString("bloc");
                textBloc.setText(_bloc != null ? _bloc : "-");

                String _app=rs.getString("apartament");
                textApartament.setText(_app != null ? _app : "-");

                textNrTelefon.setText(rs.getString("numarTel"));

                textEmail.setText(rs.getString("email"));
                textProfesie.setText(rs.getString("profesie"));
                textLocMunca.setText(rs.getString("locmunca"));

                int _id_medic= rs.getInt("idMedicApartinator");
                textMedic.setText(String.valueOf(_id_medic));
                String sql2 = "SELECT * FROM medici WHERE id_medic= ?";
//
                try {

                    PreparedStatement ps2 = connection.prepareStatement(sql2);
                    ps2.setInt(1, _id_medic);

                    // Executați interogarea SQL
                   ResultSet rs2 = ps2.executeQuery();
                    if (rs2.next())
                    {
                        String _num=rs2.getString("Nume");
                        String _pren=rs2.getString("Prenume");

                        String _nume_medic=_num+" "+_pren;
                        textMedic.setText(_nume_medic);
                    }

                }catch (SQLException e) {
                    // În caz de eroare, afișați un mesaj de eroare
                    Toast.makeText(getApplicationContext(), "Eroare la medic", Toast.LENGTH_SHORT).show();
                    e.printStackTrace();
                }



            }
        } catch (SQLException e) {
            // În caz de eroare, afișați un mesaj de eroare
            Toast.makeText(getApplicationContext(), "Eroare la încărcarea profilului utilizatorului", Toast.LENGTH_SHORT).show();
            e.printStackTrace();
        }
    }

}
