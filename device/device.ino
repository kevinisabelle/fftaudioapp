#include <FastLED.h>

#define LED_PIN 6
#define NUM_LEDS 12*22
#define BRIGHTNESS 64
#define LED_TYPE WS2812B
#define COLOR_ORDER RGB
#define REC_BUFFER_SIZE NUM_LEDS*3+1
CRGB leds[NUM_LEDS];
int receptionIndex = 0;
int ledIndex = 0;

byte receivedInts[REC_BUFFER_SIZE];

#define UPDATES_PER_SECOND 60
bool stringComplete = false; // whether the string is complete
byte endMarker = 255;

void setup()
{
  pinMode(LED_BUILTIN, OUTPUT);

  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);
  pinMode(10, OUTPUT);
  Serial.begin(500000); //115200); //, SERIAL_8N1);
  delay(1000); //wait 1 s
  FastLED.addLeds<LED_TYPE, LED_PIN>(leds, NUM_LEDS);
  fill_solid(leds, NUM_LEDS, CRGB(50, 0, 200));
  FastLED.show();
}

void loop()
{

  if (Serial.available() > 0)
  {
    byte dataIn = Serial.read();
    receivedInts[receptionIndex++] = dataIn;
    digitalWrite(LED_PIN, HIGH);
    if (dataIn == endMarker)
    {
      stringComplete = true;
      receptionIndex = 0;
    }
  }
  
  if (stringComplete == true)
  {
    digitalWrite(LED_PIN, LOW);
    stringComplete = false;
    displayLeds();
  }
}

void displayLeds()
{
  for (int i = 0; i < NUM_LEDS; i++)
  {
    leds[i] = CRGB(receivedInts[i*3], receivedInts[(i*3)+1], receivedInts[(i*3)+2]); 
  }
  FastLED.show();
  
}
