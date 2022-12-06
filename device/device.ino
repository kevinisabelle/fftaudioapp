#include <FastLED.h>

#define LED_PIN 5
#define NUM_LEDS 80
#define BRIGHTNESS 64
#define LED_TYPE WS2812
#define COLOR_ORDER RGB
CRGB leds[NUM_LEDS];
CRGB ledsBuffer[NUM_LEDS];

#define UPDATES_PER_SECOND 60
bool stringComplete = false; // whether the string is complete

void setup()
{
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(2000000, SERIAL_8O1);

  FastLED.addLeds<LED_TYPE, LED_PIN, COLOR_ORDER>(leds, NUM_LEDS);
}

void loop()
{
}

void serialEvent()
{
  int ledIndex = 0;
  int r = 0;
  int g = 0;
  int b = 0;
  int currentColor = -1;
  stringComplete = false;

  digitalWrite(LED_BUILTIN, HIGH);

  while (Serial.available())
  {
    int value = Serial.read();
    currentColor++;

    switch (currentColor)
    {
    case 0:
      r = value;
      break;
    case 1:
      g = value;
      break;
    case 2:
      b = value;
    }

    if (currentColor == 2)
    {
      ledsBuffer[ledIndex] = CRGB(g, r, b);
      currentColor = -1;
      ledIndex++;
    }
  }

  for (int i = 0; i < NUM_LEDS; i++)
  {
    leds[i] = ledsBuffer[i];
  }
  FastLED.show();
  digitalWrite(LED_BUILTIN, LOW);
  stringComplete = true;
}
