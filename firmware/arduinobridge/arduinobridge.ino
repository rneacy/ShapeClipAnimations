/*void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);

  Serial.begin(9600);
}

void loop() {
  if(Serial.available() > 0){
    Serial.write(Serial.read());
  }
}*/

void setup(){
  pinMode(0, INPUT);
  pinMode(1, INPUT);
}

void loop(){
}
