/*
 Created by Rui Santos
 Visit: http://randomnerdtutorials.com for more arduino projects

 Arduino with Ethernet Shield
 */
#include <Arduino.h>
#include <SPI.h>
#include <Ethernet.h>
#include "Servo.h" 

Servo servo1; //Servo initialization
Servo servo2;
Servo servo3;
Servo servo4;
Servo servo5;
Servo servo6;

int initial_position = 140; //initial position of each servo
int initial_position1 = 120;
int initial_position2 = 50;
int initial_position3 = 110;
int initial_position4 = 160;
int initial_position5 = 140;

int servo1_pin= 8; //pine of the different servos
int servo2_pin= 9;
int servo3_pin= 5;
int servo4_pin= 3;
int servo5_pin= 6;
int servo6_pin= 7;

int x_key = A1; //pin of the x-axis
int y_key =A0; //pin of the y-axis
int SW = 2; //pin of the switch
int SW_state = 0; //state of the switch
int BP = 4;//pin of the button
int x_pos; //horizontal position variable
int y_pos; //vertical position variable

int MODE=0;//variable to choose the part controlled by the joystick

byte mac[] = { 0x90, 0xA2, 0xDA, 0x10, 0x30, 0x8F };   //physical mac address
byte ip[] = { 192, 168, 1, 177 };                      // ip in lan (that's what you need to use in your browser. ("192.168.1.178")
byte gateway[] = { 192, 168, 1, 1 };                   // internet access via router
byte subnet[] = { 255, 255, 255, 0 };                  //subnet mask
EthernetServer server(4200);                             //server port     
String readString;

void setup() {
 // Open serial communications and wait for port to open:
  Serial.begin(9600);
   while (!Serial) {
    ; // wait for serial port to connect. Needed for Leonardo only
  }
  servo1.attach (servo1_pin ) ; //where the servo pins are connected
  servo1.write (initial_position); //moved the servo motors to the initial position
  servo2.attach (servo2_pin ) ; 
  servo2.write (initial_position1);
  servo3.attach (servo3_pin ) ; 
  servo3.write (initial_position2);
  servo4.attach (servo4_pin ) ; 
  servo4.write (initial_position3); 
  servo5.attach (servo5_pin ) ; 
  servo5.write (initial_position4);
  servo6.attach (servo6_pin ) ; 
  servo6.write (initial_position5);
  pinMode (x_key, INPUT) ;//declaration of the vertical and horizontal pins of the joystick module                   
  pinMode (y_key, INPUT) ;
  pinMode(SW, INPUT_PULLUP);//declaration of the switch button pins 
  pinMode(BP, INPUT_PULLUP);//declaration of the pins of the BP button
  // start the Ethernet connection and the server:
  Ethernet.begin(mac, ip, gateway, subnet);
  server.begin();
  Serial.print("server is at ");
  Serial.println(Ethernet.localIP());
}


void loop() {
  // Create a client connection
  EthernetClient client = server.available();
  if (client) {
    while (client.connected()) {   
      if (client.available()) {
        char c = client.read();
     
        //read char by char HTTP request
        if (readString.length() < 100) {
          //store characters to string
          readString += c;
          //Serial.print(c);
         }

         //if HTTP request has ended
         if (c == '\n') {          
           Serial.println(readString); //print to serial monitor for debuging
     
           client.println("HTTP/1.1 200 OK"); //send new page
           client.println("Content-Type: text/html");
           client.println();     
           client.println("<HTML>");
           client.println("<HEAD>");
           client.println("<meta name='apple-mobile-web-app-capable' content='yes' />");
           client.println("<meta name='apple-mobile-web-app-status-bar-style' content='black-translucent' />");
           client.println("<link rel='stylesheet' type='text/css' href='http://randomnerdtutorials.com/ethernetcss.css' />");
           //client.println("<link rel="stylesheet" type="text/css" href="/Switch_button.css" />");
           client.println("<TITLE>Web Interface</TITLE>");
           client.println("</HEAD>");
           client.println("<BODY>");
           client.println("<H1>Control Panel</H1>");
           client.println("<hr />");
           client.println("<br />"); 
           client.println("<H2>Arduino with Ethernet Shield</H2>");     
           client.println("<br />"); 
           client.println("Shoulder:");
           client.println(initial_position);
           client.println("<a href=\"/?button2on\"\">Rotate Left</a>");
           client.println("<a href=\"/?button2off\"\">Rotate Right</a><br />"); 
           client.println("<br />");
           client.println("Elbow:"); 
           client.println(initial_position1);
           client.println("<a href=\"/?button3on\"\">Rotate Left</a>");
           client.println("<a href=\"/?button3off\"\">Rotate Right</a><br />");
           client.println("<br />");
           client.println("Direction of the hand:");
           client.println(initial_position2); 
           client.println("<a href=\"/?button4on\"\">Rotate Left</a>");
           client.println("<a href=\"/?button4off\"\">Rotate Right</a><br />"); 
           client.println("<br />"); 
           client.println("Rotation of the hand:");
           client.println(initial_position3);
           client.println("<a href=\"/?button5on\"\">Rotate Left</a>");
           client.println("<a href=\"/?button5off\"\">Rotate Right</a><br />"); 
           client.println("<br />");
           client.println("Opening of the hand:");
           client.println(initial_position4); 
           client.println("<a href=\"/?button6on\"\">Rotate Left</a>");
           client.println("<a href=\"/?button6off\"\">Rotate Right</a><br />");
           client.println("<br />");
           client.println("Reset Position"); 
           client.println("<a href=\"/?button8\"\">Reset</a><br />");  
           client.println("<br />"); 
           
           client.println("</BODY>");
           client.println("</HTML>");
     
           delay(1);
           //stopping client
           client.stop();
           //controls the Arduino if you press the buttons
        
           if (readString.indexOf("?button2on") >0){
                initial_position += 5;  // goes from 0 degrees to 180 degrees 
                {                                  // in steps of 1 degree 
                  servo1.write(initial_position);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button2off") >0){
                initial_position -=5;     // goes from 180 degrees to 0 degrees 
                {                                
                  servo1.write(initial_position);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button3on") >0){
                initial_position1 += 5;  // goes from 0 degrees to 180 degrees 
                {                                  // in steps of 1 degree 
                  servo2.write(initial_position1);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button3off") >0){
                initial_position1 -=5;     // goes from 180 degrees to 0 degrees 
                {                                
                  servo2.write(initial_position1);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button4on") >0){
                initial_position2 += 5;  // goes from 0 degrees to 180 degrees 
                {                                  // in steps of 1 degree 
                  servo3.write(initial_position2);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button4off") >0){
                initial_position2 -=5;     // goes from 180 degrees to 0 degrees 
                {                                
                  servo3.write(initial_position2);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button5on") >0){
                initial_position3 += 5;  // goes from 0 degrees to 180 degrees 
                {                                  // in steps of 1 degree 
                  servo4.write(initial_position3);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button5off") >0){
                initial_position3 -=5;     // goes from 180 degrees to 0 degrees 
                {                                
                  servo4.write(initial_position3);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button6on") >0){
                initial_position4 += 5;  // goes from 0 degrees to 180 degrees 
                {                                  // in steps of 1 degree 
                  servo5.write(initial_position4);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button6off") >0){
                initial_position4 -=5;     // goes from 180 degrees to 0 degrees 
                {                                
                  servo5.write(initial_position4);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button7on") >0){
                initial_position5 += 5;  // goes from 0 degrees to 180 degrees 
                {                                  // in steps of 1 degree 
                  servo6.write(initial_position5);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button7off") >0){
                initial_position5 -=5;     // goes from 180 degrees to 0 degrees 
                {                                
                  servo6.write(initial_position5);              // tell servo to go to position in variable 'pos' 
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
           if (readString.indexOf("?button8") >0){
                {                                
                  servo1.write (140);
                  servo2.write (120);
                  servo3.write (50);
                  servo4.write (110); 
                  servo5.write (160); 
                  servo6.write (140);              // tell servo to go to position in variable 'pos' 
                  initial_position = 140; //initial position of each servo
                  initial_position1 = 120;
                  initial_position2 = 50;
                  initial_position3 = 110;
                  initial_position4 = 160;
                  initial_position5 = 140;
                  delay(15);                       // waits 15ms for the servo to reach the position 
                } 
           }
            //clearing string for next read
            readString="";  
           
         }
       }
    }
}
x_pos = analogRead (x_key) ;  //read horizontal position values from the joystick module and record them in the variables
  y_pos = analogRead (y_key) ;  //read vertical position values from the joystick module and record them in the variables                    
  
  
if (digitalRead(SW)==0)// If we're here, it's because the button was just pushed.                  
  {delay(50);// We're still waiting 50ms, just long enough to stop the rebounds of the button
  while (digitalRead(SW)==0)//Time the button is not released, we're still waiting
  {  delay(1);}
    // And finally, when we get here, then it's because the button just got released.
    // So we can do an action, like change the MODE value.
  MODE=!MODE;}
if (digitalRead(BP)==1)
  {delay(50);
  while(digitalRead(BP)==1)
  {delay(1);}
  MODE=2;}
 
Serial.println(MODE);//Prints data to the serial port

switch (MODE)//Using the switch case to choose the part controlled by the joystick
{
  case 0:

  if (x_pos < 300)//if the horizontal position value is less than 300, the first servo will move to the right.
  {
  if (initial_position < 10) 
  { } 
  else{ initial_position = initial_position - 5; 
  servo1.write ( initial_position ) ; 
  delay (100) ; 
  } } 
  if (x_pos > 700)//If the horizontal position value is greater than 700, the servo will move to the left. 
  {
  if (initial_position > 180)
  {  
  }  //The same applies to the vertical position of the joystick module
  else{
  initial_position = initial_position + 5;
  servo1.write ( initial_position ) ;
  delay (100) ;
  }
  }

  if (y_pos < 300)
  {
  if (initial_position1 < 10) 
  { } 
  else{ initial_position1 = initial_position1 - 5; 
  servo2.write ( initial_position1 ) 
  ; delay (100) ; 
  } } 
  if (y_pos > 700)
  {
  if (initial_position1 > 180)
  {  
  }        
  else{
  initial_position1 = initial_position1 + 5;
  servo2.write ( initial_position1 ) ;
  delay (100) ;
  }
  }
  break;
  
  case 1:
  
  if (x_pos < 300)
  {
  if (initial_position2 < 10)
  { } 
  else{ initial_position2 = initial_position2 - 5; 
  servo3.write ( initial_position2 ) ; 
  delay (100) ; 
  } } 
  if (x_pos > 700)
  {
  if (initial_position2 > 180)
  {  
  }  
  else{
  initial_position2 = initial_position2 + 5;
  servo3.write ( initial_position2 ) ;
  delay (100) ;
  }
  }

  if (y_pos < 300)
  {
  if (initial_position3 < 10) 
  { } 
  else{ initial_position3 = initial_position3 - 5; 
  servo4.write ( initial_position3 ) ; 
  delay (100) ;
  } } 
  if (y_pos > 700)
  {
  if (initial_position3 > 180)
  {  
  }        
  else{
  initial_position3 = initial_position3 + 5;
  servo4.write ( initial_position3 ) ;
  delay (100) ;
  }
  }
    break;
   case 2:
  if (x_pos < 300)
  {
  if (initial_position4 < 10) 
  { } 
  else{ initial_position4 = initial_position4 - 5; 
  servo5.write ( initial_position4 ) ; 
  delay (100) ; 
  } } 
  if (x_pos > 700)
  {
  if (initial_position4 > 180)
  {  
  }  
  else{
  initial_position4 = initial_position4 + 5;
  servo5.write ( initial_position4 ) ;
  delay (100) ;
  }
  }
   if (y_pos < 300)
   {
  if (initial_position5 < 10) 
  { } 
  else{ initial_position5 = initial_position5 - 5; 
  servo6.write ( initial_position5 ) ; 
  delay (100) ; 
  } } 
  if (y_pos > 700)
  {
  if (initial_position5 > 180)
  {  
  }        
  else{
  initial_position5 = initial_position5 + 5;
  servo6.write ( initial_position5 ) ;
  delay (100) ;
  }
  }
    break;
  }}
