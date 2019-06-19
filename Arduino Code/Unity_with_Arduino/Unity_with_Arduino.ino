#include <Servo.h>

Servo myServoA;
Servo myServoB;
Servo myServoC;
Servo myServoD;

const int pinServo1 = 7;
const int pinServo2 = 8;
const int pinServo3 = 9;
const int pinServo4 = 5;

String incomming;
String maChaine1;
String maChaine2;
String maChaine3;
String maChaine4;

int servoPos1 = 130;
int servoPos2 = 140;
int servoPos3 = 110;
int servoPos4 = 50;
int oldServoPos1;
int oldServoPos2;
int oldServoPos3;
int oldServoPos4;

void setup()
{
  Serial.begin(9600);
  Serial.setTimeout(10);

  myServoA.attach(pinServo1);
  myServoB.attach(pinServo2);
  myServoC.attach(pinServo3);
  myServoD.attach(pinServo4);

  myServoA.write(servoPos1);
  myServoB.write(servoPos2);
  myServoC.write(servoPos3);
  myServoD.write(servoPos4);
}

void loop()
{
  if (Serial.available() > 0)
  {
    incomming = Serial.readString();

    maChaine1 = incomming.substring(0, 3);
    maChaine2 = incomming.substring(3, 6);
    maChaine3 = incomming.substring(6, 9);
    maChaine4 = incomming.substring(9, 12);

    servoPos1 = maChaine1.toInt();
    servoPos2 = maChaine2.toInt();
    servoPos3 = maChaine3.toInt();
    servoPos4 = maChaine4.toInt();

    oldServoPos1 = servoPos1;
    oldServoPos2 = servoPos2;
    oldServoPos3 = servoPos3;
    oldServoPos4 = servoPos4;

    servoPos1 = limitePosServo(servoPos1); //vérifier l'intervalle [0 ; 175]
    servoPos2 = limitePosServo(servoPos2);
    servoPos3 = limitePosServo(servoPos3);
    servoPos4 = limitePosServo(servoPos4);

    /*  Serial.print ("servoPos1: ");// Serial.print reset la carte quand arduino essaye d'envoyer une info vers unity.
      Serial.println (servoPos1);
      Serial.print ("servoPos2: ");
      Serial.println (servoPos2);
      Serial.println ("");*/


 }
  myServoA.write(servoPos1);
  myServoB.write(servoPos2);
  myServoC.write(servoPos3);
  myServoD.write(servoPos4);
}//end loop


int limitePosServo(int valeur)
{
  if (valeur < 0)
  {
    valeur = 0;
  }

  if (valeur > 175)
  {
    valeur = 175;
  }
  return valeur;

}
