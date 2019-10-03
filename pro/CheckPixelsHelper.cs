using pro.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pro
{
    public class CheckPixelsHelper
    {
        private int _pp1;
        private int _pp2;
        private int _pp3;
        private int _pp4;
        public CheckPixelsHelper(Data data)
        {
            _pp1 = data.PP1;
            _pp2 = data.PP2;
            _pp3 = data.PP3;
            _pp4 = data.PP4;
        }

        public bool Pokemon(SendDataHelper sendDataHelper, bool counter, Data data)
        {
            if (sendDataHelper.checkPixels(data.InA, data.InR, data.InG, data.InB, data.InX, data.InY))
            {
                return false;
            }
            else if (!sendDataHelper.checkPixels(data.PlayerA, data.PlayerR, data.PlayerG, data.PlayerB, data.PlayerX, data.PlayerY))
            {
                switch (counter)
                {
                    case true:
                        sendDataHelper.SendKeyPressToProcess(0x41, data.Time1);
                        break;
                    case false:
                        sendDataHelper.SendKeyPressToProcess(0x44, data.Time1);
                        break;
                }
            }
            else
            {
                sendDataHelper.SendKeyToQueue(data.Key4);
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
                        break;
                    case false:
                        sendDataHelper.SendKeyPressToProcess(0x44, data.Time1);
                        break;
                }
            }
            else
            {
                sendDataHelper.SendKeyToQueue(data.Key1);

                if (sendDataHelper.checkPixels(data.FightA, data.FightR, data.FightG, data.FightB, data.FightX, data.FightY))
                {
                    if (_pp1 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key1);
                        _pp1--;
                    }
                    else if (_pp2 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key2);
                        _pp2--;
                    }
                    else if (_pp3 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key3);
                        _pp3--;
                    }
                    else if (_pp4 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key4);
                        _pp4--;
                    }
                    else
                    {
                        return false;
                    }
                }
                
                Thread.Sleep(2000);
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
                        break;
                    case false:
                        sendDataHelper.SendKeyPressToProcess(0x44, data.Time1);
                        break;
                }
            }
            else if (sendDataHelper.checkPixels(data.InA, data.InR, data.InG, data.InB, data.InX, data.InY) || 
                    sendDataHelper.checkPixels(data.EvA, data.EvR, data.EvG, data.EvB, data.EvX, data.EvY))
            {
                sendDataHelper.SendKeyToQueue(data.Key1);
                if (sendDataHelper.checkPixels(data.FightA, data.FightR, data.FightG, data.FightB, data.FightX, data.FightY))
                {
                    if (_pp1 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key1);
                        _pp1--;
                    }
                    else if (_pp2 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key2);
                        _pp2--;
                    }
                    else if (_pp3 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key3);
                        _pp3--;
                    }
                    else if (_pp4 > 0)
                    {
                        sendDataHelper.SendKeyToQueue(data.Key4);
                        _pp4--;
                        return false;
                    }
                }
                Thread.Sleep(2000);
            } else
            {
                sendDataHelper.SendKeyToQueue(data.Key4);
            }
            return true;
        }
    }
}
