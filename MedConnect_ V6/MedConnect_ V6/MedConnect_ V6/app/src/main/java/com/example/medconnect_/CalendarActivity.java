package com.example.medconnect_;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CalendarView;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;
import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class CalendarActivity extends AppCompatActivity {

    private EditText startDateEditText, endDateEditText;
    private Button submitButton, backButton;
    private TextView activitiesTextView;
    private CalendarView calendarView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_calendar);

        startDateEditText = findViewById(R.id.startDateEditText);
        endDateEditText = findViewById(R.id.endDateEditText);
        submitButton = findViewById(R.id.submitButton);
        backButton = findViewById(R.id.backButton);
        activitiesTextView = findViewById(R.id.activitiesTextView);
        calendarView = findViewById(R.id.calendarView);

        submitButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String startDateString = startDateEditText.getText().toString();
                String endDateString = endDateEditText.getText().toString();

                SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy");
                try {
                    Date startDate = sdf.parse(startDateString);
                    Date endDate = sdf.parse(endDateString);

                    displayActivities(startDate, endDate);
                } catch (ParseException e) {
                    e.printStackTrace();
                    Toast.makeText(CalendarActivity.this, "Invalid date format", Toast.LENGTH_SHORT).show();
                }
            }
        });

        backButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                // Navigate back to the previous activity
                finish(); // Finish the current activity
            }
        });

        calendarView.setOnDateChangeListener(new CalendarView.OnDateChangeListener() {
            @Override
            public void onSelectedDayChange(@NonNull CalendarView view, int year, int month, int dayOfMonth) {
                // Do something when a date is selected
                String selectedDate = (month + 1) + "/" + dayOfMonth + "/" + year;
                Toast.makeText(CalendarActivity.this, "Selected Date: " + selectedDate, Toast.LENGTH_SHORT).show();
            }
        });
    }

    private void displayActivities(Date startDate, Date endDate) {
        // Here you can implement your logic to fetch and display activities
        // For demonstration purposes, let's just display a sample message
        SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy");
        String activities = "Activities from " + sdf.format(startDate) + " to " + sdf.format(endDate) + ":\n";
        activities += "1. Meeting\n";
        activities += "2. Presentation\n";
        activities += "3. Lunch with clients\n";
        activitiesTextView.setText(activities);
    }
}
