# Bowling-Score-Challenge
A bowling game consists in 10 frames, as shown in the
image below. For each frame the player has two moves to
knock down 10 pins. The score for each frame is the sum
of the number of pins knocked down, plus the bonus for
strike or spare.

![image](https://user-images.githubusercontent.com/89717502/213884522-10bed28b-4f41-491a-9086-f8dab08efd93.png)


A spare consists in knocking down all 10 pins in two
attempts. The bonus is the number of pins knocked down
on the next turn. For example, in frame 3 of image above
the score is 10 (number of pins knocked down) plus the
bonus of 5 (pins knocked down on the next turn), making
a total of 15.

A strike consists in knocking down all 10 pins in the first
attempt of the frame. In this case the bonus for the board
is the value of the next two moves.

On the tenth move, if the player gets a spare or strike, he
can play extra balls to complete the frame. However, no
more than three balls can be played in the tenth frame.

REQUIREMENTS
Write a class called BowlingGame that has two methods:

Play(int pins): void

GetScore(): int
