# vrrobotics-clementlb
vrrobotics-clementlb created by GitHub Classroom



Robotic arm controlled by Arduino

Internship report of 2nd year 
DUT GEII Year 2019:

Internship Tutor  : 
John Barrett
IUT Tutor  : 
Claire Constantin




Author :
Le Berre Clément
IUT of Rennes

Acknowledgement
First of all, I would like to thank the teaching team of the Electrical Engineering and Industrial Informatics department of the IUT of Rennes, for having provided the training within the establishment. I also thank Claire Constantin for giving me the opportunity to do my internship at the Athlone Institute of Technology.
The internship opportunity I had at the Athlone Institute of Technology was an excellent opportunity for learning and professional development. Therefore, I consider myself a very lucky person because I was given the opportunity to be a part of it. I am also grateful to have had the opportunity to meet so many wonderful people and professionals who guided me during this internship period.
Keeping in mind that I would like to take this opportunity to express my deep gratitude and special thanks to John Barrett, my internship tutor, who, despite being extraordinarily busy with his duties, took the time to listen, guide and keep me on the right track and allow me to carry out my project.
In addition, I would like to thank Cian Bregazzi-Nevin and Tom Bennett, who are colleagues of John's from the Polymer, Mechanics and Design Department. They provided me with the help I needed to move forward with my project.
I see this opportunity as an important step in my career development. I will strive to use the skills and knowledge acquired in the best possible way and will continue to work to improve them in order to achieve the desired career objectives. 
TABLE OF CONTENT
Acknowledgement	2
Introduction	4
1. Presentation of the Athlone Institute of Technology	5
A. Main presentation of the university	5
B. The different teachings	8
C. University culture	8
D. Faculty of Engineering & Informatics	9
2. General presentation of the subject	10
A. The RV-M1 robot	10
B. Test robot	12
C. The equipment	13
D. Terms of reference	15
3. Control of the test robot	16
A. Reconstruction and connections	16
B. Programming	17
4. Creation of the web interface	20
A. Web interface with the notepad	20
B. Web interface in Arduino code	21
5. Connection and operation	23
A. Using Shield Ethernet	23
B. The functions of the buttons	24
C. Using the web interface	24
6. Interface Unity	25
A. Blender	25
B. Unity	25
C. Unity to Arduino communication	27
7. Centronics connector	29
8. Control of the RV-M1	31
9. Virtual reality simulation	33
Conclusion	34
Appendix 1	35
Appendix 2	40

Introduction
As part of my training as a DUT in Electrical Engineering and Industrial Computing, I had the opportunity to do my final internship at the Athlone Institute of Technology in Ireland. This internship lasted for 11 weeks, from April 8, 2019 to June 21, 2019. It was an opportunity to put into practice many of the skills I had acquired over the past two years, to learn new ones and to improve my English.
Robotics is a field that particularly attracts me, because this field represents new technologies. So, to have a first experience in this field, I decided to do my final internship at Athlone Institute of Technology which offered an internship in the field that interested me. In addition, during my university career, I was very interested in electronics and programming. This internship is therefore for me, the opportunity to confirm my taste for these two subjects, in a field in line with my professional aspirations, embedded systems.
The mission I have been assigned will be to control a Mitsubishi articulated arm robot. It will be necessary to study the documentation, configure the robotic arm and wire it. It will then be a question of removing the TeachBox part of the robot and replacing it with a program on an Arduino card that will be controlled on a web interface first. It will be necessary to create a web interface to be able to control the robot from a computer or smartphone connected to the same network. The technical challenge will be to create a 3D interface to control the robot from a virtual reality headset.
In the first part, I would present the AIT and more particularly, the engineering and informatics entity where my internship took place. The second part will focus on the mission that has been proposed to me, addressing the problem, research and related solutions. Finally, the third part will discuss the progress of the project and a personal synthesis of this experience. 
1. Presentation of the Athlone Institute of Technology
A. Main presentation of the university
The AIT was officially founded in 1970 as the Athlone Regional Technical College. It is a higher education institution in the Irish Midlands region. It is located in the city of Athlone, which is in the heart of Ireland. 






In 2004, AIT opened the Midlands Innovation and Research Centre at the Athlone Institute of Technology (MIRC), in partnership with Enterprise Ireland. It is a centre that provides incubation and support services to innovative companies. Since its opening it has supported more than 200 knowledge-based start-ups, including 20 Enterprise Ireland High Potential start-ups. Innocoll. Enterprise Ireland recently provided €3 million in funding to support MIRC's expansion over 1,135 m². The extension will bring the total footprint of the Centre to nearly 2,000 m².

Since September 2016, MIRC has been a site of ESA Space Solutions Ireland, which supports the application of space technologies to solve problems on Earth and beyond, and works with entrepreneurs to transform business ideas connected to space into business ventures.
Since its creation, the campus has expanded slightly, including two recent buildings for hotel, tourism and leisure studies (2003), nursing and health (2005), as well as a midlands innovation and research centre (2005), an engineering and IT building (2010) and finally a postgraduate research centre (2010). In 2013, the institute inaugurated a 2,000-seat stadium and 9,715 m² of sports facilities.

Today, AIT employs about 500 people. The university staff is divided into three branches, first there is the President's Office which brings together the direct partners of the AIT President to manage different sectors such as finance, communication, marketing. Then there are the administrative staff who take care of all the administrative procedures of each department. Finally, we have the academic staff that brings together lecturers and research professors who offer courses from academic staff who are experts in their field, many of whom have extensive experience in the sector.
As we have seen from its structure, AIT works in the same way as a company with different services.
Currently, Athlone IT welcomes more than 6,000 students of more than 53 nationalities. International students are welcomed and supported by AIT staff.
B. The different teachings
The AIT is composed of several faculties divided into departments. 

C. University culture
The culture of the university institute is oriented towards current life issues such as sexism and immigration. The University Institute aims to address gender imbalances in science, technology, engineering, mathematics and medicine (STEMM), based on the belief that efforts in these fields will be enriched when they can benefit from the talent of the entire population and when barriers to progress in academic careers are removed. This is why the Athena SWAN Charter was created, it recognizes and celebrates good practices in the recruitment, retention and promotion of women in STEMM fields in higher education.
Athlone Institute of Technology (AIT) has been designated a College of Sanctuaries in recognition of numerous initiatives demonstrating its commitment to welcoming asylum seekers and refugees into the academic community and promoting a culture of inclusion for all.
D. Faculty of Engineering & Informatics
The Faculty of Engineering and Computer Science is the most off-centre building on campus, you have to cross the entire campus to be able to access it. It is composed of two floors and the ground floor. The ground floor is mainly composed of a practical work room, then on the first floor we have classrooms and research labs, and finally on the top floor we have offices reserved for research professors and the administration of the faculty. This faculty is divided into three departments, the Mechanical/Polymer Engineering Department, the Civil, Construction & Mineral Department and finally the Electronics and Computer Science Department.
It was in the electronics and computer science department that I did my internship. I worked in several rooms to which I had access thanks to my badge, I spent the most time in a room with doctoral students who were carrying out innovative projects.
2. General presentation of the subject
The main objective of my internship is to be able to remotely control the RV-M1 robot from Mitsubishi with a virtual reality headset. 
To begin with, I had an Arduino UNO R3 card with a joystick, a Labdec plate and a robotic arm to repair. From that point on, I knew that to achieve the main objective I had to work step by step. The recommendations I received to carry out this project were to create a program to be able to control the robotic arm with a joystick and a button. The button was used to change the part controlled by the joystick. Then, the creation of a web interface to be able to control the Arduino card from a website. This web interface will make it possible to remove the TeachBox part of the robot and replace it with a program on the Arduino card that will be remotely controlled on a web interface. Finally, it will be necessary to set up a system to control the RV-M1 robot with virtual reality. 
A. The RV-M1 robot
The RV-M1 robot is a robotic arm that copies the movements of the human arm from the waist to the hand. The robot articulation names correspond to the human body as illustrated below. This robot is divided into several parts to represent the different members of the human being. To begin with, we have the size that is represented by the base with the rotating part placed on J1 axis. Then we have the shoulder which is represented by J2 axis, which is assembled with the upper arm and then the elbow by J3 axis, followed by the lower part of the arm and finally the wrist which is divided into two parts one for the wrist inclination represented by J4 axis and the other for the wrist rotation represented by J5 axis.

The robot can be controlled in different ways, the main way to control this robot is to use the teachbox which has all the controls but is not very intuitive. Then we can use the CENTRONICS and RS232C connectors to control the robot from a computer. And we have the possibility to use the connector for external I/O equipment to control the robot with microcontrollers for example. In our project we will use the CENTRONICS connector to connect it to the Arduino card.

As can be seen in the figure above, this robot is used in different fields for various tasks that require a certain precision.
B. Test robot
To carry out this internship, I was provided with a smaller robotic arm than the RV-M1 robot, this robot served as a guinea pig to understand the operation of the servomotors, the connections to be made to connect the robot to the Arduino board. When I received the robot, the hand was not connected to the arm. It was therefore necessary to repair the robot and check the connections of each servomotor connected to a plate. On this plate, all the robot connections are on it to facilitate the supply of each servomotor. Each servomotor has a cable to communicate with the Arduino card.

C. The equipment
During this internship, I was provided with an Arduino Uno R3 card, it is a very basic programmable card. It was used to program the test robot. A LABDEC plate was at my disposal to make all the connections to be made between the robot and the Arduino board, as well as the possibility of adding buttons, leds and a joystick. A little later in my project the need to use an Ethernet shield came to connect the Arduino card to a web interface. On the laptop computer that was provided to me the software I used was the Arduino's integrated development environment, then I used the Notepad to create the HTML and CSS pages and a search engine to connect the web interface with the card and be able to control it. A 26-pin connector is required to connect the robot to the Arduino board.





D. Terms of reference
To successfully complete this project, knowledge of the use of software such as Unity is important to be able to perform the digital representation of the robot. This will allow you to see the robot's position in real life when the robot is controlled from a virtual reality headset. The technical documentation as well as the realization of wiring diagrams between the robot and the Arduino board will be necessary so that the project can be understood and taken over by someone else. Replacing the robot's teachbox with an Arduino card requires complex wiring because we use a connector similar to that of the teachbox. The purpose of creating the web interface is to connect the robot to the Internet and therefore to be able to control it remotely without being in the same room. The web interface must be simple and easy to understand for quick use. Knowledge of HTML, CSS and Javascript programming languages is essential to create this web interface. Programming the Arduino card will allow the robot to operate correctly so it will be necessary to describe the code and explain it so that everyone can understand the program. The connection and communication between each part represents the last step of the project, the robot will be able to send the positions of each of its members on the website or the Unity interface, and it will be able to be controlled by joystick, website, Unity interface and therefore by virtual reality.
3. Control of the test robot
A. Reconstruction and connections
To start working on the robot that will be used as a guinea pig before moving on to the RV-M1 robot, it was necessary to screw the robot's hand to the rest of the arm and connect the servomotors to the Arduino card. After identifying the location and movement of each servomotor, it was possible to connect each servomotor to a pin that will be dedicated to it on the Arduino card. The robot consists of six servomotors that are connected to a control module to facilitate their power supply and connection. To be able to control the servomotors, the solution implemented was to use a joystick and a button also connected to the Arduino card. The LABDEC plate used was mainly used to supply each part of the assembly. For the proper functioning of the robot, the assembly diagram used is as follows:


B. Programming
After making all the necessary connections for the robot to work properly, we must proceed to programming the card to control the robot using the joystick and the button we have. Before starting it was important to research the programming of the servomotors and joystick to understand how they work and to find out the different possibilities. Once the research was done, we found a simple way to control the servomotors with the joystick.


When the joystick module moves in the horizontal or in the vertical direction, it gives us values from 0 to 1023. So we can apply a condition in the code that if the value is less than 300 or greater than 700, then the servos will move. When the joystick is moved in the horizontal direction, the first servo will move toward right or left and upon moving the joystick in the vertical direction, the second servo will move towards the right or left. But at this point we have a problem that corresponds to the number of servomotors that can be controlled with only one joystick. 

To solve this problem, the solution found was to use the joystick switch and the button we have to change the joystick mode. That is, when the joystick changes mode it controls two other servomotors. As we have six servomotors, there are three modes, the first one when the card is started we are in mode 0, to control the robot's shoulder and elbow. Then mode 1, allows you to control the direction and rotation of the hand. And finally mode 2, allows you to control the opening of the hand as well as the rotation of the waist. 


To conclude on the first programming of the test robot, the arm control with the joystick works correctly and as follows. When you switch on the Arduino car, the robotic arm takes its initial position. With the joystick we can control the movement of shoulder and elbow. When we press on the joystick, we change mode of control. Now the joystick control the rotation and position of the hand. When we press on the button, we already change mode of control. Now the joystick control the opening of the hand and rotation of waist. Each time the joystick control two servo motors at the same time. The position vertical allows for control first servo motor and the position horizontal allows for control the second servo motor. 
See appendix 1 Control Servomotor code
4. Creation of the web interface
The objective of the web interface is to be able to control the Arduino card and therefore the robot from a computer or smartphone. To create this interface, you must first create an HTML page, a CSS page and use Javascript to connect the interface with the Arduino code.
A. Web interface with the notepad
The creation of an HTML page is done intuitively thanks to the presence of tags that allow to structure the content of the page. HTML is a computer language used on the Internet. This language is used to create web pages. The acronym stands for HyperText Markup Language. This meaning is well named because this language allows to realize hypertext based on a tagging structure. 
After a lot of research to know how to create an organized HTML page, I created an interface composed of four pages that I thought was necessary to control the robot but it was too complicated because I was interested in using links to navigate from page to page using a menu, as well as using an ON/OFF switch. 


Each item in the menu leads to a new page, it works like links. 
The Switch changes when it is pressed.
This allowed me to understand how these functions work, but it was necessary to make a simple page with only the movements that the robot can perform. In addition, there is a method to connect the Arduino card to the HTML page, but this solution was complicated to understand. It involved creating URLs in the Arduino code to find the page on the Internet. So it is possible to connect the Arduino card to an HTML page that is not in the Arduino code, this track is complex but can be useful to modify the HTML page more easily than in the Arduino code and also use the layout with a CSS page. 




B. Web interface in Arduino code
After the problem encountered to connect the HTML page with the Arduino code. The solution to this problem was to create the HTML code in the Arduino code. Some modifications must be made to layout the HTML page. As the connection to the server is made in the Arduino code, it allows you to use the HTML page when you enter the IP address of the server in a search browser. It is therefore not necessary to make a connection between the two different codes. The use of a CSS page is more complicated to link to the HTML page because the codes are not in the same folder. 

The HTML code being located in the Arduino code, it is quite repetitive because Arduino is not made to create HTML pages. The 'client.println' function allows you to send the HTML code to the browser. So to be able to read the code correctly and perform the different tags associated with each part of it such as the layout with titles and line breaks for example. The tag <a> defines a hypertext link to link the functions that control the servomotors to the buttons on the HTML page.
5. Connection and operation
A. Using Shield Ethernet

#include is used to include external libraries such as Serial Peripheral Interface and Ethernet.

Here, we have entered the different addresses used and we use port 4200 to communicate between the website and the Arduino card.

We activate the server and the Ethernet connection.


In the loop, we wait for a client to connect before launching the HTML page. To connect, the client must enter the IP address of the server on its browser. This action will give the server the indication that a client is connected and therefore display the HTML page.
B. The functions of the buttons

The function 'readString.indexOf' allows you to know when there is a press on 'button2on'. In this case, the HTML page sends a message to the Arduino card telling it to change the value of' initial_position' to 'servo1'. As a result, the corresponding actuator in the robot changes position.
C. Using the web interface

At the top left of the page, we can see the IP address and port used to communicate entered in the search bar. In the middle, for each part of the robot we have a value that represents the angle of the servomotor. We can choose to increase it by 5 or reduce it by 5 depending on the button we choose. At the bottom, we have a 'Reset' button that allows you to reset the initial values for all servomotors.
6. Interface Unity
A. Blender
To create the Unity interface I encountered some problems to create parent/child links between the different objects. In Unity, child objects distorted according to the parent object, creating unwanted shapes. I asked my colleagues for help in finding a solution to my problem and the solution I received was to use Blender to model the robot with parallelograms. 
Indeed on Blender modeling is easier than on Unity, we can change the shape of parent objects without changing the shape of children. In addition there is the possibility to change the center of rotation of an object. This feature makes it possible to make the object closer to reality when rotating it. The children's objects follow the rotation of the parents' objects, which makes it possible to obtain an articulated arm with only parallelograms. Before being able to move each of the objects we must import the model on Unity, for that we must export the model from Blender in .fbx format. 

B. Unity
Once the model is imported into Unity, we can see that it is present in the assets folder. To display it in the Unity scene, simply create a GameObject and add the template to it. We can see that the model is divided into four parts, so we need four sliders to control the rotation of each part. All you have to do is create a canvas and add four sliders, change their values from 0 to 180. For the moment, we only have the visual, nothing works because it lacks the programming of the sliders with the models. The code used must be added to the GameObject. Then we can see that we have the possibility to enter new data. Each part of the model is assigned to a slider. And each slider is linked to the GameObject code. 

When the scene is started, we are in the Game menu, the code sends a string of numbers corresponding to the rotation angle of each servomotor. The number string is sent in the variable'My String' in the script. The sliders allow you to change the number string sent and therefore to change the rotation angle of the servomotors concerned.

See Appendix 2 for the code.
C. Unity to Arduino communication 
We have seen that sliders make it possible to change the number string sent and therefore to change the rotation angle of the servomotors concerned. But in reality, the number chain is processed by the Arduino code that sends the data to the servomotors. 




I created strings of 3 characters each time.

Then the STring is converted to Int. 

So that the information sent to the Arduino card is numbers and not letters.

Each servo motor therefore receives a position corresponding to the sent character string.
The servomotors execute the information received.

7. Centronics connector
To connect the Arduino board to the RV-M1 robot we used a connector to connect it to the Centronics part of the control box. The cable consists of 26 pins. We need to use only 12 pins. We have the Strobe pin, that when the host (computer) pulls the STROBE pin low, the device reads the 8 data pins. Then we have 8 data pins, each corresponding to a single bit. There is also a BUSY and an ACK pin. And to finish a mass on pine 12.




8. Control of the RV-M1
Once the connections are made, we will program the Arduino card to be able to control the RV-M1. The code we will use is the one found on the LEARN-CNC website (https://learn-cnc.com/mitsubishi-movemaster-joint-tracking-part-2-communication/ ). Once the card is programmed, all that remains is to turn on the robot and connect the card to the computer. We must send commands already initialized in the control box memory. The commands we can use are in the robot's technical documentation. I have noted the commands we can use to control the robot and I have created a protocol to initialize the robot before we can enter the commands we want. To start, type the "NT" and "GC" commands into the serial monitor. The robot will initialize its starting positions and close its hand when it is finished. Then we can change its speed with the command "SP" followed by a number to choose the speed. 

From there, we have several possibilities. We can initialize the robot's original position 0 with the "OG" command, which allows us to use the "MJ" command to choose the rotation of the angle of each servomotor precisely. This method only allows to increase or decrease the angle, it does not allow to choose the robot position directly. As an alternative, we can move the hand in space with the "MP" command but with this command we encounter too much illegal position for the robot when it is the command that allows to position the hand in the most precise way. And as a last solution, it is possible to use the "MO" command followed by a number. This allows the robot to move to an initialized position in its memory. This solution would allow to add new positions in the robot's memory that correspond to what you want to have. 





9. Virtual reality simulation
The equipment that was provided to me to control the robot in a virtual reality simulation is a HTC Vive with its controllers. The objective is to use the Unity interface with sliders by being in virtual reality to control the robot only by using his hands to drag the sliders and thus move the robot as we have seen in the Unity interface. To start in virtual reality we will simply use two buttons that have different positions to send to the robot, it will be enough to press one of the buttons for the robot to move but for the virtual reality part it will be necessary to record the position that the robot is supposed to have and perform the movement to have a visual on the position of the robot without removing the virtual reality helmet.
Conclusion
First of all, from a technical point of view, the mission I was given was to control a Mitsubishi articulated arm robot. I succeeded in achieving this objective at the end of the internship. The majority of the specifications were respected. We found the way to cable the robot with the Arduino card, the Unity interface works on the test robot but the time of the internship did not allow to explore the part to control the robot with a virtual reality headset, I would have liked to finish this very interesting part. The web interface also works on the small robot and allows it to be controlled on a local network. So what remains to be finalized is the part on virtual reality that remains something possible to achieve.
Finally, from a personal point of view, it was a very rewarding internship for me. Indeed, this was my first professional experience directly related to my studies.
This allowed me to apply many of the concepts covered at the IUT, particularly in industrial computing and electronics. I was also able to deepen these notions through the learning of new programming languages and the use of new software such as Unity and Blender.
I am very satisfied with the results of this internship, I have mobilized thinking and research skills to solve the problems I have encountered. This internship confirms my desire to work in the field of programming and electronics. I had a lot of fun programming and using different means to control the small robot. The best moments were when I got stuck for several days on a stage and finally managed to get the robot running. This experience allowed me to improve my English, learn to use new software and know how to solve problems encountered during this internship.
Appendix 1
#include <Arduino.h>
#include "Servo.h"

Servo servo1; //Servo initialization
Servo servo2;
Servo servo3;
Servo servo4;
Servo servo5;
Servo servo6;

//Initialization of pins and variables
int x_key = A1; //pin of the x-axis
int y_key =A0; //pin of the y-axis
int SW = 2; //pin of the switch
int SW_state = 0; //state of the switch
int BP = 4;//pin of the button
int x_pos; //horizontal position variable
int y_pos; //vertical position variable
int servo1_pin= 8; //pine of the different servos
int servo2_pin= 9;
int servo3_pin= 10;
int servo4_pin= 11;
int servo5_pin= 12;
int servo6_pin= 7;
int initial_position = 140; //initial position of each servo
int initial_position1 = 120;
int initial_position2 = 50;
int initial_position3 = 110;
int initial_position4 = 160;
int initial_position5 = 140;

int MODE=0;//variable to choose the part controlled by the joystick

void setup() {
  Serial.begin (9600);
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
}

void loop() {
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
  }
}

Appendix 2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;

public class ControlArduino : MonoBehaviour
{
    SerialPort serial;
    public string myString;
    public float comRapidity = 2.0f;
    public string portName;
    float temps = 0.0f;
    float delay = 0.0f;
    bool setPort = true;

    public int servoDegre1; //Degré value
    public Transform servo1;
    public Slider sliderServo1;
    string A;
    public int servoDegre2; //Degré value
    public Transform servo2;
    public Slider sliderServo2;
    string B;
    public int servoDegre3; //Degré value
    public Transform servo3;
    public Slider sliderServo3;
    string C;
    public int servoDegre4; //Degré value
    public Transform servo4;
    public Slider sliderServo4;
    string D;


    // Start is called before the first frame update
    void Start()
    {
        serial = new SerialPort();
    }

    // Update is called once per frame
    void Update()
    {
        if (servoDegre1 != (sliderServo1.value))
        {
            if (servoDegre1 < (sliderServo1.value))
            {
                servoDegre1 = servoDegre1 + 1;
            }
            if (servoDegre1 > (sliderServo1.value))
            {
                servoDegre1 = servoDegre1 - 1;
            }
        }
        servo1.localRotation = Quaternion.AngleAxis(servoDegre1, Vector3.back);
        A = servoDegre1.ToString("000");
        if (servoDegre2 != (sliderServo2.value))
        {
            if (servoDegre2 < (sliderServo2.value))
            {
                servoDegre2 = servoDegre2 + 1;
            }

            if (servoDegre2 > (sliderServo2.value))
            {
                servoDegre2 = servoDegre2 - 1;
            }
        }
        servo2.localRotation = Quaternion.AngleAxis(servoDegre2, Vector3.up);
        B = servoDegre2.ToString("000");
        if (servoDegre3 != (sliderServo3.value))
        {
            if (servoDegre3 < (sliderServo3.value))
            {
                servoDegre3 = servoDegre3 + 1;
            }

            if (servoDegre3 > (sliderServo3.value))
            {
                servoDegre3 = servoDegre3 - 1;
            }
        }
        servo3.localRotation = Quaternion.AngleAxis(servoDegre3, Vector3.up);
        C = servoDegre3.ToString("000");
        if (servoDegre4 != (sliderServo4.value))
        {
            if (servoDegre4 < (sliderServo4.value))
            {
                servoDegre4 = servoDegre4 + 1;
            }

            if (servoDegre4 > (sliderServo4.value))
            {
                servoDegre4 = servoDegre4 - 1;
            }
        }
        servo4.localRotation = Quaternion.AngleAxis(servoDegre4, Vector3.up);
        D = servoDegre4.ToString("000");

        myString = string.Concat(A, B, C, D);

        temps = Time.time;
        if ((delay + comRapidity) < temps)
        {
            Envoyer();
            delay = temps;
            //Debug.Log (myString);
        }

    }
    public void Envoyer()
    {
        if (setPort == true)
        {
            serial.PortName = portName;
            serial.Parity = Parity.None;
            serial.BaudRate = 9600;
            serial.DataBits = 8;
            serial.StopBits = StopBits.One;
            serial.Open();
            setPort = false;
        }
        serial.Write(myString + "\n");

    }
}
