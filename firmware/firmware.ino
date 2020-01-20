#include <Stepper.h>
#include "Adafruit_NeoPixel.h"

/* SERIAL SETTINGS */
const int BAUD_RATE = 9600;       	// Baud rate to use in serial comms.

/* MOTOR SETTINGS */
const int MOTOR_STEPS = 470;      	// Number of steps on the motor.
const int MOTOR_SPEED = 80;       	// Speed of the motor in RPM.
int motorCurrent = 0;				// Current position of the motor in steps.

/* PINS */
const int P_MOTOR_COIL_1_POS = 4;   // Coil 1 positive.
const int P_MOTOR_COIL_1_NEG = 5;   // Coil 1 negative.
const int P_MOTOR_COIL_2_POS = 6;   // Coil 2 positive.
const int P_MOTOR_COIL_2_NEG = 7;   // Coil 2 negative.
const int P_LED = A5;              	// Onboard LED.
const int P_NEOPIXEL = 9;			// Neopixel LED.

/* OPERATING PARAMS */
#define ANIMATION_BUFFER_SIZE 128	// How big an animation is allowed to be.
#define ANIMATION_NODE_LENGTH 5		// Defines how many sections there are to each animation 'row'.
const int APEX_DELAY = 1000;      	// Time to wait in between up and downs.
bool motorReady = false;    		// If the motor has been set up.
bool running = true;				// If the uploaded animation should currently be playing.
bool uploaded = false;				// If the user has uploaded an animation to the clip.

/* ANIMATION DATA */
int animation[ANIMATION_BUFFER_SIZE][ANIMATION_NODE_LENGTH];
int animationLength = 0;

/* TESTS */
int testAnimation[4][5] = {100,255,0,0,1, 50,0,255,0,1, 66,0,0,255,1, 50,255,178,243,3};
bool runningTest = false;
bool animDone = false;

/* REQUIRED OBJECTS */
Stepper stepper (MOTOR_STEPS, P_MOTOR_COIL_1_POS, P_MOTOR_COIL_1_NEG, P_MOTOR_COIL_2_POS, P_MOTOR_COIL_2_NEG);
Adafruit_NeoPixel led (1, P_NEOPIXEL);

void setup() {
    Serial.begin(BAUD_RATE);
	led.begin();
	resetMotor();
}

// The ShapeClip will sit idle until the driver has uploaded an animation to it.
void waitForUpload(){
	bool done = false;

	led.setPixelColor(0, led.Color(203, 103, 235)); // PURPLE, AWAITING DRIVER
	led.show();

	//? In future, add an upload timeout.
	//Serial.write("@"); // Acknowledge driver request.
	while(Serial.available() == 0); // Wait for the driver to send the serial.

	led.setPixelColor(0, led.Color(255, 127, 0)); // ORANGE UPLOAD IN PROGRESS
	led.show();

	animationLength = 0;

	// Being sent animation...
	//! For now relies on correct formatting of the SCA file.
	// Driver will automatically strip the file of its comments (eventually...)

	int animRow = 0;
	int animColumn = 0;
	while(Serial.available() > 0){
		int current = Serial.parseInt(); // Get next number stored in buffer. Should ignore the : and .
		animation[animRow][animColumn] = current;

		animRow = (++animColumn % ANIMATION_NODE_LENGTH == 0) ? animRow + 1 : animRow;
		animColumn %= ANIMATION_NODE_LENGTH;
	}

	animationLength = animRow;
	
	// If file columns not multiple of 5 then automatically is not valid
	if(--animColumn == 0){
		uploaded = true;
		led.setPixelColor(0, 0, 255, 0); // GREEN, OK
		led.show();
	}
	else{
		uploaded = false;
		led.setPixelColor(0, 255, 0, 0); // RED, ERROR
		led.show();
	}
}

void loop() {
	if(!runningTest){
		if(uploaded){
			if(running){
				playAnimation(animation, animationLength);
			}
		}

		//* check here later for if the driver has asked for start, stop or reuploads
		if(Serial.available() > 0){
			String cmd = Serial.readString();
			
			// start/stop toggle
			if(cmd.equals("#")){
				running = !running;
			}

			// reupload trigger
			if(cmd.equals("@")){
				uploaded = false;
				running = false;
				waitForUpload();
			}
		}

		led.show();
	}
	else{
		playAnimation(testAnimation, sizeof(testAnimation) / sizeof(testAnimation[0]));
	}
}

void playAnimation(int animation[][5], int animSize){
	led.setPixelColor(0, 0, 0, 0);
	led.show();
	delay(1000);
	
	int animRow = 0;
	for(animRow; animRow < animSize; animRow++){
		float height;
		int d;

		// Calculate height as percentage of the max motor steps.
		if(animation[animRow][0] < 0) animation[animRow][0] = 0;
		if(animation[animRow][0] > 100) animation[animRow][0] = 100;
		height = (MOTOR_STEPS * ((float)animation[animRow][0] / 100));

		// Wrap colour
		animation[animRow][1] %= 256;
		animation[animRow][2] %= 256;
		animation[animRow][3] %= 256;

		d = animation[animRow][4];

		// Calculate difference between current motor height and the desired height, move motor, set LED
		int nextPos = height - motorCurrent;
		led.setPixelColor(0, led.Color(animation[animRow][1], animation[animRow][2], animation[animRow][3]));
		led.show();
		stepper.step(nextPos);

		delay(d * 1000);

		motorCurrent = (animation[animRow][0] == 0) ? 0 : height;
	}

	resetMotor();
	running = false;
}

// Reset motor down to base.
void resetMotor(){
  	led.setPixelColor(0,0,0,0);
  	led.show();
  
	motorReady = false;

    stepper.setSpeed(MOTOR_SPEED);
	stepper.step(-MOTOR_STEPS);

	motorCurrent = 0;
	motorReady = true;
}

// still blocking but doesn't matter lol
void millisDelay(int m){
	unsigned long start = millis();
	unsigned long curr = 0;

	while(curr < m){
		curr = millis() - start;
	}
}
