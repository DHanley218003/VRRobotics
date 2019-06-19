/*
 * Controlling a Mitsubishi Movemaster with an Arduino
 * by Taylor Schweizer
 * 
 * This sketch copies whatever characters are sent to the serial monitor to the parallel cable
 * 
 * More info at learn-cnc.com
 * 
 */
 
 
 
//Parallel Port Pins
const int strobe = 2;
const int data0 = 3;
const int data1 = 4;
const int data2 = 5;
const int data3 = 6;
const int data4 = 7;
const int data5 = 8;
const int data6 = 9;
const int data7 = 10;
const int ack = 11;
const int busypin = 12;
const int strobeDelayMicroseconds = 2;
 
//Serial monitor variables
const byte maxCommandLength = 32;//max length of data to be read on serial monitor
char receivedChars[maxCommandLength];//array to hold serial data
boolean newData = false;//variable if data was sent by serial monitor
 
 
void setup() {
  
  Serial.begin(9600);
 
  pinMode(strobe, OUTPUT);      // is active LOW
  digitalWrite(strobe, HIGH);   // set HIGH
  
  pinMode(data0, OUTPUT);//set all data pins to outputs
  pinMode(data1, OUTPUT);
  pinMode(data2, OUTPUT);
  pinMode(data3, OUTPUT);
  pinMode(data4, OUTPUT);
  pinMode(data5, OUTPUT);
  pinMode(data6, OUTPUT);
  pinMode(data7, OUTPUT);
 
  pinMode(ack, INPUT);     // is active LOW
  pinMode(busypin, INPUT);
 
  delay(5000);
 
  Serial.println("Startup complete");
}
 
void loop() {
    getSerialData();
 
    showNewData();
  
}
void showNewData() {
 
  if (newData == true) {
 
    for (int i = 0; i < maxCommandLength; i++) {
      if (receivedChars[i] != 0) {
        printByte(receivedChars[i]);
        Serial.print("Printing ");
        Serial.println(receivedChars[i],HEX);
        receivedChars[i] = 0;
      }
    }
 
 
    newData = false;
  }
}
void getSerialData() {
  static byte index = 0;
  char termChar = '\n';
  char rc;
 
  while (Serial.available() > 0 && newData == false) {
    rc = Serial.read();
 
    if (rc != termChar) {
      receivedChars[index] = rc;
      index++;
      if (index >= maxCommandLength) {
        index = maxCommandLength - 1;
      }
    }
    else {
      index = 0;
      newData = true;
    }
  }
}
 
void printByte(byte byteToPrint) {
  Serial.print("Printing Byte ");
  Serial.println(byteToPrint);
  while (digitalRead(busypin) == HIGH) {
    // wait for busy to go low
  }
 
  int b0 = bitRead(byteToPrint, 0);
  int b1 = bitRead(byteToPrint, 1);
  int b2 = bitRead(byteToPrint, 2);
  int b3 = bitRead(byteToPrint, 3);
  int b4 = bitRead(byteToPrint, 4);
  int b5 = bitRead(byteToPrint, 5);
  int b6 = bitRead(byteToPrint, 6);
  int b7 = bitRead(byteToPrint, 7);
 
  digitalWrite(data0, b0);        // set data bit pins
  digitalWrite(data1, b1);
  digitalWrite(data2, b2);
  digitalWrite(data3, b3);
  digitalWrite(data4, b4);
  digitalWrite(data5, b5);
  digitalWrite(data6, b6);
  digitalWrite(data7, b7);
 
  digitalWrite(strobe, LOW);       // strobe to input data bits
  delayMicroseconds(strobeDelayMicroseconds);
  digitalWrite(strobe, HIGH);
 
  while (digitalRead(busypin) == HIGH) {
    // wait for busy line to go low
  }
}
