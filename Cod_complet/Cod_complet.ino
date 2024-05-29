#include <PulseSensorPlayground.h>     // Includes the PulseSensorPlayground Library. 
#include <SoftwareSerial.h>
#include <DHT.h>

#define RX 0
#define TX 1

SoftwareSerial bluetooth(RX, TX);

#define DHTPIN 2
#define DHTTYPE DHT22
DHT dht(DHTPIN, DHTTYPE);

const int analogPin = A2;      // Pinul analogic pentru citirea semnalului EKG
const int baseline = 512;      // Valoarea de bază a semnalului
const long interval_EKG = 10;      // Intervalul de eșantionare (10 ms)
unsigned long previousMillis_EKG = 0; // Variabilă pentru stocarea timpului anterior
float pWaveAmplitude, qrsAmplitude, tWaveAmplitude; // Variabile pentru amplitudinea undelor P, QRS și T
float heartRate; // Variabilă pentru frecvența cardiacă
float lastQRSpeak = -10000; // Variabilă pentru stocarea timpului ultimului vârf QRS
int lastECGValue = baseline; // Variabilă pentru stocarea valorii anterioare a semnalului EKG

const int PulseWire = 0;       // PulseSensor WIRE connected to ANALOG PIN 0
const int LED = LED_BUILTIN;          // The on-board Arduino LED, close to PIN 13.
int Threshold = 550;           // Determine which Signal to "count as a beat" and which to ignore.
                               // Use the "Gettting Started Project" to fine-tune Threshold Value beyond default setting.
                               // Otherwise leave the default "550" value. 

PulseSensorPlayground pulseSensor;  // Creates an instance of the PulseSensorPlayground object called "pulseSensor"

int i=0;
int bpm;
unsigned long previousMillis_PULSE = 0;  // Variable to store the last time the value was printed
const unsigned long interval_PULSE = 5000;  // Interval in milliseconds (5 seconds)

void setup() {
  Serial.begin(9600); // Inițializare comunicare serială pentru debug
  bluetooth.begin(9600);
  dht.begin();

   // Configure the PulseSensor object, by assigning our variables to it. 
  pulseSensor.analogInput(PulseWire);   
  pulseSensor.blinkOnPulse(LED);       //auto-magically blink Arduino's LED with heartbeat.
  pulseSensor.setThreshold(Threshold);  
}

void loop() {

  // Citirea temperaturii și umidității
  float temp = dht.readTemperature();
  float hum = dht.readHumidity();

  unsigned long currentMillis_EKG = millis(); // Obține timpul curent
  
  // Verifică dacă este timpul pentru a eșantiona semnalul EKG
  if (currentMillis_EKG - previousMillis_EKG >= interval_EKG) {
    previousMillis_EKG = currentMillis_EKG; // Actualizează timpul anterior
    
    // Citirea semnalului EKG
    int ecgValue = analogRead(analogPin);
    
    // Procesarea semnalului pentru a extrage caracteristicile
    detectECGFeatures(ecgValue);
    // Actualizarea valorii anterioare a semnalului EKG
    lastECGValue = ecgValue;
  }

 unsigned long currentMillis_PULSE = millis(); // Obține timpul curent
   while (pulseSensor.sawStartOfBeat())
  {               
		  i++;                     
	}     

  if (currentMillis_PULSE - previousMillis_PULSE >= interval_PULSE) {
    // Update the previousMillis variable to store the current time
    previousMillis_PULSE = currentMillis_PULSE;

    // Print the value after 60 seconds
    bpm = i*12;
    i=0;
  }    

String data = String(temp) + "," + String(hum) + "," + String(bpm) + "," + String(pWaveAmplitude) + "," + String(qrsAmplitude) + "," + String(tWaveAmplitude);
Serial.println(data);
delay(10000);      
}

// Funcție pentru detectarea caracteristicilor semnalului EKG
void detectECGFeatures(int ecgValue) {
  // Detectarea vârfurilor QRS și calcularea frecvenței cardiace
  if (ecgValue > baseline && lastECGValue <= baseline) {
    float currentMillisSeconds = millis() / 1000.0;
    float qrsInterval = currentMillisSeconds - lastQRSpeak;
    if (qrsInterval >= 0.1 && qrsInterval <= 2.0) { // Asigură-te că intervalul este într-un interval rezonabil pentru o frecvență cardiacă normală
      heartRate = 60.0 / qrsInterval;
      lastQRSpeak = currentMillisSeconds;
    }
  }
  
  // Calcularea amplitudinilor undelor P, QRS și T
  if (ecgValue > pWaveAmplitude) {
    pWaveAmplitude = ecgValue;
  }
  if (ecgValue < baseline && lastECGValue >= baseline && millis() / 1000.0 - lastQRSpeak >= 0.03) {
    qrsAmplitude = lastECGValue - baseline;
  }
  if (ecgValue < tWaveAmplitude && millis() / 1000.0 - lastQRSpeak >= 0.35) {
    tWaveAmplitude = ecgValue;
  }
}