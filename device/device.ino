#include <FastLED.h>

#define LED_PIN 6
#define NUM_LEDS 50
#define BRIGHTNESS 64
#define LED_TYPE WS2812B
#define COLOR_ORDER RGB
#define REC_BUFFER_SIZE NUM_LEDS*4
CRGB leds[NUM_LEDS];
CRGB ledsBuffer[NUM_LEDS];
int receptionIndex = 0;

int receivedInts[REC_BUFFER_SIZE];

#define UPDATES_PER_SECOND 60
bool stringComplete = false; // whether the string is complete
byte endMarker = 255;

void setup()
{
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(2000000, SERIAL_8N1);

  FastLED.addLeds<LED_TYPE, LED_PIN>(leds, NUM_LEDS);
  fill_solid(leds, NUM_LEDS, CRGB(50, 0, 200));
  FastLED.show();
}

void loop()
{
  if (stringComplete == true)
  {
    stringComplete = false;
    displayLeds();
          
  }
}

void displayLeds()
{
  digitalWrite(LED_BUILTIN, HIGH);
  for (int i = 0; i < NUM_LEDS; i++)
  {
    leds[i] = CRGB(receivedInts[i*3], receivedInts[(i*3)+1], receivedInts[(i*3)+2]); 
  }
  FastLED.show();
  digitalWrite(LED_BUILTIN, LOW);
}

void serialEvent()
{
  
  // stringComplete = false;

  

  /*if (receptionIndex == 0)
  {
    for (int x = 0; x < sizeof(receivedInts) / sizeof(receivedInts[0]); x++)
    {
      receivedInts[x] = 0;
    }
  }*/

  if (Serial.available() > 0)
  {
    byte dataIn = Serial.read();
    receivedInts[receptionIndex++] = dataIn;
    if (dataIn == endMarker)
    {
      receptionIndex = 0;
      stringComplete = true;
    }
  }

  /* if (stringComplete == true)
  {
    // displayLeds();
    stringComplete = false;
  }*/ 

  
}
