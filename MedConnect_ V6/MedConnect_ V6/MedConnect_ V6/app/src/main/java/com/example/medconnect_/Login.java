package com.example.medconnect_;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.StrictMode;
import android.text.InputType;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.Observer;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class Login extends AppCompatActivity {

    EditText nume_prenume, cnp;
    Button login_btn;
    ImageButton imageButtonTogglePassword;
    Connection connection;

    DatabaseManager conBD;
    BluetoothManager bluetoothManager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        nume_prenume = findViewById(R.id.NumePrenume);
        cnp = findViewById(R.id.CNP);
        login_btn = findViewById(R.id.loginBtn);
        imageButtonTogglePassword = findViewById(R.id.imageButtonTogglePassword);

        bluetoothManager = new BluetoothManager(this);
        bluetoothManager.getReceivedData().observe(this, new Observer<String>() {
            @Override
            public void onChanged(String data) {
                // Afiseaza datele primite de la Arduino
                Toast.makeText(Login.this, "Date primite de la Arduino: " + data, Toast.LENGTH_SHORT).show();
            }
        });

    // Set initial state
        final boolean[] passwordVisible = {false};

        // Set onClickListener for the toggle password button
        imageButtonTogglePassword.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (passwordVisible[0]) {
                    // Hide password
                    cnp.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);
                    imageButtonTogglePassword.setImageResource(R.drawable.visibility_off);
                } else {
                    // Show password
                    cnp.setInputType(InputType.TYPE_CLASS_TEXT);
                    imageButtonTogglePassword.setImageResource(R.drawable.visibility_on);
                }

                // Invert the state
                passwordVisible[0] = !passwordVisible[0];

                // Move cursor to the end of the text
                cnp.setSelection(cnp.getText().length());
            }
        });

        login_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int okDate, okNumePrenume, okCNP;
                okDate = 0;
                okNumePrenume = 0;
                okCNP = 0;

                String _np = nume_prenume.getText().toString();
                String _cnp = cnp.getText().toString();

                String[] numeSiPrenume = _np.split(" ");

                if (_np.isEmpty() || numeSiPrenume.length != 2) {
                } else
                    okNumePrenume = 1;

                if (_cnp.length() != 13) {
                } else
                    okCNP = 1;

                if (okNumePrenume == 0 && okCNP == 0) {
                    Toast.makeText(getApplicationContext(), "Ambele campuri sunt incorecte!", Toast.LENGTH_SHORT).show();
                } else if (okNumePrenume == 0 && okCNP == 1) {
                    Toast.makeText(getApplicationContext(), "Primul camp este incorect!", Toast.LENGTH_SHORT).show();
                    okDate = 0;
                } else if (okNumePrenume == 1 && okCNP == 0) {
                    Toast.makeText(getApplicationContext(), "Al doilea camp este incorect!", Toast.LENGTH_SHORT).show();
                    okDate = 0;
                } else if (okNumePrenume == 1 && okCNP == 1) {
                    //Toast.makeText(getApplicationContext(), "Campurile cunt corect introduse!", Toast.LENGTH_SHORT).show();
                    okDate = 1;
                }


                if (okDate == 1) {
                    conBD = new DatabaseManager();

                    int nr = 0;

                    String _nume = numeSiPrenume[0];
                    String _prenume = numeSiPrenume[1];
                    String sql = "select * from pacienti where nume=? and prenume=? and cnp=?";

                    try {
                        connection = conBD.connectionClass();

                        PreparedStatement ps = connection.prepareStatement(sql);
                        ps.setString(1, _nume);
                        ps.setString(2, _prenume);
                        ps.setString(3, _cnp);

                        ResultSet rs = ps.executeQuery();
                        while (rs.next())
                            nr++;

                        if (nr == 0) {
                            AlertDialog.Builder builder = new AlertDialog.Builder(Login.this);
                            builder.setMessage("Acesta pacient nu se afla in baza noastra de date.")
                                    .setPositiveButton("Reintrodu date", new DialogInterface.OnClickListener() {
                                        public void onClick(DialogInterface dialog, int id) {
                                            nume_prenume.setText("");
                                            cnp.setText("");
                                            dialog.dismiss();
                                        }
                                    })
                                    .setNegativeButton("Verificare date", new DialogInterface.OnClickListener() {
                                        public void onClick(DialogInterface dialog, int id) {

                                            dialog.dismiss();
                                        }
                                    });
                            AlertDialog dialog = builder.create();
                            dialog.show();
                            //Toast.makeText(getApplicationContext(), "Acest pacient nu exista!", Toast.LENGTH_SHORT).show();
                        } else {
                            Toast.makeText(getApplicationContext(), "Pacient gasit", Toast.LENGTH_SHORT).show();
                            Intent intent = new Intent(Login.this, WelcomeActivity.class);
                            intent.putExtra("cnp", _cnp);
                            startActivity(intent);

                            // Inițializează conexiunea Bluetooth
                            Toast.makeText(getApplicationContext(), "Connect", Toast.LENGTH_SHORT).show();
                            bluetoothManager.connect(Login.this);
                        }

                    } catch (SQLException e) {
                        Toast.makeText(getApplicationContext(), "Catch exception", Toast.LENGTH_SHORT).show();
                        throw new RuntimeException(e);
                    }
                }
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 1 && resultCode == RESULT_OK) {
            bluetoothManager.connect(this);
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        if (requestCode == 1 && grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
            bluetoothManager.connect(this);
        }
    }
}
