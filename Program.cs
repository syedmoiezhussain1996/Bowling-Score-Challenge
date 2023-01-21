using System;

namespace ConsoleApp2
{

	class Frame
	{
		public int _move1Score { get; set; }
		public int _move2Score { get; set; }
		public int _move3Score { get; set; }
		public int _totalScore { get; set; }
		public bool _isStrike { get; set; }
		public bool _isSpare { get; set; }

		public Frame(int move1Score, int move2Score, int move3Score, int totalScore, bool isStrike, bool isSpare)
		{
			_move1Score = move1Score;
			_move2Score = move2Score;
			_totalScore = totalScore;
			_isStrike = isStrike;
			_isSpare = isSpare;
			_move3Score = move3Score;
		}
	}
	class BowlingGame
	{
		private Frame[] _frames = new Frame[10];
		private int _currentFrame = 0;
		private int _currentBall = 0;
		private bool _gameEnd { get; set; }

		public void Play(int pins)
		{
			if (_gameEnd)
			{
				return;
			}
			if (_currentFrame == 9) // logic for last frame
			{
				if (pins == 10) // check for strike moves
				{
					if (_currentBall == 0)
						_frames[_currentFrame] = new Frame(pins, 0, 0, 0, true, false);
					else if (_currentBall == 1)
					{
						_frames[_currentFrame]._move2Score = pins;
						_frames[_currentFrame]._isSpare = true;
					}
					else
					{
						_frames[_currentFrame]._move3Score = pins;
						_frames[_currentFrame]._isStrike = true;
						_gameEnd = true;
					}
					_currentBall++;
				}
				else
				{
					if (_currentBall == 0) // first move of last frame non strike
					{
						_frames[_currentFrame] = new Frame(pins, 0, 0, 0, false, false);
						_currentBall++;
					}
					else if (_currentBall == 1)// second move
					{
						if (pins == 10) // for case (2,X) 
						{
							Console.WriteLine("Wrong Input score on move {0} and frame {1}", _currentBall, _currentFrame);
							_gameEnd = true;
							return;
						}
						_frames[_currentFrame]._move2Score = pins;

						// logic for spare
						if ((_frames[_currentFrame]._move1Score + _frames[_currentFrame]._move2Score) == 10)
						{
							_frames[_currentFrame]._isSpare = true;
						}

						if (!_frames[_currentFrame]._isSpare && !_frames[_currentFrame]._isStrike)
						{
							_gameEnd = true;
						}
						_currentBall++;
					}
					else
					{
						if (!_frames[_currentFrame]._isSpare && !_frames[_currentFrame]._isStrike)
						{
							// there should be no move 3
							_gameEnd = true;
							return;
						}
						_frames[_currentFrame]._move3Score = pins;

						// logic for spare
						if ((_frames[_currentFrame]._move2Score + _frames[_currentFrame]._move3Score) == 10)
						{
							_frames[_currentFrame]._isSpare = true;
						}
						_gameEnd = true;
					}
				}
			}
			else
			{
				if (pins == 10)// strike
				{
					if (_currentBall == 0)
					{
						_frames[_currentFrame] = new Frame(0, 0, 0, 0, true, false);
					}
					else
					{
						_frames[_currentFrame]._isSpare = true;
					}
					_currentBall = 0;
					_currentFrame++;
				}
				else
				{
					if (_currentBall == 0) // first move
					{
						_frames[_currentFrame] = new Frame(pins, 0, 0, 0, false, false);
						_currentBall++;
					}
					else // second move
					{
						if (pins == 10) // for case (2,X) 
						{
							Console.WriteLine("Wrong Input score on move {0} and frame {1}", _currentBall, _currentFrame);
							_gameEnd = true;
							return;
						}
						_frames[_currentFrame]._move2Score = pins;

						// logic for spare
						if ((_frames[_currentFrame]._move1Score + _frames[_currentFrame]._move2Score) == 10)
						{
							_frames[_currentFrame]._isSpare = true;
						}

						_currentBall = 0;
						_currentFrame++;
					}
				}
			}

		}

		public void GetScore()
		{
			for (int i = 0; i < 10; i++)
			{
				var prevFrame = _frames[i - 1 < 0 ? 0 : i - 1];


				if (i == 9)
				{
					// for last frame
					if (_frames[i]._isSpare && _frames[i]._isStrike)
						_frames[i]._totalScore = prevFrame._totalScore + 20;
					else if (_frames[i]._isSpare)
						_frames[i]._totalScore = prevFrame._totalScore + _frames[i]._move3Score + 10;
					else
						_frames[i]._totalScore = prevFrame._totalScore + _frames[i]._move1Score + _frames[i]._move2Score + _frames[i]._move3Score;
					continue;
				}
				var nextFrame = _frames[i + 1];
				if (_frames[i]._isSpare)
				{
					if (nextFrame._isSpare)
						_frames[i]._totalScore = prevFrame._totalScore + nextFrame._move1Score + 10;
					else if(nextFrame._isStrike)
						_frames[i]._totalScore = prevFrame._totalScore + 20;
					else
						_frames[i]._totalScore = prevFrame._totalScore + 10 + nextFrame._move1Score;
					continue;
				}
				if (_frames[i]._isStrike)
				{
					if (!nextFrame._isStrike && !nextFrame._isSpare)
						_frames[i]._totalScore = prevFrame._totalScore + nextFrame._move1Score + nextFrame._move2Score + 10;
					else
					{
						if (nextFrame._isSpare)
						{
							_frames[i]._totalScore += 10;

						}
						if (nextFrame._isStrike)
						{
							if ((i + 2) <= _frames.Length - 1) // handle for move 9 if its strike
							{
								_frames[i]._totalScore += 10;
								if (_frames[i + 2]._isStrike)
									_frames[i]._totalScore += 10;
								else
									_frames[i]._totalScore += _frames[i + 2]._move1Score;
							}
							else
							{
								_frames[i]._totalScore += nextFrame._move1Score + nextFrame._move2Score;
							}
						}

						if (i == 0) // for first frame
							_frames[i]._totalScore += 10;
						else
							_frames[i]._totalScore += prevFrame._totalScore + 10;
					}
					continue;
				}


				_frames[i]._totalScore = prevFrame._totalScore + _frames[i]._move1Score + _frames[i]._move2Score;

			}
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine("Score of round {0} = {1}", i + 1, _frames[i]._totalScore);
			}
			Console.WriteLine("====================================");
			Console.WriteLine("Total score of all round is = {0}", _frames[_frames.Length - 1]._totalScore);
		}
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			BowlingGame obj = new BowlingGame();

			while (true)
			{
				var a = (Console.ReadLine());
				if(a=="s")
				{ obj.GetScore();
					break;
				}
				obj.Play(int.Parse(a));

			}
			
			//// frame 1
			//obj.Play(2); 
			//obj.Play(8);

			//// frame 2
			//obj.Play(4);
			//obj.Play(4);

			//// frame 3
			//obj.Play(10);


			//// frame 4
			//obj.Play(5);
			//obj.Play(5);


			//// frame 5
			//obj.Play(10);

			//// frame 6
			//obj.Play(10);

			//// frame 7
			//obj.Play(10);


			//// frame 8
			//obj.Play(4);
			//obj.Play(6);

			//// frame 9
			//obj.Play(4);
			//obj.Play(4);

			//// frame 10
			//obj.Play(4);
			//obj.Play(4);
			//obj.Play(10); //166
			//obj.GetScore();
			//obj = new BowlingGame();
			//// frame 1
			//obj.Play(2);
			//obj.Play(8);

			//// frame 2
			//obj.Play(4);
			//obj.Play(4);

			//// frame 3
			//obj.Play(10);


			//// frame 4
			//obj.Play(5);
			//obj.Play(5);


			//// frame 5
			//obj.Play(10);

			//// frame 6
			//obj.Play(10);

			//// frame 7
			//obj.Play(10);


			//// frame 8
			//obj.Play(4);
			//obj.Play(6);

			//// frame 9
			//obj.Play(4);
			//obj.Play(4);

			//// frame 10
			//obj.Play(4);
			//obj.Play(4);
			//obj.Play(10); //166
			//obj.GetScore();
			//obj = new BowlingGame();
			////frame 1
			//obj.Play(1);
			//obj.Play(4);

			//// frame 2
			//obj.Play(4);
			//obj.Play(5);

			//// frame 3
			//obj.Play(6);
			//obj.Play(4);

			//// frame 4
			//obj.Play(5);
			//obj.Play(5);


			//// frame 5
			//obj.Play(10);

			//// frame 6
			//obj.Play(0);
			//obj.Play(1);

			//// frame 7
			//obj.Play(7);
			//obj.Play(3);


			//// frame 8
			//obj.Play(6);
			//obj.Play(4);

			//// frame 9
			//obj.Play(10);

			//// frame 10
			//obj.Play(2);
			//obj.Play(8);
			//obj.Play(6);
			//obj.GetScore();
			//obj = new BowlingGame();
			////frame 1
			//obj.Play(8);
			//obj.Play(2);

			////frame 2
			//obj.Play(7);
			//obj.Play(3);

			////frame 3
			//obj.Play(3);
			//obj.Play(4);

			////frame 4
			//obj.Play(10);



			////frame 5
			//obj.Play(2);
			//obj.Play(8);

			////frame 6
			//obj.Play(10);

			////frame 7
			//obj.Play(10);


			////frame 8
			//obj.Play(8);
			//obj.Play(0);

			////frame 9
			//obj.Play(10);

			////frame 10
			//obj.Play(8);
			//obj.Play(2);
			//obj.Play(9);
			//obj.GetScore();
			//obj = new BowlingGame();
			////frame 1
			//obj.Play(10);

			//// frame 2
			//obj.Play(10);

			//// frame 3
			//obj.Play(10);

			//// frame 4
			//obj.Play(10);



			//// frame 5
			//obj.Play(10);

			//// frame 6
			//obj.Play(10);

			//// frame 7
			//obj.Play(10);


			//// frame 8
			//obj.Play(10);

			//// frame 9
			//obj.Play(10);

			//// frame 10
			//obj.Play(10);
			//obj.Play(10);
			//obj.Play(10);
			//obj.GetScore();
			//obj = new BowlingGame();


			//// frame 1
			//obj.Play(0);
			//obj.Play(0);

			//// frame 2
			//obj.Play(0);
			//obj.Play(0);

			//// frame 3
			//obj.Play(0);
			//obj.Play(0);

			//// frame 4
			//obj.Play(0);
			//obj.Play(10);



			//// frame 5
			//obj.Play(0);
			//obj.Play(10);

			//// frame 6
			//obj.Play(10);

			//// frame 7
			//obj.Play(0);
			//obj.Play(10);


			//// frame 8
			//obj.Play(10);

			//// frame 9
			//obj.Play(4);
			//obj.Play(6);

			//// frame 10
			//obj.Play(4);
			//obj.Play(6);
			//obj.Play(10);
			//obj.GetScore();
			Console.ReadLine();
		}
	}
}
