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

int testAnimation[4][5] = {100,255,0,0,1,50,0,255,0,1,0,0,0,255,1,66,255,20,140,3};

void setup(){
  //pinMode(0, INPUT);
  //pinMode(1, INPUT);

  Serial.begin(9600);
}

void loop(){
  Serial.println(sizeof(testAnimation) / sizeof(testAnimation[0]));
  Serial.println(sizeof(testAnimation[0]) / sizeof(int));

  delay(2000);
}
