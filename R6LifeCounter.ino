int GREEN = 7;
int YELLOW = 4;
int RED = 2;
int BUZZER = 3;
int COLOR = 3;

void setup() {
  pinMode(GREEN, OUTPUT);
  pinMode(YELLOW, OUTPUT);
  pinMode(RED, OUTPUT);
  pinMode(BUZZER, OUTPUT);
  Serial.begin(19200);
}

void loop() {
  COLOR = Serial.read();
  
  if (COLOR == '2')
  {
    MakeSound(50);
    digitalWrite(GREEN, HIGH);
    digitalWrite(YELLOW, LOW);
    digitalWrite(RED, LOW);
    return;
  }
  else if (COLOR == '1')
  {
    MakeSound(50);
    digitalWrite(GREEN, LOW);
    digitalWrite(YELLOW, HIGH);
    digitalWrite(RED, LOW);
    return;
  }
  else if (COLOR == '0')
  {
    MakeSound(50);
    digitalWrite(GREEN, LOW);
    digitalWrite(YELLOW, LOW);
    digitalWrite(RED, HIGH);
    return;
  }
  else if (COLOR == '3')
  {
    MakeSound(50);
    delay(50);
    MakeSound(50);
    digitalWrite(GREEN, LOW);
    digitalWrite(YELLOW, LOW);
    digitalWrite(RED, LOW);
    return;
  }
  else
  return;
}

void MakeSound(int Time)
{
  digitalWrite(BUZZER, HIGH);
  delay(Time);
  digitalWrite(BUZZER, LOW);
}
