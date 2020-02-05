void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);

  Serial.begin(9600);
  pinMode(0, INPUT);
  pinMode(1, INPUT);
}

void loop() {}
/*int identity = 1;
int ANIMATION_NODE_LENGTH = 6;
int animRow = 0;
  int animColumn = 0;
  bool relevant = false;
  bool identifying = true;

int animation[128][6];

void loop(){
  while(Serial.available() > 0){
    int current = Serial.parseInt(); // Get next number stored in buffer. Should ignore the : and .

    // Verify ShapeClip identity
    if(identifying){
      Serial.print("Identity: ");
      Serial.println(current);
      relevant = (current == identity);
      identifying = false;
      continue;
    }
    else if(relevant){
      animation[animRow][animColumn] = current;
      Serial.print("Added: ");
      Serial.println(animation[animRow][animColumn]);
      animRow = (++animColumn % ANIMATION_NODE_LENGTH == 0) ? animRow + 1 : animRow; // array only stores what is relevant
      animColumn %= ANIMATION_NODE_LENGTH;
    }

    identifying = (animColumn == 0);
  }
}*/
