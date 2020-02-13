#include <Servo.h>

Servo myServoA;
Servo myServoB;
Servo myServoC;
Servo myServoD;
Servo myServoE;
Servo myServoF;

const int pinServo1 = 7; //shoulder
const int pinServo2 = 8; //elbow
const int pinServo3 = 9; //wrist
const int pinServo4 = 5; //waist
const int pinServo5 = 6; //wrist rotate
const int pinServo6 = 3; //hand

String incomming;
String maChaine1;
String maChaine2;
String maChaine3;
String maChaine4;
String maChaine5;
String maChaine6;

int servoPos1 = 130;
int servoPos2 = 140;
int servoPos3 = 110;
int servoPos4 = 50;
int servoPos5 = 50;
int servoPos6 = 50;

int oldServoPos1;
int oldServoPos2;
int oldServoPos3;
int oldServoPos4;
int oldServoPos5;
int oldServoPos6;

void setup()
{
  Serial.begin(115200);
  Serial.setTimeout(10);

  myServoA.attach(pinServo1);
  myServoB.attach(pinServo2);
  myServoC.attach(pinServo3);
  myServoD.attach(pinServo4);
  myServoE.attach(pinServo5);
  myServoF.attach(pinServo6);

  myServoA.write(servoPos1);
  myServoB.write(servoPos2);
  myServoC.write(servoPos3);
  myServoD.write(servoPos4);
  myServoE.write(servoPos5);
  myServoF.write(servoPos6);
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
    maChaine5 = incomming.substring(12, 15);
    maChaine6 = incomming.substring(15, 18);

    servoPos1 = maChaine1.toInt();
    servoPos2 = maChaine2.toInt();
    servoPos3 = maChaine3.toInt();
    servoPos4 = maChaine4.toInt();
    servoPos5 = maChaine5.toInt();
    servoPos6 = maChaine6.toInt();

    oldServoPos1 = servoPos1;
    oldServoPos2 = servoPos2;
    oldServoPos3 = servoPos3;
    oldServoPos4 = servoPos4;
    oldServoPos5 = servoPos5;
    oldServoPos6 = servoPos6;

    servoPos1 = limitePosServo(servoPos1); //vérifier l'intervalle [0 ; 175]
    servoPos2 = limitePosServo(servoPos2);
    servoPos3 = limitePosServo(servoPos3);
    servoPos4 = limitePosServo(servoPos4);
    servoPos5 = limitePosServo(servoPos5);
    servoPos6 = limitePosServo(servoPos6);

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
  myServoE.write(servoPos5);
  myServoF.write(servoPos6);
}//end loop


int limitePosServo(int valeur)
{
  if (valeur < 0)
  {
    valeur = 0;
  }

  if (valeur > 180)
  {
    valeur = 180;
  }
  return valeur;

}
