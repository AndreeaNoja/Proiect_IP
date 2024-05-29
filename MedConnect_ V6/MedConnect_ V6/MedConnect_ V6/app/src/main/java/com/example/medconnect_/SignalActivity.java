package com.example.medconnect_;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;

public class SignalActivity extends AppCompatActivity {

    private EditText param1EditText, param2EditText, param3EditText, param4EditText;
    private Button calculateButton;
    private TextView resultTextView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_signal);

        param1EditText = findViewById(R.id.param1EditText);
        param2EditText = findViewById(R.id.param2EditText);
        param3EditText = findViewById(R.id.param3EditText);
        param4EditText = findViewById(R.id.param4EditText);
        calculateButton = findViewById(R.id.calculateButton);
        resultTextView = findViewById(R.id.resultTextView);

        calculateButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v)
            {
                // Get input parameters from EditText fields
                double param1 = Double.parseDouble(param1EditText.getText().toString());
                double param2 = Double.parseDouble(param2EditText.getText().toString());
                double param3 = Double.parseDouble(param3EditText.getText().toString());
                double param4 = Double.parseDouble(param4EditText.getText().toString());

                // Calculate signal based on the parameters (you can replace this with your own calculation)
                double signal = param1 * param2 / (param3 + param4);

                // Display the result in the TextView
                resultTextView.setText("Signal: " + signal);
            }
        });
    }
}