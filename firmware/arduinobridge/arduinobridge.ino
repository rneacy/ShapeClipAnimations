void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);

  Serial.begin(9600);
  //pinMode(0, INPUT);
  //pinMode(1, INPUT);
}

int animRow = 0;
int animColumn = 0;
int animationLength = 0;
int animation[256][5] = {};

void loop() {
  while(Serial.available() > 0){
    int current = Serial.parseInt(); // Get next number stored in buffer. Should ignore the : and .
    animation[animRow][animColumn] = current;

    animRow = (++animColumn % 5 == 0) ? animRow + 1 : animRow;
    animColumn %= 5;
  }

  animationLength = animRow;

  for(int i = 0; i < animationLength; i++){
    for(int j = 0; j < 5; j++){
      Serial.println(animation[i][j]);
    }
  }
}
