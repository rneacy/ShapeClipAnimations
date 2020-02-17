#include <Stepper.h>
#include "Adafruit_NeoPixel.h"

/* SERIAL SETTINGS */
#define BAUD_RATE 9600       	// Baud rate to use in serial comms.
#define TIMEOUT 200
#define CONTROL_UPLOAD "@"
#define CONTROL_PLAYSTOP "#"
#define CONTROL_IDENTITY '$'

/* MOTOR SETTINGS */
#define MOTOR_STEPS 470    		// Number of steps on the motor.
#define MOTOR_SPEED 100       	// Speed of the motor in RPM.
int motorCurrent = 0;			// Current position of the motor in steps.

/* PINS */
#define P_MOTOR_COIL_1_POS 4   	// Coil 1 positive.
#define P_MOTOR_COIL_1_NEG 5   	// Coil 1 negative.
#define P_MOTOR_COIL_2_POS 6   	// Coil 2 positive.
#define P_MOTOR_COIL_2_NEG 7   	// Coil 2 negative.
#define P_LED A5              	// Onboard LED.
#define P_NEOPIXEL 9			// Neopixel LED.

/* OPERATING PARAMS */
#define ANIMATION_BUFFER_SIZE 128	// How big an animation is allowed to be.
#define ANIMATION_NODE_LENGTH 6		// Defines how many sections there are to each animation 'row'
#define UNIVERSAL_START_DELAY 1000	// The baseline wait time when the user requests play to attempt to negate serial transfer time.
#define SERIAL_TRANSFER_DELAY 210	// The approximate amount of extra delay between each clip.
bool motorReady = false;    		// If the motor has been set up.
bool running = true;				// If the uploaded animation should currently be playing.
bool uploaded = false;				// If the user has uploaded an animation to the clip.
int identity;						// This clip's position in the chain.

/* ANIMATION DATA */
//* Wrapper class for individual animation nodes.
#pragma pack(1)
typedef struct Node {
	byte height;
	byte speed;
	byte r,g,b;
	unsigned long time;
};

// Converts raw animation data into structured format for storage.
Node rawToNode(unsigned long raw[ANIMATION_NODE_LENGTH]) {
  return Node { raw[0], raw[1], raw[2], raw[3], raw[4], raw[5] };
}

Node animation[ANIMATION_BUFFER_SIZE];
int animationLength = 0;

/* TESTS */
Node testAnimation[] = { Node{100,100,255,0,0,1000}, Node{50,100,0,255,0,500}, Node{66,100,0,0,255,800}, Node{50,100,255,178,243,2000} };
bool runningTest = false;
bool animDone = false;

/* REQUIRED OBJECTS */
Stepper stepper (MOTOR_STEPS, P_MOTOR_COIL_1_POS, P_MOTOR_COIL_1_NEG, P_MOTOR_COIL_2_POS, P_MOTOR_COIL_2_NEG);
Adafruit_NeoPixel led (1, P_NEOPIXEL);

void setup() {
	Serial.setTimeout(TIMEOUT);
    Serial.begin(BAUD_RATE);
	led.begin();
	resetMotor();
}

// The ShapeClip will sit idle until the driver has uploaded an animation to it.
void waitForUpload(bool master){
	bool done = false;

	int nextClip = identity + 1;

	Serial.print(CONTROL_IDENTITY);
	Serial.print(nextClip, DEC); // let the next shapeclip (if there is one) where in the line it is
	Serial.print("\0");

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
	bool relevant = false;
	bool identifying = true;
	unsigned long thisLine[ANIMATION_NODE_LENGTH];
	
	// if master, wait until the driver doesn't send.
	// if slave, wait until another clip sends the end of anim marker (-1)
	while(Serial.available() > 0 || !master){
		long current = Serial.parseInt(); // Get next number stored in buffer. Should ignore the : and .

		if(!master && current == -1){
			break;
		}

		// Verify ShapeClip identity
		if(identifying){
			relevant = (current == identity);
			identifying = false;
			//relevantFor = current;
			Serial.print(current); // forward identity
			Serial.print(":"); // forward section separator
			continue;
		}

		thisLine[animColumn] = current;
		Serial.print(current); // forward literal to next clip
		animColumn++;
		
		if((animColumn % ANIMATION_NODE_LENGTH) == 0){
			Serial.print("."); // forward node separator
      		if(relevant){
        		animation[animRow] = rawToNode(thisLine);
        		animRow++;
      		}

			animColumn = 0;
      		identifying = true;
		}
		else{
			Serial.print(":"); // forward section separator
		}
	}

	Serial.print(-1); // slave end
	animationLength = animRow;
	
	// If file columns not multiple of ANIMATION_NODE_LENGTH then automatically is not valid (was --animColumn)
	if(animColumn == 0){
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

		if(Serial.available() > 0){
			String cmd = Serial.readString();
			
			// start/stop toggle
			if(cmd.equals(CONTROL_PLAYSTOP)){
				running = !running;
				Serial.print(CONTROL_PLAYSTOP);

				// Wait the calculated delay to negate serial timings...
				int t = identity - 1;
				delay(UNIVERSAL_START_DELAY - (t * SERIAL_TRANSFER_DELAY));
			}

			// reupload trigger
			if(cmd.equals(CONTROL_UPLOAD)){
				identity = 1; // we can assume this as a shapeclip will never send an '@' itself
				uploaded = false;
				running = false;
				waitForUpload(true);
			}

			// upload trigger from another ShapeClip ($2)
			if(cmd.charAt(0) == CONTROL_IDENTITY){
				String extractedID = cmd.substring(1);
				identity = extractedID.toInt();

				if(identity > 0){
					uploaded = false;
					running = false;
					waitForUpload(false);
				}
				else{
					led.setPixelColor(0, 255, 0, 0);
					while(true); // not good
				}
			}
		}

		led.show();
	}
	else{
		led.setPixelColor(0, 0, 0, 0);
		led.show();
		delay(1000);

		playAnimation(testAnimation, sizeof(testAnimation) / sizeof(testAnimation[0]));
	}
}

void playAnimation(Node animation[], int animSize){
	int animRow = 0;
	unsigned long startTime = millis();

	for(animRow; animRow < animSize; animRow++){
		float height;
		unsigned long time;

		// Calculate height as percentage of the max motor steps.
		if(animation[animRow].height < 0) animation[animRow].height = 0;
		if(animation[animRow].height > 100) animation[animRow].height = 100;
		height = (MOTOR_STEPS * ((float)animation[animRow].height / 100));

		if(animation[animRow].speed < 0 || animation[animRow].speed > MOTOR_SPEED) animation[animRow].speed = MOTOR_SPEED;

		time = animation[animRow].time + startTime;

		// Calculate difference between current motor height and the desired height, move motor, set LED
		int nextPos = height - motorCurrent;

		while(!readyToGo(time)); //! for now just wait until it's ready; bad for non-ordered files, and is not async yet

		led.setPixelColor(0, led.Color(animation[animRow].r, animation[animRow].g, animation[animRow].b));
		led.show();
		stepper.setSpeed(animation[animRow].speed);
		stepper.step(nextPos);
		motorCurrent = (animation[animRow].height == 0) ? 0 : height;

		// Check if the driver has requested the animation to stop.
		if(Serial.available() > 0){
			String cmd = Serial.readString();
			if(cmd.equals(CONTROL_PLAYSTOP)){ // stop
				running = false;
				Serial.print(CONTROL_PLAYSTOP);
				break;
			}
			if(cmd.equals("~")){ // pause
				Serial.setTimeout(0xFFFFFFFFUL); // wait for 50 days for pause retoggle
				Serial.readString().equals("~");
				Serial.setTimeout(TIMEOUT);
			}
		}
	}

	resetMotor();
}

// Whether the requested time has been surpassed.
bool readyToGo(unsigned long requestedTime) {
	return millis() >= requestedTime;
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

	if(uploaded){
		led.setPixelColor(0, 0, 255, 0); // GREEN, OK
		led.show();
	}
}

// still blocking but doesn't matter lol
void millisDelay(int m){
	unsigned long start = millis();
	unsigned long curr = 0;

	while(curr < m){
		curr = millis() - start;
	}
}
