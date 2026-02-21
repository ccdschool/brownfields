using System;

namespace SmellyMarsRover
{
    public class Rover
    {
        private string _direction;
        private int _y;
        private int _x;

        public Rover(int x, int y, string direction)
        {
            _direction = direction;
            _y = y;
            _x = x;
        }

        public void Receive(string commandsSequence)
        {
            for (var i = 0; i < commandsSequence.Length; ++i)
            {
                var command = commandsSequence.Substring(i, 1);

                if (command.Equals("l") || command.Equals("r"))
                {
                    // Rotate Rover
                    if (_direction.Equals("N"))
                    {
                        if (command.Equals("r"))
                        {
                            _direction = "E";
                        }
                        else
                        {
                            _direction = "W";
                        }
                    }
                    else if (_direction.Equals("S"))
                    {
                        if (command.Equals("r"))
                        {
                            _direction = "W";
                        }
                        else
                        {
                            _direction = "E";
                        }
                    }
                    else if (_direction.Equals("W"))
                    {
                        if (command.Equals("r"))
                        {
                            _direction = "N";
                        }
                        else
                        {
                            _direction = "S";
                        }
                    }
                    else
                    {
                        if (command.Equals("r"))
                        {
                            _direction = "S";
                        }
                        else
                        {
                            _direction = "N";
                        }
                    }
                }
                else
                {
                    // Displace Rover
                    var displacement1 = -1;

                    if (command.Equals("f"))
                    {
                        displacement1 = 1;
                    }

                    var displacement = displacement1;

                    if (_direction.Equals("N"))
                    {
                        _y += displacement;
                    }
                    else if (_direction.Equals("S"))
                    {
                        _y -= displacement;
                    }
                    else if (_direction.Equals("W"))
                    {
                        _x -= displacement;
                    }
                    else
                    {
                        _x += displacement;
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rover)obj);
        }

        protected bool Equals(Rover other)
        {
            return _direction == other._direction && _y == other._y && _x == other._x;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_direction, _y, _x);
        }

        public override string ToString()
        {
            return $"{nameof(_direction)}: {_direction}, {nameof(_y)}: {_y}, {nameof(_x)}: {_x}";
        }
    }
}