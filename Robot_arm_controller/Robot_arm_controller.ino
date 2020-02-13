#include <Servo.h>

Servo shoulderServo;
Servo elbowServo;
Servo wristServo;
Servo waistServo;
Servo wristRotateServo;
Servo handServo;

const int shoulderPin = 7; //shoulder
const int elbowPin = 8; //elbow
const int wristPin = 9; //wrist
const int waistPin = 5; //waist
const int wristRotatePin = 6; //wrist rotate
const int handPin = 3; //hand

String incomming;
String shoulderString;
String elbowString;
String wristString;
String waistString;
String wristRotateString;
String handString;

int shoulderServoPosition = 130;
int elbowServoPosition = 140;
int wristServoPosition = 110;
int waistServoPosition = 50;
int wristRotateServoPosition = 50;
int handServoPosition = 50;

int oldShoulderServoPosition;
int oldElbowServoPosition;
int oldWristServoPosition;
int oldWaistServoPosition;
int oldWristRotateServoPosition;
int oldHandServoPosition;

void setup()
{
  Serial.begin(2000000);
  Serial.setTimeout(10);

  shoulderServo.attach(shoulderPin);
  elbowServo.attach(elbowPin);
  wristServo.attach(wristPin);
  waistServo.attach(waistPin);
  wristRotateServo.attach(wristRotatePin);
  handServo.attach(handPin);

  shoulderServo.write(shoulderServoPosition);
  elbowServo.write(elbowServoPosition);
  wristServo.write(wristServoPosition);
  waistServo.write(waistServoPosition);
  wristRotateServo.write(wristRotateServoPosition);
  handServo.write(handServoPosition);

  Serial.println("RobotArm Ready.");
}

void loop()
{
  if (Serial.available() > 0)
  {
    incomming = Serial.readString();

    if(incomming.substring(0,1) == "R")
    {
      resetPositions();
    } else
    if(incomming.substring(0,1) == "?")
    {
      printPositions();
    } else
    if(incomming.substring(0,1) == "W" && incomming.length() >=4)
    {
      waistString = incomming.substring(1, 4);
    } else
    if(incomming.substring(0,1) == "S" && incomming.length() >=4)
    {
      shoulderString = incomming.substring(1, 4);
    } else
    if(incomming.substring(0,1) == "E" && incomming.length() >=4)
    {
      elbowString = incomming.substring(1, 4);
    } else
    if(incomming.substring(0,1) == "Q" && incomming.length() >=4)
    {
      wristString = incomming.substring(1, 4);
    } else
    if(incomming.substring(0,1) == "T" && incomming.length() >=4)
    {
      wristRotateString = incomming.substring(1, 4);
    } else
    if(incomming.substring(0,1) == "H" && incomming.length() >=4)
    {
      handString = incomming.substring(1, 4);
    } else
    if(incomming.length() >= 18)
    {
      waistString = incomming.substring(0, 3);
      shoulderString = incomming.substring(3, 6);
      elbowString = incomming.substring(6, 9);
      wristString = incomming.substring(9, 12);
      wristRotateString = incomming.substring(12, 15);
      handString = incomming.substring(15, 18);
  
      shoulderServoPosition = shoulderString.toInt();
      elbowServoPosition = elbowString.toInt();
      wristServoPosition = wristString.toInt();
      waistServoPosition = waistString.toInt();
      wristRotateServoPosition = wristRotateString.toInt();
      handServoPosition = handString.toInt();
  
      oldShoulderServoPosition = shoulderServoPosition;
      oldElbowServoPosition = elbowServoPosition;
      oldWristServoPosition = wristServoPosition;
      oldWaistServoPosition = waistServoPosition;
      oldWristRotateServoPosition = wristRotateServoPosition;
      oldHandServoPosition = handServoPosition;
  
      shoulderServoPosition = limitePosServo(shoulderServoPosition); //vérifier l'intervalle [0 ; 175]
      elbowServoPosition = limitePosServo(elbowServoPosition);
      wristServoPosition = limitePosServo(wristServoPosition);
      waistServoPosition = limitePosServo(waistServoPosition);
      wristRotateServoPosition = limitePosServo(wristRotateServoPosition);
      handServoPosition = limitePosServo(handServoPosition);
    } else
    if(incomming.length() > 0)
    {
      Serial.print("Invalid command: ");
      Serial.print(incomming);
      incomming = "";
    }
   }
   shoulderServo.write(shoulderServoPosition);
   elbowServo.write(elbowServoPosition);
   wristServo.write(wristServoPosition);
   waistServo.write(waistServoPosition);
   wristRotateServo.write(wristRotateServoPosition);
   handServo.write(handServoPosition);
}//end loop

void resetPositions()
{
 shoulderServoPosition = 130;
 elbowServoPosition = 140;
 wristServoPosition = 110;
 waistServoPosition = 50;
 wristRotateServoPosition = 50;
 handServoPosition = 50;
}

void printPositions()
{
 Serial.print ("WaistServoPosition: ");// Serial.print reset la carte quand arduino essaye d'envoyer une info vers unity.
 Serial.println (waistServoPosition);
 Serial.print ("ShoulderServoPosition: ");
 Serial.println (shoulderServoPosition);
 Serial.print ("elbowServoPosition: ");
 Serial.println (elbowServoPosition);
 Serial.print ("WristServoPosition: ");
 Serial.println (wristServoPosition);
 Serial.print ("WristRotationServoPosition: ");
 Serial.println (wristRotateServoPosition);
 Serial.print ("HandServoPosition: ");
 Serial.println (handServoPosition);
}

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
