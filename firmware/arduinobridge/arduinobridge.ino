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
  playAnimation(testAnimation, sizeof(testAnimation) / sizeof(testAnimation[0]));
  delay(5000);
}

int motorCurrent = 0;

void playAnimation(int animation[][5], int animSize){
  delay(1000);

  int animRow = 0;
  
  for(animRow; animRow < animSize); animRow++){
    float height;
    int d;

    // Calculate height as percentage of the max motor steps.
    if(animation[animRow][0] < 0) animation[animRow][0] = 0;
    if(animation[animRow][0] > 100) animation[animRow][0] = 100;
    height = (470 * ((float)animation[animRow][0] / 100));

    // Wrap colour
    animation[animRow][1] %= 256;
    animation[animRow][2] %= 256;
    animation[animRow][3] %= 256;

    d = animation[animRow][4];

    // Calculate difference between current motor height and the desired height, move motor, set LED
    int nextPos = height - motorCurrent;
    //led.setPixelColor(0, led.Color(animation[animRow][1], animation[animRow][2], animation[animRow][3]));
    //led.show();
    //stepper.step(nextPos);

    delay(d * 1000);

    motorCurrent = (animation[animRow][0] == 0) ? 0 : height;
  }

  //resetMotor();
}
