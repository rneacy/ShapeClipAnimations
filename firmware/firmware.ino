#include <Stepper.h>
#include "Adafruit_NeoPixel.h"

/* SERIAL SETTINGS */
const int BAUD_RATE = 9600;       // Baud rate to use in serial comms.

/* MOTOR SETTINGS */
const int MOTOR_STEPS = 470;      // Number of steps on the motor.
const int MOTOR_SPEED = 80;       // Speed of the motor in RPM.
int motorCurrent = 0;

/* PINS */
const int P_MOTOR_COIL_1_POS = 4;   // Coil 1 positive.
const int P_MOTOR_COIL_1_NEG = 5;   // Coil 1 negative.
const int P_MOTOR_COIL_2_POS = 6;   // Coil 2 positive.
const int P_MOTOR_COIL_2_NEG = 7;   // Coil 2 negative.
const int P_LED = A5;              	// Onboard LED.
const int P_NEOPIXEL = 9;			// Neopixel LED.

/* OPERATING PARAMS */
const int APEX_DELAY = 1000;      // Time to wait in between up and downs.
bool motorReady = false;    // If the motor has been set up.
bool running = true;
bool uploaded = false;		// If the user has uploaded an animation to the clip.

/* TESTS */
int testAnimation[3][5] = {100,255,0,0,1,50,0,255,0,1,0,0,0,255,1};
bool runningTest = true;
bool animDone = false;

/* REQUIRED OBJECTS */
Stepper stepper (MOTOR_STEPS, P_MOTOR_COIL_1_POS, P_MOTOR_COIL_1_NEG, P_MOTOR_COIL_2_POS, P_MOTOR_COIL_2_NEG);
Adafruit_NeoPixel led (1, P_NEOPIXEL);

void setup() {
    Serial.begin(BAUD_RATE);

	led.begin();
	led.setPixelColor(0, 255, 0, 0); // RED
	led.setBrightness(128); // Half brightness, full brightness makes you go blind.
	led.show();

	resetMotor();
}

// The ShapeClip will sit idle until the driver has uploaded an animation to it.
void waitForUpload(){
	bool done = false;

	led.setPixelColor(0, led.Color(173, 7, 255)); // PURPLE, AWAITING DRIVER
	led.show();

	//? In future, add an upload timeout.
	Serial.print("@"); // Acknowledge driver request.
	while(Serial.available() == 0); // Wait for the driver to send the serial.

	led.setPixelColor(0, led.Color(173, 255, 255)); // CYAN, UPLOAD IN PROGRESS
	led.show();

	// Being sent animation...
	//! For now relies on correct formatting of the SCA file.
	// Driver will automatically strip the file of its comments (eventually...)

	int buff[256];
	while(Serial.available() > 0){
		int current = Serial.parseInt(); // Get next number stored in buffer. Should ignore the : and .s.
	}

	uploaded = true;
}

void loop() {
	if(!runningTest){
		if(uploaded){
			if(running){
				led.setPixelColor(0, ((motorReady) ? led.Color(0, 255, 0) : led.Color(255, 0, 0)));
			}
		}
		else{
			//led.setPixelColor(0, led.Color(255, 127, 0)); // ORANGE (NOT UPLOADED)
		}

		// check here later for if the driver has asked for start, stop or reuploads
		if(Serial.available() > 0){
			char cmd = (char)Serial.read();
			
			// start/stop toggle
			if(cmd == '#'){
				running = !running;
			}

			// reupload trigger
			if(cmd == '@'){
				uploaded = false;
				running = false;
				waitForUpload();
			}
		}

		led.show();
	}
	else{
    	led.setPixelColor(0, led.Color(0, 255, 0));
		led.show();
		delay(1000);
  
		// Play test animation
		int animColumn = 0;
		int animRow = 0;
		
		//for(animRow; animRow < (sizeof(testAnimation) / sizeof(testAnimation[0])); animRow++){
		for(animRow; animRow < 3; animRow++){
			float height;
			int d;

			// Calculate height as percentage of the max motor steps.
			if(testAnimation[animRow][0] < 0) testAnimation[animRow][0] = 0;
			if(testAnimation[animRow][0] > 100) testAnimation[animRow][0] = 100;
			height = (MOTOR_STEPS * ((float)testAnimation[animRow][0] / 100));

			// Wrap colour
			testAnimation[animRow][1] %= 256;
			testAnimation[animRow][2] %= 256;
			testAnimation[animRow][3] %= 256;

			d = testAnimation[animRow][4];

			// Calculate difference between current motor height and the desired height, move motor, set LED
			int nextPos = height - motorCurrent;
			stepper.step(nextPos);
			led.setPixelColor(0, led.Color(testAnimation[animRow][1], testAnimation[animRow][2], testAnimation[animRow][3]));
			led.show();

			delay(d * 1000);

			motorCurrent = height;
		}

		//runningTest = false;

		resetMotor();
	}
}

// Reset motor down to base.
void resetMotor(){
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
