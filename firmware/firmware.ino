#include <Stepper.h>

const int BAUD_RATE = 9600;       // Baud rate to use in serial comms.

/* MOTOR SETTINGS */
const int MOTOR_COIL_1_POS = 4;   // Coil 1 positive.
const int MOTOR_COIL_1_NEG = 5;   // Coil 1 negative.
const int MOTOR_COIL_2_POS = 6;   // Coil 2 positive.
const int MOTOR_COIL_2_NEG = 7;   // Coil 2 negative.
const int MOTOR_STEPS = 470;      // Number of steps on the motor.
const int MOTOR_SPEED = 80;       // Speed of the motor in RPM.

const int APEX_DELAY = 1000;      // Time to wait in between up and downs.

Stepper stepper (MOTOR_STEPS, MOTOR_COIL_1_POS, MOTOR_COIL_1_NEG, MOTOR_COIL_2_POS, MOTOR_COIL_2_NEG);

void setup() {
  // Set up serial comms
  pinMode(0, INPUT);
  pinMode(1, INPUT);
  Serial.begin(BAUD_RATE);

  stepper.setSpeed(MOTOR_SPEED);
}

void loop() {
    // Move up and down to max that motor can.
    stepper.step(MOTOR_STEPS);
    delay(APEX_DELAY);
    stepper.step(-MOTOR_STEPS);
    delay(APEX_DELAY);
}
