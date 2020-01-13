#include <Stepper.h>

/* SERIAL SETTINGS */
const int BAUD_RATE = 9600;       // Baud rate to use in serial comms.

/* MOTOR SETTINGS */
const int MOTOR_STEPS = 470;      // Number of steps on the motor.
const int MOTOR_SPEED = 80;       // Speed of the motor in RPM.

/* PINS */
const int P_MOTOR_COIL_1_POS = 4;   // Coil 1 positive.
const int P_MOTOR_COIL_1_NEG = 5;   // Coil 1 negative.
const int P_MOTOR_COIL_2_POS = 6;   // Coil 2 positive.
const int P_MOTOR_COIL_2_NEG = 7;   // Coil 2 negative.
const int P_LED = 165;              // Onboard LED.

/* OPERATING PARAMS */
const int APEX_DELAY = 1000;      // Time to wait in between up and downs.
bool motorReady = false;    // If the motor has been set up.
bool running = true;

/* REQUIRED OBJECTS */
Stepper stepper (MOTOR_STEPS, P_MOTOR_COIL_1_POS, P_MOTOR_COIL_1_NEG, P_MOTOR_COIL_2_POS, P_MOTOR_COIL_2_NEG);

void setup() {
    // Set up serial comms (this would actually only be on the serial bridge firmware)
    //pinMode(0, INPUT);
    //pinMode(1, INPUT);
    Serial.begin(BAUD_RATE);

    digitalWrite(P_LED, LOW);
	resetMotor();
	digitalWrite(P_LED, HIGH);
}

void loop() {
	if(running){
		// Ensure motor has been set up first.
		if(motorReady){
			// Move up and down to max that motor can.
			stepper.step(MOTOR_STEPS);
			delay(APEX_DELAY);
			stepper.step(-MOTOR_STEPS);
			delay(APEX_DELAY);
		}
	}
}

// Reset motor down to base.
void resetMotor(){
	motorReady = false;

    stepper.setSpeed(MOTOR_SPEED);
	stepper.step(-MOTOR_STEPS);

	motorReady = true;
}
