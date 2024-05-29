package com.example.medconnect_;

import android.Manifest;
import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.util.Log;
import android.widget.Toast;

import androidx.core.app.ActivityCompat;
import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;

import java.io.IOException;
import java.io.InputStream;
import java.util.Set;
import java.util.UUID;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class BluetoothManager {
    private static final String TAG = "BluetoothManager";
    private static final UUID MY_UUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"); // Standard SPP UUID
    private static final String DEVICE_NAME = "HC-05"; // Change this to the name of your Bluetooth device

    private BluetoothAdapter bluetoothAdapter;
    private BluetoothSocket bluetoothSocket;
    private BluetoothDevice bluetoothDevice;
    private InputStream inputStream;
    private ExecutorService executorService;

    private MutableLiveData<String> receivedData;
    private Context context;

    public BluetoothManager(Context context) {
        this.context = context;
        bluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
        executorService = Executors.newSingleThreadExecutor();
        receivedData = new MutableLiveData<>();
    }

    public LiveData<String> getReceivedData() {
        return receivedData;
    }

    public void connect(Context context) {
        this.context = context;
        if (context instanceof Activity) {
            Activity activity = (Activity) context;
            Toast.makeText(activity, "Încearcă să se conecteze la Bluetooth.", Toast.LENGTH_SHORT).show();
            if (bluetoothAdapter == null) {
                Toast.makeText(activity, "Bluetooth not supported", Toast.LENGTH_SHORT).show();
                return;
            }
            if (!bluetoothAdapter.isEnabled()) {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
                activity.startActivity(enableBtIntent);
                Toast.makeText(activity, "REQUEST ENABLE", Toast.LENGTH_SHORT).show();
            } else {
                if (ActivityCompat.checkSelfPermission(activity, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                    ActivityCompat.requestPermissions(activity, new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, 1);
                    Toast.makeText(activity, "FINE LOCATION", Toast.LENGTH_SHORT).show();
                } else {
                    connectToDevice();
                }
            }
        } else {
            Log.e(TAG, "Contextul nu este o instanță a unei activități.");
        }
    }

    private void connectToDevice() {
        if (context instanceof Activity) {
            Activity activity = (Activity) context;
            Toast.makeText(activity, "Connect to device", Toast.LENGTH_SHORT).show();
            bluetoothDevice = findPairedDeviceByName(DEVICE_NAME);
            if (bluetoothDevice == null) {
                Toast.makeText(activity, "Device not found", Toast.LENGTH_SHORT).show();
                return;
            }

            Runnable connectToDeviceTask = new Runnable() {
                @Override
                public void run() {
                    try {
                        bluetoothSocket = bluetoothDevice.createRfcommSocketToServiceRecord(MY_UUID);
                        bluetoothSocket.connect();
                        inputStream = bluetoothSocket.getInputStream();
                        readData();
                        activity.runOnUiThread(() -> Toast.makeText(activity, "Conexiunea Bluetooth realizată", Toast.LENGTH_SHORT).show());
                    } catch (IOException e) {
                        handleBluetoothConnectionError(e);
                    }
                }
            };
            executorService.execute(connectToDeviceTask);
        } else {
            Log.e(TAG, "Contextul nu este o instanță a unei activități.");
        }
    }

    private void handleBluetoothConnectionError(IOException e) {
        if (context instanceof Activity) {
            Activity activity = (Activity) context;
            Toast.makeText(activity, "Eroare la conectarea la dispozitivul Bluetooth", Toast.LENGTH_SHORT).show();
            Log.e(TAG, "Error connecting to device", e);
        }
    }

    private BluetoothDevice findPairedDeviceByName(String name) {
        Set<BluetoothDevice> pairedDevices = bluetoothAdapter.getBondedDevices();
        if (pairedDevices.size() > 0) {
            for (BluetoothDevice device : pairedDevices) {
                if (name.equals(device.getName())) {
                    return device;
                }
            }
        }
        return null;
    }

    private void readData() {
        byte[] buffer = new byte[1024];
        int bytes;
        try {
            while ((bytes = inputStream.read(buffer)) != -1) {
                String readMessage = new String(buffer, 0, bytes);
                receivedData.postValue(readMessage);
                if (context instanceof Activity) {
                    Activity activity = (Activity) context;
                    activity.runOnUiThread(() -> Toast.makeText(activity, "Date primite de la dispozitivul Bluetooth: " + readMessage, Toast.LENGTH_SHORT).show());
                }
            }
        } catch (IOException e) {
            if (context instanceof Activity) {
                Activity activity = (Activity) context;
                Toast.makeText(activity, "Eroare la citirea datelor din dispozitivul Bluetooth", Toast.LENGTH_SHORT).show();
                Log.e(TAG, "Error reading data", e);
            }
        }
    }
}
