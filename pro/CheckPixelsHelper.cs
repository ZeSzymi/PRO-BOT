using pro.models;
using System.Collections.Generic;

namespace pro
{
    public class CheckPixelsHelper
    {
        private int _pp1;
        private int _pp2;
        private int _pp3;
        private int _pp4;
        private List<Point> _points = new List<Point>();
        public CheckPixelsHelper(Data data)
        {
            _pp1 = data.PP1;
            _pp2 = data.PP2;
            _pp3 = data.PP3;
            _pp4 = data.PP4;

            if (data.poke1)
                _points.Add(new Point { R = data.InR, G = data.InG, B = data.InB, A = data.InA, X = data.InX, Y = data.InY });
            if (data.poke2)
                _points.Add(new Point { R = data.EvR, G = data.EvG, B = data.EvB, A = data.EvA, X = data.EvX, Y = data.EvY });
            if (data.poke3)
                _points.Add(new Point { R = data.ExpR, G = data.ExpG, B = data.ExpB, A = data.ExpA, X = data.ExpX, Y = data.ExpY });
        }

        private bool CheckPixelsForArr(SendDataHelper sendDataHelper, List<Point> points)
        {
            foreach (var point in points)
            {
                if (sendDataHelper.checkPixels(point.A, point.R, point.G, point.B, point.X, point.Y))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Pokemon(SendDataHelper sendDataHelper, bool counter, Data data)
        {
            if (CheckPixelsForArr(sendDataHelper, _points))
            {
                return false;
            }
            else if (!sendDataHelper.checkPixels(data.PlayerA, data.PlayerR, data.PlayerG, data.PlayerB, data.PlayerX, data.PlayerY))
            {
                switch (counter)
                {
                    case true:
                        sendDataHelper.SendKeyPressToProcess(0x41, data.Time1);
                        return true;
                    case false:
                        sendDataHelper.SendKeyPressToProcess(0x44, data.Time1);
                        return true;
                }
            }
            else
            {
                sendDataHelper.SendKeyToQueue(data.Key4, data.Time1);
            }
            return true;
        }

        public bool Exp(SendDataHelper sendDataHelper, bool counter, Data data)
        {
            if (!sendDataHelper.checkPixels(data.PlayerA, data.PlayerR, data.PlayerG, data.PlayerB, data.PlayerX, data.PlayerY))
            {
                switch (counter)
                {
                    case true:
                        sendDataHelper.SendKeyPressToProcess(0x41, data.Time1);
                        return true;
                    case false:
                        sendDataHelper.SendKeyPressToProcess(0x44, data.Time1);
                        return true;
                }
            }
            else
            {
                if (sendDataHelper.checkPixels(data.FightA, data.FightR, data.FightG, data.FightB, data.FightX, data.FightY))
                {
                    if (_pp1 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key1, data.Time1);
                        _pp1--;
                    }
                    else if (_pp2 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key2, data.Time1);
                        _pp2--;
                    }
                    else if (_pp3 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key3, data.Time1);
                        _pp3--;
                    }
                    else if (_pp4 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key4, data.Time1);
                        _pp4--;
                    }
                    else
                    {
                        return false;
                    }
                } else
                {
                    sendDataHelper.SendKeyToQueue(data.Key1, data.Time1);
                }
            }
            return true;
        }

        public bool Evs(SendDataHelper sendDataHelper, bool counter, Data data)
        {
            if (!sendDataHelper.checkPixels(data.PlayerA, data.PlayerR, data.PlayerG, data.PlayerB, data.PlayerX, data.PlayerY))
            {
                switch (counter)
                {
                    case true:
                        sendDataHelper.SendKeyPressToProcess(0x41, data.Time1);
                        return true;
                    case false:
                        sendDataHelper.SendKeyPressToProcess(0x44, data.Time1);
                        return true;
                }
            }
            else if (CheckPixelsForArr(sendDataHelper, _points))
            {
                sendDataHelper.SendKeyToQueue(data.Key1, data.Time1);
                if (sendDataHelper.checkPixels(data.FightA, data.FightR, data.FightG, data.FightB, data.FightX, data.FightY))
                {
                    if (_pp1 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key1, data.Time1);
                        _pp1--;
                    }
                    else if (_pp2 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key2, data.Time1);
                        _pp2--;
                    }
                    else if (_pp3 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key3, data.Time1);
                        _pp3--;
                    }
                    else if (_pp4 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key4, data.Time1);
                        _pp4--;
                    } else
                    {
                        return false;
                    }
                }
            } else
            {
                sendDataHelper.SendKeyToQueue(data.Key4, data.Time1);
            }
            return true;
        }
    }
}
