const int BAUD_RATE = 9600;       // Baud rate to use in serial comms.

void setup() {
  // Set up serial comms
  pinMode(0, INPUT);
  pinMode(1, INPUT);
  Serial.begin(BAUD_RATE);
}

int receivedByte = 0;

void loop() {
    if(Serial.available() > 0){
      receivedByte = Serial.read();
      Serial.write(receivedByte);
    }
}
